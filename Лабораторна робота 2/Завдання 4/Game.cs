using System;
using System.Collections.Generic;
using System.Text;

namespace Завдання_4
{
    class Game
    {
        private BattleLocation battleLocation;
        private MainPlayer mainPlayer;

        public Game(IWarrersFactory factory)
        {
            mainPlayer = new MainPlayer();

            TreesGameObject[] trees = new TreesGameObject[]
            {
                new TreesGameObject(),
                new TreesGameObject(),
                new TreesGameObject()
            };

            HousesGameObject[] benches = new HousesGameObject[]
            {
                new HousesGameObject(),
                new HousesGameObject(),
                new HousesGameObject()
            };

            IWarrer[] warrers = new IWarrer[100];

            for (int i = 0; i < warrers.Length; i++)
            {
                warrers[i] = factory.Create();
            }

            battleLocation = new BattleLocation(warrers, trees, benches);
        }

        public void StartGame()
        {
                Console.WriteLine("Location is loaded");
                Console.WriteLine("Trees are loaded");
                Console.WriteLine("Houses are loaded");
                Console.WriteLine("Warrers are loaded");
                Console.WriteLine("Player is loaded");
                Console.WriteLine("Game starting in 3..2..1.. GO");

                battleLocation.WarrersInfo();

        }
    }
}
