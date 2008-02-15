using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Edu.Psu.Ist.DynamicMail.Interface;
using System.Windows.Forms;
using Edu.Psu.Ist.Keystone.Data;
using Edu.Psu.Ist.Keystone.Dimensions;
using System.Threading;

namespace Edu.Psu.Ist.DynamicMail.Parse
{
    /// <summary>
    /// Class to ready data for clustering
    /// </summary>
    public class PrepareClusterData
    {
        InfoBox infobox = new InfoBox();
        Hashtable addressMsgs = new Hashtable();
        Space clusterSpace = null;
        Cluster[] socialNetworks = null;

        /// <summary>
        /// Constructor, opens and loads index data
        /// </summary>
        public PrepareClusterData()
        {
            Indexes indices = Indexes.Instance;
            this.addressMsgs = indices.receivedEmailIndex;
            this.infobox.AddText("Size: " + addressMsgs.Count);
            this.clusterSpace = this.CreateSpace();
            ThreadStart ts = new ThreadStart(Cluster);
            Thread t = new Thread(ts);
            t.Start();
        }

        /// <summary>
        /// Create the space for clustering
        /// </summary>
        private Space CreateSpace()
        {
            int count = 10;
            Space space = new Space(count, 20);
            foreach (Object o in this.addressMsgs.Keys)
            {
                String address = (String)o;
                ArrayList msgIds = (ArrayList) this.addressMsgs[o];

                // ignore addresses with one message
                if (msgIds.Count <= 1)
                {
                    continue;
                }

                Vector d1 = new Vector(address, new DataType(), new InvertedBooleanPlane());
                space.AddDimension(d1);
                foreach (Object a in msgIds)
                {
                    String msgId = (String)a;
                    d1.AddElement(new StringData(msgId));
                }
                d1.Complete();
            }
            return space;
        }

        /// <summary>
        /// Actually perform the clustering and return the 
        /// final clusters
        /// </summary>
        /// <returns></returns>
        private void Cluster()
        {
            this.clusterSpace.CreateRandomCentroids();
            while (clusterSpace.CentroidsComplete == false)
            {
                // start by comparing centroids to dimensions
                this.clusterSpace.CompareCentroids();

                foreach (Centroid cent in clusterSpace.Centroids)
                {
                    Cluster cluster = cent.Cluster;
                    this.infobox.AddText(cluster.Count.ToString());
                }
                this.infobox.AddText("");

                // now, get new centroids
                 this.clusterSpace.GenerateNewCentroids();

            }
            this.socialNetworks = clusterSpace.GetCurrentClusters();
        }
    }
}
