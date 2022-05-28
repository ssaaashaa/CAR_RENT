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
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
            login.PreviewTextInput += new TextCompositionEventHandler(loginText);
            password.PreviewTextInput += new TextCompositionEventHandler(lettersAndNumbers);
            passwordConfirmation.PreviewTextInput += new TextCompositionEventHandler(lettersAndNumbers);
            surname.PreviewTextInput += new TextCompositionEventHandler(letters);
            name.PreviewTextInput += new TextCompositionEventHandler(letters);
            patronymic.PreviewTextInput += new TextCompositionEventHandler(letters);
            

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
        private void Next(object sender, RoutedEventArgs e)
        {
            try
            {
                string log = login.Text.Trim();
                string pass = password.Password.Trim();
                string passConf = passwordConfirmation.Password.Trim();
                string surn = surname.Text.Trim();
                string n = name.Text.Trim();
                string patron = patronymic.Text.Trim();
                string patternLogin = @"^[a-zA-Zа-яА-Я0-9_-]{3,16}$";
                string patternPassword = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])[A-Za-z0-9]{8,12}$";
                string patternName = @"^[А-Я][а-я'-]+$";

                using (CAR_RENTEntities db = new CAR_RENTEntities())
                {
                    CLIENT client = db.CLIENTS.Where(u => u.LOGIN.Trim() == log).AsEnumerable().Where(u => u.LOGIN.Trim() == log).FirstOrDefault();
                    if (client != null)
                    {
                        busy.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        if (Regex.IsMatch(log, patternLogin)
                            && Regex.IsMatch(pass, patternPassword)
                            && Regex.IsMatch(passConf, patternPassword)
                            && Regex.IsMatch(surn, patternName)
                            && Regex.IsMatch(n, patternName)
                            && Regex.IsMatch(patron, patternName)
                            && logDone.Visibility == Visibility.Visible
                            && passDone.Visibility == Visibility.Visible
                            && passConfDone.Visibility == Visibility.Visible
                            && surnDone.Visibility == Visibility.Visible
                            && nDone.Visibility == Visibility.Visible
                            && patronDone.Visibility == Visibility.Visible)
                        {
                            this.NavigationService.Navigate(new AuthorizationPageDataInfo(log, pass, surn, n, patron));
                        }
                        else
                        {
                            if (pass != passConf)
                            {
                                notmatch.Visibility = Visibility.Visible;
                            }
                            else if (pass == passConf)
                            {
                                fillAll.Visibility = Visibility.Visible;
                            }


                        }
                    }
                }

            }
            catch { }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            try
            {
                this.NavigationService.GoBack();
            }
            catch { }
        }
        private void login_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                loginToolTip.Visibility = Visibility.Visible;
            }
            catch { }
        }

        private void login_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                loginToolTip.Visibility = Visibility.Hidden;
            }
            catch { }
        }

        private void password_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                passwordToolTip.Visibility = Visibility.Visible;
            }
            catch { }
        }

        private void password_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                passwordToolTip.Visibility = Visibility.Hidden;
            }
            catch { }
        }

        private void passwordConfirmation_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                passwordConfToolTip.Visibility = Visibility.Visible;
            }
            catch { }
        }

        private void passwordConfirmation_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                passwordConfToolTip.Visibility = Visibility.Hidden;
            }
            catch { }
        }
        private void name_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                nameConfToolTip.Visibility = Visibility.Visible;
            }
            catch { }
        }
        private void name_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                nameConfToolTip.Visibility = Visibility.Hidden;
            }
            catch { }
        }
        private void login_SelectionChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                string log = login.Text.Trim();
                using (CAR_RENTEntities db = new CAR_RENTEntities())
                {
                    CLIENT client = db.CLIENTS.Where(c => c.LOGIN.Trim() == log).FirstOrDefault();
                    string patternLogin = @"^[a-zA-Zа-яА-Я0-9_-]{3,16}$";
                    if (client != null)
                    {
                        busy.Visibility = Visibility.Visible;
                    }
                    if (client == null && Regex.IsMatch(log, patternLogin))
                    {
                        logDone.Visibility = Visibility.Visible;
                        busy.Visibility = Visibility.Hidden;
                    }

                    else
                    {
                        logDone.Visibility = Visibility.Hidden;
                    }
                }
            }
            catch { }

        }

        private void password_SelectionChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                string patternPassword = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])[A-Za-z0-9]{8,12}$";
                string pass = password.Password.Trim();
                if (Regex.IsMatch(pass, patternPassword))
                {
                    passDone.Visibility = Visibility.Visible;
                }
                else
                {
                    passDone.Visibility = Visibility.Hidden;
                }
            }
            catch { }
        }

        private void passwordConfirmation_SelectionChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                string pass = password.Password.Trim();
                string passConf = passwordConfirmation.Password.Trim();
                if (pass == passConf)
                {
                    passConfDone.Visibility = Visibility.Visible;
                    notmatch.Visibility = Visibility.Hidden;
                }
                if (passConf.Length == 0)
                {
                    passConfDone.Visibility = Visibility.Hidden;
                }
                else if (pass != passConf)
                {
                    notmatch.Visibility = Visibility.Visible;
                    passConfDone.Visibility = Visibility.Hidden;
                }
            }
            catch { }
        }




        private void surname_SelectionChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                string surn = surname.Text.Trim();
                string patternName = @"^[А-Я][а-я'-]+$";
                if (Regex.IsMatch(surn, patternName))
                {
                    surnDone.Visibility = Visibility.Visible;
                }
                else
                {
                    surnDone.Visibility = Visibility.Hidden;
                }
            }
            catch { }
        }

        private void name_SelectionChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                string n = name.Text.Trim();
                string patternName = @"^[А-Я][а-я'-]+$";
                if (Regex.IsMatch(n, patternName))
                {
                    nDone.Visibility = Visibility.Visible;
                }
                else
                {
                    nDone.Visibility = Visibility.Hidden;
                }
            }
            catch { }
        }

        private void patronymic_SelectionChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                string patron = patronymic.Text.Trim();
                string patternName = @"^[А-Я][а-я'-]+$";
                if (Regex.IsMatch(patron, patternName))
                {
                    patronDone.Visibility = Visibility.Visible;
                }
                else
                {
                    patronDone.Visibility = Visibility.Hidden;
                }
            }
            catch { }
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
               
            }
            catch { }
        }

        private void leftArrow_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
               
            }
            catch { }
        }

        private void next_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                NextPage.Width = 80;
            }
            catch { }
        }

        private void next_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                NextPage.Width = 70;
            }
            catch { }
        }
    }
}
