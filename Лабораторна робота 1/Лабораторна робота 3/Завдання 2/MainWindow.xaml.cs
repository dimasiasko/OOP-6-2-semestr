using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Завдання_2
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

        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public event EventHandler Plus, Minus, Multi, Divide;

        private void BtnPlus_OnClick(object sender, RoutedEventArgs e)
        {
            Plus(sender, e);
        }

        private void BtnMinus_OnClick(object sender, RoutedEventArgs e)
        {
            Minus(sender, e);
        }

        private void BtnMulti_OnClick(object sender, RoutedEventArgs e)
        {
            Multi(sender, e);
        }

        private void BtnDivide_OnClick(object sender, RoutedEventArgs e)
        {
            Divide(sender, e);
        }
    }
}
