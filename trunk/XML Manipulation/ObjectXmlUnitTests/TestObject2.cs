using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectXmlUnitTests
{
    public class TestObject2
    {
        private TestObject1 a, b, c, d;

        public TestObject1 A
        {
            get { return a; }
            set { a = value; }
        }

        public TestObject1 B
        {
            get { return b; }
            set { b = value; }
        }

        public TestObject1 C
        {
            get { return c; }
            set { c = value; }
        }

        public TestObject1 D
        {
            get { return d; }
            set { d = value; }
        }
        public TestObject2(TestObject1 a, TestObject1 b, TestObject1 c, TestObject1 d)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
        }
        override public bool Equals(Object obj)
        {
            if (obj == null || !Type.Equals(obj.GetType(), this.GetType())) return false;
            TestObject2 o = obj as TestObject2;
            return
                this.a.Equals(o.a) &&
                this.b.Equals(o.b) &&
                this.c.Equals(o.c) &&
                this.d.Equals(o.d);
        }
    }
}
