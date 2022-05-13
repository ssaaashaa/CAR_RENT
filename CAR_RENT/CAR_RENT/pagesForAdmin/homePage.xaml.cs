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

namespace CAR_RENT.pagesForAdmin
{
    /// <summary>
    /// Логика взаимодействия для homePage1.xaml
    /// </summary>
    public partial class homePage : Page
    {
        public homePage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            pageForRegistrationOfAccidents pageForRegistrationOfAccidents = new pageForRegistrationOfAccidents();
            AdminWindow.Frame.Content= pageForRegistrationOfAccidents;
        }
    }
}
