using System;

namespace Завдання_4
{
    class Program
    {
        static void Main(string[] args)
        {
            var customClass = MyClass<CustomClass>.FactoryMethod();

            Console.ReadLine();
        }
    }

    public class MyClass<T>
    {
        public static T FactoryMethod()
        {
            return Activator.CreateInstance<T>();
        }
    }
    public class CustomClass
    {

    }
}
