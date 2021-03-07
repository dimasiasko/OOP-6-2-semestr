using System;
using System.Threading;

namespace Завдання_4
{

    class Program
    {
        static void MultiPlication(object numbers)
        {
            int i = (int)numbers;
            if (i == 0)
                return;

            Thread thread = new Thread(MultiPlication);
            thread.Start(i - 1);

            Console.WriteLine($"Второй поток {i * i}");
        }

        private static void Main(string[] args)
        {
            MultiPlication(10);

            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine($"\t\tГлавный поток: {i * i}");
            }

            Console.ReadKey();
        }
    }
}
