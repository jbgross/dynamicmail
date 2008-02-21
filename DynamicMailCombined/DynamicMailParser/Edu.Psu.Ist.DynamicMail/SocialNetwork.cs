using System;
using System.Collections.Generic;
using System.Text;
using Edu.Psu.Ist.DynamicMail.Interface;
using Edu.Psu.Ist.DynamicMail;
using Edu.Psu.Ist.Keystone.Data;
using Edu.Psu.Ist.DynamicMail.Parse;
using System.Collections;

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
                    Hashtable ht = Indexes.Instance.contactsAddresses;
                    ArrayList al = (ArrayList) ht[s];
                    acct.Name = (String) al[0];
                    //acct.Name = (String) ((ArrayList) Indexes.Instance.contactsAddresses[s])[0];
                }
                accts.Add(acct);
            }
            //accts.Sort();
            Accounts = accts.ToArray();

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
            this.manager.Close();
            // move to the next
            this.finish.Cancel();
        }

    }
}
