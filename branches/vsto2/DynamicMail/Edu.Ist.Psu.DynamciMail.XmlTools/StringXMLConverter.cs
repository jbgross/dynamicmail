using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace Edu.Psu.Ist.DynamicMail
{
    class StringXMLConverter : IObjectXmlConverter
    {
        public Dictionary<String, String> GetPrimitives(Object o)
        {
            String s = (String) o;
            Dictionary<string, string> IndexDic = new Dictionary<string, string>();
            IndexDic.Add(s, s);
            return IndexDic;
        }

        public Dictionary<String, Object> GetReferences(Object o)
        {
            String s = (String)o;

            Dictionary<string, Object> IndexDic = new Dictionary<string, Object>();
            IndexDic.Add(s, (Object) s);

            return IndexDic;
        }

        public Object GenerateInstance(Dictionary<String, String> primitives, Dictionary<String, Object> references)
        {
            Hashtable Index = new Hashtable();

            foreach (KeyValuePair<string, Object> kvp in references)
            {
                Index.Add(kvp.Key, kvp.Value);
            }

            return Index;
        }
    }
}
