using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        public Frame Frame { get; set; }



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

        private void CONTRACTS_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_contracts.PlacementTarget = CONTRACTS;
            popup_contracts.Placement = PlacementMode.Right;
            popup_contracts.IsOpen = true;
        }

        private void CONTRACTS_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_contracts.Visibility = Visibility.Collapsed;
            popup_contracts.IsOpen = false;
        }

        private void CARS_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_cars.PlacementTarget = CARS;
            popup_cars.Placement = PlacementMode.Right;
            popup_cars.IsOpen = true;
        }

        private void CARS_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_cars.Visibility = Visibility.Collapsed;
            popup_cars.IsOpen = false;
        }

        private void MODEL_INFO_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_model.PlacementTarget = MODEL_INFO;
            popup_model.Placement = PlacementMode.Right;
            popup_model.IsOpen = true;
        }

        private void MODEL_INFO_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_model.Visibility = Visibility.Collapsed;
            popup_model.IsOpen = false;
        }

        private void CLIENTS_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_clients.PlacementTarget = CLIENTS;
            popup_clients.Placement = PlacementMode.Right;
            popup_clients.IsOpen = true;
        }

        private void CLIENTS_MouseLeave(object sender, MouseEventArgs e)
        {

            popup_clients.Visibility = Visibility.Collapsed;
            popup_clients.IsOpen = false;
        }

        private void ACCIEDNTS_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_accidents.PlacementTarget = ACCIEDNTS;
            popup_accidents.Placement = PlacementMode.Right;
            popup_accidents.IsOpen = true;
        }

        private void ACCIEDNTS_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_accidents.Visibility = Visibility.Collapsed;
            popup_accidents.IsOpen = false;
        }

        private void promocode_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_promo.PlacementTarget =promocode;
            popup_promo.Placement = PlacementMode.Right;
            popup_promo.IsOpen = true;
        }

        private void promocode_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_promo.Visibility = Visibility.Collapsed;
            popup_promo.IsOpen = false;
        }

       
    }
}
