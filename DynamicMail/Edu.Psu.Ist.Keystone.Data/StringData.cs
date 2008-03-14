using System;
using System.Collections.Generic;
using System.Text;

namespace Edu.Psu.Ist.Keystone.Data
{
    /// <summary>
    /// Store a DataElement of String type
    /// </summary>
    public class StringData : DataElement
    {
        /// <summary>
        /// Constructor, takes a String
        /// </summary>
        /// <param name="data"></param>
        public StringData(String data)
        {
            Data = data;
        }

        private String data;

        public String Data
        {
            get { return data; }
            private set { data = value; }
        }

        /// <summary>
        /// Return the data element, in this case a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Data;
        }

        public override int CompareTo(object o)
        {
            // this cast is safe, as this will only ever be called
            // by sort
            return Data.CompareTo(((StringData)o).Data);
        }

        /// <summary>
        /// Can't believe it took me this long
        /// Two StringData elements are equal if they contain
        /// the same string
        /// JBG
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            StringData sd = (StringData)obj;
            return Data.Equals(sd.Data);
        }

        /// <summary>
        /// Same with hashcode - needs to be identical
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Data.GetHashCode();
        }

    }
}
