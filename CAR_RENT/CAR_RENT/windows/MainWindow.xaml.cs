using CAR_RENT.pages;
using CAR_RENT.windows;
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

namespace CAR_RENT
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow mainWindow = new MainWindow();
        //public static Frame Frame;
        public MainWindow()
        {
            InitializeComponent();
            LogPage LogPage = new LogPage();
            frame.Navigate(LogPage);
            mainWindow = this;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            foreach (Window w in App.Current.Windows)
                w.Close();

        }
    }
}
