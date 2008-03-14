using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Edu.Psu.Ist.Keystone.Data;

namespace Edu.Psu.Ist.Keystone.Dimensions
{
    class ArtificialVectors
    {
        private Space space = new Space(5, 20);

        public Space Space
        {
            get { return space; }
        }

        public ArtificialVectors()
        {
            StreamReader sr = File.OpenText("test-address.txt");
            List<String> names = new List<String>();
            String name = null;
            do
            {
                name = sr.ReadLine();
                names.Add(name);
                //Console.WriteLine(name);
            } while (name != null);

            // get rid of last name, which will be null
            names.RemoveAt(names.Count - 1);

            Random rand = new Random();


            int numberOfMessages = rand.Next(20, 31);
            foreach (String nameData in names)
            {
                Vector d1 = new Vector(nameData,
                    new DataType(),
                    new InvertedBooleanPlane());
                int howManyMessages = rand.Next(5, 200);
                for (int k = 0; k < howManyMessages; k++)
                {
                    d1.AddElement(new StringData(rand.Next(1, 1000).ToString()));
                }
                space.AddDimension(d1);
                Console.WriteLine(d1);
                d1.Complete();
            }

        }
    }
}
