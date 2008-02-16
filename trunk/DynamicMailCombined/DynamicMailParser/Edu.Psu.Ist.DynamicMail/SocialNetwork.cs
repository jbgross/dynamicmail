using System;
using System.Collections.Generic;
using System.Text;
using Edu.Psu.Ist.DynamicMail.Interface;
using Edu.Psu.Ist.DynamicMail;
using Edu.Psu.Ist.Keystone.Data;

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

        public Account[] Accounts
        {
            get { return accounts; }
            private set { accounts = value; }
        }

        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="addresses"></param>
        public SocialNetwork(List<DataElement> accounts)
        {
            List<Account> accts = new List<Account>();
            foreach (DataElement de in accounts)
            {
                Account acct = new Account();
                String s = de.ToString();
                if (s.Contains("@"))
                {
                    acct.Address = s;
                }
                else
                {
                    acct.Name = s;
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

        public void Finish()
        {
            //create an XMP writer object
            ObjectXmlWriter WriteXML = new ObjectXmlWriter();

            //create Index List of the dataGridView's columns
            List<Object> groupIList = new List<Object>();
            groupIList.Add(this.manager.NetworkData.Columns.IndexOf(this.manager.NameColumn));
            groupIList.Add(this.manager.NetworkData.Columns.IndexOf(this.manager.AddressColumn));

            //send list to XML writer to write to the specified file
            WriteXML.WriteObjectXml(groupIList, "c:\\groupList.xml");

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
            this.finish.Finish();
        }

    }
}
