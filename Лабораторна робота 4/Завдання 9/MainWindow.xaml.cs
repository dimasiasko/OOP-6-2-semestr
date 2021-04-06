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
using Завдання_9.Annotations;

namespace Завдання_9
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
        private int firstnumber;
        private int secondnumber;
        private void CmdFind_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(int.TryParse(txtStart.Text, out firstnumber)) || !(int.TryParse(txtStop.Text, out secondnumber)) || secondnumber < 0 || firstnumber < 0)
            {
                MessageBox.Show("Введите целые числа БОЛЬШЕ 0");
            }
            else
            {
                cmdFind.IsEnabled = false;
                cmdFind.Content = "Подождите";
            }

            StartEnd se = new StartEnd(firstnumber, secondnumber);

            Thread thread = new Thread(Search);
            thread.Start(se);

        }

        private string number;
        public string Number
        {
            get { return number; }
            set
            {
                number = value;
                OnPropertyChanged(nameof(Number));
            }
        }

        public void Search(object x)
        {
            StartEnd se = (StartEnd) x;

            if (se.start == 0)
            {
                Number += se.end;
            }
            else
            {
                while (se.end != 0)
                { 
                    if (se.start > se.end) 
                    {
                        se.start -= se.end;
                    }
                    else
                    { 
                        se.end -= se.start;
                    }
                }

                Number += se.start;
            }
        }
        

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
