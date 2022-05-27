using CAR_RENT.models;
using CAR_RENT.userControls;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для Accident.xaml
    /// </summary>
    public partial class Accident : Window
    {

        private int Contract;
        public Accident(int currentContractID)
        {
            InitializeComponent();

            try
            {
                Date.PreviewTextInput += new TextCompositionEventHandler(dateInput);
                damageCost.PreviewTextInput += new TextCompositionEventHandler(numbers);
                ACCIDENT currentAccident = new ACCIDENT();
                CAR_RENTEntities.GetContext().ACCIDENTS.Add(currentAccident);
                try
                {
                    CAR_RENTEntities.GetContext().SaveChanges();
                }
                catch
                {
                }
                CONTRACT currentContract = CAR_RENTEntities.GetContext().CONTRACTS.Where(c => c.ID == currentContractID).FirstOrDefault();
                contractID.Text = currentContract.ID.ToString().Trim();
                accidentID.Text = currentAccident.ID.ToString().Trim();
                Contract = currentContractID;
                CONTRACT contract = CAR_RENTEntities.GetContext().CONTRACTS.Where(c => c.ID.ToString().Trim() == Contract.ToString().Trim()).FirstOrDefault();
                DateTime contract_start = new DateTime();
                DateTime.TryParse(contract.CONTRACT_START.ToString().Trim(), out contract_start);
                DateTime contract_end = new DateTime();
                DateTime.TryParse(contract.CONTRACT_END.ToString().Trim(), out contract_end);
                Date.DisplayDateStart = contract_start;
                Date.DisplayDateEnd = contract_end;
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
        private void dateInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                if (!Char.IsDigit(e.Text, 0) && e.Text != "." && e.Text != "-" && e.Text != "/")
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
        void Clear()
        {
            try
            {
                Date.Text = null;
                description.Text = null;
                damageCost.Clear();
            }
            catch { }

        }
        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ACCIDENT currentAccident = CAR_RENTEntities.GetContext().ACCIDENTS.Where(a => a.ID.ToString().Trim() == accidentID.Text.ToString()).FirstOrDefault();
                if (currentAccident != null)
                {
                    CAR_RENTEntities.GetContext().ACCIDENTS.Remove(currentAccident);
                    try
                    {
                        CAR_RENTEntities.GetContext().SaveChanges();
                        Clear();
                        MessageBox.Show("ДТП не оформлено!");
                        this.Close();
                    }
                    catch { }
                }
                this.Close();
            }
            catch { }
          
        }

        private void accept_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ACCIDENT currentAccident = CAR_RENTEntities.GetContext().ACCIDENTS.Where(a => a.ID.ToString().Trim() == accidentID.Text.ToString().Trim()).FirstOrDefault();
                using (CAR_RENTEntities db=new CAR_RENTEntities()) 
                {
                    CONTRACT contract = CAR_RENTEntities.GetContext().CONTRACTS.Where(c => c.ID.ToString().Trim() == Contract.ToString().Trim()).FirstOrDefault();
                    DateTime contract_start = new DateTime();
                    DateTime.TryParse(contract.CONTRACT_START.ToString().Trim(), out contract_start);
                    DateTime contract_end = new DateTime();
                    DateTime.TryParse(contract.CONTRACT_END.ToString().Trim(), out contract_end);
                    DateTime date = new DateTime();
                    DateTime.TryParse(Date.Text, out date);
                    StringBuilder errors = new StringBuilder();
                    if (Date.Text.Length == 0)
                    {
                        errors.AppendLine("Дата обязательна!");
                    }
                    if (damageCost.Text.Length == 0)
                    {
                        errors.AppendLine("Стоимость ущерба обязательна!");
                    }
                    if ((date.Date < contract_start.Date && date.Date < contract_end.Date) || (date.Date > contract_start.Date && date.Date > contract_end.Date))
                    {
                        errors.AppendLine("Выбранная дата не входит в период аренды, указанного в контракте!");
                       
                    }
                    if(errors.Length > 0)
                    {
                        MessageBox.Show(errors.ToString());
                        return;
                    }
                    db.Entry(currentAccident).State = EntityState.Modified;
                    currentAccident.CONTRACT_ID = Convert.ToInt32(contractID.Text.Trim());
                    currentAccident.DATE = Convert.ToDateTime(Date.Text.Trim()).Date;
                    currentAccident.DAMAGE_DESCRIPTION = description.Text.ToString().Remove(0, 33).Trim();
                    currentAccident.DAMAGE_COST = Convert.ToInt32(damageCost.Text);
                    if (currentAccident != null)
                    {
                        try
                        {
                            CAR_RENTEntities.GetContext().SaveChanges();
                            Clear();
                            MessageBox.Show("ДТП оформлено!");
                            this.Close();
                        }
                        catch { }
                    }
                }

            }
            catch { }
        }
    }
}
