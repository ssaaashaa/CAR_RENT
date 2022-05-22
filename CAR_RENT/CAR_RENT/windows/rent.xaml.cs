using CAR_RENT.models;
using CAR_RENT.userControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
            CONTRACT_START.BlackoutDates.AddDatesInPast();
            CONTRACT_END.BlackoutDates.AddDatesInPast();
            CONTRACT_START.PreviewTextInput+= new TextCompositionEventHandler(dateInput);
        }

        private void dateInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0) && e.Text!=".")
            {
                e.Handled = true; //не обрабатывать введеный символ
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
       
        private void send_Click(object sender, RoutedEventArgs e)
        {
            CONTRACT contract = new CONTRACT();
            StringBuilder errors = new StringBuilder();
            DateTime contract_start = new DateTime();
            DateTime.TryParse(CONTRACT_START.Text, out contract_start);
            DateTime contract_end = new DateTime();
            DateTime.TryParse(CONTRACT_END.Text, out contract_end);
            TimeSpan days = contract_end - contract_start;
            
            PROMO_CODE.BorderBrush = Brushes.White;
            
            if (string.IsNullOrWhiteSpace(CONTRACT_START.Text))
            {
                CONTRACT_START.BorderBrush = Brushes.Red;
                errors.AppendLine();
            }
            else
            {

            }
            if (string.IsNullOrWhiteSpace(CONTRACT_END.Text))
            {
                CONTRACT_END.BorderBrush = Brushes.Red;
                errors.AppendLine();
            }
            double promocode = 0;
            PROMO_CODE promo_code = null;
            if (PROMO_CODE.Text.Length != 0)
            {
                contract.PROMO_CODE = PROMO_CODE.Text;
                promo_code = CAR_RENTEntities.GetContext().PROMO_CODE.Where(c => c.PROMO_CODE1 == PROMO_CODE.Text).FirstOrDefault();
                if (promo_code != null)
                {
                    promocode = Convert.ToDouble(promo_code.DISCOUNT_AMOUNT) * 0.01;
                }
                else
                {
                    MessageBox.Show("Такого промокода не существует!");
                    PROMO_CODE.BorderThickness= new Thickness(1);
                    PROMO_CODE.BorderBrush = Brushes.Red;
                    PROMO_CODE.Clear();
                    errors.AppendLine();
                }
            }
            else contract.PROMO_CODE = null;
            if (days.Days < 0)
            {
                CONTRACT_START.BorderBrush = Brushes.Red;
                CONTRACT_END.BorderBrush = Brushes.Red;
                errors.AppendLine();
            }
            if(errors.Length > 0 && days.Days>0)
            {
                CONTRACT_START.BorderBrush = Brushes.White;
                CONTRACT_END.BorderBrush = Brushes.White;
            }
            if (errors.Length > 0)
            {
                return;
            }          
           
            contract.CLIENT_ID = App.currentClient.ID;
            contract.CAR_ID = Convert.ToInt32(Car.Id);
            contract.CONTRACT_START = Convert.ToDateTime(CONTRACT_START.Text);
            contract.CONTRACT_END = Convert.ToDateTime(CONTRACT_END.Text);  
            CAR car = CAR_RENTEntities.GetContext().CARS.Where(c => c.ID.ToString() == Car.Id.ToString()).FirstOrDefault();            
            double total_price = Convert.ToInt32(car.RENT_PRICE) * days.Days - Convert.ToInt32(car.RENT_PRICE) * days.Days * promocode;
            contract.TOTAL_COST = Convert.ToInt32(total_price);
            contract.STATUS = "Новая заявка";
            contract.CONTRACT_STATUS = "Ждет подтверждения";
            CAR_RENTEntities.GetContext().CONTRACTS.Add(contract);
            try
            {
                CAR_RENTEntities.GetContext().SaveChanges();
                try
                {
                    MailAddress from = new MailAddress("aleksa-vesna@mail.ru", "Aleksandra");
                    // кому отправляем
                    MailAddress to = new MailAddress("aleksa-vesna@mail.ru");
                    // создаем объект сообщения
                    MailMessage m = new MailMessage(from, to);
                    // тема письма
                    m.Subject = "Belcar";
                    // текст письма
                    m.Body = "<h2>Вам поступила новая заявка на бронь авто!</h2>";
                    // письмо представляет код html
                    m.IsBodyHtml = true;
                    //адрес smtp-сервера и порт, с которого будем отправлять письмо
                    SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
                    //логин и пароль
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("aleksa-vesna@mail.ru", "mQYMHqTfzpsjDzbnZ59b");
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(m);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                //Console.Read();
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
