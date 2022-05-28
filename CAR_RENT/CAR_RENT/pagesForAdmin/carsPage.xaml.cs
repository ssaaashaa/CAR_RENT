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
                MODEL.PreviewTextInput += new TextCompositionEventHandler(numbers);
                REGISTRATION_NUMBER.PreviewTextInput += new TextCompositionEventHandler(lettersAndNumbers);
                regNum.PreviewTextInput += new TextCompositionEventHandler(lettersAndNumbers);
                id.PreviewTextInput += new TextCompositionEventHandler(numbers);
                RENT_PRICE.PreviewTextInput += new TextCompositionEventHandler(numbers);
                ListView listID = new ListView();
                DGridCars.ItemsSource = CAR_RENTEntities.GetContext().CARS.ToList();

                using (CAR_RENTEntities db = new CAR_RENTEntities())
                {
                    var currentModels = from models in db.MODEL_INFO
                                        select new
                                        {
                                            ID = models.ID
                                        };
                    foreach (var model in currentModels)
                    {
                        MODEL.Items.Add(model.ID);

                    }
                }
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
        void lettersAndNumbers(object sender, TextCompositionEventArgs e)
        {
            try
            {
                if (!Char.IsLetterOrDigit(e.Text, 0) && e.Text != "-")
                {
                    e.Handled = true; //не обрабатывать введеный символ
                }
            }
            catch { }
        }
        private void space_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Space)
                {
                    e.Handled = true;
                }
            }
            catch { }
        }
        void Clear()
        {
            try
            {
                ID.Clear();
                MODEL.SelectedIndex = -1;
                CLASS.SelectedIndex = -1;
                REGISTRATION_NUMBER.Clear();
                STATUS.SelectedIndex = -1;
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
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string patternRegNum = @"^([0-9]{4}\s[A-Z]{2}-[0-9])$";
                StringBuilder errors = new StringBuilder();
                if(string.IsNullOrWhiteSpace(MODEL.Text.Trim())
                    && CLASS.SelectedIndex == -1
                    && string.IsNullOrWhiteSpace(REGISTRATION_NUMBER.Text.Trim())
                    && STATUS.SelectedIndex == -1
                    && string.IsNullOrWhiteSpace(RENT_PRICE.Text.Trim())
                    && string.IsNullOrWhiteSpace(link.Text.Trim()))
                {
                    errors.AppendLine("Необходиомо заполнить все поля!");
                    MessageBox.Show(errors.ToString());
                    errors.Clear();
                    return;
                }
                if (string.IsNullOrWhiteSpace(MODEL.Text.Trim()))
                {
                    errors.AppendLine("Выберите модель!");
                }
                if (CLASS.SelectedIndex == -1)
                {
                    errors.AppendLine("Выберите класс!");
                }
                if (string.IsNullOrWhiteSpace(REGISTRATION_NUMBER.Text.Trim()))
                {
                    errors.AppendLine("Введите регистрационный номер!");

                }
                if (!Regex.IsMatch(REGISTRATION_NUMBER.Text.Trim(), patternRegNum))
                {
                    errors.AppendLine("Введите корректный регистрационный номер! Например, 1234 AB-4");
                }
                CAR car = CAR_RENTEntities.GetContext().CARS.FirstOrDefault(u => u.REGISTRATION_NUMBER.Trim() == REGISTRATION_NUMBER.Text.Trim());
                if (car != null)
                {
                    errors.AppendLine("Машина с таким регистрационным номером уже существует. Попробуйте снова!");
                }
                if (STATUS.SelectedIndex == -1)
                {
                    errors.AppendLine("Выберите статус!");
                }
                if (string.IsNullOrWhiteSpace(RENT_PRICE.Text.Trim()))
                {
                    errors.AppendLine("Введите стоимость проката!");
                }
                if (string.IsNullOrWhiteSpace(link.Text.Trim()))
                {
                    errors.AppendLine("Загрузите фото!");
                }
                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    errors.Clear();
                    return;
                }
                CAR currentCar = new CAR();
                currentCar.CLASS = CLASS.Text.Trim();
                currentCar.MODEL = Convert.ToInt32(MODEL.SelectedValue);
                currentCar.REGISTRATION_NUMBER = REGISTRATION_NUMBER.Text.Trim();
                currentCar.STATUS = STATUS.Text.Trim();
                currentCar.RENT_PRICE = Convert.ToInt32(RENT_PRICE.Text.Trim());
                currentCar.IMAGE = link.Text.Trim();
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
            catch { }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder errors = new StringBuilder();

                string patternRegNum = @"^([0-9]{4}\s[A-Z]{2}-[0-9])$";
                if (ID.Text.Equals(""))
                {
                    errors.AppendLine("Выделите запись, которую требуется изменить!");
                    MessageBox.Show(errors.ToString());
                    errors.Clear();
                    return;
                }
                if (string.IsNullOrWhiteSpace(MODEL.Text.Trim()))
                {
                    errors.AppendLine("Выберите модель!");
                }
                if (CLASS.SelectedIndex == -1)
                {
                    errors.AppendLine("Выберите класс!");
                }
                if (string.IsNullOrWhiteSpace(REGISTRATION_NUMBER.Text.Trim()))
                {
                    errors.AppendLine("Введите регистрационный номер!");
                }
                if (!Regex.IsMatch(REGISTRATION_NUMBER.Text.Trim(), patternRegNum))
                {
                    errors.AppendLine("Введите корректный регистрационный номер! Например, 1234 AB-4");
                }
                CAR currentCar = CAR_RENTEntities.GetContext().CARS.Where(m => m.ID.ToString() == ID.Text.ToString()).FirstOrDefault();
                CAR car = CAR_RENTEntities.GetContext().CARS.FirstOrDefault(u => u.REGISTRATION_NUMBER.Trim() == REGISTRATION_NUMBER.Text.Trim());

                if (car != null && currentCar.REGISTRATION_NUMBER.Trim() != REGISTRATION_NUMBER.Text.Trim())
                {
                    errors.AppendLine("Машина с таким регистрационным номером уже существует. Попробуйте снова!");
                }
                if (string.IsNullOrWhiteSpace(RENT_PRICE.Text.Trim()))
                {
                    errors.AppendLine("Введите стоимость проката!");
                }
                if (string.IsNullOrWhiteSpace(link.Text.Trim()))
                {
                    errors.AppendLine("Загрузите фото!");
                }
                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    errors.Clear();
                    return;
                }
                currentCar.MODEL = Convert.ToInt32(MODEL.Text.Trim());
                currentCar.CLASS = CLASS.Text.Trim();
                currentCar.REGISTRATION_NUMBER = REGISTRATION_NUMBER.Text.Trim();
                currentCar.STATUS = STATUS.Text.Trim();
                currentCar.RENT_PRICE = Convert.ToInt32(RENT_PRICE.Text.Trim());
                currentCar.IMAGE = link.Text.Trim();
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
            try
            {
                if (ID.Text.Equals(""))
                {
                    MessageBox.Show("Выделите запись, которую требуется удалить!");
                }
                CAR currentCar = CAR_RENTEntities.GetContext().CARS.Where(m => m.ID.ToString().Trim() == ID.Text.ToString().Trim()).FirstOrDefault();
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
                else if (currentCar == null && string.IsNullOrEmpty(ID.Text.Trim()) == false)
                {
                    MessageBox.Show("Такого авто не существует!");
                }
            }
            catch { }
        }


        private void LoadImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
                if (openFileDlg.ShowDialog() == true)
                    link.Text = openFileDlg.FileName;
                IMAGE.Source = new BitmapImage(new Uri(link.Text));
            }
            catch { }
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                id.Clear();
                for (int i = 0; i < DGridCars.Items.Count; i++)
                {
                    string param = regNum.Text.Trim();
                    DGridCars.ScrollIntoView(DGridCars.Items[i]);
                    DataGridRow row = (DataGridRow)DGridCars.ItemContainerGenerator.ContainerFromIndex(i);
                    TextBlock cellContentRegistr = DGridCars.Columns[3].GetCellContent(row) as TextBlock;
                   
                    if (cellContentRegistr != null && cellContentRegistr.Text.ToLower().Trim().Equals(param.ToLower()))
                       
                    {
                        object item = DGridCars.Items[i];
                        DGridCars.SelectedItem = item;
                        DGridCars.ScrollIntoView(item);
                        row.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                        break;
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


