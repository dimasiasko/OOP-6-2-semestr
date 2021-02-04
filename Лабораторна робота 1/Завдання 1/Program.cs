using System;
using System.Threading.Channels;

namespace Завдання_1
{
    class Program
    {
        delegate double AverageNumber(int a, int b, int c);

        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Введите 1 число: ");
                    int numberOne = int.Parse(Console.ReadLine());

                    Console.WriteLine("Введите 2 число: ");
                    int numberTwo = int.Parse(Console.ReadLine());

                    Console.WriteLine("Введите 3 число: ");
                    int numberThree = int.Parse(Console.ReadLine());

                    AverageNumber averageNumber = delegate (int d, int d1, int d2)
                    {
                        return (d + d1 + d2) / 3;
                    };

                    Console.WriteLine("Среднее ваших 3 чисел: " + averageNumber(numberOne, numberTwo, numberThree));
                    Console.WriteLine();
                    
                }
                catch (Exception)
                {
                    Console.WriteLine("Вы ввели НЕ число");
                    continue;
                }
                
            }
            
        }
    }
}
