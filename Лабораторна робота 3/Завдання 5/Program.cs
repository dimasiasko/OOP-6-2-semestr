using System;
using System.Threading;

namespace Завдання_5
{
    class Matrix
    {
        private static object _locker = new object();
        private Random random;
        const string Litters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public int Column { get; set; }
        public bool GoNext { get; set; }

        public Matrix(int column, bool goNext)
        {
            Column = column;
            random = new Random((int) DateTime.Now.Ticks);
            GoNext = goNext;
        }

        private char Symbol => Litters.ToCharArray()[random.Next(0, 35)];

        public void SlidingDown()
        {
            int lenght;
            int counter;

            while (true)
            {
                counter = random.Next(3, 12);
                lenght = 0;
                Thread.Sleep(random.Next(20, 5000));
                for (int i = 0; i < 40; i++)
                {
                    lock (_locker)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.CursorTop = i - lenght;

                        for (int j = i - lenght; j < i; j++)
                        {
                            Console.CursorLeft = Column;
                            Console.WriteLine("---");
                        }

                        if (lenght<counter)
                            lenght++;
                        else if (lenght == counter)
                            counter = 0;

                        if (GoNext && i < 20 && i > lenght + 2 && (random.Next(1, 5) == 3))
                        {
                            new Thread((new Matrix(Column,false)).SlidingDown).Start();
                            GoNext = false;
                        }

                        if (39 - i < lenght)
                            lenght--;

                        Console.CursorTop = i - lenght + 1;
                        Console.ForegroundColor = ConsoleColor.DarkGreen;

                        for (int j = 0; j < lenght - 2; j++)
                        {
                            Console.CursorLeft = Column;
                            Console.WriteLine(Symbol);
                        }

                        if (lenght >= 2)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.CursorLeft = Column;
                            Console.WriteLine(Symbol);
                        }

                        if (lenght >= 1)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.CursorLeft = Column;
                            Console.WriteLine(Symbol);
                        }

                        Thread.Sleep(10);

                    }
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(70, 50);

            Matrix matrixRain;

            for (int i = 0; i < 26; i++)
            {
                matrixRain = new Matrix(i * 3, false);
                new Thread(matrixRain.SlidingDown).Start();
            }
        }
    }
}
