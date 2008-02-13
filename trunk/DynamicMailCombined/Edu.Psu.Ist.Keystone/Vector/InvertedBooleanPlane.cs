using System;
using System.Collections.Generic;
using System.Text;
using Edu.Psu.Ist.Keystone.Data;

namespace Edu.Psu.Ist.Keystone.Dimensions
{
    /// <summary>
    /// A boolean plane, in which an element can exist or not.
    /// This is an inverted dimension because if the element 
    /// exists in the plane, then it returns 0, or 1 if it does
    /// not exist. This is to correlate with the rest of the planes.
    /// </summary>
    public class InvertedBooleanPlane : DiscretePlane
    {

        /// <summary>
        /// Get the distance between a Centroid and a DataElement
        /// If the data element is present, then the distance is 0 (present).
        /// If the data element is null, then the distance is null (absent).
        /// </summary>
        /// <param name="centroid"></param>
        /// <param name="el"></param>
        /// <returns></returns>
        public override float GetDistance(Centroid centroid, DataElement el)
        {
            if (centroid.ContainsElement(el))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public override DataElement GetRandomDataElement()
        {
            return new IntegerData(rand.Next(0, 2));
        }

    }
}
