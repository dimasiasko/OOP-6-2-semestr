using System;
using System.Collections.Generic;
using System.Text;

namespace Завдання_4
{
    class ArcherUnit : IWarrer
    {
        public void Ultimate()
        {
            Console.WriteLine("AOE Sword Attack");
        }

        public override string ToString()
        {
            return "Archer";
        }
    }
}
