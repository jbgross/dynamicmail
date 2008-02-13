using System;
using System.Collections.Generic;
using System.Text;
using Edu.Psu.Ist.Keystone.Data;

namespace Edu.Psu.Ist.Keystone.Dimensions
{
    /// <summary>
    /// This class identifies the type of dimension,
    /// and by extension, the distance computation.
    /// </summary>
    public abstract class PlaneType
    {
        protected static Random rand = new Random();

        /// <summary>
        /// This 
        /// </summary>
        /// <param name="centroid">The centroid to compare to </param>
        /// <param name="el"></param>
        /// <returns>The square of the Euclidean distance, 0-1, from centroid to element</returns>
        public abstract float GetDistance(Centroid centroid, DataElement el);

        public abstract DataElement GetRandomDataElement();
    }
}
