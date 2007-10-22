using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using NUnit.Framework;
using Office = Microsoft.Office.Core;
using System.Runtime.InteropServices;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.IO;

namespace Edu.Psu.Ist.DynamicMail
{
    [TestFixture]
    public class UnitTest
    {
        //test inbox
        Outlook.Items inbox;
        
        //test sentBox
        private Outlook.Items sentBox;

        //test contacts
        private Outlook.MAPIFolder contacts;

        //test stemmer object
        private Stemmer testStemmer = new Stemmer();

        //Test index object
        private Indexes testIndexes = Indexes.Instance;

        //Unit test to verify that the stemmer can accept a string and then stemm it
        //Pass it Testing and should return Test
        [Test]
        public void TestStemmer()
        {
            String testString = "Testing";

            testStemmer.stem(testString);

            Assert.AreEqual(testStemmer.ToString(), "Test");
        }

        //Unit test to verify that when a subject is passed it is ran through the stop word list
        //then the words are stemmed, then they are put into the inverted index
        [Test]
        public void TestSubjectStem()
        {
            //reset the inverted indexes
            Indexes InvertedIndexes = Indexes.Instance;
            InvertedIndexes.receivedEmailIndex.Clear();
            InvertedIndexes.sentEmailIndex.Clear();
            InvertedIndexes.SubjectIndex.Clear();

            //create subject lines to stem
            String subject1 = "Dave.:; is cool";
            String emailID1 = "12345";

            //test the stemming of the subject
            testStemmer.stemSubject(subject1, emailID1);

            //create a hashtable of how the stemmer should have processed the subject line
            Hashtable compareTable = new Hashtable();
            ArrayList emailIDs = new ArrayList();
            emailIDs.Add(emailID1);
            compareTable.Add("Dave", emailIDs.Clone());
            compareTable.Add("cool", emailIDs.Clone());

            //add another subject and email to the stemmer and add the correct output to the test hashtable
            String subject2 = "Dave is awesome";
            String emailID2 = "11111";
            testStemmer.stemSubject(subject2, emailID2);
            emailIDs.Add(emailID2);
            compareTable["Dave"] = emailIDs.Clone();
            emailIDs.Remove(emailID1);
            compareTable.Add("awesom", emailIDs.Clone());

            //go through the test table and the indexes and make sure that they are equal
            foreach (object keys in InvertedIndexes.SubjectIndex.Keys)
            {
                Assert.Contains(keys, compareTable.Keys);
                Console.WriteLine(keys.ToString());
                foreach (String ids in (ArrayList)InvertedIndexes.SubjectIndex[keys.ToString()])
                {
                    
                    Assert.Contains(ids, (ArrayList)compareTable[keys.ToString()]);
                    Console.WriteLine(ids);
                }
            }
            //save the indexes to a test file
            InvertedIndexes.saveTxt();

        }

        //unit test to verify that the binary search is working
        [Test]
        public void BianarySearch()
        {
            //add indexes to the aleardy indexed array
            testIndexes.AddIndexedID("12r489ia");
            testIndexes.AddIndexedID("112kr500kda5");

            //verify that the two indexes are found
            Assert.IsTrue(testIndexes.SearchAlreadyIndexed("12r489ia"));
            Assert.IsTrue(!testIndexes.SearchAlreadyIndexed("11222ddsa"));
        }

        //unit test to verify that the indexes for already indexed emails is sorting
        [Test]
        public void TestIndexSort()
        {
            //add indexs to the already indexed array
            testIndexes.AddIndexedID("111111d");
            testIndexes.AddIndexedID("111111c");
            testIndexes.AddIndexedID("11111a1");
            testIndexes.AddIndexedID("111111a");

            //get the arraylist of all the indexed emails
            ArrayList TestIndexSortArray = testIndexes.GetAllIndexed();

            //validate that they are indexed
            Assert.AreEqual("111111d", TestIndexSortArray[2]);
            Assert.AreEqual("111111a", TestIndexSortArray[0]);
        }

        //Unit test to verify that the logging is working
        [Test]
        public void TestLogger()
        {
            //get the Instance of logger
            Logger testLogger = Logger.Instance;

            //String to store the message to be logged
            String message = "Unit testing the dynamic mail logger";
            //string to store the last line of the reader
            String ReadLine = "";

            //write  message through the log file
            testLogger.logMessage(message);

            //read through the log file to get the last line in the file
            using (StreamReader sr = new StreamReader(testLogger.LogLocation + testLogger.LogFileName))
            {
                String line;

                while ((line = sr.ReadLine()) != null)
                {
                    ReadLine = line;
                }

            }

            //verify that the message was written
            Assert.IsTrue(ReadLine.Contains(message));

            //verify that the date and time the log was written is less than or equal to the current time
            char[] denom = { '\t', '-', ':' };

            String[] time = ReadLine.Split(denom);

            Assert.LessOrEqual(Convert.ToInt32(time[0]),DateTime.Now.Year);
            Assert.LessOrEqual(Convert.ToInt32(time[1]), DateTime.Now.Month);
            Assert.LessOrEqual(Convert.ToInt32(time[2]), DateTime.Now.Day);
            Assert.LessOrEqual(Convert.ToInt32(time[3]), DateTime.Now.Hour);
            Assert.LessOrEqual(Convert.ToInt32(time[4]), DateTime.Now.Minute);
            Assert.LessOrEqual(Convert.ToInt32(time[5]), DateTime.Now.Second);
            
        }

        //Unit test to verify that the serialization save and load methods work
        //currently serialization is not used
        /*[Test]
        public void TestSerialize()
        {
            Indexes InvertedIndexes = new Indexes();
            ArrayList receive = new ArrayList();
            receive.Add("11111");
            InvertedIndexes.receivedEmailIndex.Add("Test@received.com", receive);


            ArrayList sent = new ArrayList();
            sent.Add("2222");
            InvertedIndexes.sentEmailIndex.Add("Test@sent.com", sent);

            ArrayList subject = new ArrayList();
            subject.Add("333");
            InvertedIndexes.SubjectIndex.Add("Dave", subject);

            InvertedIndexes.SaveSerial();
            Hashtable TestRecieve = (Hashtable)InvertedIndexes.receivedEmailIndex.Clone();
            Hashtable TestSent = (Hashtable)InvertedIndexes.sentEmailIndex.Clone();
            Hashtable TestSubject = (Hashtable)InvertedIndexes.SubjectIndex.Clone();

            InvertedIndexes.receivedEmailIndex.Clear();
            InvertedIndexes.sentEmailIndex.Clear();
            InvertedIndexes.SubjectIndex.Clear();

            InvertedIndexes.LoadSerial();

            foreach (object keys in InvertedIndexes.receivedEmailIndex.Keys)
            {
                Assert.Contains(keys, TestRecieve.Keys);
                Console.WriteLine(keys.ToString());
                foreach (String ids in (ArrayList)InvertedIndexes.receivedEmailIndex[keys.ToString()])
                {

                    Assert.Contains(ids, (ArrayList)TestRecieve[keys.ToString()]);
                    Console.WriteLine(ids);
                }
            }

            foreach (object keys in InvertedIndexes.sentEmailIndex.Keys)
            {
                Assert.Contains(keys, TestSent.Keys);
                Console.WriteLine(keys.ToString());
                foreach (String ids in (ArrayList)InvertedIndexes.sentEmailIndex[keys.ToString()])
                {

                    Assert.Contains(ids, (ArrayList)TestSent[keys.ToString()]);
                    Console.WriteLine(ids);
                }
            }

            foreach (object keys in InvertedIndexes.SubjectIndex.Keys)
            {
                Assert.Contains(keys, TestSubject.Keys);
                Console.WriteLine(keys.ToString());
                foreach (String ids in (ArrayList)InvertedIndexes.SubjectIndex[keys.ToString()])
                {

                    Assert.Contains(ids, (ArrayList)TestSubject[keys.ToString()]);
                    Console.WriteLine(ids);
                }
            }

            

        }*/

        [Test]
        public void TestInboxIndexer()
        {
            //Outlook.MailItem email1 = (Outlook.MailItem)this.CreateItem(Outlook.OlItemType.olMailItem);
            //email1.Subject = "This is the subject";
            
            

        }

    }
}
