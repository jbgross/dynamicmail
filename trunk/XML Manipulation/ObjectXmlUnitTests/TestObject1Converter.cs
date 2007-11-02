using System;
using System.Collections.Generic;
using System.Text;
using ObjectXml;

namespace ObjectXmlUnitTests
{
    public class TestObject1Converter : IObjectXmlConverter
    {
        public Dictionary<String, String> GetPrimitives(Object o)
        {
            TestObject1 obj = o as TestObject1;
            Dictionary<String, String> p = new Dictionary<string, string>();
            p.Add("A", obj.A.ToString());
            p.Add("B", obj.B.ToString());
            p.Add("C", obj.C.ToString());
            p.Add("D", obj.D.ToString());
            p.Add("E", obj.E.ToString());
            p.Add("F", obj.F.ToString());
            p.Add("G", obj.G.ToString());
            return p;
        }
        public Dictionary<String, Object> GetReferences(Object o)
        {
            return new Dictionary<string, object>();
        }
        public Object GenerateInstance(Dictionary<String, String> primitives, Dictionary<String, Object> references)
        {
            if (references.Count > 0) throw new ReconstructionException("Unexpected references");
            try
            {
                return new TestObject1(
                    int.Parse(primitives["A"]),
                    short.Parse(primitives["B"]),
                    long.Parse(primitives["C"]),
                    float.Parse(primitives["D"]),
                    double.Parse(primitives["E"]),
                    char.Parse(primitives["F"]),
                    primitives["G"] as String);
            }
            catch (Exception e)
            {
                throw new ReconstructionException("Missing primitive value", e);
            }
        }
    }
}
