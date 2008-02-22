using System;
using System.Collections.Generic;
using System.Text;
using Edu.Psu.Ist.DynamicMail.Interface;
using Edu.Psu.Ist.Keystone.Dimensions;
using System.Collections;

namespace Edu.Psu.Ist.DynamicMail
{
    /// <summary>
    /// The class to manage all of the SocialNetwork objects
    /// </summary>
    public class SocialNetworkManager : Finishable
    {
        private Cluster[] socialNetworks;
        private int index = 0;
        private Hashtable allNetworks = new Hashtable();
        private SocialNetwork currentSn;

        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="sns"></param>
        public SocialNetworkManager(Cluster[] sns)
        {
            this.socialNetworks = sns;
            this.Manage();
        }


        /// <summary>
        /// Manage all of the social networks
        /// </summary>
        public void Manage()
        {
            if (index >= socialNetworks.Length)
            {
                // eventually, we need to create a button for each
                this.Save();
                return;
            }
            Cluster cluster = this.socialNetworks[index++];
            currentSn = new SocialNetwork(cluster.TopAccounts, this);
            currentSn.Manage(this);
        }

        /// <summary>
        /// Determine if a name is unique.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool NameIsUnique(String name)
        {
            return !this.allNetworks.ContainsKey(name);
        }

        /// <summary>
        /// Add a network to the list to save
        /// </summary>
        /// <param name="sn"></param>
        public void AddNetwork(SocialNetwork sn)
        {
            String name = currentSn.Name;
            // throw an exception if name isn't set or isn't unique
            if (name == null || name.Equals("") || !this.NameIsUnique(name))
            {
                throw new SocialNetworkException("Social Network name must be set and unique.");
            }

            Hashtable nw = new Hashtable();
            foreach (Account acct in currentSn.Accounts)
            {
                nw[acct.Address] = acct.Name;
            }
            this.allNetworks[currentSn.Name] = nw;

        }

        /// <summary>
        /// Save the social networks to a file
        /// </summary>
        private void Save()
        {
            //create an XMP writer object
            ObjectXmlWriter WriteXML = new ObjectXmlWriter();

            //send list to XML writer to write to the specified file
            WriteXML.WriteObjectXml(this.allNetworks, "c:\\groupList.xml");
        }

        /// <summary>
        /// If finished, move to next social network
        /// </summary>
        public void Finish()
        {
            this.AddNetwork(this.currentSn);
            this.Manage();
        }

        /// <summary>
        /// If canceled, move to next
        /// </summary>
        public void Cancel()
        {
            this.Manage();
        }

    }
}
