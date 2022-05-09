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
        }
        public static string Log { get; set; }
        public static string Pass { get; set;}
        public static string PassConf{ get; set;}
        public static string Surn { get; set;}
        public static string N { get; set;}
        public static string Patron { get; set;}
        public static DateTime Date { get; set;}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string log = login.Text;
            string pass = password.Text;
            string passConf = passwordConfirmation.Text;
            string surn = surname.Text;
            string n = name.Text;
            string patron=patronymic.Text;
            DateTime date = new DateTime();
            DateTime.TryParse(bday.Text, out date);

            //цифры, строчные буквы, символы - и _, длина 3-16 знаков
            string patternLogin = @"^[a-zA-Zа-яА-Я0-9_-]{3,16}$";
            //одна буква маленькая, одна большая, одна цифра, один любой знак(ни цифра/буква)
            string patternPassword = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9])\S{8,12}$";
            string patternName= @"^([a-zA-ZА-Яа-я])+$";
            //Windows.notificationWindow window = new Windows.notificationWindow();

            /* if (.Name.|| User.Email == "Email" || User.Login == "Login" || User.Password == "Password" || ConfirmPassword == "Confirm password")
             {
                 window.Show();
                 window.notificationText.Text = "Please fill in the fields!";
             }*/
            using (CAR_RENTEntities db = new CAR_RENTEntities())
            {
                CLIENT client = db.CLIENTS.FirstOrDefault(u => u.LOGIN == log);
                if (client != null)
                {
                    MessageBox.Show("Пользователь с таким логином уже существует. Попробуйте снова!");
                }
                else
                {
                    if (Regex.IsMatch(log, patternLogin, RegexOptions.IgnoreCase)
                        && Regex.IsMatch(pass, patternPassword, RegexOptions.IgnoreCase)
                        && Regex.IsMatch(passConf, patternPassword, RegexOptions.IgnoreCase)
                        && Regex.IsMatch(surn, patternName, RegexOptions.IgnoreCase)
                        && Regex.IsMatch(n, patternName, RegexOptions.IgnoreCase)
                        && Regex.IsMatch(patron, patternName, RegexOptions.IgnoreCase)
                        && bday.Text!="Выбор даты"&& bday.Text != ""
                        &&pass==passConf)
                  
                    {
                        Log = log;
                        Pass= pass;
                        PassConf= passConf;
                        Surn= surn;
                        N = n;
                        Patron= patron;
                        Date = date;
                        this.NavigationService.Navigate(new Uri("/AuthorizationPageDataInfo", UriKind.Relative));
                        AuthorizationPageDataInfo authorizationPageDataInfo = new AuthorizationPageDataInfo();
                        NavigationService.Navigate(authorizationPageDataInfo); 
                    }
                    else
                    {
                        MessageBox.Show("Заполните все поля!");

                    }
                }
            }

        }

        private void login_GotFocus(object sender, RoutedEventArgs e)
        {
            loginToolTip.Visibility = Visibility.Visible;
        }

        private void login_LostFocus(object sender, RoutedEventArgs e)
        {
            loginToolTip.Visibility = Visibility.Hidden;
        }

        private void password_GotFocus(object sender, RoutedEventArgs e)
        {
            passwordToolTip.Visibility = Visibility.Visible;
        }

        private void password_LostFocus(object sender, RoutedEventArgs e)
        {
            passwordToolTip.Visibility = Visibility.Hidden;
        }

        private void passwordConfirmation_GotFocus(object sender, RoutedEventArgs e)
        {
            passwordConfToolTip.Visibility = Visibility.Visible;
        }

        private void passwordConfirmation_LostFocus(object sender, RoutedEventArgs e)
        {
            passwordConfToolTip.Visibility = Visibility.Hidden;
        }

        private void login_SelectionChanged(object sender, RoutedEventArgs e)
        {
            string patternLogin = @"^[a-zA-Zа-яА-Я0-9_-]{3,16}$";
            string log = login.Text;
            if (Regex.IsMatch(log, patternLogin, RegexOptions.IgnoreCase))
            {
                logDone.Visibility = Visibility.Visible;
            }
            else
            {
                logDone.Visibility = Visibility.Hidden;
            }
        }

        private void password_SelectionChanged(object sender, RoutedEventArgs e)
        {
            string patternPassword = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9])\S{8,12}$";
            string pass = password.Text;
            if (Regex.IsMatch(pass, patternPassword, RegexOptions.IgnoreCase))
            {
                passDone.Visibility = Visibility.Visible;
            }
            else
            {
                passDone.Visibility = Visibility.Hidden;
            }

        }

        private void passwordConfirmation_SelectionChanged(object sender, RoutedEventArgs e)
        {
            string pass = password.Text;
            string passConf = passwordConfirmation.Text;
            if (pass==passConf)
            {
                passConfDone.Visibility = Visibility.Visible;
            }
            else 
            { 
                passConfDone.Visibility = Visibility.Hidden;
            }

        }
    }
}
