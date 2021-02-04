using System;
using System.Threading.Channels;

namespace Завдання_3
{
    
    
    class Program
    {
        public delegate int Number();

        public delegate int Average(Number[] arrayNumbers);

        public static int RandomNumber()
        {
            Random random = new Random();
            return random.Next(1, 100);
        }

        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Введите сколько рандомных чисел вам згенерировать(от 1 до 10): ");
                    int option = int.Parse(Console.ReadLine());
                    if (!(option > 10 && option < 1))
                    {
                        Console.WriteLine("Введите числа от 1 до 10!!!! \n");
                        continue;
                    }
                    Number[] numberArray = new Number[option];

                    for (int i = 0; i < option; i++)
                    {
                        numberArray[i] = RandomNumber;
                    }

                    Average avg = delegate (Number[] numbers)
                    {
                        int sum = 0;
                        for (int i = 0; i < option; i++)
                        {
                            sum += numbers[i]();

                        }
                        return sum / option;
                    };

                    Console.WriteLine("Среднее число: " + avg(numberArray));
                }
                catch (Exception)
                {
                    Console.WriteLine("ВВОДИТЕ ЧИСЛА ОТ 1 до 10 !");
                    continue;
                } 
            }
            
        }
    }
}
