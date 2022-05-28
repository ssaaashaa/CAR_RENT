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
        public static Frame Frame { get; set; }
        public CatalogWindow()
        {
            InitializeComponent();
            frame.Content = new CatalogPage();
            Frame = frame;
            
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

        private void main_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                main.TextDecorations = TextDecorations.Underline;
            }catch { }
        }
        private void main_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                main.TextDecorations = null;
            }
            catch { }
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                exit.TextDecorations = TextDecorations.Underline;
            }
            catch { }
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                exit.TextDecorations = null;
            }
            catch { }
        }

        private void userContracts_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                contracts.Width = 45;
            }
            catch { }
        }

        private void userContracts_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                contracts.Width = 40;
            }
            catch { }
        }

        private void userAccount_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                account.Width = 45;
            }
            catch { }
        }

        private void userAccount_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                account.Width = 40;
            }
            catch { }
        }
    }
}
