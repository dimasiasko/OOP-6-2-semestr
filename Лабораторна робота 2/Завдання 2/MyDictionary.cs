using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Завдання_2
{
    class MyDictionary<TKey, TValue> : IEnumerable
    {
        private TKey[] _keys;
        private TValue[] _values;
        private int _counter = 0;

        public int Lenght
        {
            get { return _keys.Length; }
        }

        public MyDictionary()
        {
            _keys = new TKey[0];
            _values = new TValue[0];
        }

        public void Add(TKey key, TValue value)
        {
            this._counter++;

            Array.Resize(ref _keys, _counter);
            _keys[_counter - 1] = key;

            Array.Resize(ref _values, _counter);
            _values[_counter - 1] = value;
        }

        public TValue this[TKey key]
        {
            get
            {
                int index = 0;
                for (int i = 0; i < Lenght; i++)
                {
                    if (key.Equals(_keys[i]))
                        index = i;
                }
                return _values[index];
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i < Lenght; i++)
            {
                yield return new KeyValuePair<TKey, TValue>(_keys[i], _values[i]);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
