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
        List<int> IDs=new List<int>();
        public CatalogPage()
        {
            InitializeComponent();
            mainInfo.Visibility = Visibility.Visible;
            mainCar.Visibility = Visibility.Visible;
            classes.Visibility = Visibility.Visible;
            line.Visibility = Visibility.Visible;
        }
        //public CatalogPage(ItemsControl itemsControl, Car car)
        //{
        //    itemsControl.Items.Clear();
        //    StackPanel = itemsControl;
        //}
        
        private void autopark_Click(object sender, RoutedEventArgs e)
        {
            try
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
                                      CurrentName = models.BREND+" "+models.MODEL,
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
            catch { }
        }

       
        private void request(string CLASS)
        {
            try
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
                                      CurrentName = models.BREND + " " + models.MODEL,
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
            catch { }
        }
        private void econom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                request("Эконом");
                type_of_class.Text = "Эконом ";
                klass.Visibility = Visibility.Visible;
                klass.Text = "класс";
            }
            catch { }
        }

        private void middle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                request("Средний");
                Class.Visibility = Visibility.Visible;
                type_of_class.Text = "Средний ";
                klass.Visibility = Visibility.Visible;
                klass.Text = "класс";
            }
            catch { }
        }

        private void business_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                request("Бизнес");
                Class.Visibility = Visibility.Visible;
                type_of_class.Text = "Бизнес ";
                klass.Visibility = Visibility.Visible;
                klass.Text = "класс";
            }
            catch { }
          
        }

        private void cabriolet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                request("Кабриолет");
                Class.Visibility = Visibility.Visible;
                type_of_class.Text = "Кабриолет";
                klass.Visibility = Visibility.Visible;
                klass.Text = "ы";
            }
            catch { }
        
        }

        private void offroad_cars_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                request("Внедорожник");
                Class.Visibility = Visibility.Visible;
                type_of_class.Text = "Внедорожник";
                klass.Visibility = Visibility.Visible;
                klass.Text = "и";
            }
            catch { }
        }

        private void minibus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                request("Микроавтобус");
                Class.Visibility = Visibility.Visible;
                type_of_class.Text = "Микроавтобус";
                klass.Visibility = Visibility.Visible;
                klass.Text = "ы";
            }
            catch { }
          
        }

        private void truck_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                request("Грузовые");
                Class.Visibility = Visibility.Visible;
                type_of_class.Text = "Грузовые";
                klass.Visibility = Visibility.Hidden;
            }
            catch { }

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
            try 
            { 
            if (sender is RadioButton item)
            {
                CheckedTransmission = item.Content.ToString();
            }
            }
            catch { }
        }
        private void Brand_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is RadioButton item)
                {
                    CheckedBrand = item.Content.ToString();
                }
            }
            catch { }
            
        }
        private void Class_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is RadioButton item)
                {
                    CheckedClass = item.Content.ToString();
                }
            }
            catch { }
           
        }

        private void filters_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StackPanel.Items.Clear();
                string transmission = CheckedTransmission;
                string brand = CheckedBrand;
                string classs = CheckedClass;
                using (CAR_RENTEntities db = new CAR_RENTEntities())
                {
                    if (transmission == null && brand == null && classs == null)
                    {
                        var carsCatalog = from cars in db.CARS
                                          join models in db.MODEL_INFO
                                          on cars.MODEL equals models.ID
                                          select new
                                          {
                                              CurrentName = models.BREND + " " + models.MODEL,
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
                    if (transmission != null && brand != null && classs != null)
                    {
                        var carsCatalog = from cars in db.CARS
                                          join models in db.MODEL_INFO
                                          on cars.MODEL equals models.ID
                                          where models.TRANSMISSION == transmission && models.BREND == brand && cars.CLASS == classs
                                          select new
                                          {
                                              CurrentName = models.BREND + " " + models.MODEL,
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
                    if (transmission != null && brand != null && classs == null)
                    {
                        var carsCatalog = from cars in db.CARS
                                          join models in db.MODEL_INFO
                                          on cars.MODEL equals models.ID
                                          where models.TRANSMISSION == transmission && models.BREND == brand
                                          select new
                                          {
                                              CurrentName = models.BREND + " " + models.MODEL,
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
                    if (transmission == null && brand != null && classs != null)
                    {
                        var carsCatalog = from cars in db.CARS
                                          join models in db.MODEL_INFO
                                          on cars.MODEL equals models.ID
                                          where models.BREND == brand && cars.CLASS == classs
                                          select new
                                          {
                                              CurrentName = models.BREND + " " + models.MODEL,
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
                    if (transmission != null && brand == null && classs != null)
                    {
                        var carsCatalog = from cars in db.CARS
                                          join models in db.MODEL_INFO
                                          on cars.MODEL equals models.ID
                                          where models.TRANSMISSION == transmission && cars.CLASS == classs
                                          select new
                                          {
                                              CurrentName = models.BREND + " " + models.MODEL,
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
                    if (transmission != null && brand == null && classs == null)
                    {
                        var carsCatalog = from cars in db.CARS
                                          join models in db.MODEL_INFO
                                          on cars.MODEL equals models.ID
                                          where models.TRANSMISSION == transmission
                                          select new
                                          {
                                              CurrentName = models.BREND + " " + models.MODEL,
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
                    if (brand != null && transmission == null && classs == null)
                    {
                        var carsCatalog = from cars in db.CARS
                                          join models in db.MODEL_INFO
                                          on cars.MODEL equals models.ID
                                          where models.BREND == brand
                                          select new
                                          {
                                              CurrentName = models.BREND + " " + models.MODEL,
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
                                              CurrentName = models.BREND + " " + models.MODEL,
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
            catch { }
        }

        private void filters_for_classes_Click(object sender, RoutedEventArgs e)
        {
            try
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
                                              CurrentName = models.BREND + " " + models.MODEL,
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
                                              CurrentName = models.BREND + " " + models.MODEL,
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
                                              CurrentName = models.BREND + " " + models.MODEL,
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
                                              CurrentName = models.BREND + " " + models.MODEL,
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
                                              CurrentName = models.BREND + " " + models.MODEL,
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
                                              CurrentName = models.BREND + " " + models.MODEL,
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
                                              CurrentName = models.BREND + " " + models.MODEL,
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
                                              CurrentName = models.BREND + " " + models.MODEL,
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
            catch { }
        }

        private void econom_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                Econom.TextDecorations = TextDecorations.Underline;
            }
            catch { }
        }

        private void econom_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                Econom.TextDecorations = null;
            }
            catch { }
        }

        private void middle_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                Middle.TextDecorations = TextDecorations.Underline;
            }
            catch { }
        }

        private void middle_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                Middle.TextDecorations = null;
            }
            catch { }
        }

        private void business_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                Business.TextDecorations = TextDecorations.Underline;
            }
            catch { }
        }
   

        private void business_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                Business.TextDecorations = null;
            }
            catch { }
        }

        private void cabriolet_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                Cabriolet.TextDecorations = TextDecorations.Underline;
            }
            catch { }
        }

        private void cabriolet_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                Cabriolet.TextDecorations = null;
            }
            catch { }

        }

        private void offroad_cars_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                Offroad.TextDecorations = TextDecorations.Underline;
            }
            catch { }
        }

        private void offroad_cars_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                Offroad.TextDecorations = null;
            }
            catch { }
        }

        private void minibus_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                Minibus.TextDecorations = TextDecorations.Underline;
            }
            catch { }
        }

        private void minibus_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                Minibus.TextDecorations = null;
            }
            catch { }
        }

        private void truck_MouseEnter(object sender, MouseEventArgs e)
        {

            try
            {
                Truck.TextDecorations = TextDecorations.Underline;
            }
            catch { }
        }

        private void truck_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                Truck.TextDecorations = null;
            }
            catch { }
        }
    }
}
