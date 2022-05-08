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
            AuthorizattionIsDone authorizattionIsDone = new AuthorizattionIsDone();
            NavigationService.Navigate(authorizattionIsDone);
        }
    }
}
