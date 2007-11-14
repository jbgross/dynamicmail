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

            for (int x = 0; x < Index.Count; x++)
            {
                IndexDic.Add(x.ToString(), Index[x].ToString());
            }

            return IndexDic;
        }

        public Dictionary<String, Object> GetReferences(Object o)
        {
            Dictionary<string, Object> IndexDic = new Dictionary<string, Object>();
            /* Console.WriteLine(o.GetType().AssemblyQualifiedName);
             Console.Out.Flush();
             ArrayList Index = (ArrayList)o;


             for(int x = 0; x < Index.Count; x++)
             {
                 IndexDic.Add(x.ToString(), (Object)Index[x]);
             }
*/
             return IndexDic;
        }

        public Object GenerateInstance(Dictionary<String, String> primitives, Dictionary<String, Object> references)
        {
            ArrayList Index = new ArrayList();

            foreach (KeyValuePair<String, String> kvp in primitives)
            {
                Index[Convert.ToInt32(kvp.Key)] = kvp.Value;
                
            }

            return Index;
        }
    }
}
