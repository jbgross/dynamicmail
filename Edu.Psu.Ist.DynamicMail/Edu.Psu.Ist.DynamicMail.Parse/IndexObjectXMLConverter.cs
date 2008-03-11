using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace Edu.Psu.Ist.DynamicMail.Parse
{
    class IndexObjectXMLConverter : IObjectXmlConverter
    {
        public Dictionary<String, String> GetPrimitives(Object o)
        {
            Hashtable Index = (Hashtable)o;
            Dictionary<string, string> IndexDic = new Dictionary<string, string>();

            foreach (String x in Index.Keys)
            {
                IndexDic.Add(x, x);
            }

            return IndexDic;
        }

        public Dictionary<String, Object> GetReferences(Object o)
        {
            Hashtable Index = (Hashtable)o;

            Dictionary<string, Object> IndexDic = new Dictionary<string, Object>();

            foreach (String x in Index.Keys)
            {
                IndexDic.Add(x, Index[x]);
            }

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
