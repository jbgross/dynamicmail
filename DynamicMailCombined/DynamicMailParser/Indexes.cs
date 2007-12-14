using System;
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
        

        
        /// <summary>
        /// hashtable to hold the inverted index of subject words
        /// </summary>
        public Hashtable SubjectIndex = new Hashtable();
        /// <summary>
        /// hashtable to hold the inverted index of received emails
        /// </summary>
        public Hashtable receivedEmailIndex = new Hashtable();
        /// <summary>
        /// hashtable to hold the invereted index of sent emails
        /// </summary>
        public Hashtable sentEmailIndex = new Hashtable();
        /// <summary>
        /// hashtable to hold the invereted index of contacts
        /// </summary>
        public Hashtable contactsIndex = new Hashtable();
        /// <summary>
        /// hashtable to hold the inverted index of contacts
        /// </summary>
        public Hashtable contactsAddresses = new Hashtable();

        /// <summary>
        /// Blank private constructor.  All instanciations should be done through the singelton instance.
        /// </summary>
        private Indexes()
        {
            
        }

        /// <summary>
        ///     Method to write the all of the indexes to XML.  It only needs called from the singleton instance.
        /// </summary>
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

        /// <summary>
        /// public method to read the XML datafile and place the data within the indexes
        /// </summary>
        
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

        /// <summary>
        /// method to get the singelton instance of indexes
        /// </summary>
        /// <returns> Current instance of Indexes</returns>
        
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
                        try
                        {
                            instance.ReadIndexFromXML();
                        }

                        catch (Exception e)
                        {
                       }
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
        /// <summary>
        /// Method to search for an index and return a boolean whether or not it is indexed
        /// </summary>
        /// <param name="SearchIndex">GUID for Mail Item represented as a string which is used to search the already indexed array</param>
        /// <returns>Boolean value that represents whether or not the GUID exists in the already indexed array</returns>
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
        /// <summary>
        /// method to add a new indexid to the Already Indexed arraylist
        /// </summary>
        /// <param name="NewIndex">GUID for Mail Item represented as a string</param>
        public void AddIndexedID(String NewIndex)
        {
            AlreadyIndexed.Add(NewIndex);
            AlreadyIndexed.Sort();
        }

        /// <summary>
        /// method to get the arraylist of already indexed emails
        /// </summary>
        /// <returns>Array list of all the indexed Mail Object GUIDs</returns>
        public ArrayList GetAllIndexed()
        {
            return AlreadyIndexed;
        }

        /// <summary>
        /// method to save the singelton instace as a serialized object
        /// </summary>
        
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

        /// <summary>
        /// method to deserialize the singelton class
        /// </summary>
        
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

        /// <summary>
        ///  Method to write the indexes to a txt file
        /// </summary>
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
