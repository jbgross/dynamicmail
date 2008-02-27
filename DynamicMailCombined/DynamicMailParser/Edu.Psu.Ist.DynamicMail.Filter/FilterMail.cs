using System;
using System.Collections.Generic;
using System.Text;
using Outlook = Microsoft.Office.Interop.Outlook;
using Edu.Psu.Ist.DynamicMail.Interface;

namespace Edu.Psu.Ist.DynamicMail.Filter
{
    /// <summary>
    /// Class to filter mail and display in a new window
    /// </summary>
	public class FilterMail : Finishable
	{
        private SocialNetwork socialNetwork;
        private List<Outlook.MAPIFolder> folders = new List<Outlook.MAPIFolder>();
        private List<Outlook.MAPIFolder> searchedFolders = new List<Outlook.MAPIFolder>();
        private List<Outlook.MailItem> display = new List<Outlook.MailItem>();
        private FilterDisplay filterDisplay;

        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="socialNetwork">The SocialNetwork to filter on</param>
        /// <param name="folder">The folder to start filtering on</param>
        public FilterMail (SocialNetwork socialNetwork, Outlook.MAPIFolder folder)
        {
            this.socialNetwork = socialNetwork;
            this.folders.Add(folder);
            this.FilterMembers();
            this.DisplayMail();
        }

        /// <summary>
        /// Generate display list
        /// </summary>
        /// <returns></returns>
        private void FilterMembers()
        {
            List<String> addrs = this.socialNetwork.GetAddresses();
            List<String> names = this.socialNetwork.GetNames();
            foreach (Outlook.MAPIFolder folder in this.folders)
            {
                // if we've already searched this folder,
                // move to the next folder
                if(this.searchedFolders.Contains(folder)) 
                {
                    continue;
                }

                // loop through each email
                foreach (object o in folder.Items)
                {
                    // only look at MailItem objects
                    if (! (o is Outlook.MailItem))
                    {
                        continue;
                    }
                    Outlook.MailItem mail = (Outlook.MailItem)o;

                    // loop through each recipient, and see if it belongs
                    // if so, add email to display list and stop, moving
                    // to next message
                    foreach (Outlook.Recipient rec in mail.Recipients)
                    {
                        String address = rec.Address;
                        if (address != null && addrs.Contains(address))
                        {
                            this.display.Add(mail);
                            break;
                        }
                        String name = rec.Name;
                        if (name != null && names.Contains(name))
                        {
                            this.display.Add(mail);
                            break;
                        }
                    }
                }

                // add this folder to the searched folder list, so we don't search it again
                this.searchedFolders.Add(folder);
            }
        }

        private void DisplayMail()
        {
            this.filterDisplay = new FilterDisplay(this.socialNetwork.RootFolders, this.folders[0]);
            this.filterDisplay.Show();
        }

        public void Finish()
        {
            this.filterDisplay.Close();
            this.filterDisplay = null;
        }

        public void Cancel()
        {
            this.filterDisplay.Close();
            this.filterDisplay = null;
        }

	}
}
