using System;
using System.Collections.Generic;
using System.Text;

namespace Edu.Psu.Ist.Keystone.Data
{
    class Point2D : DataElement
    {
        decimal x, y;

        public Point2D()
        {
            Random r = new Random();
            X = (decimal)r.NextDouble();
            Y = (decimal)r.NextDouble();
        }
        public Point2D(Decimal x, Decimal y)
        {
            X = x;
            Y = y;
        }
        public decimal Y
        {
            get { return y; }
            private set { y = value; }
        }

        public decimal X
        {
            get { return x; }
            private set { x = value; }
        }

        public override string ToString()
        {
            return "X: " + X + " Y: " + Y;
        }

        public override int CompareTo(object o)
        {
            return X.CompareTo(o);
        }

        public override bool Equals(object obj)
        {
            Point2D p = (Point2D)obj;
            if (p.X.Equals(X) && p.Y.Equals(Y))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode();
        }
    }
}
