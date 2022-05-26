using CAR_RENT.models;
using System;
using System.Data.Entity;
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
        StringBuilder errors = new StringBuilder();
        public clientsPage()
        {
            InitializeComponent();
            try
            {
                DGridClients.ItemsSource = CAR_RENTEntities.GetContext().CLIENTS.ToList();
                LOGIN.PreviewTextInput += new TextCompositionEventHandler(loginText);
                PASSPORT.PreviewTextInput += new TextCompositionEventHandler(lettersAndNumbers);
                SURNAME.PreviewTextInput += new TextCompositionEventHandler(letters);
                NAME.PreviewTextInput += new TextCompositionEventHandler(letters);
                PATRONYMIC.PreviewTextInput += new TextCompositionEventHandler(letters);
                BDAY.PreviewTextInput += new TextCompositionEventHandler(date);
                TELEPHONE.PreviewTextInput += new TextCompositionEventHandler(numbers);
                PASSPORT.PreviewTextInput += new TextCompositionEventHandler(lettersAndNumbers);
                LICENSE_ID.PreviewTextInput += new TextCompositionEventHandler(lettersAndNumbers);
                EXPERIENCE.PreviewTextInput += new TextCompositionEventHandler(experienceText);
                TYPE.PreviewTextInput += new TextCompositionEventHandler(userType);
            }
            catch { }

        }
        private void loginText(object sender, TextCompositionEventArgs e)
        {
            try
            {
                if (!Char.IsLetterOrDigit(e.Text, 0) && e.Text != "-" && e.Text != "_")
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
                if (!Char.IsLetterOrDigit(e.Text, 0))
                {
                    e.Handled = true; //не обрабатывать введеный символ
                }
            }
            catch { }
        }
        void letters(object sender, TextCompositionEventArgs e)
        {
            try
            {
                if (!Char.IsLetter(e.Text, 0) && e.Text != "-" && e.Text != "'")
                {
                    e.Handled = true; //не обрабатывать введеный символ
                }
            }
            catch { }
        }
        private void date(object sender, TextCompositionEventArgs e)
        {
            try
            {
                if (!Char.IsDigit(e.Text, 0) && e.Text != "." && e.Text != "-" && e.Text != "/")
                {
                    e.Handled = true; //не обрабатывать введеный символ
                }
            }
            catch { }
        }
        void numbers(object sender, TextCompositionEventArgs e)
        {
            try
            {
                if (!Char.IsDigit(e.Text, 0))
                {
                    e.Handled = true; //не обрабатывать введеный символ
                }
            }
            catch { }
        }
        private void experienceText(object sender, TextCompositionEventArgs e)
        {
            try
            {
                if (!Char.IsLetterOrDigit(e.Text, 0) && e.Text != "." && e.Text != ",")
                {
                    e.Handled = true; //не обрабатывать введеный символ
                }
            }
            catch { }
        }
        private void userType(object sender, TextCompositionEventArgs e)
        {
            try
            {
                if (e.Text != "0" && e.Text != "1")
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
        private void Validation()
        {
            try
            {
               
                string patternLogin = @"^[a-zA-Zа-яА-Я0-9_-]{3,16}$";
                string patternName = @"^[А-Я][а-я'-]+$";
                string patternTelephone = @"^\+375 \((25|29|33|44)\) [0-9]{3}-[0-9]{2}-[0-9]{2}$";
                string patternPassport = @"^([A-Z]{2}[0-9]{7})$";
                string patternLicense = @"^([0-9]{1}[A-Z]{2}\s{1}[0-9]{6,7})$";
                string patternPassword = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])[A-Za-z0-9]{8,12}$";

                DateTime date = new DateTime();
                DateTime.TryParse(BDAY.Text.Trim(), out date);
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
                    errors.AppendLine("Пароль должен состоять из 8-12 символов и включать хотя бы одну большую и маленькую латинские буквы, цифру!");
                }
                if (string.IsNullOrEmpty(SURNAME.Text))
                {
                    errors.AppendLine("Введите фамилию!");
                }
                if (!Regex.IsMatch(SURNAME.Text, patternName, RegexOptions.IgnoreCase))
                {
                    errors.AppendLine("Ввод фамилии с большой буквы и кириллицей!");
                }
                if (string.IsNullOrEmpty(NAME.Text))
                {
                    errors.AppendLine("Введите имя!");
                }
                if (!Regex.IsMatch(NAME.Text, patternName, RegexOptions.IgnoreCase))
                {
                    errors.AppendLine("Ввод имени с большой буквы и кириллицей!");
                }
                if (string.IsNullOrEmpty(PATRONYMIC.Text))
                {
                    errors.AppendLine("Введите отчество!");
                }
                if (!Regex.IsMatch(PATRONYMIC.Text, patternName, RegexOptions.IgnoreCase))
                {
                    errors.AppendLine("Ввод отчества с большой буквы и кириллицей!");
                }
                if (string.IsNullOrEmpty(BDAY.Text))
                {
                    errors.AppendLine("Введите дату рождения!");
                }
                if ((today.Year - date.Year) <18)
                {
                    errors.AppendLine("Пользователь может быть только совершеннолетним!");
                }
                if ((today.Year - date.Year) >= 90)
                {
                    errors.AppendLine("Извините! Некорректно введенные данные!");
                }
                if (string.IsNullOrEmpty(PASSPORT.Text))
                {
                    errors.AppendLine("Введите номер и серию паспорта!");
                }
                if (!Regex.IsMatch(PASSPORT.Text, patternPassport, RegexOptions.IgnoreCase))
                {
                    errors.AppendLine("Введите корректную серию и номер паспорта! Например, KH 7842563");
                }
               
                if (string.IsNullOrEmpty(LICENSE_ID.Text))
                {
                    errors.AppendLine("Введите номер ВУ!");
                }
                if (!Regex.IsMatch(LICENSE_ID.Text, patternLicense, RegexOptions.IgnoreCase))
                {
                    errors.AppendLine("Введите корректный номер ВУ! Например, 4AC 875966.");
                }
               
                if (string.IsNullOrEmpty(EXPERIENCE.Text))
                {
                    errors.AppendLine("Введите опыт вождения!");
                }
                if (!Regex.IsMatch(TELEPHONE.Text, patternTelephone, RegexOptions.IgnoreCase))
                {
                    errors.AppendLine("Введите корректный номер телефона!");
                }
                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    return;
                }
            }
            catch { }

        }

        private void Clear()
        {
            try
            {
                LOGIN.Clear();
                PASSWORD.Clear();
                SURNAME.Clear();
                NAME.Clear();
                PATRONYMIC.Clear();
                BDAY.Clear();
                PASSPORT.Clear();
                LICENSE_ID.Clear();
                EXPERIENCE.Clear();
                TELEPHONE.Clear();
                TYPE.Clear();
            }
            catch { }

        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Validation();
                DateTime date = new DateTime();
                DateTime.TryParse(BDAY.Text, out date);
                CLIENT currentClient = new CLIENT();
                CLIENT client = CAR_RENTEntities.GetContext().CLIENTS.FirstOrDefault(u => u.LOGIN.Trim() == LOGIN.Text.Trim());
                currentClient.LOGIN = LOGIN.Text;
                currentClient.PASSWORD = PASSWORD.Text;
                currentClient.SURNAME = SURNAME.Text;
                currentClient.NAME = NAME.Text;
                currentClient.PATRONYMIC = PATRONYMIC.Text;
                currentClient.BDAY = date;
                currentClient.PASSPORT = PASSPORT.Text;
                currentClient.DRIVER_LICENSE_ID = LICENSE_ID.Text;
                currentClient.DRIVING_EXPERIENCE = EXPERIENCE.Text;
                currentClient.TELEPHONE = TELEPHONE.Text;
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

            catch { }


        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {
                Validation();
                DateTime date = new DateTime();
                DateTime.TryParse(BDAY.Text, out date);
                using (CAR_RENTEntities db = new CAR_RENTEntities())
                {
                CLIENT currentClient = CAR_RENTEntities.GetContext().CLIENTS.Where(u => u.ID.ToString() == ID.Text).FirstOrDefault();

                    if (currentClient != null && currentClient.LOGIN != LOGIN.Text.Trim())
                    {
                        errors.AppendLine("Пользователь с таким логином уже существует. Попробуйте снова!");
                    }
                    CLIENT clientPassport = CAR_RENTEntities.GetContext().CLIENTS.FirstOrDefault(u => u.PASSPORT.Trim() == PASSPORT.Text.Trim());
                if (clientPassport != null && currentClient.PASSPORT!=PASSPORT.Text.Trim())
                    {
                        errors.AppendLine("Пользователь с таким номером паспорта уже существует!");
                    }
                    CLIENT clientLicense = CAR_RENTEntities.GetContext().CLIENTS.FirstOrDefault(u => u.DRIVER_LICENSE_ID.Trim() == LICENSE_ID.Text.Trim());
                    if (clientLicense != null && currentClient.DRIVER_LICENSE_ID.Trim()!=LICENSE_ID.Text.Trim())
                    {
                        errors.AppendLine("Пользователь с таким номером ВУ уже существует!");
                    }
                    CLIENT clientTelephone = CAR_RENTEntities.GetContext().CLIENTS.FirstOrDefault(u => u.TELEPHONE.Trim() == TELEPHONE.Text.Trim());
                    if (clientTelephone != null && currentClient.TELEPHONE.Trim()!=TELEPHONE.Text.Trim())
                    {
                        errors.AppendLine("Пользователь с таким номером телефона уже существует!");
                    }
                   
                currentClient.LOGIN = LOGIN.Text;
                currentClient.PASSWORD = PASSWORD.Text;
                currentClient.SURNAME = SURNAME.Text;
                currentClient.NAME = NAME.Text;
                currentClient.PATRONYMIC = PATRONYMIC.Text;
                currentClient.BDAY = date;
                currentClient.PASSPORT = PASSPORT.Text;
                currentClient.DRIVER_LICENSE_ID = LICENSE_ID.Text;
                currentClient.DRIVING_EXPERIENCE = EXPERIENCE.Text;
                currentClient.TELEPHONE = TELEPHONE.Text;
                 db.Entry(currentClient).State = EntityState.Modified;
                    if (currentClient != null)
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


        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
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
            else if (LOGIN.Text != "" && currentClient == null)
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
            catch { }


        }




        private void DGridClients_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
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
                    PASSPORT.Text = selectedClient.PASSPORT;
                    LICENSE_ID.Text = selectedClient.DRIVER_LICENSE_ID;
                    EXPERIENCE.Text = selectedClient.DRIVING_EXPERIENCE;
                    TELEPHONE.Text = selectedClient.TELEPHONE;
                    TYPE.Text = selectedClient.USER_TYPE.ToString();
                    ID.Text = selectedClient.ID.ToString();
                }
                else
                {
                    MessageBox.Show("Запись не выбрана!");
                }
            }
            catch { }
           
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch { }
          
        }

    }
}
