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
using CAR_RENT.pagesForAdmin;

namespace CAR_RENT.windows
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {

        public AdminWindow()
        {
            InitializeComponent();
            Frame = frame;

        }
        public static Frame Frame { get; set; }



        private void promocode_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Frame.Content = new promo_codePage();
            }
            catch { }
          
        }

        private void CLIENTS_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                clientsPage clientPage = new clientsPage();
                Frame.Content = clientPage;
            }
            catch { }
          
        }

        private void CARS_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                carsPage carsPage = new carsPage();
                Frame.Content = carsPage;
            }
            catch { }
        }



        private void MODEL_INFO_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                modelInfo modelInfo = new modelInfo();
                Frame.Content = modelInfo;
            }
            catch { }
        }

        private void CONTRACTS_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                contractsPage contractsPage = new contractsPage();
                Frame.Content = contractsPage;
            }
            catch { }
           
        }

        private void ACCIEDNTS_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                accidentsPage accidentsPage = new accidentsPage();
                Frame.Content = accidentsPage;
            }
            catch { }
           
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            catch { }
       
        }
        private void HOME_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Frame.Content = null;
            }
            catch { }
           
        }
    }
}
