using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Завдання_1
{
    public class MyList<T> : IEnumerable<T>
    {
        private T[] _elements;

        public int Count
        {
            get { return _elements.Length; }
        }

        // констурктор без параметров - размер списка = 0
        public MyList()
        {
            _elements = new T[0];
        }

        // конструктор установки размера списка
        public MyList(int capacity)
        {
            if (capacity < 0)
                throw new Exception("Нельзя задать размер МЕНЬШЕ НУЛЯ");

            if (capacity == 0)
                _elements = new T[0];
            else
                _elements = new T[capacity];
        }

        // индексатор для получения и установки по индексу
        public T this[int index]
        {
            get
            {
                // Following trick can reduce the range check by one
                if ((uint)index >= (uint)Count)
                {
                    throw new Exception("Ошибка! Значение аргумента находится вне диапазона допустимых значений");
                }
                return _elements[index];
            }

            set
            {
                //if ((uint)index >= (uint)Count)
                //{
                //    throw new Exception("Ошибка! Значение аргумента находится вне диапазона допустимых значений");
                //}

                //_elements[index] = value;
                if ((uint)index >= (uint)Count)
                {
                    Capacity = index + 1;
                    _elements[index] = value;
                }
                else
                {
                    _elements[index] = value;
                }
            }
        }

        // метод добавления в конец списка
        public void Add(T item)
        {
            T[] newItems = new T[Count + 1];
            _elements.CopyTo(newItems, 0);
            newItems[Count] = item;
            _elements = newItems;
        }

        // метод вставки по указанному индексу
        public void Insert(T item, int index)
        {
            T[] newItems = new T[Count + 1];

            if ((uint)index <= (uint)Count)
            {
                for (int i = 0; i < Count; i++)
                {
                    if (i == index)
                        newItems[i] = item;
                    else
                        newItems[i] = _elements[i];
                }
                _elements = newItems;
            }
        }



        public int Capacity
        {
            get
            {
                return _elements.Length;
            }
            set
            {
                if (value < Count)
                {
                    throw new Exception("Ошибка! Значение аргумента находится вне диапазона допустимых значений");
                }

                if (value != Count)
                {
                    if (value > 0)
                    {
                        T[] newItems = new T[value];
                        if (Count > 0)
                        {
                            Array.Copy(_elements, 0, newItems, 0, Count);
                        }
                        _elements = newItems;
                    }
                    else
                    {
                        _elements = new T[0];
                    }
                }
            }
        }
        public int IndexOf(T item)
            => Array.IndexOf(_elements, item, 0, Count);


        public IEnumerator<T> GetEnumerator()
        {
            foreach (T item in _elements)
                yield return item;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _elements.GetEnumerator();
        }
    }
}
