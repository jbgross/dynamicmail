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
