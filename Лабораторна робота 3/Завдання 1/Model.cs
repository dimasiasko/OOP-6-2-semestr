using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Завдання_1
{
    class Model
    {
        internal int s;
        public string Time()
        {
            s += 1;
            return (s >= 60) ? "min. " + s / 60 + " s. " + s % 60 : s.ToString();
        }
        public void Clear()
        {
            s = 0;
        }
    }
}
