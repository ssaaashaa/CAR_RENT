using CAR_RENT.models;
using CAR_RENT.userControls;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public static StringBuilder errors = new StringBuilder();

        public carsPage()
        {
            InitializeComponent();
            try
            {
             
                RENT_PRICE.PreviewTextInput += new TextCompositionEventHandler(numbers);
                DGridCars.ItemsSource = CAR_RENTEntities.GetContext().CARS.ToList();
                foreach (CAR car in DGridCars.Items)
                {
                    try
                    {
                        string id = car.ID.ToString();
                        CONTRACT contract = CAR_RENTEntities.GetContext().CONTRACTS.Where(c => c.CAR_ID.ToString().Trim() == id.Trim()).FirstOrDefault();

                        if (contract != null)
                        {
                            if (contract.CONTRACT_STATUS == "Прокат активен")
                            {
                                car.STATUS = "В прокате";
                            }
                            else car.STATUS = "Свободна";
                            CAR_RENTEntities.GetContext().SaveChanges();
                        }


                        else if (contract == null)
                        {
                            car.STATUS = "Свободна";
                            CAR_RENTEntities.GetContext().SaveChanges();
                        }
                    }
                    catch { }

                }


            }
            catch { }

        }
        void numbers(object sender, TextCompositionEventArgs e)
        {
            try
            {
                if (!Char.IsDigit(e.Text, 0) && e.Text != ".")
                {
                    e.Handled = true; //не обрабатывать введеный символ
                }
            }
            catch { }
        }
      
        void Clear()
        {
            try
            {
                ID.Clear();
                MODEL.Text = null;
                CLASS.Text = null;
                REGISTRATION_NUMBER.Text = null;
                STATUS.Text = null;
                RENT_PRICE.Clear();
                IMAGE.Source = null;
                link.Clear();
            }
            catch { }
        }

        private void DGridCars_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                CAR selectedCar = new CAR();
                selectedCar = DGridCars.SelectedItem as CAR;
                if (selectedCar != null)
                {
                    ID.Text = selectedCar.ID.ToString().Trim();
                    MODEL.Text = selectedCar.MODEL.ToString().Trim();
                    CLASS.Text = selectedCar.CLASS.Trim();
                    REGISTRATION_NUMBER.Text = selectedCar.REGISTRATION_NUMBER.Trim();
                    STATUS.Text = selectedCar.STATUS.Trim();
                    string Link = selectedCar.IMAGE;
                    BitmapImage myBitmapImage = new BitmapImage(new Uri(Link));
                    myBitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    IMAGE.Source = myBitmapImage;
                    link.Text = Link.Trim();
                    RENT_PRICE.Text = selectedCar.RENT_PRICE.ToString().Trim();
                }
                else
                {
                    MessageBox.Show("Вы не выбрали запись!");
                }
            }
            catch { }

        }
       
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ID.Text.Equals(""))
                {
                    MessageBox.Show("Выделите запись, в которую требуется внести изменения!");
                    return;
                }
                CAR currentCar = CAR_RENTEntities.GetContext().CARS.Where(m => m.ID.ToString() == ID.Text.ToString()).FirstOrDefault();

                currentCar.RENT_PRICE = Convert.ToInt32(RENT_PRICE.Text.Trim());
                if (currentCar != null)
                {
                    try
                    {
                        CAR_RENTEntities.GetContext().SaveChanges();
                        DGridCars.ItemsSource = CAR_RENTEntities.GetContext().CARS.ToList();
                        Clear();
                        MessageBox.Show("Цена успешно обновлена!");
                    }
                    catch
                    {
                        MessageBox.Show("Необходимо выбрать запись для редактирования!");
                    }
                }
            }
            catch
            { }
        }

       


        private void LoadImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ID.Text.Equals(""))
                {
                    MessageBox.Show("Выделите запись, которую требуется изменить!");
                    return;
                }
                CAR currentCar = CAR_RENTEntities.GetContext().CARS.Where(m => m.ID.ToString() == ID.Text.ToString()).FirstOrDefault();
                Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
                if (openFileDlg.ShowDialog() == true)
                    link.Text = openFileDlg.FileName;
                IMAGE.Source = new BitmapImage(new Uri(link.Text));
                currentCar.IMAGE = link.Text.Trim();
                if (currentCar != null)
                {
                    try
                    {
                        CAR_RENTEntities.GetContext().SaveChanges();
                        DGridCars.ItemsSource = CAR_RENTEntities.GetContext().CARS.ToList();
                    }
                    catch
                    {
                        MessageBox.Show("Необходимо выбрать запись для загрузки фото!");
                    }
                }
            }
            catch { }
        }

      

        private void id_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                DGridCars.ItemsSource = CAR_RENTEntities.GetContext().CARS.Where(c => c.MODEL.ToString().Trim() == id.Text.Trim()).ToList();
                if (id.Text.Trim().Length == 0)
                {
                    DGridCars.ItemsSource = CAR_RENTEntities.GetContext().CARS.ToList();
                }
            }
            catch { }
        }
    }
}


