using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ObjectXml;

namespace ObjectXmlUnitTests
{
    [TestFixture]
    public class ObjectXmlConverterRegistryUnitTests
    {
        [TestFixtureSetUp]
        public void SetupConverters()
        {
            ObjectXmlConverterRegistry.Instance.RegisterConverter(typeof(TestObject1), new TestObject1Converter());
            ObjectXmlConverterRegistry.Instance.RegisterConverter(typeof(TestObject2), new TestObject2Converter());
            ObjectXmlConverterRegistry.Instance.RegisterConverter(typeof(TestObject3), new TestObject3Converter());
        }

        [Test]
        public void TestSingleton()
        {
            Assert.AreSame(ObjectXmlConverterRegistry.Instance, ObjectXmlConverterRegistry.Instance);
        }

        [Test]
        [ExpectedException(typeof(NoConverterAvailable))]
        public void TestInvalidConverterLookup()
        {
            ObjectXmlConverterRegistry.Instance.LookUpConverter(typeof(ObjectXmlConverterRegistry));
                    }

        [Test]
        public void TestValidConverterLookup()
        {
            Assert.IsNotNull(ObjectXmlConverterRegistry.Instance.LookUpConverter(typeof(TestObject1)));
            Assert.IsInstanceOfType(typeof(TestObject1Converter), (ObjectXmlConverterRegistry.Instance.LookUpConverter(typeof(TestObject1))));
            Assert.IsNotNull(ObjectXmlConverterRegistry.Instance.LookUpConverter(typeof(TestObject2)));
            Assert.IsInstanceOfType(typeof(TestObject2Converter), (ObjectXmlConverterRegistry.Instance.LookUpConverter(typeof(TestObject2))));
            Assert.IsNotNull(ObjectXmlConverterRegistry.Instance.LookUpConverter(typeof(TestObject3)));
            Assert.IsInstanceOfType(typeof(TestObject3Converter), (ObjectXmlConverterRegistry.Instance.LookUpConverter(typeof(TestObject3))));
        }

        [Test]
        public void TestSingletonConverter()
        {
            Assert.AreSame(ObjectXmlConverterRegistry.Instance.LookUpConverter(typeof(TestObject1)),
                ObjectXmlConverterRegistry.Instance.LookUpConverter(typeof(TestObject1)));
            Assert.AreSame(ObjectXmlConverterRegistry.Instance.LookUpConverter(typeof(TestObject2)),
                ObjectXmlConverterRegistry.Instance.LookUpConverter(typeof(TestObject2)));
            Assert.AreSame(ObjectXmlConverterRegistry.Instance.LookUpConverter(typeof(TestObject3)),
                ObjectXmlConverterRegistry.Instance.LookUpConverter(typeof(TestObject3)));
        }
    }
}
