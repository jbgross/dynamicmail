using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using NUnit.Framework;

namespace Edu.Psu.Ist.DynamicMail
{
    [TestFixture]
    public class UnitTest
    {
        //test stemmer object
        private Stemmer testStemmer = new Stemmer();

        //Test index object
        private Indexes testIndexes = new Indexes();

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
            Indexes InvertedIndexes = new Indexes();
            InvertedIndexes.receivedEmailIndex.Clear();
            InvertedIndexes.sentEmailIndex.Clear();
            InvertedIndexes.SubjectIndex.Clear();


            String subject1 = "Dave.:; is cool";
            String emailID1 = "12345";

            testStemmer.stemSubject(subject1, emailID1);

            Hashtable compareTable = new Hashtable();
            ArrayList emailIDs = new ArrayList();
            emailIDs.Add(emailID1);
            compareTable.Add("Dave", emailIDs.Clone());
            compareTable.Add("cool", emailIDs.Clone());

            String subject2 = "Dave is awesome";
            String emailID2 = "11111";
            testStemmer.stemSubject(subject2, emailID2);
            emailIDs.Add(emailID2);
            compareTable["Dave"] = emailIDs.Clone();
            emailIDs.Remove(emailID1);
            compareTable.Add("awesom", emailIDs.Clone());

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

            InvertedIndexes.saveTxt();

        }

        [Test]
        public void BianarySearch()
        {
            testIndexes.AddIndexedID("12r489ia");
            testIndexes.AddIndexedID("112kr500kda5");

            Assert.IsTrue(testIndexes.SearchAlreadyIndexed("12r489ia"));
            Assert.IsTrue(!testIndexes.SearchAlreadyIndexed("11222ddsa"));
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
    }
}
