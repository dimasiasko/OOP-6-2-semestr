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

namespace Завдання_1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();
            new Presenter(this);
        }
        public event EventHandler Start;
        public event EventHandler Pause;
        public event EventHandler Stop;

        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnStop_OnClick(object sender, RoutedEventArgs e)
        {
            Stop(sender, e);
        }

        private void BtnPause_OnClick(object sender, RoutedEventArgs e)
        {
            Pause(sender, e);
        }

        private void BtnStart_OnClick(object sender, RoutedEventArgs e)
        {
            Start(sender, e);
        }
    }
}
