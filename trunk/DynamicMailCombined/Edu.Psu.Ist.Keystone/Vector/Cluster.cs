using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Edu.Psu.Ist.Keystone.Data;

namespace Edu.Psu.Ist.Keystone.Dimensions
{
    /// <summary>
    /// The actual class to store a cluster, which is 
    /// a set of (DataElement, distance)
    /// </summary>
    public class Cluster
    {
        private Centroid centroid;

        public Centroid Centroid
        {
            get { return centroid; }
            private set { centroid = value; }
        }
        private Hashtable elements = new Hashtable();

        /// <summary>
        /// Get the Centroid for this cluster
        /// </summary>
        /// <param name="centroid"></param>
        public Cluster(Centroid centroid)
        {
            this.Centroid = centroid;
        }

        private bool complete;

        /// <summary>
        /// Determine if the Cluster is complete; if so, it is locked, and
        /// its members should be locked.
        /// </summary>
        public bool Complete
        {
            get { return complete; }
            set { complete = value; }
        }

        private int same = 0;

        /// <summary>
        /// The number of Cluster elements that 
        /// are the same from the last iteration
        /// </summary>
        public int Same
        {
            get { return same; }
            set { same = value; }
        }




        /// <summary>
        /// Add a data element
        /// </summary>
        /// <param name="el">The element to add</param>
        /// <param name="distance">The distance (score)</param>
        public void AddDataElement(DataElement el, float distance)
        {
            this.elements[el] = distance;
        }

        /// <summary>
        /// Get the distance (score)
        /// </summary>
        /// <param name="el">the distance (score)</param>
        /// <returns></returns>
        public float GetDistance(DataElement el)
        {
            return (float)this.elements[el];
        }

        /// <summary>
        /// Get all the data elements
        /// </summary>
        /// <returns></returns>
        public List<DataElement> GetDataElements()
        {
            List<DataElement> des = new List<DataElement>();
            int count = 0;
            foreach (DataElement d in elements.Keys)
            {
                des.Add(d);
            }
            return des;
        }

        /// <summary>
        /// Get the number of elements in the cluster
        /// </summary>
        public int Count
        {
            get { return elements.Count; }
        }



    }
}
