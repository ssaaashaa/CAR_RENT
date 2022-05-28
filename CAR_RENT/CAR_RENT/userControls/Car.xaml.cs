using CAR_RENT.pages;
using CAR_RENT.windows;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace CAR_RENT.userControls
{
    /// <summary>
    /// Логика взаимодействия для Car.xaml
    /// </summary>
    public partial class Car : UserControl
    {
        public string ID { get; set; }
        public string CarName { get; set; }
        public Car(string CurrentName, string Price, string Year,string BodyType, string EngineType,
        string EngineCapacity, string Transmission, string Equipment, string Image, string Id)
        {
            InitializeComponent();
            try
            {
                currentName.Text = CurrentName;
                price.Text = Price + " BYN";
                year.Text = Year;
                bodyType.Text = BodyType;
                engineType.Text = EngineType;
                engineCapacity.Text = EngineCapacity;
                transmission.Text = Transmission;
                equipment.Text = Equipment;
                BitmapImage myBitmapImage = new BitmapImage(new Uri(Image));
                myBitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                currentImage.Source = myBitmapImage;
                ID = Id;
                CarName = CurrentName;
            }
            catch { }

        }
     
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                rent rent = new rent(ID, CarName);
                rent.Show();
            }
            catch{ }
         
        }
    }
}
