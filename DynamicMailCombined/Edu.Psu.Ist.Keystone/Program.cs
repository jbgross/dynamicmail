using System;
using System.Collections.Generic;
using System.Text;
using Edu.Psu.Ist.Keystone.Dimensions;
using Edu.Psu.Ist.Keystone.Data;

namespace Edu.Psu.Ist.Keystone
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<DataElement> names = new List<DataElement>();
            //Dimension d1 = new Dimension("message1", new DataType(), new InvertedBooleanPlane());
            ArtificialVectors ad = new ArtificialVectors();
            ad.Space.CreateRandomCentroids();
            while (ad.Space.CentroidsComplete == false)
            {
                // start by comparing centroids to dimensions
                ad.Space.CompareCentroids();

                // DEBUG - print out centroids
                Console.WriteLine();
                foreach (Centroid cent in ad.Space.Centroids)
                {
                    Cluster cluster = cent.Cluster;
                    Console.WriteLine(cluster.Count);
                }

                // now, get new centroids
                ad.Space.GenerateNewCentroids();

            }
            //DEBUG stop and allow me to read
            Console.ReadLine();
        }
    }
}
