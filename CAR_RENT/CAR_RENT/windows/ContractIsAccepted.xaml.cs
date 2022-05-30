using CAR_RENT.pages;
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
using System.Windows.Shapes;

namespace CAR_RENT.windows
{
    /// <summary>
    /// Логика взаимодействия для ContractIsAccepted.xaml
    /// </summary>
    public partial class ContractIsAccepted : Window
    {
        rent Rent;
        public ContractIsAccepted(rent rent)
        {
            InitializeComponent();
            Rent=rent;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Rent.Close();
                this.DialogResult = true;
                CatalogWindow.Frame.Navigate(new UserContracts());
            }
            catch { }
       
        }
    }
}
