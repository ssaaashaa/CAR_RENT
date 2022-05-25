using CAR_RENT.models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace CAR_RENT.pages
{
    /// <summary>
    /// Логика взаимодействия для UserInfoPage.xaml
    /// </summary>
    public partial class UserInfoPage : Page
    {
        private CLIENT currentClient = new CLIENT();
        public UserInfoPage()
        {
            InitializeComponent();
            id.Text = App.currentClient.ID.ToString();
            login.Text = App.currentClient.LOGIN;
            password.Text = App.currentClient.PASSWORD;
            surname.Text = App.currentClient.SURNAME;
            name.Text = App.currentClient.NAME;
            patronymic.Text = App.currentClient.PATRONYMIC;
            bday.Text = App.currentClient.BDAY.ToString();
            telephone.Text = App.currentClient.TELEPHONE;
            passportID.Text = App.currentClient.PASSPORT_ID.ToString();
            licenseID.Text = App.currentClient.DRIVER_LICENSE_ID.ToString();
            experience.Text = App.currentClient.DRIVING_EXPERIENCE;
            login.PreviewTextInput += new TextCompositionEventHandler(loginText);
            surname.PreviewTextInput += new TextCompositionEventHandler(letters);
            name.PreviewTextInput += new TextCompositionEventHandler(letters);
            patronymic.PreviewTextInput += new TextCompositionEventHandler(letters);
            bday.PreviewTextInput+= new TextCompositionEventHandler(date);
            telephone.PreviewTextInput+= new TextCompositionEventHandler(numbers);
            passportID.PreviewTextInput += new TextCompositionEventHandler(numbers);
            licenseID.PreviewTextInput += new TextCompositionEventHandler(lettersAndNumbers);
            experience.PreviewTextInput += new TextCompositionEventHandler(experienceText);            
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
            } catch { }
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
        private void experienceText(object sender, TextCompositionEventArgs e)
        {
            try { 
            if (!Char.IsLetterOrDigit(e.Text, 0) && e.Text != "." && e.Text != ",")
            {
                e.Handled = true; //не обрабатывать введеный символ
            }
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

        private void Login_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                string patternLogin = @"^[a-zA-Zа-яА-Я0-9_-]{3,16}$"; 
                using (CAR_RENTEntities db = new CAR_RENTEntities())
            {
                    CLIENT client = db.CLIENTS.FirstOrDefault(u => u.ID.ToString() == id.Text.ToString());
                    CLIENT clientLogin = db.CLIENTS.FirstOrDefault(u => u.LOGIN == login.Text);
                    if (login.Text.Length != 0
                    && Regex.IsMatch(login.Text, patternLogin) && clientLogin==null)
                    {
                        client.LOGIN = login.Text;
                        db.Entry(client).State = EntityState.Modified;
                        db.SaveChanges();
                        App.currentClient = client;
                        loginMessage.Text = "Данные изменены!";
                    }
                    else
                    {
                        if(login.Text.Length == 0)
                        {
                            loginMessage.Text = "Поле с логином должно быть заполнено!";
                        }
                        if (clientLogin != null && Regex.IsMatch(login.Text, patternLogin))
                        {
                            loginMessage.Text = "Пользователь с таким логином уже существует!";
                        }
                        if (!Regex.IsMatch(login.Text, patternLogin) && login.Text.Length!=0)
                        {
                            loginMessage.Text = "Логин может содержать цифры, буквы, символы  - и _, длина от 3 до 16 символов";
                        }
                    }

            }
            }
            catch { }
        }

        private void Surname_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string patternName = @"^[А-Я][а-я'-]+$";
                using (CAR_RENTEntities db = new CAR_RENTEntities())
                {
                    CLIENT client = db.CLIENTS.FirstOrDefault(u => u.ID.ToString() == id.Text.ToString());
                    if (surname.Text.Length != 0
                    && Regex.IsMatch(surname.Text, patternName))
                    {
                        client.SURNAME = surname.Text;
                        db.Entry(client).State = EntityState.Modified;
                        db.SaveChanges();
                        App.currentClient = client;
                        surnameMessage.Text = "Данные изменены!";
                    }
                    if(surname.Text.Length == 0)
                    {
                        surnameMessage.Text = "Поле с фамилией должно быть заполнено!";
                    }
                    if (!Regex.IsMatch(surname.Text, patternName) && surname.Text.Length != 0)
                    {
                        surnameMessage.Text = "Ввод с большой буквы и кириллицей";
                    }

                }
            }
            catch { }
        }

        private void Bday_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime date = new DateTime();
                DateTime.TryParse(bday.Text, out date);
                DateTime today = DateTime.Today;
                using (CAR_RENTEntities db = new CAR_RENTEntities())
                {
                    CLIENT client = db.CLIENTS.FirstOrDefault(u => u.ID.ToString() == id.Text.ToString());
                    if (bday.Text.Length != 0 && (today.Year - date.Year) >= 18)
                    {
                        client.BDAY = Convert.ToDateTime(bday.Text);
                        db.Entry(client).State = EntityState.Modified;
                        db.SaveChanges();
                        bdayMessage.Text = "Данные изменены!";
                        App.currentClient = client;
                    }
                    else
                    {
                        if ((today.Year - date.Year) <= 18)
                        {
                           bdayMessage.Text= "Извините! Вам нет 18 лет! Мы не сможем предоставить вам автомобиль!";
                        }
                        if (bday.Text.Length == 0)
                        {
                            bdayMessage.Text = "Поле с датой рождения должно быть заполнено!";
                        }
                    }
                }
            }
            catch { }
        }

        private void Telephone_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                string patternTelephone = @"^\+375 \((25|29|33|44)\) [0-9]{3}-[0-9]{2}-[0-9]{2}$";
                using (CAR_RENTEntities db = new CAR_RENTEntities())
                {
                    CLIENT client = db.CLIENTS.FirstOrDefault(u => u.ID.ToString() == id.Text.ToString());
                    CLIENT clientTelephone = db.CLIENTS.FirstOrDefault(u => u.TELEPHONE == telephone.Text);
                    if (Regex.IsMatch(telephone.Text, patternTelephone, RegexOptions.IgnoreCase) && clientTelephone==null)
                    {
                        client.TELEPHONE = telephone.Text;
                        db.Entry(client).State = EntityState.Modified;
                        db.SaveChanges();
                        App.currentClient = client;
                        telephoneMessage.Text = "Данные изменены!";

                    }
                    else
                    {
                       if (!Regex.IsMatch(telephone.Text, patternTelephone, RegexOptions.IgnoreCase))
                        {
                            telephoneMessage.Text = "Введите корректный номер телефона!";
                        }
                        else
                        {
                            if (clientTelephone != null)
                            {
                                telephoneMessage.Text = "Пользователь с таким номером телефона уже существует!";
                            }
                        }
                    }

                }
            }
            catch { }
        }

        private void PassportNum_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (CAR_RENTEntities db = new CAR_RENTEntities())
                {
                    CLIENT client = db.CLIENTS.FirstOrDefault(u => u.ID.ToString() == id.Text.ToString());
                    CLIENT clientPassport = db.CLIENTS.FirstOrDefault(u => u.PASSPORT_SERIES == passportID.Text);
                    if (passportID.Text.Length == 10)
                    {
                        client.PASSPORT_SERIES = passportID.Text;
                        db.Entry(client).State = EntityState.Modified;
                        db.SaveChanges();
                        App.currentClient = client;
                        passportMessage.Text = "Данные изменены!";

                    }
                    else
                    {
                      
                        if (passportID.Text.Length != 7)
                        {
                            passportMessage.Text = "Введите корректную серию и номер паспорта! Например, KH 7842563";
                        }
                        if (passportID.Text.Length == 0)
                        {
                            passportMessage.Text = "Поле с серией и номером паспорта должно быть заполнено!";
                        }

                    }
                }
            }
            catch { }
        }

        private void License_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (CAR_RENTEntities db = new CAR_RENTEntities())
                {
                    CLIENT client = db.CLIENTS.FirstOrDefault(u => u.ID.ToString() == id.Text.ToString());
                    CLIENT clientLicense = db.CLIENTS.FirstOrDefault(u => u.DRIVER_LICENSE_ID == licenseID.Text);
                    if (licenseID.Text.Length == 10 && licenseID.Text.Length == 11 && clientLicense==null)
                    {
                        client.DRIVER_LICENSE_ID = licenseID.Text;
                        db.Entry(client).State = EntityState.Modified;
                        db.SaveChanges();
                        App.currentClient = client;
                        licenseMessage.Text = "Данные изменены!";

                    }
                    else
                    {
                        if (licenseID.Text.Length == 0)
                        {
                            licenseMessage.Text = "Поле с номером ВУ должно быть заполнено!";
                        }
                        if (licenseID.Text.Length != 10 && licenseID.Text.Length != 11)
                        {
                            licenseMessage.Text = "Введите корректный номер ВУ! Например, 4AC 875966.";
                        }
                        if (clientLicense != null)
                        {
                            licenseMessage.Text = "Пользователь с таким номером ВУ уже существует!";

                        }
                    }

                }
            }
            catch { }
        }

        private void Experience_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (CAR_RENTEntities db = new CAR_RENTEntities())
                {
                    CLIENT client = db.CLIENTS.FirstOrDefault(u => u.ID.ToString() == id.Text.ToString());
                    if (experience.Text.Length != 0)
                    {
                        client.DRIVING_EXPERIENCE = experience.Text;
                        db.Entry(client).State = EntityState.Modified;
                        db.SaveChanges();
                        App.currentClient = client;
                        experienceMessage.Text = "Данные изменены!";

                    }
                    else
                    {
                        if (experience.Text.Length == 0)
                        {
                            licenseMessage.Text = "Поле с опытом вождения должно быть заполнено!";

                        }
                    }

                }
            }
            catch { }
        }

        private void Password_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string patternPassword = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])[A-Za-z0-9]{8,14}$";
                using (CAR_RENTEntities db = new CAR_RENTEntities())
                {
                    CLIENT client = db.CLIENTS.FirstOrDefault(u => u.ID.ToString() == id.Text.ToString());
                    if (Regex.IsMatch(password.Text, patternPassword) && password.Text == passwordConfirmation.Text)
                    {
                        client.PASSWORD = password.Text;
                        db.Entry(client).State = EntityState.Modified;
                        db.SaveChanges();
                        App.currentClient = client;
                        passwordConfMessage.Text = "Данные изменены!";
                    }
                    else
                    {
                        if(password.Text.Length == 0)
                        {
                            passwordMessage.Text = "Поле должно быть заполнено!";
                        }
                        if(passwordConfirmation.Text.Length == 0)
                        {
                            passwordConfMessage.Text = "Поле должно быть заполнено!";

                        }
                        if(!Regex.IsMatch(password.Text, patternPassword))
                        {
                            passwordMessage.Text = "Пароль должен состоять из 8-14 символов и включать хотя бы одну большую и маленькую латинские буквы, цифру!";
                        }
                        if (Regex.IsMatch(password.Text, patternPassword))
                        {
                            passwordMessage.Text = "";
                        }
                        if (password.Text != passwordConfirmation.Text)
                        {
                            passwordConfMessage.Text = "Пароли не совпадают!";
                        }
                        if (password.Text == passwordConfirmation.Text && !Regex.IsMatch(password.Text, patternPassword))
                        {
                            passwordMessage.Text = "Пароль должен состоять из 8-14 символов и включать хотя бы одну большую и маленькую латинские буквы, цифру!";
                            passwordConfMessage.Text = "";

                        }
                    }

                }
            }
            catch { }
        }
    }
}
