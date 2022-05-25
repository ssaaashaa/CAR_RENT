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
       

        public Accident(int currentContractID)
        {
            InitializeComponent();
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
            contractID.Text=currentContract.ID.ToString();           
            accidentID.Text=currentAccident.ID.ToString();
        }
        void Clear()
        {
            date.Text=null;
            description.Text=null;
            damageCost.Clear();

        }
        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            ACCIDENT currentAccident = CAR_RENTEntities.GetContext().ACCIDENTS.Where(a => a.ID.ToString() == accidentID.Text.ToString()).FirstOrDefault();
            if(currentAccident != null)
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

        private void accept_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ACCIDENT currentAccident = CAR_RENTEntities.GetContext().ACCIDENTS.Where(a => a.ID.ToString() == accidentID.Text.ToString()).FirstOrDefault();
                using (CAR_RENTEntities db=new CAR_RENTEntities()) 
                {   
                    db.Entry(currentAccident).State = EntityState.Modified;
                    currentAccident.CONTRACT_ID = Convert.ToInt32(contractID.Text);
                    currentAccident.DATE = Convert.ToDateTime(date.Text).Date;
                    currentAccident.DAMAGE_DESCRIPTION = description.Text.ToString().Remove(0, 33);
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
