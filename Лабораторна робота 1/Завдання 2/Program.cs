using System;

namespace Завдання_2
{
    class Program
    {
        public delegate int CalculateDelegate(int x, int y);

        static void Main(string[] args)
        {
            while (true)
            {

                try
                {
                    CalculateDelegate calc = null;

                    Console.WriteLine("Введите 1 число: ");
                    int numberOne = int.Parse(Console.ReadLine());

                    Console.WriteLine("Введите 2 число: ");
                    int numberTwo = int.Parse(Console.ReadLine());

                    Console.WriteLine("Выберите что хотите сделать: ");
                    Console.WriteLine("1. Плюс '+'");
                    Console.WriteLine("2. Минус '-'");
                    Console.WriteLine("3. Умножить '*'");
                    Console.WriteLine("4. Разделить '/'");

                    int option = int.Parse(Console.ReadLine());

                    if (option == 1)
                        calc = (int numberOne, int numberTwo) => { return numberOne + numberTwo; };
                    else if (option == 2)
                        calc = (int numberOne, int numberTwo) => { return numberOne - numberTwo; };
                    else if (option == 3)
                        calc = (int numberOne, int numberTwo) => { return numberOne * numberTwo; };
                    else if(option == 4)
                    {
                        if (numberTwo == 0)
                        {
                            Console.WriteLine("На ноль делить нельзя\n");
                            continue;
                        }
                        else
                            calc = (int numberOne, int numberTwo) => { return numberOne / numberTwo; };
                    }
                    else
                    {
                        Console.WriteLine("Выберите вариант из списка!\n");
                        continue;
                    }

                    Console.WriteLine($"Результат: {calc(numberOne, numberTwo)}\n");


                }
                catch (Exception)
                {
                    Console.WriteLine("Вы ввели НЕ ЦЕЛОЕ число\n");
                    continue;
                }

            }
        }
    }
}
