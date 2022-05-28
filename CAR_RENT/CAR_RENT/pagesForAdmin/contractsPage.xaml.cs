using CAR_RENT.models;
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
    /// Логика взаимодействия для contractsPage.xaml
    /// </summary>
    public partial class contractsPage : Page
    {

        public contractsPage()
        {
            InitializeComponent();
            try
            {
                DGridContracts.ItemsSource = CAR_RENTEntities.GetContext().CONTRACTS.ToList();
                DGridNewContracts.ItemsSource = CAR_RENTEntities.GetContext().CONTRACTS.Where(c => c.STATUS.Trim() == "Новая заявка").ToList();
              
                contract_status();
            

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);    
            }
           


        }
      
        void contract_status()
        {
            try
            {
                foreach (CONTRACT contract in DGridContracts.Items)
                {
                    DateTime contract_end = new DateTime();
                    DateTime.TryParse(contract.CONTRACT_END.ToString().Trim(), out contract_end);
                    DateTime now = new DateTime();
                    DateTime.TryParse(DateTime.Now.ToString(), out now);
                    TimeSpan days = now - contract_end;
                    if (contract.STATUS.Trim() == "Подтверждена" && days.Days > 0)
                    {
                        contract.CONTRACT_STATUS = "Прокат завершен";
                    }
                    if (contract.STATUS.Trim() == "Подтверждена" && days.Days <= 0)
                    {
                        contract.CONTRACT_STATUS = "Прокат активен";
                    }
                    if (contract.STATUS.Trim() == "Отменена")
                    {
                        contract.CONTRACT_STATUS = "Прокат отменен";
                    }
                    if (contract.STATUS.Trim() == "Новая заявка")
                    {
                        contract.CONTRACT_STATUS = "Ждет подтверждения";
                    }                    
                    CAR_RENTEntities.GetContext().SaveChanges();
                }

            }
            catch { }
        }
     
        private void DGridContracts_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                CONTRACT selectedContract = new CONTRACT();
                selectedContract = DGridContracts.SelectedItem as CONTRACT;
                if (selectedContract != null)
                {
                    ID.Text = selectedContract.ID.ToString().Trim();
                    CLIENT_ID.Text = selectedContract.CLIENT_ID.ToString().Trim();
                    CAR_ID.Text = selectedContract.CAR_ID.ToString().Trim();
                    CONTRACT_START.Text = selectedContract.CONTRACT_START.ToString().Remove(10).Trim();
                    CONTRACT_END.Text = selectedContract.CONTRACT_END.ToString().Remove(10).Trim();
                    if (selectedContract.PROMO_CODE != null)
                    {
                        PROMO_CODE.Text = selectedContract.PROMO_CODE.Trim();
                    }
                    else
                    {
                        PROMO_CODE.Text = null;
                    }
                    STATUS.Text = selectedContract.STATUS.Trim();
                    SUM.Text = selectedContract.TOTAL_COST.ToString().Trim();

                }
            }
            catch { }
        }
        void Clear()
        {
            try
            {
                ID.Clear();
                CLIENT_ID.Text = null;
                CAR_ID.Text = null;
                CONTRACT_START.Text = null;
                CONTRACT_END.Text = null;
                PROMO_CODE.Text = null;
                STATUS.SelectedItem = null;
                SUM.Clear();
            }
            catch { }
           
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                if (ID.Text.Equals(""))
                {
                    MessageBox.Show("Выделите запись, которую требуется изменить!");
                    return;
                }
                DateTime contract_start = new DateTime();
                DateTime.TryParse(CONTRACT_START.Text.Trim(), out contract_start);
                DateTime contract_end = new DateTime();
                DateTime.TryParse(CONTRACT_END.Text.Trim(), out contract_end);
                TimeSpan days = contract_end - contract_start;
                CAR currentCar = CAR_RENTEntities.GetContext().CARS.Where(c => c.ID.ToString().Trim() == CAR_ID.Text.ToString().Trim()).FirstOrDefault();
                CONTRACT currentContract = CAR_RENTEntities.GetContext().CONTRACTS.Where(c => c.ID.ToString().Trim() == ID.Text.ToString().Trim()).FirstOrDefault();
 
                currentContract.STATUS = STATUS.Text.Trim();    
                if (currentContract != null)
                {
                    try
                    {
                        DateTime now = new DateTime();
                        DateTime.TryParse(DateTime.Now.ToString(), out now);
                        TimeSpan status = now - contract_end;
                        int flag = 0;
                        if (status.Days >= 0)
                        {
                            flag = 1;
                        }
                        if (STATUS.Text.Equals("Новая заявка") && flag == 1)
                        {
                            currentCar.STATUS = "Cвободна";                            
                        }
                        if (STATUS.Text.Equals("Отменена"))
                        {
                            currentCar.STATUS = "Cвободна";                          
                        }
                        if (STATUS.Text.Equals("Подтверждена") && flag == 0)
                        {
                            currentCar.STATUS = "В прокате";                           
                        }
                        else
                        {
                            currentCar.STATUS = "Cвободна";
                        }
                        contract_status();
                        CAR_RENTEntities.GetContext().SaveChanges();
                        DGridContracts.ItemsSource = CAR_RENTEntities.GetContext().CONTRACTS.ToList();
                        DGridNewContracts.ItemsSource = CAR_RENTEntities.GetContext().CONTRACTS.Where(c => c.STATUS.Trim() == "Новая заявка").ToList();
                        Clear();
                        MessageBox.Show("Данные успешно обновлены!");
                    }
                    catch
                    {
                        MessageBox.Show("Необходимо выбрать запись для редактирования!");
                    }
                }
                
            }
            catch { }
        }
       

        private void accident_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int Id = (DGridContracts.SelectedItem as CONTRACT).ID;
                Accident accident = new Accident(Id);
                accident.Show();
            }
            catch { }
        }

        private void DGridNewContracts_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                CONTRACT selectedContract = new CONTRACT();
                selectedContract = DGridNewContracts.SelectedItem as CONTRACT;
                if (selectedContract != null)
                {
                    ID.Text = selectedContract.ID.ToString().Trim();
                    CLIENT_ID.Text = selectedContract.CLIENT_ID.ToString().Trim();
                    CAR_ID.Text = selectedContract.CAR_ID.ToString().Trim();
                    CONTRACT_START.Text = selectedContract.CONTRACT_START.ToString().Remove(10).Trim();
                    CONTRACT_END.Text = selectedContract.CONTRACT_END.ToString().Remove(10).Trim();
                    if (selectedContract.PROMO_CODE != null)
                    {
                        PROMO_CODE.Text = selectedContract.PROMO_CODE.Trim();
                    }
                    else
                    {
                        PROMO_CODE.Text = null;
                    }
                    STATUS.Text = selectedContract.STATUS.Trim();
                    SUM.Text = selectedContract.TOTAL_COST.ToString().Trim();

                }
            }
            catch { }
    
        }
        private void search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                idCar.Clear();
                DGridContracts.ItemsSource = CAR_RENTEntities.GetContext().CONTRACTS.ToList();
                for (int i = 0; i < DGridContracts.Items.Count; i++)
                {
                    string param = id.Text;
                    DGridContracts.ScrollIntoView(DGridContracts.Items[i]);
                    DataGridRow row = (DataGridRow)DGridContracts.ItemContainerGenerator.ContainerFromIndex(i);
                    TextBlock cellContentID = DGridContracts.Columns[0].GetCellContent(row) as TextBlock;
                    if (cellContentID != null && cellContentID.Text.ToLower().Trim().Equals(param.ToLower()))
                    {
                        object item = DGridContracts.Items[i];
                        DGridContracts.SelectedItem = item;
                        DGridContracts.ScrollIntoView(item);
                        row.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                        break;
                    }
                }
            }
            catch { }
        }

        private void idCar_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                DGridContracts.ItemsSource = CAR_RENTEntities.GetContext().CONTRACTS.Where(c => c.CAR_ID.ToString().Trim() == idCar.Text.Trim()).ToList();
                if (idCar.Text.Trim().Length == 0)
                {
                    DGridContracts.ItemsSource = CAR_RENTEntities.GetContext().CONTRACTS.ToList();
                }
            }
            catch { }
        }

     
    }
}
