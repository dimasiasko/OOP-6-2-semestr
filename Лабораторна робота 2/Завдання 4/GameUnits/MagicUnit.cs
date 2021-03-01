using System;
using System.Collections.Generic;
using System.Text;

namespace Завдання_4
{
    class MagicUnit : IWarrer
    {
        public void Ultimate()
        {
            Console.WriteLine("Magic AOE Shield");
        }

        public override string ToString()
        {
            return "Magic";
        }
    }
}
