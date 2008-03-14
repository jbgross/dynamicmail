using System;
using System.Collections.Generic;
using System.Text;
using Edu.Psu.Ist.Keystone.Data;

namespace Edu.Psu.Ist.Keystone.Dimensions
{
    class Cartesian2DPlane : DiscretePlane
    {
        public override DataElement GetRandomDataElement()
        {
            return new Point2D();
        }

        public override float GetDistance(Centroid centroid, DataElement el)
        {
            throw new Exception("Not implemented in abstract dimensions yet.");
        }

        public virtual float GetDistance(Centroid2D centroid, DataElement el)
        {
            Point2D point = (Point2D)el;
            float xDist = (float)Math.Abs(point.X - centroid.X);
            float yDist = (float)Math.Abs(point.Y - centroid.Y);
            return xDist * xDist + yDist * yDist;
        }
    }
}
