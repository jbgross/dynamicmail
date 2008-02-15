using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Edu.Psu.Ist.Keystone.Dimensions;

namespace Edu.Psu.Ist.Keystone.Data
{
    public class Centroid
    {
        private Hashtable elementScore = new Hashtable();
        private int id;
        private Cluster cluster;
        private int highScore;

        public int HighScore
        {
            get { return this.highScore; }
            private set { this.highScore = value; }
        }

        public float MeanScore
        {
            get
            {
                float total = 0;
                int count = 0;
                foreach (object o in this.elementScore.Keys)
                {
                    total += (int)this.elementScore[o];
                    count++;
                }
                if (count == 0)
                {
                    return 0;
                }
                return total / count;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Cluster Cluster
        {
            get { return cluster; }
            private set { cluster = value; }
        }

        public DataElement[] Elements
        {
            get
            {
                DataElement[] des = new DataElement[this.elementScore.Count];
                IEnumerator keys = this.elementScore.Keys.GetEnumerator();
                for (int i = 0; i < des.Length; i++)
                {
                    keys.MoveNext();
                    des[i] = (DataElement)keys.Current;
                }
                return des;
            }
        }


        /// <summary>
        /// Public constructor, takes a unique id
        /// </summary>
        /// <param name="id"></param>
        public Centroid(int id)
        {
            this.id = id;
            this.Cluster = new Cluster(this);
        }

        /// <summary>
        /// Add a new DataElement to the cluster
        /// </summary>
        /// <param name="de"></param>
        public void AddElement(DataElement de)
        {
            this.elementScore[de] = 0;
        }
        //DEBUG
        private int totalScore = 0;
        private int totalPassedIn = 0;

        public void UpdateScores(Hashtable scores, int total)
        {
            //DEBUG
            this.totalScore += total;
            foreach (object o in scores.Keys)
            {
                DataElement de = (DataElement)o;
                int score = (int)scores[o];
                // we don't care about 0's
                if (score < 1)
                {
                    continue;
                }

                //DEBUG
                this.totalPassedIn += score;

                // the element should already be in the elementScore hash
                int oldScore = (int)this.elementScore[o];
                if (oldScore > HighScore)
                {
                    HighScore = oldScore;
                }
                this.elementScore[o] = score + oldScore;
            }
        }

        /// <summary>
        /// Override the ToString method to display info about the cluster
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.id + ": " + this.elementScore.Count;
        }


        /// <summary>
        /// Determine if this Centroid contains a specific element
        /// </summary>
        /// <param name="el"></param>
        /// <returns></returns>
        public Boolean ContainsElement(DataElement el)
        {
            return this.elementScore.ContainsKey(el);
        }

        /// <summary>
        /// Given the existing centroid and its cluster, 
        /// find the new centroid via averaging. 
        /// Temporarily, this is going to be specific code 
        /// for email only
        /// </summary>
        /// <returns></returns>
        public Centroid GenerateAverageCentroid()
        {
            Hashtable guids = new Hashtable();

            List<DataElement> addresses = this.Cluster.GetDataElements();
            foreach (DataElement d in addresses)
            {
                // convert the DE to a dimension (which it will be 
                // for facet - and the dimension an address)
                Vector address = (Vector)d;
                DataElement[] des = address.GetDataElements();
                // loop through each messageid
                foreach (DataElement messageid in des)
                {
                    // if this messageid isn't in the hashtable, add it
                    if (guids.ContainsKey(messageid) == false)
                    {
                        guids.Add(messageid, null);
                    }
                }

            }

            // create a new, empty centroid
            Centroid newCent = new Centroid(this.id);

            // loop through each messageid in hashtable and add t
            // new centroid
            foreach (DataElement guid in guids.Keys)
            {
                newCent.AddElement(guid);
            }

            return newCent;
        }
        /// <summary>
        /// Given the existing centroid and its cluster, 
        /// find the new centroid via averaging. 
        /// Temporarily, this is going to be specific code 
        /// for email only
        /// This does not return a complete centroid - needs 
        /// to be filled out
        /// </summary>
        /// <returns></returns>
        public Centroid GenerateModifiedCentroid()
        {
            // get the topmost common GUIDs
            int size = this.Cluster.Count;
            // DEBUG
            int totalShouldBe = 0;
            int highest = 0;
            foreach (object o in this.elementScore.Keys)
            {
                int score = (int)this.elementScore[o];
                totalShouldBe += score;
                if (score > highest)
                {
                    highest = score;
                }
            }
            highest = highest / 10 > 5 ? highest / 10 : 5;
            DataElement[] guids = this.GetElementsAtOrAbove(highest);
            Centroid newCent = new Centroid(this.id); // give same id as old one
            foreach (DataElement guid in guids)
            {
                newCent.AddElement(guid);
            }

            return newCent;
        }
        /// <summary>
        /// Get the elements that score higher than the value passed in
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public DataElement[] GetElementsAtOrAbove(int count)
        {
            List<DataElement> els = new List<DataElement>();
            foreach (DataElement de in this.elementScore.Keys)
            {
                int currentScore = (int)this.elementScore[de];
                if (currentScore >= count)
                {
                    els.Add(de);
                }
            }
            return (DataElement[])els.ToArray();
        }
        /// <summary>
        /// Get the N elements with the highest frequency score
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public DataElement[] GetTopElements(int count)
        {
            List<DataElement> els = new List<DataElement>();
            int lowestScore = 0;
            DataElement lowestElement = null;
            foreach (object o in this.elementScore.Keys)
            {
                DataElement de = (DataElement)o;

                int currentScore = (int)this.elementScore[de];
                if (els.Count < count)
                {
                    els.Add(de);
                    lowestElement = de;
                    lowestScore = currentScore;
                }
                else if (currentScore > lowestScore)
                {
                    els.Remove(lowestElement);
                    els.Add(de);
                    int score = 1000000;
                    DataElement lowElement = null;
                    // loop through to find the next lowest
                    foreach (DataElement cde in els)
                    {
                        int cdeScore = (int)this.elementScore[cde];
                        // first time through this will of course be true
                        if (cdeScore < score)
                        {
                            lowElement = cde;
                            score = cdeScore;
                        }
                    }
                    lowestScore = score;
                    lowestElement = lowElement;
                }
            }
            return (DataElement[])els.ToArray();
        }
    }

}
