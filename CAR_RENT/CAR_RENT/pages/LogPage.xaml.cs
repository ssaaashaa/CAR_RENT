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
        }

        private void Come_Click(object sender, RoutedEventArgs e)
        {
           

            if(login.Text !="Логин" && password.Text!="Пароль")
            {

           
            using (CAR_RENTEntities db=new CAR_RENTEntities())
            {
                string log = login.Text;
                string pass = password.Text;
                CLIENT admin = db.CLIENTS.FirstOrDefault(a => a.LOGIN == log && a.USER_TYPE == "1");
                if (admin != null)
                { 
                        AdminWindow adminWindow = new AdminWindow();
                        App.admin = admin;
                        adminWindow.Show();
                        Application.Current.MainWindow.Close();
                }
                CLIENT client = db.CLIENTS.FirstOrDefault(u=>u.LOGIN==log  && u.USER_TYPE=="0");
                if (client!=null)
                {

                        if (client.PASSWORD == pass)
                        {
                            CatalogWindow catalog = new CatalogWindow();
                            App.currentClient = client;
                            catalog.Show();
                            Application.Current.MainWindow.Close();
                        }
                        else
                        {
                            MessageBox.Show("Неправильный пароль!");                        }
                }
                    else
                    {
                        if (admin == null)
                        {
                            MessageBox.Show("Пользователь не найден. Введите данные ещё раз!");
                        }
                    }
            }
            }
            else
            {
                MessageBox.Show("Введите логин и пароль!");
            }
        }
        
        
        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
          AuthorizationPage authorizationPage = new AuthorizationPage();
          NavigationService.Navigate(authorizationPage);


        }

        private void login_GotFocus(object sender, RoutedEventArgs e)
        {
            if (login.Text == "Логин")
                login.Text = "";
        }

        private void login_LostFocus(object sender, RoutedEventArgs e)
        {
            if (login.Text == "")
                login.Text = "Логин";
        }

        private void password_GotFocus(object sender, RoutedEventArgs e)
        {
            if (password.Text == "Пароль")
                password.Text = "";
        }

        private void password_LostFocus(object sender, RoutedEventArgs e)
        {
            if (password.Text == "")
                password.Text = "Пароль";
        }
       
    }
}
