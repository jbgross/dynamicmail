2.using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Edu.Psu.Ist.DynamicMail
{
    [Serializable()]
    public class Indexes
    {
        //singlton class instance
        private static Indexes instance=null;
        private static readonly object padlock = new object();

        //arraylist to store all of the entry ids that have been indexed
        private ArrayList AlreadyIndexed = new ArrayList();
        

        //hashtable to hold the inverted index of subject words
        public Hashtable SubjectIndex = new Hashtable();
        //hashtable to hold the inverted index of received emails
        public Hashtable receivedEmailIndex = new Hashtable();
        //hashtable to hold the invereted index of sent emails
        public Hashtable sentEmailIndex = new Hashtable();
        //hashtable to hold the invereted index of contacts
        public Hashtable contactsIndex = new Hashtable();
        //hashtable to hold the inverted index of contacts
        public Hashtable contactsAddresses = new Hashtable();

        //public constructor
        private Indexes()
        {
            
        }

        //public method to write the indexes to XML
        public void WriteIndexToXML()
        {
            //create an XML writer object
            ObjectXmlWriter WriteXML = new ObjectXmlWriter();

            //creata list of all the indexes and add each index to it
            List<Object> IndexList = new List<Object>();
            IndexList.Add(SubjectIndex);
            IndexList.Add(receivedEmailIndex);
            IndexList.Add(sentEmailIndex);
            IndexList.Add(contactsIndex);
            IndexList.Add(contactsAddresses);
            IndexList.Add(AlreadyIndexed);

            //Send the list the the XML writer in order to write it to the specified file
            WriteXML.WriteObjectXml(IndexList, "c:\\emailparse2.xml");
        }


        //public method to read the XML datafile and place the data within the indexes
        public void ReadIndexFromXML()
        {
            //create an XML reader object
            ObjectXmlReader ReadXML = new ObjectXmlReader();
            //get a list of all the Hashtables within the XML file
            List<Object> IndexList = ReadXML.ReadObjectXml("c:\\emailparse2.xml");

            //assign each hashtable to the proper index based on the order that they were saved
            //within the write method
            SubjectIndex = (Hashtable)IndexList[0];
            receivedEmailIndex = (Hashtable)IndexList[1];
            sentEmailIndex = (Hashtable)IndexList[2];
            contactsIndex = (Hashtable)IndexList[3];
            contactsAddresses = (Hashtable)IndexList[4];
            AlreadyIndexed = (ArrayList)IndexList[5];
        }

        //method to get the singelton instance of indexes
        public static Indexes Instance
        {
            get
            {
                //lock to make the singleton class thread safe
                lock (padlock)
                {
                    //if there is no current instance create a new one
                    //Try to read the XML file to import any saved indexes
                    if (instance == null)
                    {
                        instance = new Indexes();
                            instance.ReadIndexFromXML();
                    }
                    //return the sigelton instance
                    return instance;
                }
            }

            private set
            {
                instance = value;
            }
        }

        //Method to search for an index and return a boolean whether or not it is indexed
        public bool SearchAlreadyIndexed(String SearchIndex)
        {
            //to a binary search and get the index
            int i = AlreadyIndexed.BinarySearch(SearchIndex);
            //if there is an index return true
            if (i >= 0)
                return true;
            //if there is no index the return false
            return false;
        }

        //method to add a new indexid to the Already Indexed arraylist
        public void AddIndexedID(String NewIndex)
        {
            AlreadyIndexed.Add(NewIndex);
            AlreadyIndexed.Sort();
        }

        //method to get the arraylist of already indexed emails
        public ArrayList GetAllIndexed()
        {
            return AlreadyIndexed;
        }

        //method to save the singelton instace as a serialized object
        public void SaveSerial()
        {
            //open a filestream
            FileStream fs = new FileStream("Store.dat", FileMode.OpenOrCreate, FileAccess.Write);
            try
            {
                //ArrayList a = new ArrayList();
                //a.Add(SubjectIndex);
                //a.Add(receivedEmailIndex);
                //a.Add(sentEmailIndex);

                //create a new binary formater
                BinaryFormatter bf = new BinaryFormatter();

                //serialize the singelton instance to the filestream
                bf.Serialize(fs, Indexes.instance);
            }
            finally
            {
                //close the file stream
                fs.Close();
            }
        }

        //method to deserialize the singelton class
        public void LoadSerial()
        {
            //open a filestream
            FileStream fs = new FileStream("Store.dat", FileMode.Open, FileAccess.Read);
            try
            {
                //ArrayList a = new ArrayList();
                
                //create a binary formater
                BinaryFormatter bf = new BinaryFormatter();

                //set the singelton class to the deserialized object
                Indexes.Instance = (Indexes)bf.Deserialize(fs);

                //a = (ArrayList)bf.Deserialize(fs);
                //SubjectIndex = (Hashtable)a[0];
                //receivedEmailIndex = (Hashtable)a[1];
                //sentEmailIndex = (Hashtable)a[2];
            }
            finally
            {
                //close the file stream
                fs.Close();
            }
        }

        public void saveTxt()
        {
             // create a writer and open the file
            TextWriter tw = new StreamWriter("hashdata.txt");

            bool firstLoop = true;
            // write a line of text to the file
            tw.WriteLine("ReceivedEmails");
            foreach (object keys in receivedEmailIndex.Keys)
            {

                tw.Write(keys.ToString());
                tw.Write(":");
                foreach (String ids in (ArrayList)receivedEmailIndex[keys.ToString()])
                {
                    if (firstLoop)
                    {
                        firstLoop = false;
                    }
                    else
                    {
                        tw.Write(",");
                    }
                    tw.Write(ids);
                }
                firstLoop = true;
                tw.WriteLine("");
            }
            tw.WriteLine("SentEmails");
            foreach (object keys in sentEmailIndex.Keys)
            {

                tw.Write(keys.ToString());
                tw.Write(":");
                foreach (String ids in (ArrayList)sentEmailIndex[keys.ToString()])
                {
                    if (firstLoop)
                    {
                        firstLoop = false;
                    }
                    else
                    {
                        tw.Write(",");
                    }
                    tw.Write(ids);
                }
                firstLoop = true;
                tw.WriteLine("");
            }

            tw.WriteLine("SubjectLines");
            foreach (object keys in SubjectIndex.Keys)
            {

                tw.Write(keys.ToString());
                tw.Write(":");
                foreach (String ids in (ArrayList)SubjectIndex[keys.ToString()])
                {
                    if (firstLoop)
                    {
                        firstLoop = false;
                    }
                    else
                    {
                        tw.Write(",");
                    }
                    tw.Write(ids);
                }
                firstLoop = true;
                tw.WriteLine("");
            }

            // close the stream
            tw.Close();
        }
    }
}