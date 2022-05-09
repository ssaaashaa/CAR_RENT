using CAR_RENT.models;
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
    /// Логика взаимодействия для AuthorizationPageDataInfo.xaml
    /// </summary>
    public partial class AuthorizationPageDataInfo : Page
    {
        public AuthorizationPageDataInfo()
        {
            InitializeComponent();
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            using (CAR_RENTEntities db = new CAR_RENTEntities())
            { 
                CLIENT client = new CLIENT();
                client.LOGIN = AuthorizationPage.Log.ToString();
                client.PASSWORD = AuthorizationPage.Pass.ToString();
                client.SURNAME = AuthorizationPage.Surn.ToString();
                client.NAME = AuthorizationPage.N.ToString();
                client.PATRONYMIC = AuthorizationPage.Patron.ToString();
                client.BDAY = AuthorizationPage.Date;
                client.TELEPHONE=telephone.Text;
                client.ADRESS=aldress.Text;
                client.PASSPORT_SERIES = passportSeries.Text;
                client.PASSPORT_ID = Int32.Parse(passportID.Text);
                client.DRIVER_LICENSE_ID=licenseID.Text;
                client.DRIVING_EXPERIENCE=experience.Text;

                db.CLIENTS.Add(client);
                db.SaveChanges();
                App.currentClient = client;

                AuthorizattionIsDone authorizattionIsDone = new AuthorizattionIsDone();
                NavigationService.Navigate(authorizattionIsDone);
                //MessageBox.Show("registration is done");
            }
            }
    }
}
