using System;
using System.Collections.Generic;
using System.Text;
using Edu.Psu.Ist.DynamicMail.Interface;
using Edu.Psu.Ist.DynamicMail;
using Edu.Psu.Ist.Keystone.Data;
using Edu.Psu.Ist.DynamicMail.Parse;
using System.Collections;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace Edu.Psu.Ist.DynamicMail
{
    /// <summary>
    /// A social network, can be saved, loaded, and managed
    /// </summary>
    public class SocialNetwork : Finishable
    {
        private Finishable finish;
        private SocialNetworkManager manager;
        private List<Account> accounts;
        private NetworkEditorForm editor;
        private String name;
        private Outlook.Folders rootFolders;
        private FilterMail filterMail;
        private bool isNew = true;

        /// <summary>
        /// If the social network is new or changed
        /// </summary>
        public bool IsNew
        {
            get { return isNew; }
            private set { isNew = value; }
        }

        /// <summary>
        /// The root folders
        /// </summary>
        public Outlook.Folders RootFolders
        {
            get { return rootFolders; }
            set { rootFolders = value; }
        }

        /// <summary>
        /// The name of the social network
        /// </summary>
        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// The accounts associated with the social network
        /// </summary>
        public List<Account> Accounts
        {
            get { return accounts; }
            private set { accounts = value; }
        }

        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="accounts"></param>
        public SocialNetwork(List<DataElement> accounts, Finishable finish, SocialNetworkManager manager)
        {
            this.finish = finish;
            this.manager = manager;
            Accounts = new List<Account>();
            foreach (DataElement de in accounts)
            {
                Account acct = new Account();
                String s = de.ToString();
                s = s.Remove(s.IndexOf(':'));
                acct.Address = s;
                if (Indexes.Instance.contactsAddresses.ContainsKey(s))
                {
                    acct.Name = (String) ((ArrayList) Indexes.Instance.contactsAddresses[s])[0];
                }
                Accounts.Add(acct);
            }
            IsNew = true;
        }

        public SocialNetwork(Finishable finish, SocialNetworkManager manager)
        {
            this.finish = finish;
            this.manager = manager;
            Accounts = new List<Account>();
            IsNew = true;
        }

        /// <summary>
        /// Create a social network from a (stored) hashtable
        /// </summary>
        /// <param name="name"></param>
        /// <param name="members"></param>
        public SocialNetwork(String name, Hashtable members, Finishable finish, SocialNetworkManager manager)
        {
            this.finish = finish;
            this.manager = manager;
            Name = name;
            Accounts = new List<Account>();
            foreach (Object o in members.Keys)
            {
                String acctAddress = (String)o;
                ArrayList al = (ArrayList)members[o];
                String acctName = (String)al[0];
                Account acct = new Account(acctName, acctAddress);
                Accounts.Add(acct);
            }
            IsNew = false;
        }

        /// <summary>
        /// Return a sorted list of addresses
        /// </summary>
        /// <returns></returns>
        public List<String> GetAddresses()
        {
            List<String> addrs = new List<String>();
            foreach (Account a in Accounts)
            {
                addrs.Add(a.Address);
            }
            addrs.Sort();
            return addrs;
        }

        /// <summary>
        /// Return a sorted list of names
        /// </summary>
        /// <returns></returns>
        public List<String> GetNames()
        {
            List<String> names = new List<String>();
            foreach (Account a in Accounts)
            {
                names.Add(a.Name);
            }
            names.Sort();
            return names;
        }


        /// <summary>
        /// Open a manager window
        /// </summary>
        public void Manage(Finishable finish)
        {
            this.finish = finish;
            this.editor = new NetworkEditorForm(null, this, this.manager);
        }

        /// <summary>
        /// Override the finish method, and call finish on 
        /// the 
        /// </summary>
        public void Finish()
        {
            // move to next
            this.finish.Finish();
        }
        
        /// <summary>
        /// If the canceled, don't save
        /// </summary>
        public void Cancel()
        {
            // move to the next
            this.finish.Cancel();
        }

        /// <summary>
        /// Filter the view to display only members of 
        /// this SocialNetwork
        /// </summary>
        public void FilterFolder(Outlook.MAPIFolder current)
        {
            this.filterMail = new FilterMail(this, current);
        }

    }
}
