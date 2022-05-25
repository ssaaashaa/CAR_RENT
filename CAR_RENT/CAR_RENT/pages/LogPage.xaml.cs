using CAR_RENT.models;
using CAR_RENT.windows;
using System;
using System.Collections.Generic;
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

namespace CAR_RENT.pages
{
    /// <summary>
    /// Логика взаимодействия для LogPage.xaml
    /// </summary>
    public partial class LogPage : Page
    {
        public LogPage()
        {
            InitializeComponent();
            try
            {
                login.PreviewTextInput += new TextCompositionEventHandler(loginText);
                var contracts = CAR_RENTEntities.GetContext().CONTRACTS.ToList();
                foreach (CONTRACT contract in contracts)
                {
                    DateTime contract_end = new DateTime();
                    DateTime.TryParse(contract.CONTRACT_END.ToString(), out contract_end);
                    DateTime now = new DateTime();
                    DateTime.TryParse(DateTime.Now.ToString(), out now);
                    TimeSpan days = now - contract_end;
                    if (contract.STATUS == "Подтверждена" && days.Days > 0)
                    {
                        contract.CONTRACT_STATUS = "Прокат завершен";
                    }
                    if (contract.STATUS == "Подтверждена" && days.Days <= 0)
                    {
                        contract.CONTRACT_STATUS = "Прокат активен";
                    }
                    if (contract.STATUS == "Отменена" && days.Days > 0)
                    {
                        contract.CONTRACT_STATUS = "Прокат отменен";
                    }
                    if (contract.STATUS == "Новая заявка")
                    {
                        contract.CONTRACT_STATUS = "Ждет подтверждения";
                    }
                    CAR_RENTEntities.GetContext().SaveChanges();
                }
                var cars = CAR_RENTEntities.GetContext().CARS.ToList();
                foreach (CAR car in cars)
                {
                    try
                    {
                        string id = car.ID.ToString();
                        CONTRACT contract = CAR_RENTEntities.GetContext().CONTRACTS.Where(c => c.CAR_ID.ToString().Trim() == id).FirstOrDefault();

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
        private void Come_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (login.Text.Length != 0 && password.Password.Length != 0)
                {
                    loginMessage.Text = "";
                    passwordMessage.Text = "";
                    using (CAR_RENTEntities db = new CAR_RENTEntities())
                    {
                        string log = login.Text.Trim();
                        string pass = password.Password.Trim();
                        CLIENT admin = db.CLIENTS.Where(a => a.LOGIN.Trim() == log && a.USER_TYPE.ToString().Trim() == "1").AsEnumerable().Where(a => a.LOGIN.Trim() == log && a.USER_TYPE.ToString().Trim() == "1").FirstOrDefault();
                        if (admin != null)
                        {
                            AdminWindow adminWindow = new AdminWindow();
                            App.admin = admin;
                            adminWindow.Show();
                            MainWindow.mainWindow.Close();
                        }

                        CLIENT client = db.CLIENTS.Where(u => u.LOGIN.Trim() == log && u.USER_TYPE.ToString().Trim() == "0").AsEnumerable().Where(u => u.LOGIN.Trim() == log && u.USER_TYPE.ToString().Trim() == "0").FirstOrDefault();
                        if (client != null)
                        {

                            if (client.PASSWORD.Trim() == pass && client.LOGIN.Trim() == log)

                            {
                                CatalogWindow catalog = new CatalogWindow();
                                App.currentClient = client;
                                catalog.Show();
                                Application.Current.MainWindow.Close();
                            }
                            else
                            {
                                passwordMessage.Visibility = Visibility.Visible;
                                passwordMessage.Text = "Неправильный пароль!";
                            }
                        }
                        else
                        {
                            if (client == null)
                            {
                                loginMessage.Visibility = Visibility.Visible;
                                loginMessage.Text = "Пользователь не найден!";

                            }
                        }
                    }
                }
                else
                {
                    loginMessage.Text = "";
                    passwordMessage.Text = "";
                    if (login.Text.Length == 0)
                    {
                        loginMessage.Visibility = Visibility.Visible;
                        loginMessage.Text = "Введите логин!";
                    }

                    if (password.Password.Length == 0)
                    {
                        passwordMessage.Visibility = Visibility.Visible;
                        passwordMessage.Text = "Введите пароль!";
                    }
                }
            }
            catch { }
        }


        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AuthorizationPage authorizationPage = new AuthorizationPage();
                NavigationService.Navigate(authorizationPage);
            }
            catch { }
        }

        private void login_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                using (CAR_RENTEntities db = new CAR_RENTEntities())
                {
                    string log = login.Text.Trim();
                    CLIENT clientLogin = db.CLIENTS.Where(c => c.LOGIN.Trim() == log).AsEnumerable().Where(c => c.LOGIN.Trim() == log).FirstOrDefault();
                    if (clientLogin == null)
                    {
                        loginMessage.Visibility = Visibility.Visible;
                        loginMessage.Text = "Пользователь не найден!";
                    }
                    else if (clientLogin != null)
                    {
                        loginMessage.Visibility = Visibility.Hidden;
                    }

                }
            }
            catch { }
        }

        private void password_PasswordChanged(object sender, RoutedEventArgs e)
        {

            try
            {
                if (password.Password.Length != 0)
                {
                    passwordMessage.Visibility = Visibility.Hidden;
                }
            }
            catch { }
        }

        private void password_PreviewKeyDown(object sender, KeyEventArgs e)
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

        private void login_PreviewKeyDown(object sender, KeyEventArgs e)
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
    }
}



