using System;
using System.Collections.Generic;
using System.Text;

namespace Edu.Psu.Ist.Keystone.Data
{
    class Point2DDataElement : DataElement
    {
        decimal value;
        public decimal Value
        {
            get { return this.value; }
            private set { this.value = value; }
        }

        Point2D point;

        public Point2D Point
        {
            get { return point; }
            private set { point = value; }
        }

        public Point2DDataElement(decimal value, Point2D point)
        {
            Value = value;
            Point = point;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override int CompareTo(object o)
        {
            return value.CompareTo(o);
        }

        public override bool Equals(object obj)
        {
            return ((Point2DDataElement)obj).Value.Equals(Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
