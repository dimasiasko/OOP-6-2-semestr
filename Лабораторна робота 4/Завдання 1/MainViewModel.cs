using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;

namespace Завдання_1
{
    class MainViewModel
    {
        static Mutex mutexObj = new Mutex();
        public MainViewModel()
        {
            
            this.MyModel = new PlotModel { Title = "First" };
            this.MyModel.Series.Add(new FunctionSeries(Math.Sin, 0, 10, 0.1, "sin(x)"));

            this.MyModel1 = new PlotModel { Title = "Second" };
            this.MyModel1.Series.Add(new FunctionSeries(Graph1, 0, 10, 0.1, "4*x2-2*x–22"));

            this.MyModel2 = new PlotModel { Title = "Third" };
            this.MyModel2.Series.Add(new FunctionSeries(Graph2, 0, 10, 0.1, "ln(x2)/x3"));
        }
        

        public double Graph1(double x)
        {
            mutexObj.WaitOne();
            return 4 * Math.Pow(x, 2) - 2 * x -22;
        }
        public double Graph2(double x)
        {
            mutexObj.WaitOne();
            return Math.Log(Math.Pow(x, 2) / Math.Pow(x, 3));
        }


        public PlotModel MyModel { get; private set; }
        public PlotModel MyModel1 { get; private set; }
        public PlotModel MyModel2 { get; private set; }
    }
}
