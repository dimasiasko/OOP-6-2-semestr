using System;
using System.Collections.Generic;
using System.Threading.Channels;

namespace Завдання_1
{
   
    class Program
    {
        static void Main(string[] args)
        {
            MyList<int> list = new MyList<int>(3); // обьект list с размером 3

            MyList<string> list2 = new MyList<string>(); // обьект list2 с размером 0

            // даже при размере списка 3 - мы можем присвоить элементу под номером 5 значение и расширить наш список
            list[0] = 222;
            list.Insert(312321, 1);
            list[6] = 1221;

            Console.WriteLine("Наш список:");
            for (int i = 0; i < list.Count; i++)
                Console.WriteLine(list[i]);

            Console.WriteLine("Поиск индекса 1221: " + list.IndexOf(1221));

            Console.WriteLine("Добавить элемент 55 в конец списка: ");

            list.Add(55);
            Console.WriteLine(list[7]);

            Console.WriteLine("Выведем все элементы классом FOREACH, который реализован интерфейсом IEnumerable<T>");

            foreach (var obj in list)
                Console.Write(obj + ", ");

            Console.WriteLine("А теперь выведем все элементы через расширяющий метод GetArray()");

            for (int i = 0; i < list.Count; i++)
                Console.Write("Extension: " + list.GetArray()[i] + ", ");

        }
    }
}
