using System;
using System.Collections.Generic;
using System.Text;
using Edu.Psu.Ist.DynamicMail;
using NUnit.Framework;

namespace Edu.Psu.Ist.DynamicMail.UnitTests
{
    [TestFixture]
    public class ObjectXmlReaderUnitTests
    {

        [TestFixtureSetUp]
        public void SetupConverters()
        {
            ObjectXmlConverterRegistry.Instance.RegisterConverter(typeof(TestObject1), new TestObject1Converter());
            ObjectXmlConverterRegistry.Instance.RegisterConverter(typeof(TestObject2), new TestObject2Converter());
            ObjectXmlConverterRegistry.Instance.RegisterConverter(typeof(TestObject3), new TestObject3Converter());
        }

        [Test]
        public void ReadValidXml()
        {
            ObjectXmlReader reader = new ObjectXmlReader();
            List<Object> objects = reader.ReadObjectXml("ValidRead.xml");
        }

        [Test]
        [ExpectedException(typeof(InvalidObjectIDException))]
        public void ReadInvalidXml()
        {
            ObjectXmlReader reader = new ObjectXmlReader();
            List<Object> objects = reader.ReadObjectXml("InvalidRead.xml");
        }

        [Test]
        public void ReadValidXmlCorrectly()
        {
            TestObject1 t11 = new TestObject1(3, 2, 67, (float)2.2323, .13123, 'r', "asfgwear"),
                t12 = new TestObject1(4, 3, 77, (float)3.2323, .223123, 'a', "aear"),
                t13 = new TestObject1(5, 4, 87, (float)4.2323, .323123, 'e', "asfgw"),
                t14 = new TestObject1(6, 2345, 9734, (float)235423.2323, 426.423123, 'c', "hadfehaar"),
                t15 = new TestObject1(8, 534, 9327, (float)5234.2323, 4234.423123, 'j', "aheahar"),
                t16 = new TestObject1(9, 5234, 32497, (float)5.2234323, 85.423123, 'q', "aeahsr"),
                t17 = new TestObject1(2, 843, 23923723, (float)235.2323, 345.423123, 'b', "eswtar");
            TestObject2 t21 = new TestObject2(t11, t12, t13, t14),
                t22 = new TestObject2(t14, t15, t13, t14),
                t23 = new TestObject2(t16, t11, t13, t14);
            TestObject3 t31 = new TestObject3(1, (float)2.3, "fasda", t21, t22, t12, t13);
            List<Object> toBeSaved = new List<object>();
            toBeSaved.Add(t17);
            toBeSaved.Add(t31);
            toBeSaved.Add(t23);
            ObjectXmlWriter writer = new ObjectXmlWriter();
            writer.WriteObjectXml(toBeSaved, "testRead.xml");
            ObjectXmlReader reader = new ObjectXmlReader();
            List<Object> readObjects = reader.ReadObjectXml("testRead.xml");
            Assert.AreEqual(3, readObjects.Count);
            foreach (Object o in readObjects)
            {
                if (o is TestObject1)
                    Assert.AreEqual(t17, o);
                else if (o is TestObject2)
                    Assert.AreEqual(t23, o);
                else if (o is TestObject3)
                    Assert.AreEqual(t31, o);
                else
                    Assert.IsTrue(false);
            }
        }
    }
}
