using CAR_RENT.models;
using CAR_RENT.windows;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для clientPage.xaml
    /// </summary>
    public partial class clientsPage : Page
    {
        public static StringBuilder errors = new StringBuilder();
        public clientsPage()
        {
            InitializeComponent();
            Load();
            PASSPORT_ID.PreviewTextInput += new TextCompositionEventHandler(passportTextInput);
            
        }

       

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                CLIENT currentClient = CAR_RENTEntities.GetContext().CLIENTS.Where(u => u.ID.ToString() == ID.Text).Single();
                Validation();
                if (errors.Length == 0)
                {
                    if (currentClient != null)
                    {
                       
                        try
                        {
                            currentClient.LOGIN = LOGIN.Text;
                            currentClient.PASSWORD = PASSWORD.Text;
                            currentClient.SURNAME=SURNAME.Text;
                            currentClient.NAME= NAME.Text;
                            currentClient.PATRONYMIC=PATRONYMIC.Text;
                            DateTime date = new DateTime();
                            DateTime.TryParse(BDAY.Text, out date);
                            currentClient.BDAY = date;
                            currentClient.PASSPORT_SERIES = PASSPORT_SERIES.Text;
                            currentClient.PASSPORT_ID = Int32.Parse(PASSPORT_ID.Text);
                            currentClient.DRIVER_LICENSE_ID=LICENSE_ID.Text;
                            currentClient.DRIVING_EXPERIENCE=EXPERIENCE.Text;
                            currentClient.TELEPHONE = TELEPHONE.Text;
                            currentClient.ADRESS=ADRESS.Text;
                            currentClient.USER_TYPE = Int32.Parse(TYPE.Text);
                            CAR_RENTEntities.GetContext().SaveChanges();
                            Load();
                            MessageBox.Show("Запись обновлена!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }
                }

            }
            catch
            {
                MessageBox.Show("Для редактирования записи необходимо выделить её!");
            }

        }

       
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Validation();
            CLIENT currentClient = CAR_RENTEntities.GetContext().CLIENTS.FirstOrDefault(u=>u.ID.ToString()==ID.Text);
            CLIENT client = CAR_RENTEntities.GetContext().CLIENTS.FirstOrDefault(u => u.LOGIN == LOGIN.Text);

            if (client != null)
            {
                MessageBox.Show("Пользователь с таким логином уже существует. Попробуйте снова!");
            }
            else 
            { 
            if (errors.Length == 0)
            {
                CAR_RENTEntities.GetContext().CLIENTS.Add(currentClient);
                try
                {
                    
                    CAR_RENTEntities.GetContext().SaveChanges();
                    MessageBox.Show("Запись добавлена!");
                    Load();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Запись не добавлена!");
            }
            }
        }
        void passportTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true; //не обрабатывать введеный символ
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CLIENT currentClient = CAR_RENTEntities.GetContext().CLIENTS.Where(u => u.ID.ToString() == ID.Text).Single();
                if (currentClient != null)
                {
                    CAR_RENTEntities.GetContext().CLIENTS.Remove(currentClient);
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

        private void Load()
        {
            CAR_RENTEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(entry => entry.Reload());
            DGridClients.ItemsSource=CAR_RENTEntities.GetContext().CLIENTS.ToList();
           
        }
       

        private void DGridClients_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CLIENT selectedClient = new CLIENT();
            selectedClient = DGridClients.SelectedItem as CLIENT;
            ID.Text = selectedClient.ID.ToString();
            LOGIN.Text = selectedClient.LOGIN;
            PASSWORD.Text = selectedClient.PASSWORD;
            SURNAME.Text = selectedClient.SURNAME;
            NAME.Text = selectedClient.NAME;
            PATRONYMIC.Text = selectedClient.PATRONYMIC;
            BDAY.Text = selectedClient.BDAY.ToString().Remove(10);
            PASSPORT_SERIES.Text = selectedClient.PASSPORT_SERIES;
            PASSPORT_ID.Text=selectedClient.PASSPORT_ID.ToString();
            LICENSE_ID.Text = selectedClient.DRIVER_LICENSE_ID;
            EXPERIENCE.Text = selectedClient.DRIVING_EXPERIENCE;
            TELEPHONE.Text = selectedClient.TELEPHONE;
            ADRESS.Text = selectedClient.ADRESS;
            TYPE.Text = selectedClient.USER_TYPE.ToString();
        }
         private void Validation()
        {
            string patternLogin = @"^[a-zA-Zа-яА-Я0-9_-]{3,16}$";
            string patternPassword = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9])\S{8,12}$";
            string patternName = @"^([a-zA-ZА-Яа-я])+$";
            string id = ID.Text;
            string login = LOGIN.Text;
            string password = PASSWORD.Text;
            string surname = SURNAME.Text;
            string name = NAME.Text;
            string patronymic = PATRONYMIC.Text;
            DateTime bday = new DateTime();
            DateTime.TryParse(BDAY.Text, out bday);
            string passportSeries = PASSPORT_SERIES.Text;
            string passportID = PASSPORT_ID.Text;
            string license = LICENSE_ID.Text;
            string experience = EXPERIENCE.Text;
            //string telephone=TELEPHONE.Text;
            string adress = ADRESS.Text;


            if (string.IsNullOrEmpty(login))
            {
                errors.AppendLine("Введите логин!");
            }
           
            if (!Regex.IsMatch(login, patternLogin, RegexOptions.IgnoreCase))
            {
                errors.AppendLine("Логин может содержать цифры, буквы, - _, быть длиной от 3 до 16 символов");
            }

            if (string.IsNullOrEmpty(password))
            {
                errors.AppendLine("Введите пароль!");
            }
            if (!Regex.IsMatch(password, patternPassword, RegexOptions.IgnoreCase))
            {
                errors.AppendLine("Пароль должен быть 8-12 символов, содержать хотя бы одну цифру, одну латинскую букву, один специальный символ. Введите новый пароль!");
            }
            if (string.IsNullOrEmpty(surname))
            {
                errors.AppendLine("Введите фамилию!");
            }
            if (!Regex.IsMatch(surname, patternName, RegexOptions.IgnoreCase))
            {
                errors.AppendLine("Фамилия может содержать только буквы!");
            }
            if (string.IsNullOrEmpty(name))
            {
                errors.AppendLine("Введите имя!");
            }
            if (!Regex.IsMatch(name, patternName, RegexOptions.IgnoreCase))
            {
                errors.AppendLine("Имя может содержать только буквы!");
            }
            if (string.IsNullOrEmpty(patronymic))
            {
                errors.AppendLine("Введите отчество!");
            }
            if (!Regex.IsMatch(patronymic, patternName, RegexOptions.IgnoreCase))
            {
                errors.AppendLine("Отчество может содержать только буквы!");
            }
            if (string.IsNullOrEmpty(bday.ToString()))
            {
                errors.AppendLine("Введите дату рождения!");
            }
            if (string.IsNullOrEmpty(passportSeries))
            {
                errors.AppendLine("Выберите серию паспорта!");
            }
            if (string.IsNullOrEmpty(passportID))
            {
                errors.AppendLine("Введите номер паспорта!");
            }
            if (string.IsNullOrEmpty(license))
            {
                errors.AppendLine("Введите номер ВУ!");
            }
            if (string.IsNullOrEmpty(experience))
            {
                errors.AppendLine("Введите опыт вождения!");
            }
            if (TELEPHONE.IsMaskCompleted == false)
            {
                errors.AppendLine("Введите номер телефона!");
            }
            if (string.IsNullOrEmpty(adress))
            {
                errors.AppendLine("Введите адрес!");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
        }

       
    }
}
