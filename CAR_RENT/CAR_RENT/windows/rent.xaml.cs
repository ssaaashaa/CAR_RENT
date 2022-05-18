using CAR_RENT.models;
using CAR_RENT.userControls;
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
    /// Логика взаимодействия для rent.xaml
    /// </summary>
    public partial class rent : Window
    { 
        
        public rent()
        {
            InitializeComponent();
            auto.Text = Car.CurrentName;            
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void send_Click(object sender, RoutedEventArgs e)
        {
            CONTRACT contract=new CONTRACT();
            contract.CLIENT_ID = App.currentClient.ID;
            contract.CAR_ID = Convert.ToInt32(Car.Id);
            contract.CONTRACT_START = Convert.ToDateTime(CONTRACT_START.Text);
            contract.CONTRACT_END=Convert.ToDateTime(CONTRACT_END.Text);
            contract.PROMO_CODE=PROMO_CODE.Text;
            DateTime contract_start = new DateTime();
            DateTime.TryParse(CONTRACT_START.Text, out contract_start);
            DateTime contract_end = new DateTime();
            DateTime.TryParse(CONTRACT_END.Text, out contract_end);
            int count_of_days = contract_end.Day - contract_start.Day;
            CAR car = CAR_RENTEntities.GetContext().CARS.Where(c => c.ID.ToString() == Car.Id.ToString()).FirstOrDefault();
            double promocode = 0;
            PROMO_CODE promo_code = null;
            if (PROMO_CODE.Text.Length != 0)
            {
                promo_code = CAR_RENTEntities.GetContext().PROMO_CODE.Where(c => c.PROMO_CODE1 == PROMO_CODE.Text).Single();
                promocode = Convert.ToDouble(promo_code.DISCOUNT_AMOUNT) * 0.01;
            }
            double total_price = Convert.ToInt32(car.RENT_PRICE) * count_of_days - Convert.ToInt32(car.RENT_PRICE) * count_of_days * promocode;
            contract.TOTAL_COST = Convert.ToInt32(total_price);
            contract.STATUS = "Новая заявка";
            CAR_RENTEntities.GetContext().CONTRACTS.Add(contract);
            try
            {
                CAR_RENTEntities.GetContext().SaveChanges();
                MessageBox.Show("Заявка отправлена!");
                this.Close();
            }
            catch
            {
                MessageBox.Show("Заявка не отправлена!");
            }

        }
    }
}
