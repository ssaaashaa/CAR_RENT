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
    /// Логика взаимодействия для accidentsPage.xaml
    /// </summary>
    public partial class accidentsPage : Page
    {
        public accidentsPage()
        {
            InitializeComponent();
            DGridAccidents.ItemsSource = CAR_RENTEntities.GetContext().ACCIDENTS.ToList();
            CONTRACT_ID.PreviewTextInput += new TextCompositionEventHandler(textInput);
            DAMAGE_DESCRIPTION.PreviewTextInput += new TextCompositionEventHandler(textInput);
        }
        private void textInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true; //не обрабатывать введеный символ
            }
        }

        private void DGridAccidents_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ACCIDENT selectedAccident=new ACCIDENT();
            selectedAccident = DGridAccidents.SelectedItem as ACCIDENT;
            if(selectedAccident != null)
            {
                ID.Text = selectedAccident.ID.ToString();
                CONTRACT_ID.Text=selectedAccident.CONTRACT_ID.ToString();
                DATE.Text = selectedAccident.DATE.ToString().Remove(10);
                DAMAGE_COST.Text = selectedAccident.DAMAGE_COST.ToString();
                DAMAGE_DESCRIPTION.Text = selectedAccident.DAMAGE_DESCRIPTION;

            }
        }
        void Clear()
        {
            ID.Clear();
            CONTRACT_ID.Clear();
            DATE.Clear();
            DAMAGE_COST.Clear();
            DAMAGE_DESCRIPTION.Clear();
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
