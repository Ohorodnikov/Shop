using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop
{
    public class Test
    {
        public readonly int n = 1;

        
        public Test(int n)
        {
            this.n = n;
        }

        public Test(int n, int multiplier) : this(n)
        {
            this.n = n * multiplier;            
        }
    }

    class X
    {
        public X()
        {
            var test = new Test(2, 2);
            var n = test.n;
        }
    }
}
