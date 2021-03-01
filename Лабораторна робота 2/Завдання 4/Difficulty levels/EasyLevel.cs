using System;
using System.Collections.Generic;
using System.Text;

namespace Завдання_4.Difficulty_levels
{
    class EasyLevel : IWarrersFactory
    {
        private static Random rnd = new Random();

        public IWarrer Create()
        {
            string[] bidlos = new string[] 
            {
                "Archer",
                "Spearer",
                "Magic"
            };

            switch (bidlos[rnd.Next(0, bidlos.Length)]) 
            {
                case "Archer":
                    return new ArcherUnit();
                case "Spearer":
                    return new SpearUnit();
                case "Magic":
                    return new MagicUnit();
                default:
                    throw new Exception("These classes are not defined early :(");
            }
        }

        public override string ToString() 
        {
            return "Easygame level mode";
        }
    }
}
