using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Math = System.Math;

namespace Завдання_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public double wi = 1;
        private double y = 1;
        private double x = 1;
        static Mutex mutexObj = new Mutex();
        private DispatcherTimer tm = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            tm.Interval = TimeSpan.FromSeconds(0.5);
            
        }

        
        private void CmdStart_OnClick(object sender, RoutedEventArgs e)
        {
            cmdStart.IsEnabled = false;
            tm.Tick += (o, args) => txtWi.Text = wi.ToString();
            tm.Tick += (o, args) => txtY.Text = y.ToString();
            tm.Tick += (o, args) => txtX.Text = $"{x} | {2 * x}";
            tm.Start();

            Thread thread = new Thread(RefreshData);
            thread.Start();
            
        }

       

        private void RefreshData()
        {
            mutexObj.WaitOne();
            for (int i = 0; i < 200; i++) 
            {
                wi = x * Math.Sin(y) - (2*x * Math.Cos(y));
                x*=4;
                y++;
                Thread.Sleep(1000);
            }
            
            mutexObj.ReleaseMutex();
        }
        private void CmdRefresh_OnClick(object sender, RoutedEventArgs e)
        {
            tm.Stop();
            tm.Tick -= (o, args) => txtWi.Text = wi.ToString();
            tm.Tick -= (o, args) => txtY.Text = y.ToString();
            tm.Tick -= (o, args) => txtX.Text = $"{x} | {2 * x}";
            x = 1;
            y = 1;
            txtX.Text = 1.ToString();
            txtY.Text = 1.ToString();
            txtWi.Text = 0.ToString();
            cmdStart.IsEnabled = true;
        }
    }
}
