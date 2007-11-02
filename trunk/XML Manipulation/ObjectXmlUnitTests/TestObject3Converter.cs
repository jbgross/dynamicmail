using System;
using System.Collections.Generic;
using System.Text;
using ObjectXml;

namespace ObjectXmlUnitTests
{
    public class TestObject3Converter : IObjectXmlConverter
    {
        public Dictionary<String, String> GetPrimitives(Object o)
        {
            TestObject3 obj = o as TestObject3;
            Dictionary<String, String> p = new Dictionary<string, string>();
            p.Add("A", obj.A.ToString());
            p.Add("B", obj.B.ToString());
            p.Add("C", obj.C.ToString());
            return p;
        }
        public Dictionary<String, Object> GetReferences(Object o)
        {
            TestObject3 obj = o as TestObject3;
            Dictionary<String, Object> r = new Dictionary<string, Object>();
            r.Add("D", obj.D);
            r.Add("E", obj.E);
            r.Add("F", obj.F);
            r.Add("F", obj.G);
            return r;
        }
        public Object GenerateInstance(Dictionary<String, String> primitives, Dictionary<String, Object> references)
        {
            try
            {
                return new TestObject3(
                    int.Parse(primitives["A"]),
                    float.Parse(primitives["B"]),
                    primitives["C"] as String,
                    references["D"] as TestObject2,
                    references["E"] as TestObject2,
                    references["F"] as TestObject1,
                    references["G"] as TestObject1);
            }
            catch (Exception e)
            {
                throw new ReconstructionException("Missing value", e);
            }
        }
    }
}
