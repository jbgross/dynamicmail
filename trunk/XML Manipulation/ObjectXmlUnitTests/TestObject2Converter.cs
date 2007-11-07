using System;
using System.Collections.Generic;
using System.Text;
using Edu.Psu.Ist.DynamicMail;

namespace Edu.Psu.Ist.DynamicMail.UnitTests
{
    public class TestObject2Converter : IObjectXmlConverter
    {
        public Dictionary<String, String> GetPrimitives(Object o)
        {
            return new Dictionary<string, string>();
        }
        public Dictionary<String, Object> GetReferences(Object o)
        {
            TestObject2 obj = o as TestObject2;
            Dictionary<String, Object> r = new Dictionary<String, Object>();
            r.Add("A", obj.A);
            r.Add("B", obj.B);
            r.Add("C", obj.C);
            r.Add("D", obj.D);
            return r;
        }
        public Object GenerateInstance(Dictionary<String, String> primitives, Dictionary<String, Object> references)
        {
            if (primitives.Count > 0) throw new ReconstructionException("Unexpected primitives");
            try
            {
                return new TestObject2(
                    references["A"] as TestObject1,
                    references["B"] as TestObject1,
                    references["C"] as TestObject1,
                    references["D"] as TestObject1);
            }
            catch (Exception e)
            {
                throw new ReconstructionException("Missing object reference", e);
            }
        }
    }
}
