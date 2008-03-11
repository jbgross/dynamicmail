using System;
using System.Collections.Generic;
using System.Text;
using Edu.Psu.Ist.Keystone.Data;


namespace Edu.Psu.Ist.Keystone.Dimensions
{
    /// <summary>
    /// This is soooo not done. Use Cartesian2DPlane. JBG 2007-11-30
    /// </summary>
    class DecimalPlane : DiscretePlane
    {
        public override DataElement GetRandomDataElement()
        {
            return new Point2D();
        }

        public override float GetDistance(Centroid centroid, DataElement el)
        {
            return 0;
        }
    }


}
