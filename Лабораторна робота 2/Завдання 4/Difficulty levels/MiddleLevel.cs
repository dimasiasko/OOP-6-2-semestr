using System;
using System.Collections.Generic;
using System.Text;

namespace Завдання_4.Difficulty_levels
{
    class MiddleLevel : IWarrersFactory
    {
        private static int HardWarrerCount = 0;

        public IWarrer Create()
        {
            if (HardWarrerCount < 10) 
            {
                HardWarrerCount += 1;
                return new MagicUnit();
            }

            
            IWarrersFactory ezFactory = new EasyLevel();
            return ezFactory.Create();
        }

        public override string ToString()
        {
            return "Middlegame level mode";
        }
    }
}
