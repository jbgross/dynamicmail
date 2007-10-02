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

        //string to hold the location of the folder to hold the various output and input files
        private String Location = "C:\\Documents and Settings\\David\\Desktop\\Fall07\\IST496\\OutlookAddin2\\OutlookAddin2\\bin\\Debug\\";
        
        //public constructor
        public Indexes()
        {
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
                    if (instance == null)
                    {
                        instance = new Indexes();
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

        //method to save the singelton instace as a serialized object
        public void SaveSerial()
        {
            //open a filestream
            FileStream fs = new FileStream(Location + "Store.dat", FileMode.OpenOrCreate, FileAccess.Write);
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
            FileStream fs = new FileStream(Location + "Store.dat", FileMode.Open, FileAccess.Read);
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
            TextWriter tw = new StreamWriter(Location + "hashdata.txt");

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
