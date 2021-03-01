using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Завдання_4
{
    class BattleLocation
    {
        private IWarrer[] warrers;
        private TreesGameObject[] trees;
        private HousesGameObject[] houses;

        public BattleLocation(IWarrer[] warrers, TreesGameObject[] trees, HousesGameObject[] houses)
        {
            this.warrers = warrers;
            this.trees = trees;
            this.houses = houses;
        }

        public void WarrersInfo()
        {
            var archer = warrers.Where((item) => 
            { return item.ToString() == "Archer";}).Count();

            Console.WriteLine($"{archer} Archer");


            var spearer = warrers.Where((item) => 
            { return item.ToString() == "Spearer"; }).Count();

            Console.WriteLine($"{spearer} Spearer");


            var magic = warrers.Where((item) => 
            { return item.ToString() == "Magic"; }).Count();

            Console.WriteLine($"{magic} Magic");
        }
    }
}
