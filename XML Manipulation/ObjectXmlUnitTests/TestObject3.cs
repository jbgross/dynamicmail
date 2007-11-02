using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectXmlUnitTests
{
    public class TestObject3
    {
        private int a;

        public int A
        {
            get { return a; }
            set { a = value; }
        }
        private float b;

        public float B
        {
            get { return b; }
            set { b = value; }
        }
        private String c;

        public String C
        {
            get { return c; }
            set { c = value; }
        }
        private TestObject2 d, e;

        public TestObject2 D
        {
            get { return d; }
            set { d = value; }
        }

        public TestObject2 E
        {
            get { return e; }
            set { e = value; }
        }
        private TestObject1 f, g;

        public TestObject1 F
        {
            get { return f; }
            set { f = value; }
        }

        public TestObject1 G
        {
            get { return g; }
            set { g = value; }
        }

        public TestObject3(int a, float b, String c, TestObject2 d, TestObject2 e, TestObject1 f, TestObject1 g)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
            this.e = e;
            this.f = f;
            this.g = g;
        }
        public bool Equals(Object obj)
        {
            TestObject3 o = obj as TestObject3;
            return
                this.a == o.a &&
                this.b == o.b &&
                this.c.Equals(o.c) &&
                this.d.Equals(o.d) &&
                this.e.Equals(o.e) &&
                this.f.Equals(o.f) &&
                this.g.Equals(o.g);
        }
    }
}
