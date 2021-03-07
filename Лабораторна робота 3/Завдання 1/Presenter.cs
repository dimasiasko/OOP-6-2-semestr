using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Завдання_1
{
    class Presenter
    {
        private readonly MainWindow view;
        private Model model;
        
        DispatcherTimer timer1 = new DispatcherTimer();


        public Presenter(MainWindow view)
        {
            model = new Model();
            this.view = view;

            this.view.Start += View_Start;
            this.view.Stop += View_Stop;
            this.view.Pause += View_Pause;

            timer1.Tick += TimerTick;
            timer1.Interval = new TimeSpan(0, 0, 1);
            timer1.Start();
            timer1.IsEnabled = false;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            view.txtTime.Text = model.Time();
        }

        private void View_Pause(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.IsEnabled = false;
        }

        private void View_Stop(object sender, EventArgs e)
        {
            model.s = 0;
            timer1.Stop();
            view.txtTime.Text = "0";
        }

        private void View_Start(object sender, EventArgs e)
        {
            timer1.Start();
            timer1.IsEnabled = true;
        }
    }
}
