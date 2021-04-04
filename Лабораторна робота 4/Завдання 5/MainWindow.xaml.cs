using System;
using System.Windows;

namespace Завдання_5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int[] arrayInts;
        public string Array
        {
            get { return arrayInts.ToString(); }
        }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Random random = new Random();
            arrayInts = new int[random.Next(10)];

            for (int i = 0; i < arrayInts.Length; i++)
                arrayInts[i] = random.Next(-50, 50);

        }

        private void CmdStart_OnClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
