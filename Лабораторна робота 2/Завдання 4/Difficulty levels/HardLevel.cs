using System;
using System.Collections.Generic;
using System.Text;

namespace Завдання_4.Difficulty_levels
{
    class HardLevel : IWarrersFactory
    {
        private static Random rnd = new Random();
        public IWarrer Create()
        {
            string[] bidlos = new string[]
            {
                "Spearer",
                "Magic"
            };

            switch (bidlos[rnd.Next(0, bidlos.Length)])
            {
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
            return "Hardgame level mode";
        }
    }
}
