using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Завдання_2
{
    class Presenter
    {
        private readonly MainWindow window;
        private Model model;

        private double first;
        private double second;

        private bool tryFirst;
        private bool trySecond;

        public Presenter(MainWindow window1)
        {
            model = new Model();
            window = window1;
            window.Plus += Window_Plus;
            window.Minus += Window_Minus;
            window.Multi += Window_Multi;
            window.Divide += Window_Divide;
        }

        private bool CheckInput()
        {
            tryFirst = double.TryParse(window.tbFirst.Text, out first);
            trySecond = double.TryParse(window.tbSecond.Text, out second);

            if (tryFirst && trySecond)
                return true;
            else
            {
                MessageBox.Show("ВВОДИТЕ ЧИСЛА!");
                Clear();
                return false;
            }
        }

        private void Window_Divide(object sender, EventArgs e)
        {
            if (CheckInput())
                window.txtResult.Text = model.ResultDiv(first, second).ToString();
        }

        private void Window_Multi(object sender, EventArgs e)
        {
            if (CheckInput())
                window.txtResult.Text = model.ResultMult(first, second).ToString();
        }

        private void Window_Minus(object sender, EventArgs e)
        {
            if (CheckInput())
                window.txtResult.Text = model.ResultMinus(first, second).ToString();
        }

        private void Window_Plus(object sender, EventArgs e)
        {
            if (CheckInput())
                window.txtResult.Text = model.ResultPlus(first, second).ToString();
        }

        private void Clear()
        {
            window.txtResult.Text = 0.ToString();
            window.tbFirst.Text = String.Empty;
            window.tbSecond.Text = String.Empty;
        }
        
    }
}
