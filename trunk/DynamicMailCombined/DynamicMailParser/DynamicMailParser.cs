using System;
using System.Collections.Generic;
using System.Text;
using Office = Microsoft.Office.Core;
using System.Runtime.InteropServices;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Collections;
using System.IO;
using System.Threading;
using Edu.Psu.Ist.DynamicMail.Interface;
using System.Windows.Forms;


namespace Edu.Psu.Ist.DynamicMail 
{
    public class DynamicMailParser : Stoppable
    {
        int checkAt = 0;
        int count = 0;
        int lookedAt = 1;
        private bool continueParsing = true;
        private String[] splitArray = { "; " };
        private InfoBox infoBox;

        public InfoBox InfoBox
        {
            get { return infoBox; }
            set { infoBox = value; }
        }


        public bool ContinueParsing
        {
            get { return continueParsing; }
            set { continueParsing = value; }
        }

        private ProgressInfoBox pib;

        public ProgressInfoBox Pib
        {
            get { return pib; }
            set { pib = value; }
        }

        private Outlook.MAPIFolder contacts;

        public Outlook.MAPIFolder Contacts
        {
            get { return contacts; }
            set { contacts = value; }
        }
        private Outlook.MAPIFolder mailbox;

        public Outlook.MAPIFolder Mailbox
        {
            get { return mailbox; }
            set { mailbox = value; }
        }
 
        //Singelton Instance of indexes
        private Indexes InvertedIndexes;

        /// <summary>
        /// public default constructor
        /// </summary>
        public DynamicMailParser()
        {
            InvertedIndexes = Indexes.Instance;
        }

        /// <summary>
        /// Index whatever Mailbox is specified in the Mailbox property
        /// </summary>
        public void Indexer()
        {
            // for now, if Box or Contacts has not been set, do nothing
            if (Mailbox == null || Contacts == null)
            {
                return;
            }

            checkAt = (Mailbox.Items.Count / 100) + 1;
            Outlook.Items searchFolder = Mailbox.Items;

            int total = searchFolder.Count;     //total items in mailbox

            //variables to hold found email data
            String foundSender, foundEmailEntryID, foundSubject;
            String[] foundRecipients;

            //do this for every item in the searchFolder (inbox)
            while (lookedAt < total && this.continueParsing)
            {
                // let's see if we need to update the progress window
                if (++count >= checkAt)
                {
                    this.pib.Increment(total - lookedAt);
                    count = 0;
                }

                try
                {

                    Outlook.MailItem foundEmail = (Outlook.MailItem)searchFolder[lookedAt];
                    if (!Indexes.Instance.SearchAlreadyIndexed(foundEmail.EntryID))
                    {
                        //sender email address of current email
                        foundSender = foundEmail.SenderEmailAddress;
                        //email entry ID of current email
                        foundEmailEntryID = foundEmail.EntryID;
                        // get all the recipients
                        String[] recipients = this.GetAllRecipients(foundEmail);
                        foreach (String addr in recipients)
                        {
                            addToReceivedEmailIndex(addr, foundEmailEntryID);
                        }

                        //add email address and emailID to the received email index
                        addToReceivedEmailIndex(foundSender, foundEmailEntryID);
                        Indexes.Instance.AddIndexedID(foundEmailEntryID);

                    }
                }
                catch (Exception e) //catches items that are not emails
                {
                    Console.WriteLine(e.ToString());
                }
                lookedAt++;
            }

            Indexes SaveIndexes = Indexes.Instance;

            SaveIndexes.WriteIndexToXML();

            // get rid of the progress bar box
            Pib.Visible = false;
            Pib.Refresh();
            this.pib = null;

            InfoBox.Visible = false;
            InfoBox.Refresh();
            InfoBox = null;
        }

        /// <summary>
        /// Get all the recipients - from recipients, CC, BCC
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        private String [] GetAllRecipients(Outlook.MailItem msg) 
        {
            List<String> allRec = new List<String>();
            if (msg.To != null && msg.To.Equals("") == false)
            {
                this.ParseRecipients(msg.To, allRec);
            }
            if (msg.CC != null && msg.CC.Equals("") == false)
            {
                this.ParseRecipients(msg.CC, allRec);
            }
            if (msg.BCC != null && msg.BCC.Equals("") == false)
            {
                this.ParseRecipients(msg.BCC, allRec);
            }
            return (String[]) allRec.ToArray();
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
            String [] addrs = addresses.Split(this.splitArray, StringSplitOptions.RemoveEmptyEntries);
            foreach (String addr in addrs)
            {
                if (allRec.Contains(addr) == false)
                {
                    allRec.Add(addr);
                }
            }
        }

        /// <summary>
        /// Index Sent email box
        /// </summary>
        /// <param name="sentMail"></param>
        /// <param name="contacts"></param>
        public void sentBoxIndexer(Outlook.MAPIFolder sentMail, Outlook.MAPIFolder contacts)
        {
            Outlook.Items searchFolder = sentMail.Items;
            
            int total = searchFolder.Count;     //total items in mailbox
            int lookedAt = 1;                   //email items looked at so far    


            //arraylist to hold email entryIDs
            ArrayList emailIDs = new ArrayList();

            //variables to hold found email data
            string foundEmailEntryID, foundSubject;
            Outlook.Recipients foundRecipients;

            //do this for every item in the searchFolder (inbox)
            while (lookedAt <= total)
            {

                try
                {
                    Outlook.MailItem foundEmail = (Outlook.MailItem)searchFolder[lookedAt];

                    //email entry ID of current email
                    foundEmailEntryID = foundEmail.EntryID;
                    //subject of current email
                    foundSubject = foundEmail.Subject;
                    //clear the array list for storage
                    emailIDs = new ArrayList();

                    //get all email recipients (individuals you sent the email to)
                    foundRecipients = foundEmail.Recipients;

                    addtoSentEmailIndex(foundRecipients, foundEmailEntryID, contacts);


                }
                catch (Exception e) //catch items that arenot emails
                {

                }

                lookedAt++;
            }
        }

        public void contactsIndexer(Outlook.MAPIFolder contacts)
        {
            Outlook.Items searchFolder = contacts.Items;

            string foundContactID, foundEmail1, foundEmail2, foundEmail3;

            int total = searchFolder.Count;
            int lookedAt = 1;

            do
            {

                Outlook.ContactItem foundContact = (Outlook.ContactItem)searchFolder[lookedAt];

                foundContactID = foundContact.EntryID;
                foundEmail1 = foundContact.Email1Address;
                foundEmail2 = foundContact.Email2Address;
                foundEmail3 = foundContact.Email3Address;


                addContactEmailtoIndex(foundEmail1, foundContactID);
                addContactEmailtoIndex(foundEmail2, foundContactID);
                addContactEmailtoIndex(foundEmail3, foundContactID);

            } while (lookedAt < total);
        }

        /// <summary>
        /// Adds the contactID (value) and emailAddress(key) to the index
        /// </summary>
        /// <param name="emailAddress">key</param>
        /// <param name="contactID">value</param>
        private void addContactEmailtoIndex(string emailAddress, string contactID)
        {
            ArrayList contactIDs = new ArrayList();

            //checks to make sure the email address is not null
            if (emailAddress != null)
            {
                //if contact email address is NOT already in the index
                if (InvertedIndexes.contactsAddresses.Contains(emailAddress) == false)
                {
                    //clear the arraylist
                    contactIDs.Clear();

                    //add the contactID to the list array
                    contactIDs.Add(contactID);

                    //Add the email address (key) and contact ID (value) to the index
                    InvertedIndexes.contactsAddresses.Add(emailAddress, contactIDs);

                }
                else  //contact email address is already in the index
                {
                    //get the arraylist for the email address
                    contactIDs = (ArrayList)InvertedIndexes.contactsAddresses[emailAddress];

                    //add contact ID to the listarray
                    contactIDs.Add(contactID);

                    //put the array back in the index, overwriting the old one.
                    InvertedIndexes.contactsAddresses[emailAddress] = contactIDs;

                }
            }

        }
        /// <summary>
        /// Check all contacts
        /// </summary>
        /// <param name="contacts"></param>
        /// <param name="searchAddress"></param>
        /// <returns></returns>
        public string contactChecker(Outlook.MAPIFolder contacts, string searchAddress)
        {
            string contactID = null;

            Outlook.Items searchFolder = contacts.Items;

            foreach (Outlook.ContactItem foundContact in searchFolder)
            {
                if ((foundContact.Email1Address == searchAddress) || (foundContact.Email2Address == searchAddress)
                    || (foundContact.Email3Address == searchAddress))
                {
                    contactID = foundContact.EntryID;
                } 
            }
            return contactID;
        }

        /// <summary>
        /// Add this email address to the index
        /// </summary>
        /// <param name="address"></param>
        /// <param name="emailID"></param>
        /// <param name="contacts"></param>
        public void addToReceivedEmailIndex(string address, string emailID)
        {
            //Key = email address (senderAddress)
            //Value = ArrayList of Email EntryIDs that were sent by the email address (emailIDs)
            ArrayList emailIDs = new ArrayList();


            //if sender email address is NOT already in the index
            if (InvertedIndexes.receivedEmailIndex.Contains(address) == false)
            {

                //put the emailID into an ArrayList
                emailIDs.Add(emailID);
                //add sender email address (key) and ArrayList of Email Entry ID (value) to the index
                InvertedIndexes.receivedEmailIndex.Add(address, emailIDs);

            }
            else //sender email address is already in the index
            {
                //get the ArrayList containg the Email Entry IDs for the key (sender Email address) 
                emailIDs = (ArrayList)InvertedIndexes.receivedEmailIndex[address];

                //add the found Emails ID to the array
                emailIDs.Add(emailID);

                //put the array back into the index, overwriting the old one
                InvertedIndexes.receivedEmailIndex[address] = emailIDs;

            }
        }

        public void addToContactsEmailIndex(string contactID, string emailID)
        {
            //Key = contact EntryID
            //Value = ArrayList of Email EntryIDs that were sent by the contact
            ArrayList emailIDs = new ArrayList();


            //if contact is NOT already index
            if (InvertedIndexes.contactsIndex.Contains(contactID) == false)
            {
                //put the emailID into an ArrayList
                emailIDs.Add(emailID);
                //add contactID (key) and ArrayList of Email Entry ID (value) to the index
                InvertedIndexes.contactsIndex.Add(contactID, emailIDs);
            }
            else //contact is already in the index
            {
                //get the ArrayList containing the Email Entry IDs for the key (contact EntryID)
                emailIDs = (ArrayList)InvertedIndexes.contactsIndex[contactID];

                //add the found emails ID to the array
                emailIDs.Add(emailID);

                //put the array back into the index, overwriting the old one
                InvertedIndexes.contactsIndex[contactID] = emailIDs;
            }

        }

        public void addtoSentEmailIndex(Outlook.Recipients foundRecipients, string emailID, Outlook.MAPIFolder contacts)
        {
            ArrayList emailIDs = new ArrayList();

            //do this for all recipients of the email
            foreach (Outlook.Recipient recipient in foundRecipients)
            {

                //clear the array of entryIDs
                emailIDs.Clear();

                //if recipient address is already in index
                if (InvertedIndexes.sentEmailIndex.Contains(recipient.Address) == true)
                {
                    //get listarray of email entryIDs associated with the email address
                    emailIDs = (ArrayList)InvertedIndexes.sentEmailIndex[recipient.Address];

                    //if this email entryID is not already in the array of values
                    if (emailIDs.Contains(emailID) == false)
                    {
                        //add it to the array
                        emailIDs.Add(emailID);

                        //put the array back into the index, overwriting the previous one
                        InvertedIndexes.sentEmailIndex[recipient.Address] = emailIDs;
                    }
                    //else this email entryID is already in the arraylist, do nothing
                }
                else //recipient address is NOT already in the index
                {
                    //add the email entryID to the array of entryIDs
                    emailIDs.Add(emailID);

                    //put the arraylist of values and the key (address)into the index
                    InvertedIndexes.sentEmailIndex.Add(recipient.Address, emailIDs);
                }


                //gets the contact ID. (contactID is null if email address is not in contacts)
                string contactID = contactChecker(contacts, recipient.Address);

                if (contactID == null)
                {
                    //do nothing, because the address does not belong to a contact
                }
                else //address does belong to a contact
                {
                    //add contact and email to index
                    addToContactsEmailIndex(contactID, emailID);
                }
            }


        }

        /// <summary>
        /// Stop parsing and write to file.
        /// </summary>
        public void Stop()
        {
            ContinueParsing = false;
        }
        
    }
}