using System;
using System.Collections;
using System.Collections.Generic;

namespace Завдання_2
{
    class Program
    {
        static void Main(string[] args)
        {
            MyDictionary<int, string> dict = new MyDictionary<int, string>();
            Console.WriteLine("Количество пар в dict: " + dict.Lenght);

            Console.WriteLine("Добавляем ключ-значение");

            dict.Add(1, "Hello");
            dict.Add(3, ", World");
            dict.Add(14, "!");
            dict.Add(155, " I love");
            dict.Add(1421, " C#");
            dict.Add(132, " !");

            Console.WriteLine("Теперь количество наших пар = " + dict.Lenght);

            Console.WriteLine("\nВыводим все наши ключ-значения");

            foreach (var keyValue in dict)
            {
                Console.WriteLine(keyValue);
            }
            

            Console.ReadLine();
        }
    }
}
