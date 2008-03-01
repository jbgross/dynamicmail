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
        private String fileName = "c:\\groupList.xml";
        private Cluster[] networkClusters;
        private int index = 0;
        private Hashtable writableNeworks = new Hashtable();
        private NetworkManagerForm managerForm;
        private bool runThroughMode = false;

        /// <summary>
        /// The number of SocialNetwork objects
        /// </summary>
        public int Count
        {
            get { return SocialNetworks.Count; }
        }

        private List<SocialNetwork> socialNetworks = new List<SocialNetwork>();

        /// <summary>
        /// A list of all SocialNetwork objects
        /// </summary>
        public List<SocialNetwork> SocialNetworks
        {
            get { return socialNetworks; }
            private set { socialNetworks = value; }
        }
        private SocialNetwork currentSn;

        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="sns"></param>
        public SocialNetworkManager(Cluster[] sns)
        {
            this.networkClusters = sns;
            this.runThroughMode = true;
            this.EditNewNetworks();
        }

        /// <summary>
        /// Public constructor to determine if we have any SocialNetworks
        /// </summary>
        public SocialNetworkManager()
        {
            try
            {
                ObjectXmlReader reader = new ObjectXmlReader();
                SocialNetworks = new List<SocialNetwork>();
                List<Object> obj = reader.ReadObjectXml(this.fileName);
                if (obj.Count == 0)
                {
                    return;
                }
                else
                {
                    Hashtable outer = (Hashtable)obj[0];
                    foreach (Object nameObj in outer.Keys)
                    {
                        String snName = (String)nameObj;
                        Hashtable inner = (Hashtable)outer[snName];
                        SocialNetwork sn = new SocialNetwork(snName, inner, this, this);
                        SocialNetworks.Add(sn);
                    }
                }
            }
            catch
            {
                // probably no file found
                // might just log the exception
            }
        }

        /// <summary>
        /// Manage existing networks
        /// </summary>
        public void ManageNetworks()
        {
            this.managerForm = new NetworkManagerForm(this);
        }


        /// <summary>
        /// Manage all of the new social networks
        /// </summary>
        public void EditNewNetworks()
        {
            if (index >= networkClusters.Length)
            {
                // eventually, we need to create a button for each
                this.Save();
                return;
            }
            Cluster cluster = this.networkClusters[index++];
            this.currentSn = new SocialNetwork(cluster.TopAccounts, this, this);
            this.currentSn.Manage(this);
        }

        /// <summary>
        /// Determine if a name is unique.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool NameIsUnique(String name)
        {
            return !this.writableNeworks.ContainsKey(name);
        }

        /// <summary>
        /// Add a network to the list to save
        /// </summary>
        /// <param name="sn"></param>
        public void AddNetwork(SocialNetwork sn)
        {
            this.currentSn = sn;
            String name = currentSn.Name;
            // throw an exception if name isn't set or isn't unique
            if (name == null || name.Equals(""))
            {
                throw new SocialNetworkException("Social Network name must be set.");
            }
            
            // throw an exception if the name isn't unique and this is a new network
            if(currentSn.IsNew && !this.NameIsUnique(name))
            {
                throw new SocialNetworkException("Social Network name unique.");
            }

            Hashtable nw = new Hashtable();
            foreach (Account acct in currentSn.Accounts)
            {
                // stupid - need to save as an arraylist
                ArrayList al = new ArrayList();
                al.Add(acct.Name);
                nw[acct.Address] = al;
            }
            this.writableNeworks[currentSn.Name] = nw;
            this.currentSn = null;
        }

        /// <summary>
        /// Save the social networks to a file
        /// </summary>
        public void Save()
        {
            //create an XMP writer object
            ObjectXmlWriter WriteXML = new ObjectXmlWriter();

            //send list to XML writer to write to the specified file
            WriteXML.WriteObjectXml(this.writableNeworks, this.fileName);
        }

        /// <summary>
        /// If finished, move to next social network
        /// </summary>
        public void Finish()
        {
            this.AddNetwork(this.currentSn);
            if (this.runThroughMode)
            {
                this.EditNewNetworks();
            }
        }

        /// <summary>
        /// If canceled, move to next
        /// </summary>
        public void Cancel()
        {
            if (this.runThroughMode)
            {
                this.EditNewNetworks();
            }
        }

    }
}
