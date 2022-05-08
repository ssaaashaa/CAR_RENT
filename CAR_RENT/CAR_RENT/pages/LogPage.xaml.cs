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
            if(login.Text=="admin" && password.Text=="admin")
            {
                return;
            }

            if(login.Text!=String.Empty && login.Text != String.Empty)
            {

           
            using (CAR_RENTEntities db=new CAR_RENTEntities())
            {
                string log = login.Text;
                string pass = password.Text;

                CLIENT user = db.CLIENTS.FirstOrDefault(u=>u.LOGIN==log  && u.USER_TYPE=="0");
                if (user!=null)
                {

                        if (user.PASSWORD == pass)
                        {
                            CatalogWindow catalog = new CatalogWindow();
                            App.currentClient = user;
                            catalog.Show();
                            Application.Current.MainWindow.Close();
                        }
                        else
                        {
                            MessageBox.Show("Неправильный пароль!");                        }
                }
                    else
                    {
                        MessageBox.Show("Пользователь не найден. Введите данные ещё раз!");
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
    }
}
