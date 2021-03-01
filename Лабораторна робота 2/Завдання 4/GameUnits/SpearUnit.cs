using System;
using System.Collections.Generic;
using System.Text;

namespace Завдання_4
{
    class SpearUnit : IWarrer
    {
        public void Ultimate()
        {
            Console.WriteLine("Long-range spear throw");
        }

        public override string ToString()
        {
            return "Spear";
        }
    }
}
