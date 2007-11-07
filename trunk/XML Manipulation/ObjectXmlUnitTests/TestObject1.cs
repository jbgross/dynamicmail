using System;
using System.Collections.Generic;
using System.Text;

namespace Edu.Psu.Ist.DynamicMail.UnitTests
{
    public class TestObject1
    {
        private int a;

        public int A
        {
            get { return a; }
            set { a = value; }
        }
        private short b;

        public short B
        {
            get { return b; }
            set { b = value; }
        }
        private long c;

        public long C
        {
            get { return c; }
            set { c = value; }
        }
        private float d;

        public float D
        {
            get { return d; }
            set { d = value; }
        }
        private double e;

        public double E
        {
            get { return e; }
            set { e = value; }
        }
        private char f;

        public char F
        {
            get { return f; }
            set { f = value; }
        }
        private string g;

        public string G
        {
            get { return g; }
            set { g = value; }
        }

        public TestObject1(int a, short b, long c, float d, double e, char f, string g)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
            this.e = e;
            this.f = f;
            this.g = g;
        }
        override public bool Equals(Object obj)
        {
            if (obj == null || !Type.Equals(obj.GetType(), this.GetType())) return false;
            TestObject1 o = obj as TestObject1;
            return
                this.a == o.a &&
                this.b == o.b &&
                this.c == o.c &&
                Math.Abs(this.d - o.d) < 0.1 &&
                Math.Abs(this.e - o.e) < 0.1 &&
                this.f == o.f &&
                this.g.Equals(o.g);
        }
    }
}
