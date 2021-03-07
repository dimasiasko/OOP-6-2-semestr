using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Завдання_2
{
    class Model
    {
        public double ResultDiv(double a, double b)
        {
            if (b == 0)
            {
                MessageBox.Show("На ноль делить нельзя");
                return 0;
            }
            else
                return a / b;
        }

        public double ResultMult(double a, double b)
        {
            return a * b;
        }

        public double ResultPlus(double a, double b)
        {
            return a + b;
        }

        public double ResultMinus(double a, double b)
        {
            return a - b;
        }
    }
}
