using CAR_RENT.pages;
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
using System.Windows.Shapes;

namespace CAR_RENT.windows
{
    /// <summary>
    /// Логика взаимодействия для CatalogWindow.xaml
    /// </summary>
    public partial class CatalogWindow : Window
    {
        public CatalogWindow()
        {
            InitializeComponent();
            frame.Content = new CatalogPage();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try 
            { 
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
            }
            catch { }
        }


        private void userAccount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                frame.Content = new UserInfoPage();
            }
            catch { }
            
        }

        private void userContracts_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                frame.Content = new UserContracts();
            }
            catch { }
            
        }

        private void main_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                frame.Content = new CatalogPage();
            }
            catch { }
        }
    }
}
