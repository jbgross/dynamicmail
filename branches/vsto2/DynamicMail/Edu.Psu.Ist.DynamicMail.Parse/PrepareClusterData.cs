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
    public class PrepareClusterData : Finishable
    {
        private InfoBox infoBox = new InfoBox();
        private Hashtable addressMsgs = new Hashtable();
        private Space clusterSpace = null;
        private Indexes indices = null;

        private GetNumericInput getClusterCount = null;
        private GetNumericInput getClusterSize = null;

        private int clusterCount = 0;
        private int clusterSize = 0;

        // for the caller
        private Cluster[] networks = null;
        private Finishable finish = null;

        public Cluster[] Networks
        {
            get { return networks; }
            private set { networks = value; }
        }

        /// <summary>
        /// Constructor, opens and loads index data
        /// </summary>
        public PrepareClusterData(Finishable finish)
        {
            // set the caller
            this.finish = finish;

            // load the indexes, if they haven't already been loaded
            this.indices = Indexes.Instance;

            // get the number and size of clusters
            this.GetInputs();
        }

        /// <summary>
        /// Start by getting the number of clusters (social networks)
        /// </summary>
        private void GetInputs()
        {
            this.getClusterCount = new
                GetNumericInput("Number of Social Networks:", 10, this);
        }

        /// <summary>
        /// Called by a window to indicate that it is finished
        /// We have two windows - one for social network size
        /// and one for number of social networks, so we have to
        /// figure out which is finished
        /// </summary>
        public void Finish()
        {
            // if it's clusterCount, get that value
            if (this.getClusterCount != null)
            {
                this.clusterCount = this.getClusterCount.Count;
                this.getClusterCount = null; // release from memory
                this.getClusterSize = new 
                    GetNumericInput("Number of members for each Social Network:", 20, this);
                return;
            } 
            else if (this.getClusterSize != null)
            {
                this.clusterSize = this.getClusterSize.Count;
                this.getClusterSize = null; // release from memory
                this.StartClustering();
            }
        }

        /// <summary>
        /// Called from window, just stop the process
        /// </summary>
        public void Cancel()
        {
            this.infoBox.Close();
        }

        private void StartClustering()
        {
            Logger.Instance.LogMessage("Started Clustering");
            Logger.Instance.LogMessage("Cluster Count:\t" + this.clusterCount);
            Logger.Instance.LogMessage("Cluster Size:\t" + this.clusterSize);
            this.addressMsgs = indices.receivedEmailIndex;
            this.infoBox.AddText("Size: " + addressMsgs.Count);
            this.clusterSpace = this.CreateSpace();
            this.Cluster();
        }

        /// <summary>
        /// Create the space for clustering
        /// </summary>
        private Space CreateSpace()
        {
            Space space = new Space(clusterCount, clusterSize);
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
                // now, get new centroids
                this.clusterSpace.GenerateNewCentroids();

                foreach (Centroid cent in clusterSpace.Centroids)
                {
                    Cluster cluster = cent.Cluster;
                    this.infoBox.AddText(cluster.Same.ToString());
                }
                this.infoBox.AddText("");
            }
            Networks = clusterSpace.GetCurrentClusters();
            this.infoBox.Close();
            this.finish.Finish();
            Logger.Instance.LogMessage("Clustering Complete");
        }
    }
}
