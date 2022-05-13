using CAR_RENT.models;
using CAR_RENT.userControls;
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

namespace CAR_RENT.pagesForAdmin
{
    /// <summary>
    /// Логика взаимодействия для carsPage.xaml
    /// </summary>
    public partial class carsPage : Page
    {
        public carsPage()
        {
            InitializeComponent();
            Load();
        }
        private void Load()
        {
            CAR_RENTEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(entry => entry.Reload());
            DGridCars.ItemsSource = CAR_RENTEntities.GetContext().CARS.ToList();

        }

        private void DGridCars_MouseEnter(object sender, MouseEventArgs e)
        {
            CAR selectedCar = new CAR();
            selectedCar = DGridCars.SelectedItem as CAR;
            ID.Text = selectedCar.ID.ToString();
            BREND.Text = selectedCar.BREND;
            MODEL.Text = selectedCar.MODEL;
            CLASS.Text = selectedCar.CLASS;
            REGISTRATION_NUMBER.Text=selectedCar.REGISTRATION_NUMBER;
            STATUS.Text = selectedCar.STATUS;
            string link = selectedCar.IMAGE;
            BitmapImage myBitmapImage = new BitmapImage(new Uri(link));
            myBitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            IMAGE.Source = myBitmapImage;
            RENT_PRICE.Text = selectedCar.RENT_PRICE.ToString();

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CAR currentCar = CAR_RENTEntities.GetContext().CARS.Where(u => u.ID.ToString() == ID.Text).Single();
                if (currentCar != null)
                {
                    CAR_RENTEntities.GetContext().CARS.Remove(currentCar);
                    try
                    {
                        CAR_RENTEntities.GetContext().SaveChanges();
                        MessageBox.Show("Запись удалена!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    Load();

                }

            }
            catch
            {
                MessageBox.Show("Для удаления записи необходимо выделить её!");
            }
        }
    }
}
