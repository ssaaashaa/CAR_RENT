using CAR_RENT.models;
using CAR_RENT.userControls;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
        private CAR currentCar = new CAR();
        public static StringBuilder errors = new StringBuilder();

        public carsPage()
        {
            InitializeComponent();
            ListView listID=new ListView();
            ListView listMODEL=new ListView();
            DGridCars.ItemsSource = CAR_RENTEntities.GetContext().CARS.ToList();
            RENT_PRICE.PreviewTextInput += new TextCompositionEventHandler(rent_priceTextInput);
            using (CAR_RENTEntities db = new CAR_RENTEntities())
            {
                var currentModels = from models in db.MODEL_INFO                                  
                                    select new
                                    {
                                        ID = models.ID,
                                        MODEL=models.MODEL
                                    };
                foreach (var model in currentModels)
                {
                    listID.Items.Add(model.ID);
                    listMODEL.Items.Add(model.MODEL);
                }
            }
           foreach(CAR car in DGridCars.Items)
            {
                try
                {
                    string id = car.ID.ToString();
                    CONTRACT contract = CAR_RENTEntities.GetContext().CONTRACTS.Where(c => c.CAR_ID.ToString() == id).FirstOrDefault();

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

        void Clear()
        {
            ID.Clear();
            BREND.Clear();
            MODEL.SelectedIndex = -1;
            CLASS.SelectedIndex = -1;
            REGISTRATION_NUMBER.Clear();
            STATUS.SelectedIndex = -1;
            RENT_PRICE.Clear();
            IMAGE.Source = null;
        }

        private void DGridCars_MouseEnter(object sender, MouseEventArgs e)
        {
            CAR selectedCar = new CAR();
            selectedCar = DGridCars.SelectedItem as CAR;
            if(selectedCar != null)
            { 
            ID.Text = selectedCar.ID.ToString();
            //BREND.Text = selectedCar.BREND;
            MODEL.SelectedValue = selectedCar.MODEL;
            CLASS.Text = selectedCar.CLASS;
            REGISTRATION_NUMBER.Text = selectedCar.REGISTRATION_NUMBER;
            STATUS.Text = selectedCar.STATUS;
            string Link = selectedCar.IMAGE;
            BitmapImage myBitmapImage = new BitmapImage(new Uri(Link));
            myBitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            IMAGE.Source = myBitmapImage;
            link.Text=Link;
            RENT_PRICE.Text = selectedCar.RENT_PRICE.ToString();
            MODEL.ScrollIntoView(selectedCar.MODEL);
            }
            else
            {
                MessageBox.Show("Вы не выбрали запись!");
            }

        }
        void rent_priceTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true; //не обрабатывать введеный символ
            }
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(BREND.Text))
            {
                errors.AppendLine("Введите марку!");
            }
            if (MODEL.SelectedIndex == -1)
            {
                errors.AppendLine("Выберите модель!");
            }
            if (CLASS.SelectedIndex == -1)
            {
                errors.AppendLine("Выберите класс!");
            }
            if (string.IsNullOrWhiteSpace(REGISTRATION_NUMBER.Text))
            {
                errors.AppendLine("Введите регистрационный номер!");
            }
            if (STATUS.SelectedIndex == -1)
            {
                errors.AppendLine("Выберите статус!");
            }
            if (string.IsNullOrWhiteSpace(REGISTRATION_NUMBER.Text))
            {
                errors.AppendLine("Введите стоимость проката!");
            }
            if (string.IsNullOrWhiteSpace(link.Text))
            {
                errors.AppendLine("Загрузите фото!");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            CAR currentCar = new CAR();
            //currentCar.BREND = BREND.Text;
            currentCar.MODEL = Convert.ToInt32(MODEL.SelectedValue);
            currentCar.CLASS = CLASS.Text;
            currentCar.REGISTRATION_NUMBER = REGISTRATION_NUMBER.Text;
            currentCar.STATUS = STATUS.Text;
            currentCar.RENT_PRICE = Convert.ToInt32(RENT_PRICE.Text);
            currentCar.IMAGE = link.Text;
            CAR_RENTEntities.GetContext().CARS.Add(currentCar);
            try
            {
                CAR_RENTEntities.GetContext().SaveChanges();
                DGridCars.ItemsSource = CAR_RENTEntities.GetContext().CARS.ToList();
                Clear();
                MessageBox.Show("Данные успешно добавлены!");
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                {
                    foreach (DbValidationError err in validationError.ValidationErrors)
                    {
                        MessageBox.Show(err.ErrorMessage + " ");

                    }
                }
            }
            
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (ID.Text.Equals(""))
            {
                MessageBox.Show("Выделите запись, которую требуется изменить!");
            }
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(BREND.Text))
            {
                errors.AppendLine("Введите марку!");
            }
            if (MODEL.SelectedIndex == -1)
            {
                errors.AppendLine("Выберите модель!");
            }
            if (CLASS.SelectedIndex == -1)
            {
                errors.AppendLine("Выберите класс!");
            }
            if (string.IsNullOrWhiteSpace(REGISTRATION_NUMBER.Text))
            {
                errors.AppendLine("Введите регистрационный номер!");
            }
            if (string.IsNullOrWhiteSpace(REGISTRATION_NUMBER.Text))
            {
                errors.AppendLine("Введите стоимость проката!");
            }
            if (string.IsNullOrWhiteSpace(link.Text))
            {
                errors.AppendLine("Загрузите фото!");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            try
            {
                CAR currentCar = CAR_RENTEntities.GetContext().CARS.Where(m => m.ID.ToString() == ID.Text.ToString()).FirstOrDefault();
                //currentCar.BREND = BREND.Text;
                currentCar.MODEL = Convert.ToInt32(MODEL.SelectedValue);
                currentCar.CLASS = CLASS.Text;
                currentCar.REGISTRATION_NUMBER = REGISTRATION_NUMBER.Text;
                currentCar.STATUS = STATUS.Text;
                currentCar.RENT_PRICE = Convert.ToInt32(RENT_PRICE.Text);
                currentCar.IMAGE = link.Text;
                if (currentCar != null)
                {
                    try
                    {
                        CAR_RENTEntities.GetContext().SaveChanges();
                        DGridCars.ItemsSource = CAR_RENTEntities.GetContext().CARS.ToList();
                        Clear();
                        MessageBox.Show("Данные успешно обновлены!");
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

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ID.Text.Equals(""))
            {
                MessageBox.Show("Выделите запись, которую требуется удалить!");
            }
            CAR currentCar = CAR_RENTEntities.GetContext().CARS.Where(m => m.ID.ToString() == ID.Text.ToString()).FirstOrDefault();
            if (currentCar != null)
            {
                CAR_RENTEntities.GetContext().CARS.Remove(currentCar);
                try
                {
                    CAR_RENTEntities.GetContext().SaveChanges();
                    DGridCars.ItemsSource = CAR_RENTEntities.GetContext().CARS.ToList();
                    Clear();
                    MessageBox.Show("Запись удалена!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (currentCar == null && string.IsNullOrEmpty(ID.Text) == false)
            {
                MessageBox.Show("Такого авто не существует!");
            }
        }


        private void LoadImage_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            if (openFileDlg.ShowDialog() == true)
                link.Text = openFileDlg.FileName;
            IMAGE.Source = new BitmapImage(new Uri(link.Text));
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < DGridCars.Items.Count; i++)
            {
                string param = regNumber.Text;
                DGridCars.ScrollIntoView(DGridCars.Items[i]);
                DataGridRow row = (DataGridRow)DGridCars.ItemContainerGenerator.ContainerFromIndex(i);
                TextBlock cellContentRegistr = DGridCars.Columns[4].GetCellContent(row) as TextBlock;
                TextBlock cellContentID = DGridCars.Columns[0].GetCellContent(row) as TextBlock;
                if ((cellContentRegistr != null && cellContentRegistr.Text.ToLower().Trim().Equals(param.ToLower()))
                    || (cellContentID != null && cellContentID.Text.ToLower().Trim().Equals(param.ToLower())))         {
                    object item = DGridCars.Items[i];
                    DGridCars.SelectedItem = item;
                    DGridCars.ScrollIntoView(item);
                    row.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                    break;
                }
            }
        }
    }
}   
   

