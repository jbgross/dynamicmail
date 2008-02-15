using System;
using System.Collections.Generic;
using System.Text;
using Edu.Psu.Ist.DynamicMail.Interface;
using Edu.Psu.Ist.DynamicMail;

namespace Edu.Ist.Psu.DynamicMail.Edu.Psu.Ist.DynamicMail
{
    /// <summary>
    /// A social network, can be saved, loaded, and managed
    /// </summary>
    public class SocialNetwork : Finishable
    {
        private Account [] accounts;
        private NetworkManager manager;

        private Account[] Accounts
        {
            get { return accounts; }
            set { accounts = value; }
        }

        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="addresses"></param>
        public SocialNetwork(Account [] accounts)
        {
        }

        /// <summary>
        /// Open a manager window
        /// </summary>
        public void Manage()
        {
            this.manager = new NetworkManager(this, accounts);
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

        }
        
        /// <summary>
        /// Implemented for interface
        /// </summary>
        public void Cancel()
        {
            // do nothing right now
        }

    }
}
