using System;
using System.Collections.Generic;
using System.Text;
using Edu.Psu.Ist.Keystone.Data;

namespace Edu.Psu.Ist.Keystone.Dimensions
{
    class Centroid2D : Point2D
    {
        private List<Point2D> cluster = new List<Point2D>();

        public Centroid2D()
            : base()
        {
            cluster = new List<Point2D>();
        }

        public Centroid2D(Decimal xPosition, Decimal yPosition)
            : base(xPosition, yPosition)
        {
        }
        public void AddPoint2DToCluster(Point2D point)
        {
            cluster.Add(point);
        }

        public Centroid2D Average()
        {
            Decimal totalX = 0, totalY = 0;

            foreach (Point2D point in cluster)
            {
                totalX += point.X;
                totalY += point.Y;
            }

            return new Centroid2D(
                Math.Round(totalX / cluster.Count, 2),
                Math.Round(totalY / cluster.Count, 2));
        }
    }
}
