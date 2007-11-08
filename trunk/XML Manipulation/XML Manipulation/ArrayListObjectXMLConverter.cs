using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace Edu.Psu.Ist.DynamicMail
{
    class ArrayListObjectXMLConverter : IObjectXmlConverter
    {
        public Dictionary<String, String> GetPrimitives(Object o)
        {
            ArrayList Index = (ArrayList)o;
            Dictionary<string, string> IndexDic = new Dictionary<string, string>();

            foreach (int x in Index)
            {
                IndexDic.Add(x.ToString(), x.ToString());
            }

            return IndexDic;
        }

        public Dictionary<String, Object> GetReferences(Object o)
        {
            ArrayList Index = (ArrayList)o;

            Dictionary<string, Object> IndexDic = new Dictionary<string, Object>();

            foreach (int x in Index)
            {
                IndexDic.Add(x.ToString(), (Object)Index[x]);
            }

            return IndexDic;
        }

        public Object GenerateInstance(Dictionary<String, String> primitives, Dictionary<String, Object> references)
        {
            ArrayList Index = new ArrayList();

            foreach (KeyValuePair<string, Object> kvp in references)
            {
                Index[Convert.ToInt32(kvp.Key)] = kvp.Value;
                
            }

            return Index;
        }
    }
}
