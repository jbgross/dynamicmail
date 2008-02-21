using System;
using System.Collections.Generic;
using System.Text;
using Edu.Psu.Ist.DynamicMail.Interface;
using Edu.Psu.Ist.Keystone.Dimensions;

namespace Edu.Psu.Ist.DynamicMail
{
    public class SocialNetworkManager : Finishable
    {
        private Cluster[] socialNetworks;
        private int index = 0;

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
            if (index > socialNetworks.Length)
            {
                // eventually, we need to create a button for each
                return;
            }

            if (index < this.socialNetworks.Length)
            {
                Cluster cluster = this.socialNetworks[index++];
                SocialNetwork sn = new SocialNetwork(cluster.TopAccounts);
                sn.Manage(this);
            }
        }

        /// <summary>
        /// If finished, move to next social network
        /// </summary>
        public void Finish()
        {
            this.Manage();
        }

        /// <summary>
        /// If canceled, do nothing
        /// </summary>
        public void Cancel()
        {
        }

    }
}
