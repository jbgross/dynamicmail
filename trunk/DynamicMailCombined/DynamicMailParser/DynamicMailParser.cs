using System;
using System.Collections.Generic;
using System.Text;
using Office = Microsoft.Office.Core;
using System.Runtime.InteropServices;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Collections;
using System.IO;
using Edu.Psu.Ist.DynamicMail.Interface;
using System.Threading;

namespace Edu.Psu.Ist.DynamicMail
{
    public class DynamicMailParser
    {

        private Outlook.MAPIFolder contacts;

        public Outlook.MAPIFolder Contacts
        {
            get { return contacts; }
            set { contacts = value; }
        }
        private Outlook.MAPIFolder box;

        public Outlook.MAPIFolder Box
        {
            get { return box; }
            set { box = value; }
        }
 
        //Singelton Instance of indexes
        private Indexes InvertedIndexes;

        //public default constructor
        public DynamicMailParser()
        {
            InvertedIndexes = Indexes.Instance;
        }

        public void Indexer()
        {
            // for now, if Box or Contacts has not been set, do nothing
            if (Box == null || Contacts == null)
            {
                return;
            }

            Outlook.Items searchFolder = Box.Items;

            int total = searchFolder.Count;     //total items in mailbox
            int lookedAt = 1;                   //email items looked at so far    


            //variables to hold found email data
            string foundSender, foundEmailEntryID, foundSubject;

            // open up a progress window for the user
            int count = 0;
            ProgressInfoBox pib = new ProgressInfoBox(total);
            int update = pib.IncrementQuantity;
            pib.Visible = true;

            //do this for every item in the searchFolder (inbox)
            while (lookedAt < total)
            {
                // let's see if we need to update the progress window
                if (++count >= update)
                {
                    pib.Increment();
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
                        //subject of current email
                        foundSubject = foundEmail.Subject;

                        try
                        {
                            //Dave, this breaks because stoplist.txt is unavailable or in the wrong directory
                            Stemmer SubjectStemmer = new Stemmer();
                            SubjectStemmer.stemSubject(foundSubject.ToString(), foundEmailEntryID.ToString());
                        }
                        catch (Exception e)
                        {
                        }


                        //add email address and emailID to the received email index
                        addToReceivedEmailIndex(foundSender, foundEmailEntryID, contacts);
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

            // close the progress window
            pib.Visible = false;
            pib.Refresh();

            SaveIndexes.WriteIndexToXML();
        }

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

        public void addToReceivedEmailIndex(string senderAddress, string emailID, Outlook.MAPIFolder contacts)
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
            string contactID = this.contactChecker(contacts, senderAddress);

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

        
    }
}
