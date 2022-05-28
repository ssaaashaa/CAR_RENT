using CAR_RENT.models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
    /// Логика взаимодействия для accidentsPage.xaml
    /// </summary>
    public partial class accidentsPage : Page
    {
        public accidentsPage()
        {
            InitializeComponent();
            try
            {
                DGridAccidents.ItemsSource = CAR_RENTEntities.GetContext().ACCIDENTS.ToList();
               
                id.PreviewTextInput += new TextCompositionEventHandler(numbers);

              
                
            }
            catch { }

        }
       
        void numbers(object sender, TextCompositionEventArgs e)
        {
            try
            {
                if (!Char.IsDigit(e.Text, 0))
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

        private void DGridAccidents_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ACCIDENT selectedAccident = new ACCIDENT();
                selectedAccident = DGridAccidents.SelectedItem as ACCIDENT;
                if (selectedAccident != null)
                {
                    ID.Text = selectedAccident.ID.ToString().Trim();
                    CONTRACT_ID.Text = selectedAccident.CONTRACT_ID.ToString().Trim();
                    DATE.Text = selectedAccident.DATE.ToString().Remove(10).Trim();
                    DAMAGE_COST.Text = selectedAccident.DAMAGE_COST.ToString().Trim();
                    DAMAGE_DESCRIPTION.Text = selectedAccident.DAMAGE_DESCRIPTION.Trim();
                }
            }
            catch { }

        }
       
    

        private void id_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
               using(CAR_RENTEntities db=new CAR_RENTEntities())
                {
                    var contracts=(from contract in db.CONTRACTS
                                  where  contract.CLIENT_ID.ToString()==id.Text
                                  join accident in db.ACCIDENTS
                                  on contract.ID equals accident.CONTRACT_ID
                                  select accident).ToList();
                    DGridAccidents.ItemsSource=contracts;
                    if (contracts.Count == 0)
                    {
                        DGridAccidents.ItemsSource = CAR_RENTEntities.GetContext().ACCIDENTS.ToList();
                    }
                }

            }
            catch { }
        }
    }
}
