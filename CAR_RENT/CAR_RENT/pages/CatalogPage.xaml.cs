using CAR_RENT.models;
using CAR_RENT.userControls;
using CAR_RENT.windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            mainInfo.Visibility = Visibility.Visible;
            mainCar.Visibility = Visibility.Visible;
            classes.Visibility = Visibility.Visible;
            line.Visibility = Visibility.Visible;
        }
        
        private void autopark_Click(object sender, RoutedEventArgs e)
        {
            search.Visibility = Visibility.Visible;            
            mainInfo.Visibility = Visibility.Hidden;
            mainCar.Visibility = Visibility.Hidden;
            classes.Visibility = Visibility.Hidden;
            line.Visibility = Visibility.Hidden;
            Classes.Visibility = Visibility.Visible;
            filters.Visibility = Visibility.Visible;
            using (CAR_RENTEntities db = new CAR_RENTEntities())
            {
                var carsCatalog = from cars in db.CARS
                                  join models in db.MODEL_INFO
                                  on cars.MODEL equals models.ID
                                  select new
                                  {
                                      CurrentName = models.BREND + models.MODEL,
                                      Price = cars.RENT_PRICE,
                                      Year = models.YEAR_OF_ISSUE,
                                      BodyType = models.BODY_TYPE,
                                      EngineCapacity = models.ENGINE_CAPACITY,
                                      EngineType=models.ENGINE_TYPE,
                                      Transmission = models.TRANSMISSION,
                                      Equipment = models.EQUIPMENT,
                                      Image = cars.IMAGE,
                                      Id=cars.ID
                                  };
                foreach (var car in carsCatalog)
                {
                    var buf = new Car(car.CurrentName, car.Price.ToString(), 
                    car.Year.ToString().Remove(0, 6).Remove(4), car.BodyType, car.EngineType, car.EngineCapacity,
                    car.Transmission, car.Equipment, car.Image, car.Id.ToString());
                    buf.Width = 740;
                    buf.Height = 420;
                    StackPanel.Items.Add(buf);    
                }
            }
        }

       
        private void request(string CLASS)
        {
            
            StackPanel.Items.Clear();
            search.Visibility = Visibility.Visible;
            Classes.Visibility = Visibility.Hidden;
            classes.Visibility = Visibility.Visible;
            mainInfo.Visibility = Visibility.Hidden;
            mainCar.Visibility = Visibility.Hidden;
            line.Visibility = Visibility.Visible;
            filters_for_classes.Visibility = Visibility.Visible;
            using (CAR_RENTEntities db = new CAR_RENTEntities())
            {
                string transmission = CheckedTransmission;
                string brand = CheckedBrand;
                if (transmission==null && brand == null) 
                { 
                var carsCatalog = from cars in db.CARS
                                  join models in db.MODEL_INFO
                                  on cars.MODEL equals models.ID
                                  where cars.CLASS == CLASS
                                  select new
                                  {
                                      CurrentName = models.BREND + models.MODEL,
                                      Price = cars.RENT_PRICE,
                                      Year = models.YEAR_OF_ISSUE,
                                      BodyType=models.BODY_TYPE,
                                      EngineType = models.ENGINE_TYPE,
                                      EngineCapacity = models.ENGINE_CAPACITY,
                                      Transmission = models.TRANSMISSION,
                                      Equipment = models.EQUIPMENT,
                                      Image = cars.IMAGE,
                                      Id=cars.ID
                                  };
                foreach (var car in carsCatalog)
                {
                    var buf = new Car(car.CurrentName, car.Price.ToString(), car.Year.ToString().Remove(0, 6).Remove(4), car.BodyType, car.EngineType.ToString(), car.EngineCapacity, car.Transmission, car.Equipment, car.Image, car.Id.ToString());
                    buf.Width = 740;
                    buf.Height = 420;
                   // Cars.Add(buf);
                    StackPanel.Items.Add(buf);    
                    
                }
                }
               
                else
                {
                    result.Text = "Необходимо нажать на кнопку по подбору авто!";
                    result.Visibility = Visibility.Visible;
                }
            }
        }
        private void econom_Click(object sender, RoutedEventArgs e)
        {
            request("Эконом");
            type_of_class.Text = "Эконом ";
            klass.Visibility = Visibility.Visible;
            klass.Text = "класс";
        }

        private void middle_Click(object sender, RoutedEventArgs e)
        {
            request("Средний");
            Class.Visibility = Visibility.Visible;
            type_of_class.Text = "Средний ";
            klass.Visibility = Visibility.Visible;
            klass.Text = "класс";
        }

        private void business_Click(object sender, RoutedEventArgs e)
        {
            request("Бизнес");
            Class.Visibility = Visibility.Visible;
            type_of_class.Text = "Бизнес ";
            klass.Visibility = Visibility.Visible;
            klass.Text = "класс";
        }

        private void cabriolet_Click(object sender, RoutedEventArgs e)
        {
            request("Кабриолет");
            Class.Visibility = Visibility.Visible;
            type_of_class.Text = "Кабриолет";
            klass.Visibility = Visibility.Visible;
            klass.Text = "ы";
        }

        private void offroad_cars_Click(object sender, RoutedEventArgs e)
        {
            request("Внедорожник");
            Class.Visibility = Visibility.Visible;
            type_of_class.Text = "внедорожник";
            klass.Visibility = Visibility.Visible;
            klass.Text = "и";
        }

        private void minibus_Click(object sender, RoutedEventArgs e)
        {
            request("Микроавтобус");
            Class.Visibility = Visibility.Visible;
            type_of_class.Text = "Микроавтобус";
            klass.Visibility = Visibility.Visible;
            klass.Text = "ы";
        }

        private void truck_Click(object sender, RoutedEventArgs e)
        {
            request("Грузовые");
            Class.Visibility = Visibility.Visible;
            type_of_class.Text = "Грузовые";
            klass.Visibility = Visibility.Hidden;

        }

        private void ScrollViewer_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled= true;
        }
        public string CheckedTransmission { get; private set; }
        public string CheckedBrand { get; private set; }
        public string CheckedClass { get; private set; }

        private void Transmission_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton item)
            {
                CheckedTransmission = item.Content.ToString();
            }
        }
        private void Brand_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton item)
            {
                CheckedBrand = item.Content.ToString();
            }
        }
        private void Class_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton item)
            {
                CheckedClass = item.Content.ToString();
            }
        }

        private void filters_Click(object sender, RoutedEventArgs e)
        {
           
            StackPanel.Items.Clear();
            string transmission = CheckedTransmission;
            string brand = CheckedBrand;
            string classs= CheckedClass;
            using (CAR_RENTEntities db = new CAR_RENTEntities())
            {
                if(transmission==null && brand ==null && classs==null)
                {
                    var carsCatalog = from cars in db.CARS
                                      join models in db.MODEL_INFO
                                      on cars.MODEL equals models.ID
                                      select new
                                      {
                                          CurrentName = models.BREND + models.MODEL,
                                          Price = cars.RENT_PRICE,
                                          Year = models.YEAR_OF_ISSUE,
                                          BodyType = models.BODY_TYPE,
                                          EngineCapacity = models.ENGINE_CAPACITY,
                                          EngineType = models.ENGINE_TYPE,
                                          Transmission = models.TRANSMISSION,
                                          Equipment = models.EQUIPMENT,
                                          Image = cars.IMAGE,
                                          Id = cars.ID
                                      };
                    foreach (var car in carsCatalog)
                    {
                        var buf = new Car(car.CurrentName, car.Price.ToString(),
                        car.Year.ToString().Remove(0, 6).Remove(4), car.BodyType, car.EngineType, car.EngineCapacity,
                        car.Transmission, car.Equipment, car.Image, car.Id.ToString());
                        buf.Width = 740;
                        buf.Height = 420;
                        StackPanel.Items.Add(buf);
                    }
                }
                if (transmission != null && brand != null && classs!=null)
                {
                    var carsCatalog = from cars in db.CARS
                                      join models in db.MODEL_INFO
                                      on cars.MODEL equals models.ID
                                      where models.TRANSMISSION == transmission && models.BREND == brand && cars.CLASS==classs
                                      select new
                                      {
                                          CurrentName = models.BREND + models.MODEL,
                                          Price = cars.RENT_PRICE,
                                          Year = models.YEAR_OF_ISSUE,
                                          BodyType = models.BODY_TYPE,
                                          EngineCapacity = models.ENGINE_CAPACITY,
                                          EngineType = models.ENGINE_TYPE,
                                          Transmission = models.TRANSMISSION,
                                          Equipment = models.EQUIPMENT,
                                          Image = cars.IMAGE,
                                          Id = cars.ID
                                      };
                    foreach (var car in carsCatalog)
                    {
                        var buf = new Car(car.CurrentName, car.Price.ToString(),
                        car.Year.ToString().Remove(0, 6).Remove(4), car.BodyType, car.EngineType, car.EngineCapacity,
                        car.Transmission, car.Equipment, car.Image, car.Id.ToString());
                        buf.Width = 740;
                        buf.Height = 420;
                        StackPanel.Items.Add(buf);
                    }
                    if (StackPanel.Items.Count == 0)
                    {
                        result.Text = "Данные не найдены!";
                        result.Visibility = Visibility.Visible;
                    }
                }
                if (transmission != null && brand != null && classs==null)
                {
                    var carsCatalog = from cars in db.CARS
                                      join models in db.MODEL_INFO
                                      on cars.MODEL equals models.ID
                                      where models.TRANSMISSION == transmission && models.BREND == brand
                                      select new
                                      {
                                          CurrentName = models.BREND + models.MODEL,
                                          Price = cars.RENT_PRICE,
                                          Year = models.YEAR_OF_ISSUE,
                                          BodyType = models.BODY_TYPE,
                                          EngineCapacity = models.ENGINE_CAPACITY,
                                          EngineType = models.ENGINE_TYPE,
                                          Transmission = models.TRANSMISSION,
                                          Equipment = models.EQUIPMENT,
                                          Image = cars.IMAGE,
                                          Id = cars.ID
                                      };
                    foreach (var car in carsCatalog)
                    {
                        var buf = new Car(car.CurrentName, car.Price.ToString(),
                        car.Year.ToString().Remove(0, 6).Remove(4), car.BodyType, car.EngineType, car.EngineCapacity,
                        car.Transmission, car.Equipment, car.Image, car.Id.ToString());
                        buf.Width = 740;
                        buf.Height = 420;
                        StackPanel.Items.Add(buf);
                    }
                    if (StackPanel.Items.Count == 0)
                    {
                        result.Text = "Данные не найдены!";
                        result.Visibility = Visibility.Visible;
                    }
                }
                if (transmission == null && brand != null && classs!=null)
                {
                    var carsCatalog = from cars in db.CARS
                                      join models in db.MODEL_INFO
                                      on cars.MODEL equals models.ID
                                      where models.BREND == brand && cars.CLASS == classs
                                      select new
                                      {
                                          CurrentName = models.BREND + models.MODEL,
                                          Price = cars.RENT_PRICE,
                                          Year = models.YEAR_OF_ISSUE,
                                          BodyType = models.BODY_TYPE,
                                          EngineCapacity = models.ENGINE_CAPACITY,
                                          EngineType = models.ENGINE_TYPE,
                                          Transmission = models.TRANSMISSION,
                                          Equipment = models.EQUIPMENT,
                                          Image = cars.IMAGE,
                                          Id = cars.ID
                                      };
                    foreach (var car in carsCatalog)
                    {
                        var buf = new Car(car.CurrentName, car.Price.ToString(),
                        car.Year.ToString().Remove(0, 6).Remove(4), car.BodyType, car.EngineType, car.EngineCapacity,
                        car.Transmission, car.Equipment, car.Image, car.Id.ToString());
                        buf.Width = 740;
                        buf.Height = 420;
                        StackPanel.Items.Add(buf);
                    }
                    if (StackPanel.Items.Count == 0)
                    {
                        result.Text = "Данные не найдены!";
                        result.Visibility = Visibility.Visible;
                    }
                }
                if (transmission != null && brand == null && classs!=null)
                {
                    var carsCatalog = from cars in db.CARS
                                      join models in db.MODEL_INFO
                                      on cars.MODEL equals models.ID
                                      where models.TRANSMISSION == transmission  && cars.CLASS == classs
                                      select new
                                      {
                                          CurrentName = models.BREND + models.MODEL,
                                          Price = cars.RENT_PRICE,
                                          Year = models.YEAR_OF_ISSUE,
                                          BodyType = models.BODY_TYPE,
                                          EngineCapacity = models.ENGINE_CAPACITY,
                                          EngineType = models.ENGINE_TYPE,
                                          Transmission = models.TRANSMISSION,
                                          Equipment = models.EQUIPMENT,
                                          Image = cars.IMAGE,
                                          Id = cars.ID
                                      };
                    foreach (var car in carsCatalog)
                    {
                        var buf = new Car(car.CurrentName, car.Price.ToString(),
                        car.Year.ToString().Remove(0, 6).Remove(4), car.BodyType, car.EngineType, car.EngineCapacity,
                        car.Transmission, car.Equipment, car.Image, car.Id.ToString());
                        buf.Width = 740;
                        buf.Height = 420;
                        StackPanel.Items.Add(buf);
                    }
                    if (StackPanel.Items.Count == 0)
                    {
                        result.Text = "Данные не найдены!";
                        result.Visibility = Visibility.Visible;
                    }
                }
                if (transmission != null && brand==null && classs==null)
                {
                    var carsCatalog = from cars in db.CARS
                                      join models in db.MODEL_INFO
                                      on cars.MODEL equals models.ID
                                      where models.TRANSMISSION == transmission
                                      select new
                                      {
                                          CurrentName = models.BREND + models.MODEL,
                                          Price = cars.RENT_PRICE,
                                          Year = models.YEAR_OF_ISSUE,
                                          BodyType = models.BODY_TYPE,
                                          EngineCapacity = models.ENGINE_CAPACITY,
                                          EngineType = models.ENGINE_TYPE,
                                          Transmission = models.TRANSMISSION,
                                          Equipment = models.EQUIPMENT,
                                          Image = cars.IMAGE,
                                          Id = cars.ID
                                      };
                    foreach (var car in carsCatalog)
                    {
                        var buf = new Car(car.CurrentName, car.Price.ToString(),
                        car.Year.ToString().Remove(0, 6).Remove(4), car.BodyType, car.EngineType, car.EngineCapacity,
                        car.Transmission, car.Equipment, car.Image, car.Id.ToString());
                        buf.Width = 740;
                        buf.Height = 420;
                        StackPanel.Items.Add(buf);
                    }
                    if (StackPanel.Items.Count == 0)
                    {
                        result.Text = "Данные не найдены!";
                        result.Visibility = Visibility.Visible;
                    }
                }
                if (brand != null && transmission==null && classs==null)
                {
                    var carsCatalog = from cars in db.CARS
                                      join models in db.MODEL_INFO
                                      on cars.MODEL equals models.ID
                                      where models.BREND == brand
                                      select new
                                      {
                                          CurrentName = models.BREND + models.MODEL,
                                          Price = cars.RENT_PRICE,
                                          Year = models.YEAR_OF_ISSUE,
                                          BodyType = models.BODY_TYPE,
                                          EngineCapacity = models.ENGINE_CAPACITY,
                                          EngineType = models.ENGINE_TYPE,
                                          Transmission = models.TRANSMISSION,
                                          Equipment = models.EQUIPMENT,
                                          Image = cars.IMAGE,
                                          Id = cars.ID
                                      };
                    foreach (var car in carsCatalog)
                    {
                        var buf = new Car(car.CurrentName, car.Price.ToString(),
                        car.Year.ToString().Remove(0, 6).Remove(4), car.BodyType, car.EngineType, car.EngineCapacity,
                        car.Transmission, car.Equipment, car.Image, car.Id.ToString());
                        buf.Width = 740;
                        buf.Height = 420;
                        StackPanel.Items.Add(buf);
                    }
                    if (StackPanel.Items.Count == 0)
                    {
                        result.Text = "Данные не найдены!";
                        result.Visibility = Visibility.Visible;
                    }
                }
                if (brand == null && transmission == null && classs != null)
                {
                    var carsCatalog = from cars in db.CARS
                                      join models in db.MODEL_INFO
                                      on cars.MODEL equals models.ID
                                      where cars.CLASS == classs
                                      select new
                                      {
                                          CurrentName = models.BREND + models.MODEL,
                                          Price = cars.RENT_PRICE,
                                          Year = models.YEAR_OF_ISSUE,
                                          BodyType = models.BODY_TYPE,
                                          EngineCapacity = models.ENGINE_CAPACITY,
                                          EngineType = models.ENGINE_TYPE,
                                          Transmission = models.TRANSMISSION,
                                          Equipment = models.EQUIPMENT,
                                          Image = cars.IMAGE,
                                          Id = cars.ID
                                      };
                    foreach (var car in carsCatalog)
                    {
                        var buf = new Car(car.CurrentName, car.Price.ToString(),
                        car.Year.ToString().Remove(0, 6).Remove(4), car.BodyType, car.EngineType, car.EngineCapacity,
                        car.Transmission, car.Equipment, car.Image, car.Id.ToString());
                        buf.Width = 740;
                        buf.Height = 420;
                        StackPanel.Items.Add(buf);
                    }
                    if (StackPanel.Items.Count == 0)
                    {
                        result.Text = "Данные не найдены!";
                        result.Visibility = Visibility.Visible;
                    }
                }


            }
        }

        private void filters_for_classes_Click(object sender, RoutedEventArgs e)
        {
            StackPanel.Items.Clear();
            string transmission = CheckedTransmission;
            string brand = CheckedBrand;
            string classs = CheckedClass;
            using (CAR_RENTEntities db = new CAR_RENTEntities())
            {
                if (transmission == null && brand == null && type_of_class.Text == null)
                {
                    var carsCatalog = from cars in db.CARS
                                      join models in db.MODEL_INFO
                                      on cars.MODEL equals models.ID
                                      select new
                                      {
                                          CurrentName = models.BREND + models.MODEL,
                                          Price = cars.RENT_PRICE,
                                          Year = models.YEAR_OF_ISSUE,
                                          BodyType = models.BODY_TYPE,
                                          EngineCapacity = models.ENGINE_CAPACITY,
                                          EngineType = models.ENGINE_TYPE,
                                          Transmission = models.TRANSMISSION,
                                          Equipment = models.EQUIPMENT,
                                          Image = cars.IMAGE,
                                          Id = cars.ID
                                      };
                    foreach (var car in carsCatalog)
                    {
                        var buf = new Car(car.CurrentName, car.Price.ToString(),
                        car.Year.ToString().Remove(0, 6).Remove(4), car.BodyType, car.EngineType, car.EngineCapacity,
                        car.Transmission, car.Equipment, car.Image, car.Id.ToString());
                        buf.Width = 740;
                        buf.Height = 420;
                        StackPanel.Items.Add(buf);
                    }
                }
                if (transmission == null && brand == null && type_of_class.Text != null)
                {

                    var carsCatalog = from cars in db.CARS
                                      join models in db.MODEL_INFO
                                      on cars.MODEL equals models.ID
                                      where cars.CLASS == type_of_class.Text

                                      select new
                                      {
                                          CurrentName = models.BREND + models.MODEL,
                                          Price = cars.RENT_PRICE,
                                          Year = models.YEAR_OF_ISSUE,
                                          BodyType = models.BODY_TYPE,
                                          EngineCapacity = models.ENGINE_CAPACITY,
                                          EngineType = models.ENGINE_TYPE,
                                          Transmission = models.TRANSMISSION,
                                          Equipment = models.EQUIPMENT,
                                          Image = cars.IMAGE,
                                          Id = cars.ID
                                      };
                    foreach (var car in carsCatalog)
                    {
                        var buf = new Car(car.CurrentName, car.Price.ToString(),
                        car.Year.ToString().Remove(0, 6).Remove(4), car.BodyType, car.EngineType, car.EngineCapacity,
                        car.Transmission, car.Equipment, car.Image, car.Id.ToString());
                        buf.Width = 740;
                        buf.Height = 420;
                        StackPanel.Items.Add(buf);
                    }

                }
                if (transmission != null && brand != null && type_of_class.Text != null)
                {
                    var carsCatalog = from cars in db.CARS
                                      join models in db.MODEL_INFO
                                      on cars.MODEL equals models.ID
                                      where models.TRANSMISSION == transmission && models.BREND == brand && cars.CLASS == type_of_class.Text
                                      select new
                                      {
                                          CurrentName = models.BREND + models.MODEL,
                                          Price = cars.RENT_PRICE,
                                          Year = models.YEAR_OF_ISSUE,
                                          BodyType = models.BODY_TYPE,
                                          EngineCapacity = models.ENGINE_CAPACITY,
                                          EngineType = models.ENGINE_TYPE,
                                          Transmission = models.TRANSMISSION,
                                          Equipment = models.EQUIPMENT,
                                          Image = cars.IMAGE,
                                          Id = cars.ID
                                      };
                    foreach (var car in carsCatalog)
                    {
                        var buf = new Car(car.CurrentName, car.Price.ToString(),
                        car.Year.ToString().Remove(0, 6).Remove(4), car.BodyType, car.EngineType, car.EngineCapacity,
                        car.Transmission, car.Equipment, car.Image, car.Id.ToString());
                        buf.Width = 740;
                        buf.Height = 420;
                        StackPanel.Items.Add(buf);
                    }
                    if (StackPanel.Items.Count == 0)
                    {
                        result.Text = "Данные не найдены!";
                        result.Visibility = Visibility.Visible;
                    }
                }
                if (transmission != null && brand != null && type_of_class.Text == null)
                {
                    var carsCatalog = from cars in db.CARS
                                      join models in db.MODEL_INFO
                                      on cars.MODEL equals models.ID
                                      where models.TRANSMISSION == transmission && models.BREND == brand
                                      select new
                                      {
                                          CurrentName = models.BREND + models.MODEL,
                                          Price = cars.RENT_PRICE,
                                          Year = models.YEAR_OF_ISSUE,
                                          BodyType = models.BODY_TYPE,
                                          EngineCapacity = models.ENGINE_CAPACITY,
                                          EngineType = models.ENGINE_TYPE,
                                          Transmission = models.TRANSMISSION,
                                          Equipment = models.EQUIPMENT,
                                          Image = cars.IMAGE,
                                          Id = cars.ID
                                      };
                    foreach (var car in carsCatalog)
                    {
                        var buf = new Car(car.CurrentName, car.Price.ToString(),
                        car.Year.ToString().Remove(0, 6).Remove(4), car.BodyType, car.EngineType, car.EngineCapacity,
                        car.Transmission, car.Equipment, car.Image, car.Id.ToString());
                        buf.Width = 740;
                        buf.Height = 420;
                        StackPanel.Items.Add(buf);
                    }
                    if (StackPanel.Items.Count == 0)
                    {
                        result.Text = "Данные не найдены!";
                        result.Visibility = Visibility.Visible;
                    }
                }
                if (transmission == null && brand != null && type_of_class.Text != null)
                {
                    var carsCatalog = from cars in db.CARS
                                      join models in db.MODEL_INFO
                                      on cars.MODEL equals models.ID
                                      where models.BREND == brand && cars.CLASS == type_of_class.Text
                                      select new
                                      {
                                          CurrentName = models.BREND + models.MODEL,
                                          Price = cars.RENT_PRICE,
                                          Year = models.YEAR_OF_ISSUE,
                                          BodyType = models.BODY_TYPE,
                                          EngineCapacity = models.ENGINE_CAPACITY,
                                          EngineType = models.ENGINE_TYPE,
                                          Transmission = models.TRANSMISSION,
                                          Equipment = models.EQUIPMENT,
                                          Image = cars.IMAGE,
                                          Id = cars.ID
                                      };
                    foreach (var car in carsCatalog)
                    {
                        var buf = new Car(car.CurrentName, car.Price.ToString(),
                        car.Year.ToString().Remove(0, 6).Remove(4), car.BodyType, car.EngineType, car.EngineCapacity,
                        car.Transmission, car.Equipment, car.Image, car.Id.ToString());
                        buf.Width = 740;
                        buf.Height = 420;
                        StackPanel.Items.Add(buf);
                    }
                    if (StackPanel.Items.Count == 0)
                    {
                        result.Text = "Данные не найдены!";
                        result.Visibility = Visibility.Visible;
                    }
                }
                if (transmission != null && brand == null && type_of_class.Text != null)
                {
                    var carsCatalog = from cars in db.CARS
                                      join models in db.MODEL_INFO
                                      on cars.MODEL equals models.ID
                                      where models.TRANSMISSION == transmission && cars.CLASS == type_of_class.Text
                                      select new
                                      {
                                          CurrentName = models.BREND + models.MODEL,
                                          Price = cars.RENT_PRICE,
                                          Year = models.YEAR_OF_ISSUE,
                                          BodyType = models.BODY_TYPE,
                                          EngineCapacity = models.ENGINE_CAPACITY,
                                          EngineType = models.ENGINE_TYPE,
                                          Transmission = models.TRANSMISSION,
                                          Equipment = models.EQUIPMENT,
                                          Image = cars.IMAGE,
                                          Id = cars.ID
                                      };
                    foreach (var car in carsCatalog)
                    {
                        var buf = new Car(car.CurrentName, car.Price.ToString(),
                        car.Year.ToString().Remove(0, 6).Remove(4), car.BodyType, car.EngineType, car.EngineCapacity,
                        car.Transmission, car.Equipment, car.Image, car.Id.ToString());
                        buf.Width = 740;
                        buf.Height = 420;
                        StackPanel.Items.Add(buf);
                    }
                    if (StackPanel.Items.Count == 0)
                    {
                        result.Text = "Данные не найдены!";
                        result.Visibility = Visibility.Visible;
                    }
                }
                if (transmission != null && brand == null && type_of_class.Text == null)
                {
                    var carsCatalog = from cars in db.CARS
                                      join models in db.MODEL_INFO
                                      on cars.MODEL equals models.ID
                                      where models.TRANSMISSION == transmission
                                      select new
                                      {
                                          CurrentName = models.BREND + models.MODEL,
                                          Price = cars.RENT_PRICE,
                                          Year = models.YEAR_OF_ISSUE,
                                          BodyType = models.BODY_TYPE,
                                          EngineCapacity = models.ENGINE_CAPACITY,
                                          EngineType = models.ENGINE_TYPE,
                                          Transmission = models.TRANSMISSION,
                                          Equipment = models.EQUIPMENT,
                                          Image = cars.IMAGE,
                                          Id = cars.ID
                                      };
                    foreach (var car in carsCatalog)
                    {
                        var buf = new Car(car.CurrentName, car.Price.ToString(),
                        car.Year.ToString().Remove(0, 6).Remove(4), car.BodyType, car.EngineType, car.EngineCapacity,
                        car.Transmission, car.Equipment, car.Image, car.Id.ToString());
                        buf.Width = 740;
                        buf.Height = 420;
                        StackPanel.Items.Add(buf);
                    }
                    if (StackPanel.Items.Count == 0)
                    {
                        result.Text = "Данные не найдены!";
                        result.Visibility = Visibility.Visible;
                    }
                }
                if (brand != null && transmission == null && type_of_class.Text == null)
                {
                    var carsCatalog = from cars in db.CARS
                                      join models in db.MODEL_INFO
                                      on cars.MODEL equals models.ID
                                      where models.BREND == brand
                                      select new
                                      {
                                          CurrentName = models.BREND + models.MODEL,
                                          Price = cars.RENT_PRICE,
                                          Year = models.YEAR_OF_ISSUE,
                                          BodyType = models.BODY_TYPE,
                                          EngineCapacity = models.ENGINE_CAPACITY,
                                          EngineType = models.ENGINE_TYPE,
                                          Transmission = models.TRANSMISSION,
                                          Equipment = models.EQUIPMENT,
                                          Image = cars.IMAGE,
                                          Id = cars.ID
                                      };
                    foreach (var car in carsCatalog)
                    {
                        var buf = new Car(car.CurrentName, car.Price.ToString(),
                        car.Year.ToString().Remove(0, 6).Remove(4), car.BodyType, car.EngineType, car.EngineCapacity,
                        car.Transmission, car.Equipment, car.Image, car.Id.ToString());
                        buf.Width = 740;
                        buf.Height = 420;
                        StackPanel.Items.Add(buf);
                    }
                    if (StackPanel.Items.Count == 0)
                    {
                        result.Text = "Данные не найдены!";
                        result.Visibility = Visibility.Visible;
                    }
                }

            }
        }
    }
}
