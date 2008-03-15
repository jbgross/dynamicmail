using System;
using System.Collections.Generic;
using System.Text;
using Outlook = Microsoft.Office.Interop.Outlook;
using Edu.Psu.Ist.DynamicMail.Interface;
using Edu.Psu.Ist.DynamicMail.Parse;

namespace Edu.Psu.Ist.DynamicMail
{
    /// <summary>
    /// Class to filter mail and display in a new window
    /// </summary>
	public class FilterMail : Finishable
	{
        private SocialNetwork socialNetwork;
        private Dictionary<String, Outlook.MAPIFolder> folders;
        private Dictionary<String, Outlook.MAPIFolder> searchedFolders;
        private List<Outlook.MailItem> messages = new List<Outlook.MailItem>();
        private bool changed = false;
        private FolderTree folderTree;

        /// <summary>
        /// Get the messages
        /// </summary>
        public List<Outlook.MailItem> Messages
        {
            get 
            {
                // see if the filter list has changed, and
                // if it has, refilter
                if (this.changed)
                {
                    this.Refilter();
                }
                return messages; 
            }
        }

        private FilterDisplay filterDisplay;

        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="socialNetwork">The SocialNetwork to filter on</param>
        /// <param name="folder">The folder to start filtering on</param>
        public FilterMail(FolderTree folderTree, SocialNetwork socialNetwork, Outlook.MAPIFolder folder)
        {
            Logger.Instance.LogMessage("Filter Opened:\t" + socialNetwork.Name.GetHashCode());
            this.folderTree = folderTree;
            this.socialNetwork = socialNetwork;
            this.folders = new Dictionary<string,Outlook.MAPIFolder>();
            this.searchedFolders = new Dictionary<string, Outlook.MAPIFolder>();
            this.folders[folder.FullFolderPath] = folder;
            this.changed = true;
            this.DisplayMail(folder);
        }

        /// <summary>
        /// Add a folder to the search list
        /// </summary>
        /// <param name="folder"></param>
        public void AddFolder(Outlook.MAPIFolder folder)
        {
            if (! this.folders.ContainsKey(folder.FullFolderPath))
            {
                this.folders[folder.FullFolderPath] = folder;
                this.changed = true;
            }
        }

        /// <summary>
        /// Remove a folder from the search list
        /// </summary>
        /// <param name="folder"></param>
        public void RemoveFolder(Outlook.MAPIFolder folder)
        {
            if (this.folders.ContainsKey(folder.FullFolderPath))
            {
                this.folders.Remove(folder.FullFolderPath);
            }

            // check in the searchedFolders list
            if (this.searchedFolders.ContainsKey(folder.FullFolderPath))
            {
                this.searchedFolders.Remove(folder.FullFolderPath);
            }
            this.changed = true;
        }

        /// <summary>
        /// Generate display list
        /// </summary>
        /// <returns></returns>
        public void Refilter()
        {
            Logger.Instance.LogMessage("Filter Folder Count:\t" + this.folders.Values.Count);
            List<String> addrs = this.socialNetwork.GetAddresses();
            List<String> names = this.socialNetwork.GetNames();
            int totalMessagesDisplayed = 0;
            foreach (Outlook.MAPIFolder folder in this.folders.Values)
            {
                String path = folder.FullFolderPath;
                // if we've already searched this folder,
                // move to the next folder
                if(this.searchedFolders.ContainsKey(path)) 
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
                        String address = rec.Address.ToLowerInvariant();
                        if (address != null && addrs.Contains(address))
                        {
                            this.messages.Add(mail);
                            totalMessagesDisplayed++;
                            break;
                        }

                        // for comparison purposes, use lowercase
                        String name = rec.Name.ToLowerInvariant();
                        if (name != null && names.Contains(name))
                        {
                            this.messages.Add(mail);
                            totalMessagesDisplayed++;
                            break;
                        }
                    }
                }

                // add this folder to the searched folder list, so we don't search it again
                this.searchedFolders[path] = folder;
            }

            this.changed = false;
            Logger.Instance.LogMessage("Filter Message Count:\t" + totalMessagesDisplayed);
        }

        /// <summary>
        /// Create a new form to display the current mail
        /// </summary>
        private void DisplayMail(Outlook.MAPIFolder folder)
        {
            this.filterDisplay = new FilterDisplay(this.folderTree, folder.FullFolderPath, this);
        }

        /// <summary>
        /// Finish the filter
        /// </summary>
        public void Finish()
        {
            Logger.Instance.LogMessage("Filter Closed:\t" + socialNetwork.Name.GetHashCode());
            this.filterDisplay = null;
        }

        /// <summary>
        /// Cancel the filter - basically the same as Finish()
        /// </summary>
        public void Cancel()
        {
            Logger.Instance.LogMessage("Filter Closed:\t" + socialNetwork.Name.GetHashCode());
            this.filterDisplay = null;
        }

	}
}
