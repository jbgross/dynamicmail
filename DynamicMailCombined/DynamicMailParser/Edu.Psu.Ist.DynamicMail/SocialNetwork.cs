using System;
using System.Collections.Generic;
using System.Text;
using Edu.Psu.Ist.DynamicMail.Interface;
using Edu.Psu.Ist.DynamicMail;
using Edu.Psu.Ist.Keystone.Data;
using Edu.Psu.Ist.DynamicMail.Parse;
using System.Collections;
using System.Windows.Forms;

namespace Edu.Psu.Ist.DynamicMail
{
    /// <summary>
    /// A social network, can be saved, loaded, and managed
    /// </summary>
    public class SocialNetwork : Finishable
    {
        private Finishable finish;
        private Account [] accounts;
        private NetworkManager manager;
        private String name;

        /// <summary>
        /// The name of the social network
        /// </summary>
        public String Name
        {
            get { return name; }
            private set { name = value; }
        }


        /// <summary>
        /// The accounts associated with the social network
        /// </summary>
        public Account[] Accounts
        {
            get { return accounts; }
            private set { accounts = value; }
        }

        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="accounts"></param>
        public SocialNetwork(List<DataElement> accounts, Finishable finish)
        {
            this.finish = finish;

            List<Account> accts = new List<Account>();
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
                accts.Add(acct);
            }
            //accts.Sort();
            Accounts = accts.ToArray();
        }


        /// <summary>
        /// Create a social network from a (stored) hashtable
        /// </summary>
        /// <param name="name"></param>
        /// <param name="members"></param>
        public SocialNetwork(String name, Hashtable members)
        {
            Name = name;
            this.accounts = new Account[members.Count];
            int index = 0;
            foreach (Object o in members.Keys)
            {
                String acctAddress = (String) o;
                String acctName = (String) members[o];
                Account acct = new Account(acctName, acctAddress);
                this.accounts[index++] = acct;
            }
        }

        /// <summary>
        /// Open a manager window
        /// </summary>
        public void Manage(Finishable finish)
        {
            this.finish = finish;
            this.manager = new NetworkManager(this, this.accounts);
        }

        /// <summary>
        /// Override the finish method, and call finish on 
        /// the 
        /// </summary>
        public void Finish()
        {
            this.Name = this.manager.NetworkName;
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
        public void Filter()
        {
            MessageBox.Show("Hey, you tried to filter " + this.Name);
        }

    }
}
