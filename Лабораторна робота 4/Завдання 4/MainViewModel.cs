using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;

namespace Завдання_4
{
    class MainViewModel
    {
        private double step = 0.01;
        static Mutex mutexObj = new Mutex();
        private List<double> coordinates = new List<double>();
        public MainViewModel()
        {
            Thread thread = new Thread(Adding);
            thread.Start();

            Thread threadArr = new Thread(ArrayAdd);
            threadArr.Start();
        }

        public void Adding()
        {
            mutexObj.WaitOne();
            this.MyModel = new PlotModel { Title = "First" };
            this.MyModel.Series.Add(new FunctionSeries(Graph1, 0, 20, step, "y = 23*x2–33"));
            mutexObj.ReleaseMutex();
        }
        public double Graph1(double x)
        {
            return 23 * Math.Pow(x, 2) - 33;
        }

        public void ArrayAdd()
        {
            mutexObj.WaitOne();

            for (double i = 0; i < 20; i+=0.01)
                coordinates.Add(23 * Math.Pow(i, 2) - 33);

            mutexObj.ReleaseMutex();
        }
        public PlotModel MyModel { get; private set; }
        
    }
}
