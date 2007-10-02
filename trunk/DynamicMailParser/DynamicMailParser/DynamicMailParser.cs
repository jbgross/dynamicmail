using System;
using System.Collections.Generic;
using System.Text;

namespace Edu.Psu.Ist.DynamicMail
{
    class DynamicMailParser
    {
        //Singelton Instance of indexes
        private Indexes InvertedIndexes;

        //public default constructor
        public DynamicMailParser()
        {
            InvertedIndexes = new Indexes();
        }
    }
}
