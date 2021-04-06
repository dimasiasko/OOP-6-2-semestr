using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Завдання_8.Annotations;

namespace Завдання_8
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>


    public class StartEnd
    {
        public int start;
        public int end;

        public StartEnd(int s, int e)
        {
            this.start = s;
            this.end = e;
        }
    }
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private int startnumber;
        private int finishnumber;
        private Mutex mutexObj = new Mutex();
        private int counterDiapasons;
       

        private void CmdFind_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(int.TryParse(txtStart.Text, out startnumber)) || startnumber < 2 || !(int.TryParse(txtStop.Text, out finishnumber)))
            {
                MessageBox.Show("Введите целое число БОЛЬШЕ ИЛИ РАВНО 2");
            }
            else
            {
                cmdFind.IsEnabled = false;
                cmdFind.Content = "Подождите";
            }

            int tempend;
            
            if ((finishnumber - startnumber) % 2 == 0)
            {
                tempend = (finishnumber - startnumber) / 2;
            }
            else
            {
                tempend = (finishnumber - startnumber) / 2 + 1;
            }

            int tempstart = tempend;

            Thread[] threads = new Thread[2];
            for (int i = 0; i < threads.Length; i++)
            {
                if (i > 0)
                {
                    StartEnd se = new StartEnd(tempstart, (i+1) * tempend);
                    threads[i] = new Thread(FindSimple);
                    threads[i].Start(se);
                }
                else
                {
                    StartEnd se = new StartEnd(startnumber, (i+1) * tempend);
                    threads[i] = new Thread(FindSimple);
                    threads[i].Start(se);
                }
                
                
            }
        }

        private string simplenumbers;

        public string SimpleNumbers
        {
            get { return simplenumbers; }
            set
            {
                simplenumbers = value;
                OnPropertyChanged(nameof(SimpleNumbers));
            }
        }

        private void FindSimple(object end)
        {
            mutexObj.WaitOne();
            StartEnd endn = (StartEnd) end;

            for (int i = endn.start; i <= endn.end; i++)
            {
                bool b = true;
                for (int j = endn.start; j < i; j++)
                {
                    if (i % j == 0 & i % 1 == 0)
                        b = false;
                }
                if (b)
                    SimpleNumbers += $"{i}, ";
            }
            mutexObj.ReleaseMutex();
            
        }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
