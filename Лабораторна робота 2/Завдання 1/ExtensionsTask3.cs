using System;
using System.Collections.Generic;
using System.Text;

namespace Завдання_1
{
    public static class ExtensionsTask3
    {
        public static T[] GetArray<T>(this MyList<T> list)
        {
            T[] array = new T[0];
            foreach (T obj in list)
            {
                T[] newArray = new T[array.Length + 1];
                array.CopyTo(newArray, 0);
                newArray[array.Length] = obj;
                array = newArray;
            }

            return array;
        }
    }
}
