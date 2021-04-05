using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using Завдання_5.Annotations;

namespace Завдання_5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private int[] arrayInts;
        public string Array { get; set; }
        private Mutex mutexObj = new Mutex();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Random random = new Random();
            arrayInts = new int[random.Next(5,10)];

            for (int i = 0; i < arrayInts.Length; i++)
            {
                arrayInts[i] = random.Next(-50, 50);
                Array += $"{arrayInts[i]}, ";
            }

            lblUp.Text = Array;
            lblDown.Text = Array;
        }

        private void CmdStart_OnClick(object sender, RoutedEventArgs e)
        {
            lblUp.Text = String.Empty;
            lblDown.Text = String.Empty;

            Thread threadUp = new Thread(UpSort);
            threadUp.Start();

            Thread threadDown = new Thread(DownSort);
            threadDown.Start();
        }

        public string arrayUp;
        public string ArrayUp 
        {
            get { return arrayUp; }

            set
            {
                arrayUp = value;
                OnPropertyChanged("ArrayUp");
            }
        }
        public void UpSort()
        {

            int temp;
            for (int i = 0; i < arrayInts.Length - 1; i++)
            {
                for (int j = i + 1; j < arrayInts.Length; j++)
                {
                    if (arrayInts[i] > arrayInts[j])
                    {
                        ArrayUp = String.Empty;

                        temp = arrayInts[i];
                        arrayInts[i] = arrayInts[j];
                        arrayInts[j] = temp;

                        for (int k = 0; k < arrayInts.Length; k++)
                        {
                            ArrayUp += $"{arrayInts[k]}, ";
                        }

                        Thread.Sleep(300);
                    }
                }
            }
        }

        public string arrayDown;
        public string ArrayDown
        {
            get { return arrayDown; }

            set
            {
                arrayDown = value;
                OnPropertyChanged("ArrayDown");
            }
        }

        public void DownSort()
        {
            mutexObj.WaitOne();
            for (int i = 0; i < arrayInts.Length; i++)
            {
                for (int j = 0; j < arrayInts.Length - 1; j++)
                {
                    if (arrayInts[j] < arrayInts[j + 1])
                    {
                        ArrayDown = String.Empty;

                        int z = arrayInts[j];
                        arrayInts[j] = arrayInts[j + 1];
                        arrayInts[j + 1] = z;

                        for (int k = 0; k < arrayInts.Length; k++)
                        {
                            ArrayDown += $"{arrayInts[k]}, ";
                        }

                        Thread.Sleep(300);
                    }
                }
            }
            

            mutexObj.ReleaseMutex();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
