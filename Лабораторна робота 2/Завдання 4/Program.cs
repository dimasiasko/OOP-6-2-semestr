using System;
using Завдання_4.Difficulty_levels;

namespace Завдання_4
{
    class Program
    {
        static void Main(string[] args)
        {
            IWarrersFactory[] difficulties = new IWarrersFactory[] 
            {
                new EasyLevel(),
                new MiddleLevel(),
                new HardLevel()
            };

            Console.WriteLine("Choose the number of difficult:");

            for (int i = 0; i < difficulties.Length; i++) 
            {
                Console.WriteLine($"{i + 1}. {difficulties[i].ToString()}");
            }

            int difficultNumber = Convert.ToInt32(Console.ReadLine()) - 1; 

            if (difficultNumber > -1 && difficultNumber < difficulties.Length) 
            {
                Game game = new Game(difficulties[difficultNumber]); // создаём игру с выбранным уровнем сложности
                game.StartGame();
            }
            else
            {
                Console.WriteLine("Incorrect number");
            }

            Console.ReadKey();
        }
    }
    
}
