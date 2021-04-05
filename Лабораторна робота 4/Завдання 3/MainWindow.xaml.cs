using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Windows;


namespace Завдання_3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private static Mutex mutexObj = new Mutex();
        private int x = 1;
        private int y = 1;
        private int sum = 0;
        private int input;

        private void CmdChoose_OnClick(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(inputNumber.Text, out input))
            {
                if (input < 0)
                    MessageBox.Show("Введите целое ПОЛОЖИТЕЛЬНОЕ ЧИСЛО");

                MessageBox.Show("Введите целое ЧИСЛО");
            }
            else
            {
                cmdChoose.IsEnabled = false;
                cmdChoose.Content = "Подождите";
            }


            Thread threadFibonachi = new Thread(FibonachiNumbers);
            threadFibonachi.Start();

            Thread threadSimple = new Thread(SimpleNumbers);
            threadSimple.Start();

            if (!(threadFibonachi.IsAlive && threadSimple.IsAlive))
            {
                Thread threadResult = new Thread(Results);
                threadResult.Start();
            }
            
        }

        
        private void FibonachiNumbers()
        {
            
            while (input > sum)
            {
                sum = x + y;

                x = y;
                y = sum;

                try
                {
                    using (StreamWriter sw = new StreamWriter(@"D:\Fibonachi.txt", true, System.Text.Encoding.Default))
                    {
                        sw.WriteLine(sum);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
        }

        private void SimpleNumbers()
        {
            
            for (int i = 2; i <= input; i++)
            {
                bool b = true;
                for (int j = 2; j < i; j++)
                {
                    if (i % j == 0 & i % 1 == 0)
                        b = false;
                }

                if (b)
                {
                    try
                    {
                        using (StreamWriter simplWriter = new StreamWriter(@"D:\Simple.txt", true, System.Text.Encoding.Default))
                        {
                            simplWriter.WriteLine(i);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }


        private static readonly ConcurrentDictionary<string, object> _locks = new ConcurrentDictionary<string, object>();
        private void Results()
        {
            object obj;
            while (!_locks.TryGetValue(@"D:\Fibonachi.txt", out obj))
            {
                obj = new object();
                if (_locks.TryAdd(@"D:\Fibonachi.txt", obj))
                    break;
            }
            lock (obj)
            {
                using (StreamReader fib = new StreamReader(@"D:\Fibonachi.txt"))
                {
                    int count = 0;
                    string line;
                    while ((line = fib.ReadLine()) != null)
                    {
                        txtFibonachi.Text += line;
                        count++;
                    }

                    txtFibonachi.Text += "Количество чисел = " + count;
                }
                using (StreamReader sim = new StreamReader(@"D:\Simple.txt"))
                {
                    int count = 0;
                    string line;
                    while ((line = sim.ReadLine()) != null)
                    {
                        txtSimple.Text += line;
                        count++;
                    }

                    txtSimple.Text += "Количество чисел = " + count;
                }
            }

            
        }
    }
}
