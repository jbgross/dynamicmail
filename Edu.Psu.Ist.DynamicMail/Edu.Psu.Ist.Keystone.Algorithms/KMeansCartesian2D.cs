using System;
using System.Collections.Generic;
using System.Text;
using Edu.Psu.Ist.Keystone.Data;
using Edu.Psu.Ist.Keystone.Dimensions;

namespace Edu.Psu.Ist.Keystone.Algorithms
{
    class KMeansCartesian2D
    {
        protected List<Centroid2D> centroids;
        protected Int32 numberOfClusters;
        protected Int32 iterations;
        protected Vector dimension;

        protected virtual Vector Dimension
        {
            get { return dimension; }
            set { dimension = value; }
        }
        protected virtual Boolean DebugMode
        {
            get { return true; }
        }
        protected virtual List<DataElement> Points
        {
            get { return Dimension.Elements; }
        }
        protected virtual List<Centroid2D> Centroids
        {
            get { return centroids; }
            set { centroids = value; }
        }
        protected virtual Int32 NumberOfClusters
        {
            get { return numberOfClusters; }
            set { numberOfClusters = value; }
        }
        protected virtual Int32 Iterations
        {
            get { return iterations; }
            set { iterations = value; }
        }
        protected virtual Cartesian2DPlane Plane
        {
            get { return Dimension.Plane as Cartesian2DPlane; }
        }

        public KMeansCartesian2D(Vector dimension, Int32 numberOfClusters)
        {
            Iterations = 0;
            Dimension = dimension;
            NumberOfClusters = numberOfClusters;
            Centroids = new List<Centroid2D>(NumberOfClusters);

            for (int i = 0; i < NumberOfClusters; i++)
                Centroids.Add(new Centroid2D());
        }
        public virtual void GenerateClusters()
        {
            Boolean centroidsChanged = false;
            do
            {
                if (DebugMode)
                {
                    Iterations++;
                    Console.WriteLine("Iterations {0}", Iterations);
                }
                foreach (Point2D point in Points)
                {
                    Centroid2D closestCentroidToPoint = null;
                    Single closestDistanceToCentroid = Single.MaxValue;
                    foreach (Centroid2D centroid in Centroids)
                    {
                        Single distanceToCentroid = Plane.GetDistance(centroid, point);
                        if (distanceToCentroid < closestDistanceToCentroid)
                        {
                            closestDistanceToCentroid = distanceToCentroid;
                            closestCentroidToPoint = centroid;
                        }
                    }
                    closestCentroidToPoint.AddPoint2DToCluster(point);
                }
                List<Centroid2D> newCentroids = new List<Centroid2D>();
                foreach (Centroid2D centroid in Centroids)
                {
                    Centroid2D reCenteredCentroid = centroid.Average();
                    if (!centroidsChanged && Plane.GetDistance(centroid, reCenteredCentroid) > 0)
                        centroidsChanged = true;
                    newCentroids.Add(reCenteredCentroid);
                }
                Centroids = newCentroids;
            } while (centroidsChanged);
        }
    }
}
