using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Edu.Psu.Ist.DynamicMail;

namespace Edu.Psu.Ist.DynamicMail.UnitTests
{
    [TestFixture]
    public class ObjectXmlWriterUnitTests
    {
        TestObject1 t11, t12, t13, t14, t15, t16, t17;
        TestObject2 t21, t22, t23;
        TestObject3 t31;
        String singleTest1 = "SingleObjectTest1.xml",
            singleTest2 = "SingleObjectTest2.xml",
            singleTest3 = "SingleObjectTest3.xml",
            singleTest1c = "SingleObjectTest1c.xml",
            singleTest2c = "SingleObjectTest2c.xml",
            singleTest3c = "SingleObjectTest3c.xml",
            multipleTest = "MultipleObjectTest.xml",
            multipleTestc = "MultipleObjectTestc.xml";

        [TestFixtureSetUp]
        public void Setup()
        {
            t11 = new TestObject1(3, 2, 67, (float)2.2323, .13123, 'r', "asfgwear");
            t12 = new TestObject1(4, 3, 77, (float)3.2323, .223123, 'a', "aear");
            t13 = new TestObject1(5, 4, 87, (float)4.2323, .323123, 'e', "asfgw");
            t14 = new TestObject1(6, 2345, 9734, (float)235423.2323, 426.423123, 'c', "hadfehaar");
            t15 = new TestObject1(8, 534, 9327, (float)5234.2323, 4234.423123, 'j', "aheahar");
            t16 = new TestObject1(9, 5234, 32497, (float)5.2234323, 85.423123, 'q', "aeahsr");
            t17 = new TestObject1(2, 843, 23923723, (float)235.2323, 345.423123, 'b', "eswtar");
            t21 = new TestObject2(t11, t12, t13, t14);
            t21 = new TestObject2(t11, t12, t13, t14);
            t22 = new TestObject2(t14, t15, t13, t14);
            t23 = new TestObject2(t16, t11, t13, t14);
            t31 = new TestObject3(1, (float)2.3, "fasda", t21, t22, t12, t13);
            ObjectXmlConverterRegistry.Instance.RegisterConverter(typeof(TestObject1), new TestObject1Converter());
            ObjectXmlConverterRegistry.Instance.RegisterConverter(typeof(TestObject2), new TestObject2Converter());
            ObjectXmlConverterRegistry.Instance.RegisterConverter(typeof(TestObject3), new TestObject3Converter());
        }

        [Test]
        public void WriteSingleObject()
        {
            ObjectXmlWriter writer = new ObjectXmlWriter();
            writer.WriteObjectXml(t11, singleTest1);
            writer.WriteObjectXml(t21, singleTest2);
            writer.WriteObjectXml(t31, singleTest3);
        }

        [Test]
        public void WriteMultipleObjects()
        {
            List<Object> objects = new List<Object>();
            objects.Add(t11);
            objects.Add(t21);
            objects.Add(t31);
            ObjectXmlWriter writer = new ObjectXmlWriter();
            writer.WriteObjectXml(objects, multipleTest);
        }

        [Test]
        public void WriteSingleObjectCorrectly()
        {
            List<Object> readObjects;
            ObjectXmlWriter writer = new ObjectXmlWriter();
            writer.WriteObjectXml(t11, singleTest1c);
            writer.WriteObjectXml(t21, singleTest2c);
            writer.WriteObjectXml(t31, singleTest3c);
            ObjectXmlReader reader = new ObjectXmlReader();
            readObjects = reader.ReadObjectXml("SingleObjectTest1c.xml");
            Assert.AreEqual(1, readObjects.Count);
            Assert.AreEqual(typeof(TestObject1), readObjects[0].GetType());
            Assert.AreEqual(readObjects[0], t11);
            readObjects = reader.ReadObjectXml("SingleObjectTest2c.xml");
            Assert.AreEqual(typeof(TestObject2), readObjects[0].GetType());
            Assert.AreEqual(readObjects[0], t21);
            readObjects = reader.ReadObjectXml("SingleObjectTest3c.xml");
            Assert.AreEqual(typeof(TestObject3), readObjects[0].GetType());
            Assert.AreEqual(readObjects[0], t31);
        }

        [Test]
        public void WriteMultipleObjectsCorrectly()
        {
            List<Object> readObjects;
            List<Object> objects = new List<Object>();
            objects.Add(t11);
            objects.Add(t21);
            objects.Add(t31);
            ObjectXmlWriter writer = new ObjectXmlWriter();
            writer.WriteObjectXml(objects, multipleTestc);
            ObjectXmlReader reader = new ObjectXmlReader();
            readObjects = reader.ReadObjectXml(multipleTestc);
            Assert.AreEqual(3, readObjects.Count);
            foreach (Object o in readObjects)
            {
                if (o is TestObject1)
                    Assert.AreEqual(t11, o);
                else if (o is TestObject2)
                    Assert.AreEqual(t21, o);
                else if (o is TestObject3)
                    Assert.AreEqual(t31, o);
                else
                    Assert.IsTrue(false);
            }
        }
    }
}
