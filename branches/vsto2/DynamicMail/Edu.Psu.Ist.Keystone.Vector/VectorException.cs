using System;
using System.Collections.Generic;
using System.Text;

namespace Edu.Psu.Ist.Keystone.Dimensions
{
    class VectorException : Exception
    {
        public VectorException(String msg)
            : base(msg)
        {
        }
    }
}
