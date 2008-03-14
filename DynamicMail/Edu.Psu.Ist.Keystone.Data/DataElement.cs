using System;
using System.Collections.Generic;
using System.Text;

namespace Edu.Psu.Ist.Keystone.Data
{
    public abstract class DataElement : IComparable
    {
        public abstract override String ToString();
        public abstract override bool Equals(object obj);
        public abstract int CompareTo(Object o);
    }
}
