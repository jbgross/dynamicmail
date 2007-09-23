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
            indexInboxEmail();
            //indexSentEmail();
            //indexContacts();
            InvertedIndexes.saveTxt();
            MessageBox.Show("Indexing Completed");
        }

        private void indexInboxEmail()
        {
            
            //gets the contents of the inbox and puts them in searchFolder
            Outlook.MAPIFolder inbox = this.ActiveExplorer().
                Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox);
            Outlook.Items searchFolder = inbox.Items;


            int total = searchFolder.Count;     //total items in mailbox
            int lookedAt = 1;                   //email items looked at so far    
            
            


            //temporary hashtable for index and arrayList for values
            //Hashtable inboxIndex = new Hashtable();
            ArrayList emailIDs = new ArrayList();

            //variables to hold found email data
            string foundSender, foundEmailEntryID, foundSubject;

            //does this for every item in the searchFolder (inbox)
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
                    //clear the array list for storage
                    emailIDs = new ArrayList(); 

                    //Dave, this breaks because stoplist.txt is unavailable or in the wrong directory
                    Stemmer SubjectStemmer = new Stemmer();
                    SubjectStemmer.stemSubject(foundSubject, foundEmailEntryID);


                    //**********************************************************************************
                    //To populate the index of email addresses.  
                    //Key = email address
                    //Value = ArrayList of Email EntryIDs that were sent by the email address
                    //**********************************************************************************
                    //if sender email address is NOT already in the index
                    if (InvertedIndexes.receivedEmailIndex.Contains(foundSender) == false)
                    {
                        
                        //put the emailID into an ArrayList
                        emailIDs.Add(foundEmailEntryID);
                        //add sender email address (key) and ArrayList of Email Entry ID (value) to the index
                        InvertedIndexes.receivedEmailIndex.Add(foundSender, emailIDs);

                    }
                    else //sender email address is already in the index
                    {
                        //get the ArrayList containg the Email Entry IDs for the key (sender Email address) 
                        emailIDs = (ArrayList)InvertedIndexes.receivedEmailIndex[foundSender];

                        //add the found Emails ID to the array
                        emailIDs.Add(foundEmailEntryID);

                        //put the array back into the index, overwriting the old one
                        InvertedIndexes.receivedEmailIndex[foundSender] = emailIDs ;

                    }
                    //**********************************************************************************
                    //**********************************************************************************





                    //**********************************************************************************
                    //To populate the index of contacts
                    //Key = contact EntryID
                    //Value = ArrayList of Email EntryIDs that were sent by the contact
                    //**********************************************************************************
                    
                    //contactID = null if email address is not in contacts
                    //string contactID = this.checkContacts(foundEmail.SenderEmailAddress);

                    //if (contactID != null) //if email address belongs to a contact
                    //{
                    //    //if contact is NOT already index
                    //    if (InvertedIndexes.contactsIndex.Contains(contactID) == false)
                    //    {
                    //        //clears the email ID arrayList
                    //        emailIDs.Clear();
                    //        //put the emailID into an ArrayList
                    //        emailIDs.Add(foundEmailEntryID);
                    //        //add contactID (key) and ArrayList of Email Entry ID (value) to the index
                    //        InvertedIndexes.contactsIndex.Add(contactID, emailIDs);
                    //    }
                    //    else //contact is already in the index
                    //    {
                    //        //get the ArrayList containing the Email Entry IDs for the key (contact EntryID)
                    //        emailIDs = (ArrayList)InvertedIndexes.contactsIndex[contactID];

                    //        //add the found emails ID to the array
                    //        emailIDs.Add(foundEmailEntryID);

                    //        //put the array back into the index, overwriting the old one
                    //        InvertedIndexes.contactsIndex[contactID] = emailIDs;
                    //    }
           

                    //}
                    //else, do nothing because the address is not a contact

                    //**********************************************************************************
                    //**********************************************************************************

                   
                }
                catch (Exception e) //catches items that are not emails
                {

                    //MessageBox.Show(e.Message + "\n" + lookedAt);
                }


                lookedAt++;


            }
            
 


        }


        private void indexSentEmail()
        {
            //sorry dave, I screwed this up while I was working on it, and do not have a earlier 
            //version to revert to.  Also, the earlier version was not complete... 
            //I need to stop coding late at night

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
