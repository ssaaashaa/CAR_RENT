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
            try
            {
            id.Text = App.currentClient.ID.ToString().Trim();
            login.Text = App.currentClient.LOGIN.Trim();
            password.Text = App.currentClient.PASSWORD.Trim();
            surname.Text = App.currentClient.SURNAME.Trim();
            name.Text = App.currentClient.NAME.Trim();
            patronymic.Text = App.currentClient.PATRONYMIC.Trim();
            bday.Text = App.currentClient.BDAY.ToString().Trim();
            telephone.Text = App.currentClient.TELEPHONE.Trim();
            passportID.Text = App.currentClient.PASSPORT.ToString().Trim();
            licenseID.Text = App.currentClient.DRIVER_LICENSE_ID.ToString().Trim();
            experience.Text = App.currentClient.DRIVING_EXPERIENCE.Trim();
            login.PreviewTextInput += new TextCompositionEventHandler(loginText);
            surname.PreviewTextInput += new TextCompositionEventHandler(letters);
            name.PreviewTextInput += new TextCompositionEventHandler(letters);
            patronymic.PreviewTextInput += new TextCompositionEventHandler(letters);
            bday.PreviewTextInput+= new TextCompositionEventHandler(date);
            telephone.PreviewTextInput+= new TextCompositionEventHandler(numbers);
            passportID.PreviewTextInput += new TextCompositionEventHandler(lettersAndNumbers);
            licenseID.PreviewTextInput += new TextCompositionEventHandler(lettersAndNumbers);
            experience.PreviewTextInput += new TextCompositionEventHandler(experienceText);
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
        private void Login_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                string patternLogin = @"^[a-zA-Zа-яА-Я0-9_-]{3,16}$"; 
                using (CAR_RENTEntities db = new CAR_RENTEntities())
            {
                    CLIENT client = db.CLIENTS.FirstOrDefault(u => u.ID.ToString().Trim() == id.Text.ToString().Trim());
                    CLIENT clientLogin = db.CLIENTS.FirstOrDefault(u => u.LOGIN.Trim() == login.Text.Trim());
                    if (login.Text.Length != 0 && clientLogin == null
                    && Regex.IsMatch(login.Text.Trim(), patternLogin))
                    {
                        client.LOGIN = login.Text.Trim();
                        db.Entry(client).State = EntityState.Modified;
                        db.SaveChanges();
                        App.currentClient = client;
                        loginMessage.Text = "Данные сохранены!";
                    }
                    if(App.currentClient.LOGIN.Trim() == login.Text.Trim())
                    {
                        loginMessage.Text = "Данные сохранены!";
                    }
                    else
                    {
                        if(login.Text.Length == 0)
                        {
                            loginMessage.Text = "Поле с логином должно быть заполнено!";
                        }
                        if (clientLogin != null && Regex.IsMatch(login.Text.Trim(), patternLogin) && App.currentClient.LOGIN.Trim() != login.Text.Trim())
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
                    CLIENT client = db.CLIENTS.FirstOrDefault(u => u.ID.ToString().Trim() == id.Text.ToString().Trim());
                    if (surname.Text.Length != 0
                    && Regex.IsMatch(surname.Text.Trim(), patternName))
                    {
                        client.SURNAME = surname.Text.Trim();
                        db.Entry(client).State = EntityState.Modified;
                        db.SaveChanges();
                        App.currentClient = client;
                        surnameMessage.Text = "Данные сохранены!";
                    }
                    if(surname.Text.Length == 0)
                    {
                        surnameMessage.Text = "Поле с фамилией должно быть заполнено!";
                    }
                    if (!Regex.IsMatch(surname.Text.Trim(), patternName) && surname.Text.Length != 0)
                    {
                        surnameMessage.Text = "Ввод с большой буквы и кириллицей";
                    }

                }
            }
            catch { }
        }
        private void Name_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string patternName = @"^[А-Я][а-я'-]+$";
                using (CAR_RENTEntities db = new CAR_RENTEntities())
                {
                    CLIENT client = db.CLIENTS.FirstOrDefault(u => u.ID.ToString().Trim() == id.Text.ToString().Trim());
                    if (name.Text.Length != 0
                    && Regex.IsMatch(name.Text.Trim(), patternName))
                    {
                        client.NAME = name.Text.Trim();
                        db.Entry(client).State = EntityState.Modified;
                        db.SaveChanges();
                        App.currentClient = client;
                        nameMessage.Text = "Данные сохранены!";
                    }
                    if (name.Text.Length == 0)
                    {
                        nameMessage.Text = "Поле с именем должно быть заполнено!";
                    }
                    if (!Regex.IsMatch(surname.Text.Trim(), patternName) && surname.Text.Length != 0)
                    {
                        nameMessage.Text = "Ввод с большой буквы и кириллицей";
                    }

                }
            }
            catch { }
        }
        private void Patronymic_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string patternName = @"^[А-Я][а-я'-]+$";
                using (CAR_RENTEntities db = new CAR_RENTEntities())
                {
                    CLIENT client = db.CLIENTS.FirstOrDefault(u => u.ID.ToString().Trim() == id.Text.ToString().Trim());
                    if (patronymic.Text.Length != 0
                    && Regex.IsMatch(patronymic.Text.Trim(), patternName))
                    {
                        client.PATRONYMIC = patronymic.Text.Trim();
                        db.Entry(client).State = EntityState.Modified;
                        db.SaveChanges();
                        App.currentClient = client;
                        patronymicMessage.Text = "Данные сохранены!";
                    }
                    if (patronymic.Text.Length == 0)
                    {
                        patronymicMessage.Text = "Поле с именем должно быть заполнено!";
                    }
                    if (!Regex.IsMatch(surname.Text.Trim(), patternName) && surname.Text.Length != 0)
                    {
                        patronymicMessage.Text = "Ввод с большой буквы и кириллицей";
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
                DateTime.TryParse(bday.Text.Trim(), out date);
                DateTime today = DateTime.Today;
                using (CAR_RENTEntities db = new CAR_RENTEntities())
                {
                    CLIENT client = db.CLIENTS.FirstOrDefault(u => u.ID.ToString() == id.Text.ToString());
                    if (bday.Text.Length != 0 && (today.Year - date.Year) >= 18) 
                    {
                        client.BDAY = Convert.ToDateTime(bday.Text.Trim());
                        db.Entry(client).State = EntityState.Modified;
                        db.SaveChanges();
                        bdayMessage.Text = "Данные сохранены!";
                        App.currentClient = client;
                    }
                    if (App.currentClient.BDAY.ToString().Remove(10) == bday.Text)
                    {
                        bdayMessage.Text = "Данные сохранены!";
                    }
                    else
                    {
                        if ((today.Year - date.Year) <= 18)
                        {
                           bdayMessage.Text= "Извините! Вам нет 18 лет! Мы не сможем предоставить вам автомобиль!";
                        }
                        if ((today.Year - date.Year) >= 90)
                        {
                            bdayMessage.Text = "Извините! Некорректно введенные данные!";
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
                    CLIENT client = db.CLIENTS.FirstOrDefault(u => u.ID.ToString().Trim() == id.Text.ToString().Trim());
                    CLIENT clientTelephone = db.CLIENTS.FirstOrDefault(u => u.TELEPHONE.Trim() == telephone.Text.Trim());
                    if (Regex.IsMatch(telephone.Text.Trim(), patternTelephone) && clientTelephone==null)
                    {
                        client.TELEPHONE = telephone.Text.Trim();
                        db.Entry(client).State = EntityState.Modified;
                        db.SaveChanges();
                        App.currentClient = client;
                        telephoneMessage.Text = "Данные сохранены!";

                    }
                    if(App.currentClient.TELEPHONE.Trim() == telephone.Text.Trim())
                    {
                        telephoneMessage.Text = "Данные сохранены!";
                    }
                    else
                    {
                       if (!Regex.IsMatch(telephone.Text.Trim(), patternTelephone, RegexOptions.IgnoreCase))
                        {
                            telephoneMessage.Text = "Введите корректный номер телефона!";
                        }
                        else
                        {
                            if (clientTelephone != null && App.currentClient.TELEPHONE.Trim() != telephone.Text.Trim())
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
                    string patternPassport = @"^([A-Z]{2}[0-9]{7})$";
                    string pass = passportID.Text.Trim();
                    CLIENT client = db.CLIENTS.FirstOrDefault(u => u.ID.ToString().Trim() == id.Text.ToString().Trim());
                    CLIENT clientPassport = db.CLIENTS.FirstOrDefault(u => u.PASSPORT.Trim() == passportID.Text.Trim());
                    if (clientPassport == null && passportID.Text.Length == 9 && Regex.IsMatch(pass, patternPassport)
                        )
                    {
                        client.PASSPORT = passportID.Text.Trim();
                        db.Entry(client).State = EntityState.Modified;
                        db.SaveChanges();
                        App.currentClient = client;
                        passportMessage.Text = "Данные сохранены!";

                    }
                    if (App.currentClient.PASSPORT.Trim() == passportID.Text.Trim())
                    {
                        passportMessage.Text = "Данные сохранены!";
                    }
                    else
                    {
                      
                        if (!Regex.IsMatch(pass, patternPassport))
                        {
                            passportMessage.Text = "Введите корректную серию и номер паспорта! Например, KH 7842563";
                        }
                       
                        if (clientPassport != null && App.currentClient.PASSPORT.Trim() != passportID.Text.Trim())
                        {
                            licenseMessage.Text = "Пользователь с таким номером паспорта уже существует!";

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
                    string patternLicense = @"^([0-9]{1}[A-Z]{2}\s{1}[0-9]{6,7})$";
                    string license = licenseID.Text.Trim();
                    CLIENT client = db.CLIENTS.FirstOrDefault(u => u.ID.ToString() == id.Text.ToString());
                    CLIENT clientLicense = db.CLIENTS.FirstOrDefault(u => u.DRIVER_LICENSE_ID.Trim() == licenseID.Text.Trim());
                    if ((licenseID.Text.Length == 10 || licenseID.Text.Length == 11) && clientLicense==null && Regex.IsMatch(license, patternLicense)
                        )
                    {
                        client.DRIVER_LICENSE_ID = licenseID.Text.Trim();
                        db.Entry(client).State = EntityState.Modified;
                        db.SaveChanges();
                        App.currentClient = client;
                        licenseMessage.Text = "Данные сохранены!";

                    }
                    if (App.currentClient.DRIVER_LICENSE_ID.Trim() == licenseID.Text.Trim())
                    {
                        licenseMessage.Text = "Данные сохранены!";
                    }
                    else
                    {
                        
                        if (!Regex.IsMatch(license, patternLicense))
                        {
                            licenseMessage.Text = "Введите корректный номер ВУ! Например, 4AC 875966.";
                        }
                        if (clientLicense != null && App.currentClient.DRIVER_LICENSE_ID.Trim() != licenseID.Text.Trim())
                        {
                            licenseMessage.Text = "Пользователь с таким номером ВУ уже существует!";

                        }
                        if (licenseID.Text.Length == 0)
                        {
                            licenseMessage.Text = "Поле с номером ВУ должно быть заполнено!";
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
                    CLIENT client = db.CLIENTS.FirstOrDefault(u => u.ID.ToString().Trim() == id.Text.ToString().Trim());
                    if (experience.Text.Length != 0)
                    {
                        client.DRIVING_EXPERIENCE = experience.Text.Trim();
                        db.Entry(client).State = EntityState.Modified;
                        db.SaveChanges();
                        App.currentClient = client;
                        experienceMessage.Text = "Данные сохранены!";

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
                string patternPassword = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])[A-Za-z0-9]{8,12}$";
                using (CAR_RENTEntities db = new CAR_RENTEntities())
                {
                    CLIENT client = db.CLIENTS.FirstOrDefault(u => u.ID.ToString().Trim() == id.Text.ToString().Trim());
                    if (Regex.IsMatch(password.Text.Trim(), patternPassword) && password.Text.Trim() == passwordConfirmation.Text.Trim())
                    {
                        client.PASSWORD = password.Text.Trim();
                        db.Entry(client).State = EntityState.Modified;
                        db.SaveChanges();
                        App.currentClient = client;
                        passwordConfMessage.Text = "Данные сохранены!";
                    }

                    else
                    {
                        
                        
                        if(!Regex.IsMatch(password.Text.Trim(), patternPassword))
                        {
                            passwordMessage.Text = "Пароль должен состоять из 8-12 символов и включать хотя бы одну большую и маленькую латинские буквы, цифру!";
                        }
                        if (Regex.IsMatch(password.Text.Trim(), patternPassword))
                        {
                            passwordMessage.Text = "";
                        }
                        if (password.Text.Trim() != passwordConfirmation.Text.Trim())
                        {
                            passwordConfMessage.Text = "Пароли не совпадают!";
                        }
                        if (password.Text.Trim() == passwordConfirmation.Text.Trim() && !Regex.IsMatch(password.Text.Trim(), patternPassword))
                        {
                            passwordMessage.Text = "Пароль должен состоять из 8-14 символов и включать хотя бы одну большую и маленькую латинские буквы, цифру!";
                            passwordConfMessage.Text = "";

                        }
                        if (passwordConfirmation.Text.Length == 0)
                        {
                            passwordConfMessage.Text = "Поле должно быть заполнено!";

                        }
                        if (password.Text.Length == 0)
                        {
                            passwordMessage.Text = "Поле должно быть заполнено!";
                        }
                    }

                }
            }
            catch { }
        }


    }
}
