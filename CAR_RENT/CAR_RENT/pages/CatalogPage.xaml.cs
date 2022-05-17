using CAR_RENT.models;
using CAR_RENT.userControls;
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

namespace CAR_RENT.pages
{
    /// <summary>
    /// Логика взаимодействия для CatalogPage.xaml
    /// </summary>
    public partial class CatalogPage : Page
    {
        public CatalogPage()
        {
            InitializeComponent();

        }
        private void autopark_Click(object sender, RoutedEventArgs e)
        {
            catalog.Visibility = Visibility.Visible;
            mainInfo.Visibility = Visibility.Hidden;
            mainCar.Visibility = Visibility.Hidden;
            using (CAR_RENTEntities db = new CAR_RENTEntities())
            {
                var carsCatalog = from cars in db.CARS
                                  join models in db.MODEL_INFO
                                  on cars.MODEL equals models.MODEL
                                  select new
                                  {
                                      CurrentName = cars.BREND + cars.MODEL,
                                      Price = cars.RENT_PRICE,
                                      Year = models.YEAR_OF_ISSUE,
                                      BodyType = models.BODY_TYPE,
                                      EngineCapacity = models.ENGINE_CAPACITY,
                                      Transmission = models.TRANSMISSION,
                                      Equipment = models.EQUIPMENT,
                                      Image = cars.IMAGE
                                  };
                foreach (var car in carsCatalog)
                {
                    var buf = new Car(car.CurrentName, car.Price.ToString(), 
                    car.Year.ToString().Remove(0, 6).Remove(4), car.BodyType, car.EngineCapacity, 
                    car.Transmission, car.Equipment, car.Image);
                    buf.Width = 600;
                    buf.Height = 350;
                    StackPanel.Children.Add(buf);
                }
            }
        }

       
        private void request(string CLASS)
        {
            StackPanel.Children.Clear();
            catalog.Visibility = Visibility.Visible;
            mainInfo.Visibility = Visibility.Hidden;
            mainCar.Visibility = Visibility.Hidden;
            using (CAR_RENTEntities db = new CAR_RENTEntities())
            {
                var carsCatalog = from cars in db.CARS
                                  join models in db.MODEL_INFO
                                  on cars.MODEL equals models.MODEL
                                  where cars.CLASS == CLASS
                                  select new
                                  {
                                      CurrentName = cars.BREND + cars.MODEL,
                                      Price = cars.RENT_PRICE,
                                      Year = models.YEAR_OF_ISSUE,
                                      BodyType = models.BODY_TYPE,
                                      EngineCapacity = models.ENGINE_CAPACITY,
                                      Transmission = models.TRANSMISSION,
                                      Equipment = models.EQUIPMENT,
                                      Image = cars.IMAGE
                                  };
                foreach (var car in carsCatalog)
                {
                    var buf = new Car(car.CurrentName, car.Price.ToString(), car.Year.ToString().Remove(0, 6).Remove(4), car.BodyType, car.EngineCapacity, car.Transmission, car.Equipment, car.Image);
                    buf.Width = 600;
                    buf.Height = 350;
                    StackPanel.Children.Add(buf);
                }
            }
        }
        private void econom_Click(object sender, RoutedEventArgs e)
        {
            request("эконом");
            //StackPanel.ManipulationStartedEvent();
        }

        private void middle_Click(object sender, RoutedEventArgs e)
        {
            request("средний");
        }

        private void business_Click(object sender, RoutedEventArgs e)
        {
            request("бизнес");
        }

        private void cabriolet_Click(object sender, RoutedEventArgs e)
        {
            request("кабриолет");
        }

        private void offroad_cars_Click(object sender, RoutedEventArgs e)
        {
            request("внедорожник");
        }

        private void minibus_Click(object sender, RoutedEventArgs e)
        {
            request("микроавтобус");
        }

        private void truck_Click(object sender, RoutedEventArgs e)
        {
             request("грузовые");
        }

        private void StackPanel_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(1);
        }
    }
}
