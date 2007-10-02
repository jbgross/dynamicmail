using System;
using System.Windows.Forms;
using Microsoft.VisualStudio.Tools.Applications.Runtime;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using System.Collections;
using System.IO;

namespace OutlookAddin2
{
    public partial class ThisApplication
    {
        private Indexes InvertedIndexes = new Indexes();
        private void ThisApplication_Startup(object sender, System.EventArgs e)
        {
            //indexInboxEmail();
            indexSentEmail();
            //indexContacts();
            //getSortedEmailIDs();
            //InvertedIndexes.saveTxt();
            MessageBox.Show("Indexing Completed");
        }

        /// <summary>
        /// Indexes Email in Inbox
        /// Populates inboxEmail index with keys(email addresses of people who sent email to you)
        /// and values (arraylist of email entryIDs sent by the email address)
        /// 
        /// Also populates contact inded with keys (contactIDs) and values(Email entryIDs of emails
        /// sent by that contact
        /// </summary>
        private Hashtable indexInboxEmail()
        {
            
            //gets the contents of the inbox and puts them in searchFolder
            Outlook.MAPIFolder inbox = this.ActiveExplorer().
                Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox);
            Outlook.Items searchFolder = inbox.Items;


            int total = searchFolder.Count;     //total items in mailbox
            int lookedAt = 1;                   //email items looked at so far    
            
            
            //variables to hold found email data
            string foundSender, foundEmailEntryID, foundSubject;

            //do this for every item in the searchFolder (inbox)
            while (lookedAt < total)
            {
               

                try
                {
                    Outlook.MailItem foundEmail = (Outlook.MailItem)searchFolder[lookedAt];
                    
                    
                    //sender email address of current email
                    foundSender = foundEmail.SenderEmailAddress;
                    //email entry ID of current email
                    foundEmailEntryID = foundEmail.EntryID;
                    //subject of current email
                    foundSubject = foundEmail.Subject;


                    //Dave, this breaks because stoplist.txt is unavailable or in the wrong directory
                    //Stemmer SubjectStemmer = new Stemmer();
                    //SubjectStemmer.stemSubject(foundSubject, foundEmailEntryID);


                    //add email address and emailID to the received email index
                    addToReceivedEmailIndex(foundSender, foundEmailEntryID);

                    
                    
                   
                    
                    
                   
                }
                catch (Exception e) //catches items that are not emails
                {

                    MessageBox.Show(e.Message + "\n" + lookedAt);
                }
                lookedAt++;
            }

            return InvertedIndexes.receivedEmailIndex;
        }

 


        /// <summary>
        /// Indexes Email in "Sent" box
        /// Populates SentEmail index with keys(addresses you sent to) and values (arraylist of email entryIDs
        /// you sent to that address
        /// </summary>
        private void indexSentEmail()
        {
            //gets the contents of the inbox and puts them in searchFolder
            Outlook.MAPIFolder sentBox = this.ActiveExplorer().
                Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderSentMail);
            Outlook.Items searchFolder = sentBox.Items;


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

                    MessageBox.Show("entryID: " + foundEmail.EntryID);


                    //email entry ID of current email
                    foundEmailEntryID = foundEmail.EntryID;
                    //subject of current email
                    foundSubject = foundEmail.Subject;
                    //clear the array list for storage
                    emailIDs = new ArrayList();

                    //get all email recipients (individuals you sent the email to)
                    foundRecipients = foundEmail.Recipients;

                    addtoSentEmailIndex(foundRecipients, foundEmailEntryID);
                    

                }
                catch (Exception e) //catch items that arenot emails
                {
                    MessageBox.Show("Exception: " + e);

                }

                lookedAt++;
            }
    
        }


           

        /// <summary>
        /// creates an index of email addresses and all of the contacts associated with them
        /// </summary>
        private void indexContacts()
        {
             //gets the contents of the inbox and puts them in searchFolder
            Outlook.MAPIFolder contacts = this.ActiveExplorer().
                Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderContacts);
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

                    MessageBox.Show(emailAddress);
                }
                else  //contact email address is already in the index
                {
                    //get the arraylist for the email address
                    contactIDs = (ArrayList)InvertedIndexes.contactsAddresses[emailAddress];

                    //add contact ID to the listarray
                    contactIDs.Add(contactID);

                    //put the array back in the index, overwriting the old one.
                    InvertedIndexes.contactsAddresses[emailAddress] = contactIDs;

                    MessageBox.Show(emailAddress);
                }
            }

        }


       
        

       /// <summary>
       /// Checks to see if an email address is in any of the user's contacts information
       /// </summary>
       /// <param name="searchAddress"></param>
       /// <returns>contnact EntryID if email address is in contacts.  Else null</returns>
        private string checkContacts(string searchAddress)
        {
            Outlook.MAPIFolder contacts = this.ActiveExplorer().
                Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderContacts);
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

        public void addToReceivedEmailIndex(string senderAddress, string emailID)
        {
            //Key = email address (senderAddress)
            //Value = ArrayList of Email EntryIDs that were sent by the email address (emailIDs)
            ArrayList emailIDs = new ArrayList();


            //if sender email address is NOT already in the index
            if (InvertedIndexes.receivedEmailIndex.Contains(senderAddress) == false)
            {

                //put the emailID into an ArrayList
                emailIDs.Add(emailID);
                //add sender email address (key) and ArrayList of Email Entry ID (value) to the index
                InvertedIndexes.receivedEmailIndex.Add(senderAddress, emailIDs);

            }
            else //sender email address is already in the index
            {
                //get the ArrayList containg the Email Entry IDs for the key (sender Email address) 
                emailIDs = (ArrayList)InvertedIndexes.receivedEmailIndex[senderAddress];

                //add the found Emails ID to the array
                emailIDs.Add(emailID);

                //put the array back into the index, overwriting the old one
                InvertedIndexes.receivedEmailIndex[senderAddress] = emailIDs;

            }

            //gets the contact ID. (contactID is null if email address is not in contacts)
            string contactID = this.checkContacts(senderAddress);

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

        public void addtoSentEmailIndex(Outlook.Recipients foundRecipients, string emailID)
        {
            ArrayList emailIDs = new ArrayList();

            //do this for all recipients of the email
            foreach (Outlook.Recipient recipient in foundRecipients)
            {
                //testing purposes
                MessageBox.Show("recipient: " + recipient.Address);

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
                string contactID = checkContacts(recipient.Address);

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

        public ArrayList getSortedEmailIDs()
        {
            ArrayList emailIDs = new ArrayList();

            //gets the contents of the inbox and puts them in searchFolder
            Outlook.MAPIFolder inbox = this.ActiveExplorer().
                Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox);
            Outlook.Items searchFolder = inbox.Items;

            foreach (Outlook.MailItem foundEmail in searchFolder)
            {
                try
                {
                    emailIDs.Add(foundEmail.EntryID);
                }
                catch (Exception e)
                {

                }

            }

            emailIDs.Sort();

            //testing
            foreach (string id in emailIDs)
            {
                MessageBox.Show(id);
            }


            return emailIDs;
        }


        private void ThisApplication_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisApplication_Startup);
            this.Shutdown += new System.EventHandler(ThisApplication_Shutdown);
        }

        #endregion
    }
}
