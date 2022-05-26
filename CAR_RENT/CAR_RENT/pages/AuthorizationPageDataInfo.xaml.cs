using CAR_RENT.models;
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

namespace CAR_RENT.pages
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPageDataInfo.xaml
    /// </summary>
    public partial class AuthorizationPageDataInfo : Page
    {
        string login;
        string password;
        string surname;
        string name;
        string patronymic;
        public AuthorizationPageDataInfo(string log, string pass, string surn, string n, string patron)
        {
            InitializeComponent();
            bday.PreviewTextInput += new TextCompositionEventHandler(date);
            telephone.PreviewTextInput += new TextCompositionEventHandler(telephoneTextInput);
            passport.PreviewTextInput += new TextCompositionEventHandler(lettersAndNumbers);
            licenseID.PreviewTextInput += new TextCompositionEventHandler(lettersAndNumbers);
            experience.PreviewTextInput += new TextCompositionEventHandler(experienceInput);
            login = log;
            password = pass;
            surname = surn;
            name = n;
            patronymic = patron;
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
        void telephoneTextInput(object sender, TextCompositionEventArgs e)
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
        void experienceInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                if (!Char.IsLetterOrDigit(e.Text, 0) && e.Text != "," && e.Text != ".")
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (CAR_RENTEntities db = new CAR_RENTEntities())
                {
                    DateTime date = new DateTime();
                    DateTime.TryParse(bday.Text.Trim(), out date);
                    CLIENT client = new CLIENT();
                    client.LOGIN = login;
                    client.PASSWORD = password;
                    client.SURNAME = surname;
                    client.NAME = name;
                    client.PATRONYMIC = patronymic;
                    client.BDAY = date;
                    client.TELEPHONE = telephone.Text.Trim();
                    client.PASSPORT = passport.Text;
                    client.DRIVER_LICENSE_ID = licenseID.Text.Trim();
                    client.DRIVING_EXPERIENCE = experience.Text.Trim();

                    if (telephDone.Visibility == Visibility.Visible
                        && bdayDone.Visibility == Visibility.Visible
                        && passportDone.Visibility == Visibility.Visible
                        && numberDone.Visibility == Visibility.Visible
                        && numDone.Visibility == Visibility.Visible)
                    {
                        db.CLIENTS.Add(client);
                        db.SaveChanges();
                        App.currentClient = client;
                        AuthorizattionIsDone authorizattionIsDone = new AuthorizattionIsDone();
                        NavigationService.Navigate(authorizattionIsDone);
                    }
                    else
                    {
                        if (passport.Text != "")
                        {
                            MessageBox.Show("Необходимо заполнить все поля!");
                        }
                    }
                }


            }
            catch { }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try 
            { 
            this.NavigationService.GoBack();
            }
            catch{ }
        }

        private void experience_SelectionChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                if (experience.Text.Trim() != "")
                {
                    numDone.Visibility = Visibility.Visible;
                }
                else
                {
                    numDone.Visibility = Visibility.Hidden;
                }
            }
            catch { }
        }
        private void bday_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DateTime date = new DateTime();
                DateTime.TryParse(bday.Text.Trim(), out date);
                DateTime today = DateTime.Today;
                if ((today.Year - date.Year) <= 18)
                {
                    MessageBox.Show("Извините! Вам нет 18 лет! Мы не сможем предоставить вам автомобиль!");
                    return;
                }
                if ((today.Year - date.Year) >= 90)
                {
                    MessageBox.Show("Извините! Некорректно введенные данные!");
                    return;
                }
                else
                {
                    if (bday.Text.Trim() != null && bday.Text.Trim().Length == 10)
                    {
                        bdayDone.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        bdayDone.Visibility = Visibility.Hidden;
                    }
                }

            }
            catch { }

        }

        private void passportfToolTip_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                passportfToolTip.Visibility = Visibility.Visible;
            }
            catch { }
        }

        private void passportfToolTip_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                passportfToolTip.Visibility = Visibility.Hidden;
            }
            catch { }
        }
        private void licenseMessage_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                licenseToolTip.Visibility = Visibility.Visible;
            }
            catch { }
        }

        private void licenseMessage_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                licenseToolTip.Visibility = Visibility.Hidden;
            }
            catch { }
        }
        private void telephone_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                using (CAR_RENTEntities db = new CAR_RENTEntities())
                {
                    string patternTelephone = @"^\+375 \((25|29|33|44)\) [0-9]{3}-[0-9]{2}-[0-9]{2}$";
                    string teleph = telephone.Text.Trim();
                    CLIENT clientTelephone = db.CLIENTS.FirstOrDefault(u => u.TELEPHONE.Trim() == telephone.Text.Trim());
                    if (clientTelephone != null)
                    {
                        telephMessage.Visibility = Visibility.Visible;
                    }
                    else
                    {

                        if (Regex.IsMatch(teleph, patternTelephone) && clientTelephone == null)
                        {
                            telephDone.Visibility = Visibility.Visible;
                            telephMessage.Visibility = Visibility.Hidden;

                        }
                        else
                        {
                            telephDone.Visibility = Visibility.Hidden;
                        }
                    }
                }
            }
            catch { }
        }

        private void passport_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                using (CAR_RENTEntities db = new CAR_RENTEntities())
                {
                    string patternPassport = @"^([A-Z]{2}[0-9]{7})$";
                    string pass = passport.Text.Trim();
                    CLIENT clientPassport = db.CLIENTS.FirstOrDefault(u => u.PASSPORT.Trim() == passport.Text.Trim());
                    if (clientPassport != null && passport.Text.Length == 9)
                    {
                        passportMessage.Visibility = Visibility.Visible;

                    }
                    else
                    {

                        if (Regex.IsMatch(pass, patternPassport) && clientPassport == null)
                        {
                            passportDone.Visibility = Visibility.Visible;
                            passportMessage.Visibility = Visibility.Hidden;
                        }
                        else
                        {
                            passportDone.Visibility = Visibility.Hidden;
                            passportMessage.Visibility = Visibility.Hidden;

                        }
                    }
                }
            }
            catch { }
        }

        private void licenseID_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                using (CAR_RENTEntities db = new CAR_RENTEntities())
                {
                    string patternLicense = @"^([0-9]{1}[A-Z]{2}\s{1}[0-9]{6,7})$";
                    string license = licenseID.Text.Trim();
                    CLIENT clientLicense = db.CLIENTS.FirstOrDefault(u => u.DRIVER_LICENSE_ID.Trim() == licenseID.Text.Trim());
                    if (clientLicense != null && (licenseID.Text.Length == 10 || licenseID.Text.Length == 9))
                    {
                        licenseMessage.Visibility = Visibility.Visible;

                    }
                    else
                    {

                        if (Regex.IsMatch(license, patternLicense) && clientLicense == null)
                        {
                            numberDone.Visibility = Visibility.Visible;
                            licenseMessage.Visibility = Visibility.Hidden;
                        }
                        else
                        {
                            numberDone.Visibility = Visibility.Hidden;
                            licenseMessage.Visibility = Visibility.Hidden;

                        }
                    }
                }
            }
            catch { }
        }

        
    }
}
