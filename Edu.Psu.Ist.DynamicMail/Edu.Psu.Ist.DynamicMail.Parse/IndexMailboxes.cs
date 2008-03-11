using System;
using System.Collections.Generic;
using System.Text;
using Edu.Psu.Ist.DynamicMail.Interface;
using Outlook = Microsoft.Office.Interop.Outlook;
using Edu.Psu.Ist.DynamicMail;
using System.Threading;
using System.Collections;

namespace Edu.Psu.Ist.DynamicMail.Parse
{
    /// <summary>
    /// Class to parse and index mailbox content
    /// </summary>
    public class IndexMailboxes : Finishable, Stoppable
    {
        private int totalCount = 0;

        private bool continueParsing = true;
        private String[] splitArray = { "; " };
        private InfoBox infoBox;
        private Indexes index;
        private Outlook.Explorer activeExplorer;

        private string ignoreAddress;

        /// <summary>
        /// Get the infobox
        /// </summary>
        public InfoBox InfoBox
        {
            get { return infoBox; }
            private set { infoBox = value; }
        }

        public bool ContinueParsing
        {
            get { return continueParsing; }
            private set { continueParsing = value; }
        }

        private ProgressInfoBox pib;
        /// <summary>
        /// Get the progress bar
        /// </summary>
        public ProgressInfoBox Pib
        {
            get { return pib; }
            set { pib = value; }
        }

        Outlook.MAPIFolder [] indexFolders;
        SelectFolders select = null;

        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="allFolders"></param>
        public IndexMailboxes(FolderTree tree, Indexes index, Outlook.Explorer activeExplorer)
        {
            this.activeExplorer = activeExplorer;
            this.ignoreAddress = Indexes.LocalAccountAddress;
            this.select = new SelectFolders(tree, this);
            this.index = index;
        }

        /// <summary>
        /// Call when the folders are selected
        /// </summary>
        public void Finish()
        {
            this.indexFolders = select.SelectedFolders;
            this.totalCount = this.GetTotalCount(this.indexFolders);
            this.select = null;

            Pib = new ProgressInfoBox(totalCount, this);

            ThreadStart job = new ThreadStart(Indexer);
            Thread thread = new Thread(job);
            thread.Priority = ThreadPriority.Normal;
            thread.Start();
        }

        /// <summary>
        /// Index whatever Mailbox is specified in the Mailbox property
        /// </summary>
        public void Indexer()
        {

            // for now, if Box or Contacts has not been set, do nothing
            if (indexFolders == null || indexFolders.Length < 1)
            {
                // get rid of the progress bar box
                Pib.Visible = false;
                Pib.Refresh();
                Pib = null;
                
                return;
            }

            int count = 0;
            int checkAt = (this.totalCount / 100) + 1;
            int lookedAtTotal = 0;
            foreach (Outlook.MAPIFolder mailbox in this.indexFolders)
            {
                // testing
                this.activeExplorer.SelectFolder(mailbox);

                Outlook.Items searchFolder = mailbox.Items;
                //variables to hold found email data
                String foundEmailEntryId;

                //do this for every item in the searchFolder (inbox)
                foreach (Object o in searchFolder)
                {
                    lookedAtTotal++;

                    // let's see if we need to update the progress window
                    if (++count >= checkAt)
                    {
                        this.pib.Increment(this.totalCount - lookedAtTotal);
                        count = 0;
                    }

                    // stop if requested
                    if (!ContinueParsing)
                    {
                        break;
                    }

                    //type check on the item - only looking at emails
                    if (! (o is Outlook.MailItem))
                    {
                        continue;
                    }

                    try
                    {

                        // cast to email
                        Outlook.MailItem foundEmail = (Outlook.MailItem) o;
                        foundEmailEntryId = foundEmail.EntryID;
                        // if we haven't already indexed this one, index it
                        if (!this.index.SearchAlreadyIndexed(foundEmailEntryId))
                        {
                            // add each recipient that is not the current user
                            Outlook.Recipients recs = foundEmail.Recipients;
                            List<String> recipients = new List<String>();
                            foreach (Outlook.Recipient rec in recs)
                            {
                                String addr = rec.Address;
                                if (addr.Equals(this.ignoreAddress))
                                    continue;
                                addr = addr.ToLower();
                                recipients.Add(addr);
                                // add the name DEBUG
                                this.index.AddAddressName(rec.Name, addr);
                            }
                            // add the sender
                            String sender = foundEmail.SenderEmailAddress;
                            if (sender != null && sender.Equals(this.ignoreAddress) == false)
                            {
                                recipients.Add(sender);
                                sender = sender.ToLower();
                                // add the name DEBUG
                                this.index.AddAddressName(foundEmail.SenderName, sender);
                            }

                            // DEBUG
                            if (recipients.Count <= 1)
                            {
                                Indexes.Instance.AddIndexedID(foundEmailEntryId);
                                continue;
                            }
                            foreach (String addr in recipients)
                            {
                                addToReceivedEmailIndex(addr, foundEmailEntryId);
                            }
                            Indexes.Instance.AddIndexedID(foundEmailEntryId);
                        }
                    }
                    catch (InvalidCastException e)
                    {
                        // ignore bad casts
                    }
                }
                
            }

            Pib.ChangeText("Writing index to disk...");
            Pib.Refresh();

            index.WriteIndexToXML();

            // get rid of the progress bar box
            Pib.Close();
        }

        /// <summary>
        /// Parse addresses and add to List
        /// There's an open question of uniqueness here - does it matter if one address ends up on an email twice?
        /// I'm not sure, so I'm going to code around it - JBG 25 Jan 2008
        /// </summary>
        /// <param name="addresses"></param>
        /// <param name="allRec"></param>
        private void ParseRecipients(String addresses, List<String> allRec)
        {
            String[] addrs = addresses.Split(this.splitArray, StringSplitOptions.RemoveEmptyEntries);
            foreach (String addr in addrs)
            {
                if (allRec.Contains(addr) == false)
                {
                    allRec.Add(addr);
                }
            }
        }

        private int GetTotalCount(Outlook.MAPIFolder[] folders)
        {
            int total = 0;
            foreach(Outlook.MAPIFolder folder in folders) 
            {
                String name = folder.Name;
                total += folder.Items.Count;
            }
            return total;
        }
        /// <summary>
        /// Add this email address to the index
        /// </summary>
        /// <param name="address"></param>
        /// <param name="emailID"></param>
        public void addToReceivedEmailIndex(string address, string emailID)
        {

            //Key = email address (senderAddress)
            //Value = ArrayList of Email EntryIDs that were sent by the email address (emailIDs)
            ArrayList emailIDs = new ArrayList();


            //if sender email address is NOT already in the index
            if (this.index.receivedEmailIndex.Contains(address) == false)
            {

                //put the emailID into an ArrayList
                emailIDs.Add(emailID);
                //add sender email address (key) and ArrayList of Email Entry ID (value) to the index
                this.index.receivedEmailIndex.Add(address, emailIDs);

            }
            else //sender email address is already in the index
            {
                //get the ArrayList containg the Email Entry IDs for the key (sender Email address) 
                emailIDs = (ArrayList) this.index.receivedEmailIndex[address];

                //add the found Emails ID to the array
                emailIDs.Add(emailID);

                //put the array back into the index, overwriting the old one
                this.index.receivedEmailIndex[address] = emailIDs;

            }
        }

        /// Stop parsing and write to file.
        /// </summary>
        public void Stop()
        {
            ContinueParsing = false;
        }

        /// <summary>
        /// Implement the required method from Finishable
        /// </summary>
        public void Cancel()
        {
            // don't do anything
        }

    }
}
