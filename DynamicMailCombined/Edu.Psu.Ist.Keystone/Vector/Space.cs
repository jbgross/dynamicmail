using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Edu.Psu.Ist.Keystone.Data;

namespace Edu.Psu.Ist.Keystone.Dimensions
{
    /// <summary>
    /// Contains a set of dimensions and generates initial random
    /// centroids.
    /// </summary>
    public class Space
    {
        private Centroid[] centroids;
        private Centroid[] oldCentroids;

        public Centroid[] Centroids
        {
            get { return centroids; }
            private set { centroids = value; }
        }

        public Space(int clusterCount, int clusterSize)
        {
            ClusterCount = clusterCount;
            Centroids = new Centroid[ClusterCount];
            this.clusterSize = clusterSize;
        }

        int clusterCount, clusterSize;

        public int ClusterCount
        {
            get { return clusterCount; }
            private set { clusterCount = value; }
        }

        private bool centroidsComplete = false;

        public bool CentroidsComplete
        {
            get { return centroidsComplete; }
            private set { centroidsComplete = value; }
        }

        private List<Vector> dimensions = new List<Vector>();

        public void AddDimension(Vector dim)
        {
            this.dimensions.Add(dim);
        }

        /// <summary>
        /// Return the current clusters - convenience method
        /// </summary>
        /// <returns>current clusters</returns>
        public Cluster[] GetCurrentClusters()
        {
            Cluster[] currentClusters = new Cluster[ClusterCount];
            for (int i = 0; i < ClusterCount; i++)
            {
                currentClusters[i] = Centroids[i].Cluster;
            }
            return currentClusters;

        }

        /// <summary>
        /// Get a Hashtable of DataElements which point to random values for
        /// each dimension to create a random Centroid
        /// </summary>
        /// <returns></returns>
        public void CreateRandomCentroids()
        {
            // create the empty centroids
            for (int cents = 0; cents < this.clusterCount; cents++)
            {
                Centroids[cents] = new Centroid(cents);
            }

            // list of unique data points
            DataElement[] dataPoints = Vector.GetUniqueDataElements();
            Random rand = new Random();

            // assign points to randomly selected centroids
            foreach (DataElement de in dataPoints)
            {
                Centroids[rand.Next(0, this.clusterCount)].AddElement(de);
            }

            foreach (Centroid c in Centroids)
            {
                Console.WriteLine(c);
            }
        }

        /// <summary>
        /// Compare each centroid to each dimension,
        /// and assign each dimension to the closest centroid
        /// </summary>
        public void CompareCentroids()
        {
            // loop through each dimension
            foreach (Vector dim in this.dimensions)
            {
                Centroid closest = null;
                int closestCount = 0;
                Hashtable closestScores = null;

                // loop through and compare to each centroid
                // find the closest
                foreach (Centroid cent in Centroids)
                {
                    // get the distances from this centroid to 
                    // each element in the dimension (will be 
                    // 0 (not contained) or 1 (contained)
                    Hashtable distances = dim.GetDistances(cent);

                    // sum the distances
                    int total = 0;
                    foreach (int d in distances.Values)
                    {
                        total += d;
                    }

                    // if this is closer (higher number) than the 
                    // previous closest centroid, pick this centroid
                    if (total > closestCount)
                    {
                        closest = cent;
                        closestCount = total;
                        closestScores = distances;
                    }
                }

                // assign this dimension to the closest centroid
                closest.Cluster.AddDataElement(dim, closestCount);
                closest.UpdateScores(closestScores, closestCount);
            }
        }

        /// <summary>
        /// Generate new centroids from averaging prior centroids
        /// </summary>
        /// <returns></returns>
        public void GenerateNewCentroids()
        {
            // stop if old centroids and new centroids are the same
            if (this.isFinished())
            {
                CentroidsComplete = true;
                return;
            }

            // create a local copy
            DataElement[] dataPoints = Vector.GetUniqueDataElements();
            List<DataElement> allVectorsLeft = new List<DataElement>();
            foreach (DataElement de in dataPoints)
            {
                allVectorsLeft.Add(de);
            }

            Centroid[] newCents = new Centroid[ClusterCount];
            for (int i = 0; i < newCents.Length; i++)
            {
                Centroid oldCentroid = Centroids[i];
                // now, changing algorithm
                Centroid newCentroid = oldCentroid.GenerateModifiedCentroid();
                foreach (DataElement de in newCentroid.Elements)
                {
                    allVectorsLeft.Remove(de);
                }
                newCents[i] = newCentroid;
            }
            this.oldCentroids = this.Centroids;
            this.centroids = newCents;

            // now, need to randomly fill from what is left
            // assign points to randomly selected centroids
            Random rand = new Random();
            foreach (DataElement de in allVectorsLeft)
            {
                Centroids[rand.Next(0, this.clusterCount)].AddElement(de);
            }

        }

        /// <summary>
        /// Check to see if the clusters in the centroids are identical
        /// If so, return true, else return false
        /// </summary>
        /// <returns></returns>
        private bool isFinished()
        {
            // first time through, we won't have old centroids,
            // so we won't have to check
            if (this.oldCentroids == null)
            {
                return false;
            }

            for (int i = 0; i < this.centroids.Length; i++)
            {
                int high = this.centroids[i].HighScore;
                float mean = this.centroids[i].MeanScore;

                List<DataElement> newMembers = this.centroids[i].Cluster.GetDataElements();
                List<DataElement> oldMembers = this.oldCentroids[i].Cluster.GetDataElements();
                //newMembers.Sort();
                //oldMembers.Sort();
                List<DataElement> topNewMembers = this.GetTopMembers(newMembers, this.centroids[i].Cluster);
                List<DataElement> topOldMembers = this.GetTopMembers(oldMembers, this.oldCentroids[i].Cluster);
                int same = 0;
                foreach (DataElement de in topOldMembers)
                {
                    if (topNewMembers.Contains(de))
                    {
                        same++;
                    }
                }

                if (same < this.clusterSize)
                {
                    return false;
                }

            }

            // if nothing has caused a false so far, then return true
            return true;
        }

        /// <summary>
        /// Get the top N members of a particular cluster, with N set as this.clusterSize
        /// </summary>
        /// <param name="members"></param>
        /// <param name="cluster"></param>
        /// <returns></returns>
        public List<DataElement> GetTopMembers(List<DataElement> members, Cluster cluster)
        {
            Hashtable highElements = new Hashtable();
            DataElement lowDe = null;
            float lowScore = 1000000;
            foreach (DataElement de in members)
            {
                float score = cluster.GetDistance(de);
                if (highElements.Count < this.clusterSize)
                {
                    highElements[de] = score;
                    if (score < lowScore)
                    {
                        lowDe = de;
                        lowScore = score;
                    }
                }
                else
                {
                    // replace the old low
                    if (score > lowScore)
                    {
                        highElements.Remove(lowDe);
                        highElements[de] = score;
                    }
                    // get the new low
                    lowScore = 100000;
                    foreach (object o in highElements.Keys)
                    {
                        float currScore = (float)highElements[o];
                        if (currScore < lowScore)
                        {
                            lowScore = currScore;
                            lowDe = (DataElement)o;
                        }
                    }
                }
            }
            List<DataElement> highs = new List<DataElement>();
            IEnumerator keys = highElements.Keys.GetEnumerator();
            while (keys.MoveNext())
            {
                highs.Add((DataElement)keys.Current);
            }
            return highs;
        }

    }
}