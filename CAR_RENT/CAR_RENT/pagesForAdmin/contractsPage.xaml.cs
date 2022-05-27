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
                CLIENT_ID.PreviewTextInput += new TextCompositionEventHandler(textInput);
                CAR_ID.PreviewTextInput += new TextCompositionEventHandler(textInput);
                CONTRACT_START.PreviewTextInput += new TextCompositionEventHandler(dateInput);
                CONTRACT_END.PreviewTextInput += new TextCompositionEventHandler(dateInput);
                id.PreviewTextInput += new TextCompositionEventHandler(numbers);
                idCar.PreviewTextInput += new TextCompositionEventHandler(numbers);
                contract_status();
                TimeSpan time = TimeSpan.FromDays(30);
                CONTRACT_START.DisplayDateEnd = DateTime.Now + time;
                CONTRACT_END.DisplayDateEnd = DateTime.Now + time;

                ListView listIDclients = new ListView();
                using (CAR_RENTEntities db = new CAR_RENTEntities())
                {
                    var currentClients = (from clients in db.CLIENTS
                                          select new
                                          {
                                              ID = clients.ID,
                                              LOGIN=clients.LOGIN
                                          }).ToList().OrderBy(c => c.ID);
                    foreach (var client in currentClients)
                    {
                        CLIENT_ID.Items.Add(client.ID);
                       

                    }
                }
                ListView listIDcars = new ListView();
                using (CAR_RENTEntities db = new CAR_RENTEntities())
                {
                    var currentCars = (from cars in db.CARS
                                       select new
                                       {
                                           ID = cars.ID
                                       }).ToList().OrderBy(c => c.ID);
                    foreach (var car in currentCars)
                    {
                        CAR_ID.Items.Add(car.ID);

                    }
                }
                ListView listPromo = new ListView();
                using (CAR_RENTEntities db = new CAR_RENTEntities())
                {
                    var currentPromos = (from promo in db.PROMO_CODE
                                         select new
                                         {
                                             PROMO = promo.PROMO_CODE1
                                         }).ToList().OrderBy(c => c.PROMO);
                    foreach (var promo in currentPromos)
                    {
                        PROMO_CODE.Items.Add(promo.PROMO);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);    
            }
           


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
                        PROMO_CODE.SelectedValue = null;
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
                CLIENT_ID.SelectedItem = null;
                CAR_ID.SelectedItem = null;
                CONTRACT_START.Text = null;
                CONTRACT_END.Text = null;
                PROMO_CODE.SelectedItem = null;
                STATUS.SelectedItem = null;
                SUM.Clear();
            }
            catch { }
           
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder errors = new StringBuilder();
                DateTime contract_start = new DateTime();
                DateTime.TryParse(CONTRACT_START.Text.Trim(), out contract_start);
                DateTime contract_end = new DateTime();
                DateTime.TryParse(CONTRACT_END.Text.Trim(), out contract_end);
                TimeSpan days = contract_end - contract_start;
                if (string.IsNullOrWhiteSpace(CLIENT_ID.Text.Trim()))
                {
                    errors.AppendLine("Выберите клиента!");
                }
                if (string.IsNullOrWhiteSpace(CLIENT_ID.Text.Trim()))
                {
                    errors.AppendLine("Выберите авто!");
                }
                if (string.IsNullOrWhiteSpace(CONTRACT_START.Text.Trim()))
                {
                    errors.AppendLine("Введите дату начала аренды!");
                }
                if (string.IsNullOrWhiteSpace(CONTRACT_END.Text.Trim()))
                {
                    errors.AppendLine("Введите дату окончания аренды!");
                }
                if (days.Days < 0)
                {
                    errors.AppendLine("Введите корректные даты!");
                }
                if (days.Days >= 31)
                {
                    errors.AppendLine("Прокат осуществляется только на 30 дней!");
                }
                if (string.IsNullOrWhiteSpace(STATUS.Text.Trim()))
                {
                    errors.AppendLine("Введите статус!");
                }
                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    return;
                }
                double promocode = 0;
                PROMO_CODE promo_code = null;
                CAR car = CAR_RENTEntities.GetContext().CARS.Where(c => c.ID.ToString().Trim() == CAR_ID.Text.ToString().Trim()).Single();
                if (PROMO_CODE.Text.Length != 0)
                {
                    promo_code = CAR_RENTEntities.GetContext().PROMO_CODE.Where(c => c.PROMO_CODE1.Trim() == PROMO_CODE.Text.Trim()).Single();
                    promocode = Convert.ToDouble(promo_code.DISCOUNT_AMOUNT.Trim()) * 0.01;
                }
                double total_price = Convert.ToInt32(car.RENT_PRICE) * days.Days - Convert.ToInt32(car.RENT_PRICE) * days.Days * promocode;
                CONTRACT currentContract = new CONTRACT();
                currentContract.CLIENT_ID = Convert.ToInt32(CLIENT_ID.Text.Trim());
                currentContract.CAR_ID = Convert.ToInt32(CAR_ID.Text.Trim());
                currentContract.CONTRACT_START = contract_start;
                currentContract.CONTRACT_END = contract_end;
                try
                {
                    currentContract.PROMO_CODE = promo_code.PROMO_CODE1.Trim();
                }
                catch { }
                currentContract.TOTAL_COST = Convert.ToInt32(total_price);
                currentContract.STATUS = STATUS.Text.Trim();
                contract_status();
                CAR_RENTEntities.GetContext().CONTRACTS.Add(currentContract);
                try
                {
                    CAR_RENTEntities.GetContext().SaveChanges();
                    DGridContracts.ItemsSource = CAR_RENTEntities.GetContext().CONTRACTS.ToList();
                    DGridNewContracts.ItemsSource = CAR_RENTEntities.GetContext().CONTRACTS.Where(c => c.STATUS.Trim() == "Новая заявка").ToList();
                    Clear();
                    MessageBox.Show("Данные успешно добавлены!");
                }
                catch
                {
                    MessageBox.Show("Такая запись уже существует!");
                }
            }
            catch { }


        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime contract_start = new DateTime();
                DateTime.TryParse(CONTRACT_START.Text.Trim(), out contract_start);
                DateTime contract_end = new DateTime();
                DateTime.TryParse(CONTRACT_END.Text.Trim(), out contract_end);
                TimeSpan days = contract_end - contract_start;
                if (ID.Text.Equals(""))
                {
                    MessageBox.Show("Выделите запись, которую требуется изменить!");

                }
                StringBuilder errors = new StringBuilder();
                if (string.IsNullOrWhiteSpace(CLIENT_ID.Text.Trim()))
                {
                    errors.AppendLine("Введите клиента!");
                }
                if (string.IsNullOrWhiteSpace(CLIENT_ID.Text.Trim()))
                {
                    errors.AppendLine("Введите авто!");
                }
                if (string.IsNullOrWhiteSpace(CONTRACT_START.Text.Trim()))
                {
                    errors.AppendLine("Введите дату начала аренды!");
                }
                if (string.IsNullOrWhiteSpace(CONTRACT_END.Text.Trim()))
                {
                    errors.AppendLine("Введите дату окончания аренды!");
                }
                if (days.Days < 0)
                {
                    errors.AppendLine("Введите корректные даты!");
                }
                if (days.Days >= 31)
                {
                    errors.AppendLine("Прокат осуществляется только на 30 дней!");
                }
                if (string.IsNullOrWhiteSpace(STATUS.Text.Trim()))
                {
                    errors.AppendLine("Введите статус!");
                }
                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    return;
                }
            
                double promocode = 0;
                PROMO_CODE promo_code = null;
                CAR currentCar = CAR_RENTEntities.GetContext().CARS.Where(c => c.ID.ToString().Trim() == CAR_ID.Text.ToString().Trim()).FirstOrDefault();
                if (PROMO_CODE.Text.Length != 0)
                {
                    promo_code = CAR_RENTEntities.GetContext().PROMO_CODE.Where(c => c.PROMO_CODE1.Trim() == PROMO_CODE.Text.Trim()).Single();
                    promocode = Convert.ToDouble(promo_code.DISCOUNT_AMOUNT.Trim()) * 0.01;
                }
                double total_price = Convert.ToInt32(currentCar.RENT_PRICE) * days.Days - Convert.ToInt32(currentCar.RENT_PRICE) * days.Days * promocode;
                CONTRACT currentContract = CAR_RENTEntities.GetContext().CONTRACTS.Where(c => c.ID.ToString().Trim() == ID.Text.ToString().Trim()).FirstOrDefault();
                currentContract.CLIENT_ID = Convert.ToInt32(CLIENT_ID.Text.Trim());
                currentContract.CAR_ID = Convert.ToInt32(CAR_ID.Text.Trim());
                currentContract.CONTRACT_START = contract_start;
                currentContract.CONTRACT_END = contract_end;
                try
                {
                    currentContract.PROMO_CODE = promo_code.PROMO_CODE1.Trim();
                }
                catch { }
                currentContract.TOTAL_COST = Convert.ToInt32(total_price);
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
        private void Delete_Click(object sender, RoutedEventArgs ec)
        {
            try
            {
                if (ID.Text.Equals(""))
                {
                    MessageBox.Show("Выделите запись, которую требуется удалить!");

                }
                CONTRACT currentContract = CAR_RENTEntities.GetContext().CONTRACTS.Where(m => m.ID.ToString().Trim() == ID.Text.ToString().Trim()).FirstOrDefault();
                if (currentContract != null)
                {
                    CAR_RENTEntities.GetContext().CONTRACTS.Remove(currentContract);
                    try
                    {
                        CAR_RENTEntities.GetContext().SaveChanges();
                        DGridContracts.ItemsSource = CAR_RENTEntities.GetContext().CONTRACTS.ToList();
                        DGridNewContracts.ItemsSource = CAR_RENTEntities.GetContext().CONTRACTS.Where(c => c.STATUS.Trim() == "Новая заявка").ToList();
                        Clear();
                        MessageBox.Show("Запись удалена!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (currentContract == null && string.IsNullOrEmpty(ID.Text.Trim()) == false)
                {
                    MessageBox.Show("Такого контракта не существует!");
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
                        PROMO_CODE.SelectedValue = null;
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
