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
            DGridContracts.ItemsSource = CAR_RENTEntities.GetContext().CONTRACTS.ToList();
            DGridNewContracts.ItemsSource = CAR_RENTEntities.GetContext().CONTRACTS.Where(c=>c.STATUS=="Новая заявка").ToList();
            CLIENT_ID.PreviewTextInput += new TextCompositionEventHandler(textInput);
            CAR_ID.PreviewTextInput += new TextCompositionEventHandler(textInput);
            contract_status();

        }
        void contract_status()
        {
            try
            {
                foreach (CONTRACT contract in DGridContracts.Items)
                {
                    DateTime contract_end = new DateTime();
                    DateTime.TryParse(contract.CONTRACT_END.ToString(), out contract_end);
                    DateTime now = new DateTime();
                    DateTime.TryParse(DateTime.Now.ToString(), out now);
                    TimeSpan days = now - contract_end;
                    if (contract.STATUS == "Подтверждена" && days.Days > 0)
                    {
                        contract.CONTRACT_STATUS = "Прокат завершен";
                    }
                    if (contract.STATUS == "Подтверждена" && days.Days <= 0)
                    {
                        contract.CONTRACT_STATUS = "Прокат активен";
                    }
                    if (contract.STATUS == "Отменена")
                    {
                        contract.CONTRACT_STATUS = "Прокат отменен";
                    }
                    if (contract.STATUS == "Новая заявка")
                    {
                        contract.CONTRACT_STATUS = "Ждет подтверждения";
                    }                    
                    CAR_RENTEntities.GetContext().SaveChanges();
                }

            }
            catch { }
        }
        private void textInput(object sender, TextCompositionEventArgs e)
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
        private void DGridContracts_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CONTRACT selectedContract = new CONTRACT();
            selectedContract=DGridContracts.SelectedItem as CONTRACT;   
            if (selectedContract != null)
            {
                ID.Text = selectedContract.ID.ToString();
                CLIENT_ID.Text = selectedContract.CLIENT_ID.ToString();
                CAR_ID.Text=selectedContract.CAR_ID.ToString();
                CONTRACT_START.Text = selectedContract.CONTRACT_START.ToString().Remove(10);
                CONTRACT_END.Text=selectedContract.CONTRACT_END.ToString().Remove(10);
                PROMO_CODE.Text = selectedContract.PROMO_CODE;
                STATUS.Text = selectedContract.STATUS;
                SUM.Text = selectedContract.TOTAL_COST.ToString();

            }
        }
        void Clear()
        {
            ID.Clear();
            CLIENT_ID.Clear();
            CAR_ID.Clear();
            CONTRACT_START.Text = null;
            CONTRACT_END.Text= null;           
            PROMO_CODE.Clear();
            STATUS.SelectedItem = null;
           
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            DateTime contract_start = new DateTime();
            DateTime.TryParse(CONTRACT_START.Text, out contract_start);
            DateTime contract_end = new DateTime();
            DateTime.TryParse(CONTRACT_END.Text, out contract_end);
            if (string.IsNullOrWhiteSpace(CLIENT_ID.Text))
            {
                errors.AppendLine("Введите клиента!");
            }
            if (string.IsNullOrWhiteSpace(CLIENT_ID.Text))
            {
                errors.AppendLine("Введите авто!");
            }
            if (string.IsNullOrWhiteSpace(CONTRACT_START.Text))
            {
                errors.AppendLine("Введите дату начала аренды!");
            }
            if (string.IsNullOrWhiteSpace(CONTRACT_END.Text))
            {
                errors.AppendLine("Введите дату окончания аренды!");
            }
            if (string.IsNullOrWhiteSpace(STATUS.Text))
            {
                errors.AppendLine("Введите статус!");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            double promocode=0;
            PROMO_CODE promo_code=null;
            CAR car = CAR_RENTEntities.GetContext().CARS.Where(c => c.ID.ToString() == CAR_ID.Text.ToString()).Single();
            if (PROMO_CODE.Text.Length!=0)
            {
                promo_code = CAR_RENTEntities.GetContext().PROMO_CODE.Where(c => c.PROMO_CODE1 == PROMO_CODE.Text).Single();
                promocode = Convert.ToDouble(promo_code.DISCOUNT_AMOUNT) * 0.01;
            }
            int count_of_days = contract_end.Day - contract_start.Day;            
            double total_price = Convert.ToInt32(car.RENT_PRICE) * count_of_days- Convert.ToInt32(car.RENT_PRICE) * count_of_days*promocode;         
            CONTRACT currentContract =new CONTRACT(); 
            currentContract.CLIENT_ID = Convert.ToInt32(CLIENT_ID.Text);
            currentContract.CAR_ID=Convert.ToInt32(CAR_ID.Text);
            currentContract.CONTRACT_START = contract_start;
            currentContract.CONTRACT_END = contract_end;
            try
            {
                currentContract.PROMO_CODE = promo_code.PROMO_CODE1;
            }
            catch { }
            currentContract.TOTAL_COST = Convert.ToInt32(total_price);
            currentContract.STATUS = STATUS.Text;
            contract_status();
            CAR_RENTEntities.GetContext().CONTRACTS.Add(currentContract);
            try
            {
                CAR_RENTEntities.GetContext().SaveChanges();
                DGridContracts.ItemsSource = CAR_RENTEntities.GetContext().CONTRACTS.ToList();
                DGridNewContracts.ItemsSource = CAR_RENTEntities.GetContext().CONTRACTS.Where(c => c.STATUS == "Новая заявка").ToList();
                Clear();
                MessageBox.Show("Данные успешно добавлены!");
            }
            catch
            {
                MessageBox.Show("Такая запись уже существует!");
            }


        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (ID.Text.Equals(""))
            {
                MessageBox.Show("Выделите запись, которую требуется изменить!");

            }
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(CLIENT_ID.Text))
            {
                errors.AppendLine("Введите клиента!");
            }
            if (string.IsNullOrWhiteSpace(CLIENT_ID.Text))
            {
                errors.AppendLine("Введите авто!");
            }
            if (string.IsNullOrWhiteSpace(CONTRACT_START.Text))
            {
                errors.AppendLine("Введите дату начала аренды!");
            }
            if (string.IsNullOrWhiteSpace(CONTRACT_END.Text))
            {
                errors.AppendLine("Введите дату окончания аренды!");
            }
            if (string.IsNullOrWhiteSpace(STATUS.Text))
            {
                errors.AppendLine("Введите статус!");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            try
            {
                DateTime contract_start = new DateTime();
                DateTime.TryParse(CONTRACT_START.Text, out contract_start);
                DateTime contract_end = new DateTime();
                DateTime.TryParse(CONTRACT_END.Text, out contract_end);
                double promocode = 0;
                PROMO_CODE promo_code = null;
                CAR currentCar = CAR_RENTEntities.GetContext().CARS.Where(c => c.ID.ToString() == CAR_ID.Text.ToString()).FirstOrDefault();
                if (PROMO_CODE.Text.Length != 0)
                {
                    promo_code = CAR_RENTEntities.GetContext().PROMO_CODE.Where(c => c.PROMO_CODE1 == PROMO_CODE.Text).Single();
                    promocode = Convert.ToDouble(promo_code.DISCOUNT_AMOUNT) * 0.01;
                }
                int count_of_days = contract_end.Day - contract_start.Day;
                double total_price = Convert.ToInt32(currentCar.RENT_PRICE) * count_of_days - Convert.ToInt32(currentCar.RENT_PRICE) * count_of_days * promocode;
                CONTRACT currentContract = CAR_RENTEntities.GetContext().CONTRACTS.Where(c => c.ID.ToString() == ID.Text.ToString()).FirstOrDefault();
                currentContract.CLIENT_ID = Convert.ToInt32(CLIENT_ID.Text);
                currentContract.CAR_ID = Convert.ToInt32(CAR_ID.Text);
                currentContract.CONTRACT_START = contract_start;
                currentContract.CONTRACT_END = contract_end;
                try
                {
                    currentContract.PROMO_CODE = promo_code.PROMO_CODE1;
                }
                catch { }
                currentContract.TOTAL_COST = Convert.ToInt32(total_price);
                currentContract.STATUS = STATUS.Text;    
                if (currentContract != null)
                {
                    try
                    {
                        DateTime now = new DateTime();
                        DateTime.TryParse(DateTime.Now.ToString(), out now);
                        int status = now.Day - contract_end.Day;
                        int flag = 0;
                        if (status >= 0)
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
                            currentCar.STATUS = "D прокате";                           
                        }
                        else
                        {
                            currentCar.STATUS = "Cвободна";
                        }
                        contract_status();
                        CAR_RENTEntities.GetContext().SaveChanges();
                        DGridContracts.ItemsSource = CAR_RENTEntities.GetContext().CONTRACTS.ToList();
                        DGridNewContracts.ItemsSource = CAR_RENTEntities.GetContext().CONTRACTS.Where(c => c.STATUS == "Новая заявка").ToList();
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
        private void Delete_Click(object sender, RoutedEventArgs ec)
        {
            if (ID.Text.Equals(""))
            {
                MessageBox.Show("Выделите запись, которую требуется удалить!");

            }
            CONTRACT currentContract = CAR_RENTEntities.GetContext().CONTRACTS.Where(m => m.ID.ToString() == ID.Text.ToString()).FirstOrDefault();
            if(currentContract != null)
            {
                CAR_RENTEntities.GetContext().CONTRACTS.Remove(currentContract);
                try
                {
                    CAR_RENTEntities.GetContext().SaveChanges();
                    DGridContracts.ItemsSource = CAR_RENTEntities.GetContext().CONTRACTS.ToList();
                    DGridNewContracts.ItemsSource = CAR_RENTEntities.GetContext().CONTRACTS.Where(c => c.STATUS == "Новая заявка").ToList();
                    Clear();
                    MessageBox.Show("Запись удалена!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (currentContract == null && string.IsNullOrEmpty(ID.Text) == false)
            {
                MessageBox.Show("Такого контракта не существует!");
            }
        }

        private void accident_Click(object sender, RoutedEventArgs e)
        {
           int Id = (DGridContracts.SelectedItem as CONTRACT).ID;
           Accident accident= new Accident(Id);   
           accident.Show();
        }

        private void DGridNewContracts_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CONTRACT selectedContract = new CONTRACT();
            selectedContract = DGridNewContracts.SelectedItem as CONTRACT;
            if (selectedContract != null)
            {
                ID.Text = selectedContract.ID.ToString();
                CLIENT_ID.Text = selectedContract.CLIENT_ID.ToString();
                CAR_ID.Text = selectedContract.CAR_ID.ToString();
                CONTRACT_START.Text = selectedContract.CONTRACT_START.ToString().Remove(10);
                CONTRACT_END.Text = selectedContract.CONTRACT_END.ToString().Remove(10);
                PROMO_CODE.Text = selectedContract.PROMO_CODE;
                STATUS.Text = selectedContract.STATUS;
                SUM.Text = selectedContract.TOTAL_COST.ToString();

            }
        }
        private void search_Click(object sender, RoutedEventArgs e)
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

        private void searchCar_Click(object sender, RoutedEventArgs e)
        {
            DGridContracts.ItemsSource = null;
            DGridContracts.ItemsSource = CAR_RENTEntities.GetContext().CONTRACTS.Where(c => c.CAR_ID.ToString()==idCar.Text).ToList();
        }

        private void idCar_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGridContracts.ItemsSource = CAR_RENTEntities.GetContext().CONTRACTS.Where(c => c.CAR_ID.ToString() == idCar.Text.Trim()).ToList();
            if (idCar.Text.Trim().Length == 0)
            {
                DGridContracts.ItemsSource = CAR_RENTEntities.GetContext().CONTRACTS.ToList();
            }
        }

     
    }
}
