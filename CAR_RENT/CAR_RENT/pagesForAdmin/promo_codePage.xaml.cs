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

namespace CAR_RENT.pagesForAdmin
{
    /// <summary>
    /// Логика взаимодействия для promo_codePage1.xaml
    /// </summary>
    public partial class promo_codePage : Page
    {

        public promo_codePage()
        {
            InitializeComponent();
            Load();
           
        }


        private void Load()
        {
            CAR_RENTEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(entry => entry.Reload());
            DGridPromocode.ItemsSource = CAR_RENTEntities.GetContext().PROMO_CODE.ToList();

        }
        private void DGridPromocodes_MouseDown(object sender, MouseButtonEventArgs e)
        {

            PROMO_CODE selectedPromocode = new PROMO_CODE();
            selectedPromocode = DGridPromocode.SelectedItem as PROMO_CODE;
            PROMO_CODE.Text = selectedPromocode.PROMO_CODE1;
            DISCOUNT_AMOUNT.Text = selectedPromocode.DISCOUNT_AMOUNT;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
