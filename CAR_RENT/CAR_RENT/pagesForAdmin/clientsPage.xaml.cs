using CAR_RENT.models;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CAR_RENT.pagesForAdmin
{
    /// <summary>
    /// Логика взаимодействия для clientPage.xaml
    /// </summary>
    public partial class clientsPage : Page
    {
       

        public clientsPage()
        {
            InitializeComponent();
            DGridClients.ItemsSource = CAR_RENTEntities.GetContext().CLIENTS.ToList();           
            PASSPORT_ID.PreviewTextInput += new TextCompositionEventHandler(passportTextInput);
        }
        private void Validation()
        {
            StringBuilder errors = new StringBuilder();
            string patternLogin = @"^[a-zA-Zа-яА-Я0-9_-]{3,16}$";
            string patternPassword = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9])\S{8,12}$";
            string patternName = @"^[А-Я][а-я]+$";
            string patternTelephone = @"^\+375 \((17|29|33|44)\) [0-9]{3}-[0-9]{2}-[0-9]{2}$";
            DateTime date = new DateTime();
            DateTime.TryParse(BDAY.Text, out date);
            DateTime today = DateTime.Today;
            if (string.IsNullOrEmpty(LOGIN.Text))
            {
                errors.AppendLine("Введите логин!");
            }
            if (!Regex.IsMatch(LOGIN.Text, patternLogin, RegexOptions.IgnoreCase))
            {
                errors.AppendLine("Логин может содержать цифры, буквы, - _, быть длиной от 3 до 16 символов");
            }
            if (string.IsNullOrEmpty(PASSWORD.Text))
            {
                errors.AppendLine("Введите пароль!");
            }
            if (!Regex.IsMatch(PASSWORD.Text, patternPassword, RegexOptions.IgnoreCase))
            {
                errors.AppendLine("Пароль должен быть 8-12 символов, содержать хотя бы одну цифру, одну латинскую букву, один специальный символ. Введите новый пароль!");
            }
            if (string.IsNullOrEmpty(SURNAME.Text))
            {
                errors.AppendLine("Введите фамилию!");
            }
            if (!Regex.IsMatch(SURNAME.Text, patternName, RegexOptions.IgnoreCase))
            {
                errors.AppendLine("Фамилия должна начинаться с большой буквы!");
            }
            if (string.IsNullOrEmpty(NAME.Text))
            {
                errors.AppendLine("Введите имя!");
            }
            if (!Regex.IsMatch(NAME.Text, patternName, RegexOptions.IgnoreCase))
            {
                errors.AppendLine("Имя должно начинаться с большой буквы!");
            }
            if (string.IsNullOrEmpty(PATRONYMIC.Text))
            {
                errors.AppendLine("Введите отчество!");
            }
            if (!Regex.IsMatch(PATRONYMIC.Text, patternName, RegexOptions.IgnoreCase))
            {
                errors.AppendLine("Отчество  должно начинаться с большой буквы!");
            }
            if (string.IsNullOrEmpty(BDAY.Text))
            {
                errors.AppendLine("Введите дату рождения!");
            }
            if ((today.Year - date.Year) < 18)
            {
                errors.AppendLine("Пользователь может быть только совершеннолетним!");
            }
            if (string.IsNullOrEmpty(PASSPORT_SERIES.Text))
            {
                errors.AppendLine("Выберите серию паспорта!");
            }
            if (string.IsNullOrEmpty(PASSPORT_ID.Text))
            {
                errors.AppendLine("Введите номер паспорта!");
            }
            if (PASSPORT_ID.Text.Length != 7)
            {
                errors.AppendLine("Введите корректный номер паспорта!");
            }
            if (string.IsNullOrEmpty(LICENSE_ID.Text))
            {
                errors.AppendLine("Введите номер ВУ!");
            }
            if (string.IsNullOrEmpty(EXPERIENCE.Text))
            {
                errors.AppendLine("Введите опыт вождения!");
            }
            if (!Regex.IsMatch(TELEPHONE.Text, patternTelephone, RegexOptions.IgnoreCase))
            {
                errors.AppendLine("Неверный формат телефона!");
            }
            if (string.IsNullOrEmpty(ADRESS.Text))
            {
                errors.AppendLine("Введите адрес!");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
        }
       
        private void Clear()
        {
            LOGIN.Clear();
            PASSWORD.Clear();
            SURNAME.Clear();
            NAME.Clear();
            PATRONYMIC.Clear();
            BDAY.Clear();
            PASSPORT_SERIES.SelectedIndex = -1;
            PASSPORT_ID.Clear();
            LICENSE_ID.Clear();
            EXPERIENCE.Clear();
            TELEPHONE.Clear();
            ADRESS.Clear();
            TYPE.Clear();
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Validation();
            DateTime date = new DateTime();
            DateTime.TryParse(BDAY.Text, out date);
            CLIENT client = CAR_RENTEntities.GetContext().CLIENTS.FirstOrDefault(u => u.LOGIN == LOGIN.Text);
            if (client != null)
            {
                MessageBox.Show("Пользователь с таким логином уже существует. Попробуйте снова!");
            }
            else
            {
                CLIENT currentClient=new CLIENT();
                currentClient.LOGIN = LOGIN.Text;
                currentClient.PASSWORD=PASSWORD.Text;
                currentClient.SURNAME=SURNAME.Text;
                currentClient.NAME=NAME.Text;
                currentClient.PATRONYMIC=PATRONYMIC.Text;
                currentClient.BDAY = date;
                currentClient.PASSPORT_SERIES=PASSPORT_SERIES.Text;
                try
                {
                    currentClient.PASSPORT_ID = Int32.Parse(PASSPORT_ID.Text);
                }
                catch
                {
                    if (PASSPORT_ID.Text.Length == 0)
                    {
                        MessageBox.Show("Введите номер паспорта");
                        return;
                    }
                }
                currentClient.DRIVER_LICENSE_ID=LICENSE_ID.Text;
                currentClient.DRIVING_EXPERIENCE = EXPERIENCE.Text;
                currentClient.TELEPHONE=TELEPHONE.Text;
                currentClient.ADRESS=ADRESS.Text;
                CAR_RENTEntities.GetContext().CLIENTS.Add(currentClient);
                try
                {
                    CAR_RENTEntities.GetContext().SaveChanges();
                    DGridClients.ItemsSource = CAR_RENTEntities.GetContext().CLIENTS.ToList();
                    Clear();
                    MessageBox.Show("Данные успешно добавлены!");
                }
                catch
                {
                    Clear();
                    MessageBox.Show("Такая запись уже существует!");
                }


            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Validation();
            DateTime date = new DateTime();
            DateTime.TryParse(BDAY.Text, out date);
            try
            {
                CLIENT currentClient = CAR_RENTEntities.GetContext().CLIENTS.Where(u => u.ID.ToString() == ID.Text).FirstOrDefault();
                currentClient.LOGIN = LOGIN.Text;
                currentClient.PASSWORD = PASSWORD.Text;
                currentClient.SURNAME = SURNAME.Text;
                currentClient.NAME = NAME.Text;
                currentClient.PATRONYMIC = PATRONYMIC.Text;
                currentClient.BDAY = date;
                currentClient.PASSPORT_SERIES = PASSPORT_SERIES.Text;
                try
                {
                    currentClient.PASSPORT_ID = Int32.Parse(PASSPORT_ID.Text);
                }
                catch
                {
                    if (PASSPORT_ID.Text.Length == 0)
                    {
                        MessageBox.Show("Введите номер паспорта");
                    }
                }
                currentClient.DRIVER_LICENSE_ID = LICENSE_ID.Text;
                currentClient.DRIVING_EXPERIENCE = EXPERIENCE.Text;
                currentClient.TELEPHONE = TELEPHONE.Text;
                currentClient.ADRESS = ADRESS.Text;
                if (currentClient!=null)
                {
                    try
                    {
                        CAR_RENTEntities.GetContext().SaveChanges();
                        DGridClients.ItemsSource = CAR_RENTEntities.GetContext().CLIENTS.ToList();
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
            {
                if (string.IsNullOrEmpty(LOGIN.Text) == false)
                {
                    Clear();
                    MessageBox.Show("Такого клиента не существует!");
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
            CLIENT currentClient = CAR_RENTEntities.GetContext().CLIENTS.Where(u => u.ID.ToString() == ID.Text).FirstOrDefault();
          
                if (currentClient != null)
                {
                    CAR_RENTEntities.GetContext().CLIENTS.Remove(currentClient);
                    try
                    {
                        CAR_RENTEntities.GetContext().SaveChanges();
                        DGridClients.ItemsSource = CAR_RENTEntities.GetContext().CLIENTS.ToList();
                        Clear();
                        MessageBox.Show("Запись удалена!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
                else if (LOGIN.Text!=""&&currentClient==null)
            {
                Clear();
                MessageBox.Show("Такой записи не существует!");
            }
                else if (currentClient == null)
            {
                Clear();
                MessageBox.Show("Необходимо выбрать запись для удаления!");

            }


        }

       
       

        private void DGridClients_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CLIENT selectedClient = new CLIENT();
            selectedClient = DGridClients.SelectedItem as CLIENT;
            if (selectedClient != null)
            {
                LOGIN.Text = selectedClient.LOGIN;
                PASSWORD.Text = selectedClient.PASSWORD;
                SURNAME.Text = selectedClient.SURNAME;
                NAME.Text = selectedClient.NAME;
                PATRONYMIC.Text = selectedClient.PATRONYMIC;
                try
                {
                    BDAY.Text = selectedClient.BDAY.ToString().Remove(10);
                }
                catch { }
                PASSPORT_SERIES.Text = selectedClient.PASSPORT_SERIES;
                PASSPORT_ID.Text = selectedClient.PASSPORT_ID.ToString();
                try
                {
                    LICENSE_ID.Text = selectedClient.DRIVER_LICENSE_ID.ToString();
                }
                catch { }
                EXPERIENCE.Text = selectedClient.DRIVING_EXPERIENCE.ToString();
                TELEPHONE.Text = selectedClient.TELEPHONE;
                ADRESS.Text = selectedClient.ADRESS;
                TYPE.Text = selectedClient.USER_TYPE.ToString();
                ID.Text = selectedClient.ID.ToString();
            }
            else
            {
                MessageBox.Show("Запись не выбрана!");
            }
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < DGridClients.Items.Count; i++)
            {
                string param = telephNum.Text;
                DGridClients.ScrollIntoView(DGridClients.Items[i]);
                DataGridRow row = (DataGridRow)DGridClients.ItemContainerGenerator.ContainerFromIndex(i);
                TextBlock cellContentTeleph = DGridClients.Columns[11].GetCellContent(row) as TextBlock;
                TextBlock cellContentID = DGridClients.Columns[0].GetCellContent(row) as TextBlock;
                if ((cellContentTeleph != null && cellContentTeleph.Text.ToLower().Trim().Equals(param.ToLower()))
                    || (cellContentID != null && cellContentID.Text.ToLower().Trim().Equals(param.ToLower())))
                {
                    object item = DGridClients.Items[i];
                    DGridClients.SelectedItem = item;
                    DGridClients.ScrollIntoView(item);
                    row.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                    break;
                }
            }
        }

    }
}
