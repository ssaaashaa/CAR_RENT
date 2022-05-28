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
                DATE.PreviewTextInput+= new TextCompositionEventHandler(dateInput);
                DAMAGE_COST.PreviewTextInput += new TextCompositionEventHandler(numbers);
                id.PreviewTextInput += new TextCompositionEventHandler(numbers);

                ListView listIDcontracts = new ListView();
                using (CAR_RENTEntities db = new CAR_RENTEntities())
                {
                    var currentContracts = (from contracts in db.CONTRACTS
                                            select new
                                            {
                                                ID = contracts.ID

                                            }).ToList().OrderBy(c => c.ID);
                    foreach (var contract in currentContracts)
                    {
                        CONTRACT_ID.Items.Add(contract.ID);

                    }
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
        void Clear()
        {
            try
            {
                ID.Clear();
                CONTRACT_ID.SelectedValue = null;
                DATE.Clear();
                DAMAGE_COST.Clear();
                DAMAGE_DESCRIPTION.Clear();
            }
            catch { }

        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder errors = new StringBuilder();
                CONTRACT contract = CAR_RENTEntities.GetContext().CONTRACTS.Where(c => c.ID.ToString().Trim() == CONTRACT_ID.Text.ToString().Trim()).FirstOrDefault();
                DateTime contract_start = new DateTime();
                DateTime contract_end = new DateTime();
                try
                {
                    DateTime.TryParse(contract.CONTRACT_START.ToString().Trim(), out contract_start);
                    DateTime.TryParse(contract.CONTRACT_END.ToString().Trim(), out contract_end);
                }
                catch
                {
                    errors.AppendLine("Необходимо заполнить все поля!");
                    MessageBox.Show(errors.ToString());
                    errors.Clear();
                    return;
                }
                
                if (string.IsNullOrWhiteSpace(CONTRACT_ID.Text.Trim()))
                {
                    errors.AppendLine("Ввыберите ID контракта!");
                }
                if (string.IsNullOrWhiteSpace(DATE.Text.Trim()))
                {
                    errors.AppendLine("Введите дату ДТП!");
                }
             
                DateTime date = new DateTime();
                DateTime.TryParse(DATE.Text, out date);
                if ((date.Date<contract_start.Date && date.Date < contract_end.Date)|| (date.Date > contract_start.Date && date.Date > contract_end.Date))
                {
                    errors.AppendLine("Выбранная дата не входит в период аренды, указанного в контракте!");
                }
                if (string.IsNullOrWhiteSpace(DAMAGE_COST.Text.Trim()))
                {
                    errors.AppendLine("Введите величину ущерба!");
                }
                
                if (string.IsNullOrWhiteSpace(DAMAGE_DESCRIPTION.Text.Trim()))
                {
                    errors.AppendLine("Введите описание повреждения!");
                }
                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    errors.Clear();
                    return;
                }
                ACCIDENT currentAccident = new ACCIDENT();
                currentAccident.CONTRACT_ID = Convert.ToInt32(CONTRACT_ID.Text.Trim());

                currentAccident.DATE = Convert.ToDateTime(DATE.Text.Trim());
                currentAccident.DAMAGE_COST = Convert.ToInt32(DAMAGE_COST.Text.Trim());
                currentAccident.DAMAGE_DESCRIPTION = DAMAGE_DESCRIPTION.Text.Trim();
                CAR_RENTEntities.GetContext().ACCIDENTS.Add(currentAccident);
                CAR_RENTEntities.GetContext().SaveChanges();
                DGridAccidents.ItemsSource = CAR_RENTEntities.GetContext().ACCIDENTS.ToList();
                Clear();
                MessageBox.Show("Данные успешно добавлены!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CONTRACT contract = CAR_RENTEntities.GetContext().CONTRACTS.Where(c => c.ID.ToString().Trim() == CONTRACT_ID.Text.ToString().Trim()).FirstOrDefault();
                ACCIDENT currentAccident = CAR_RENTEntities.GetContext().ACCIDENTS.Where(a => a.ID.ToString() == ID.Text.ToString()).FirstOrDefault();
                StringBuilder errors = new StringBuilder();
                DateTime contract_start = new DateTime();
                DateTime contract_end = new DateTime();
                try
                {
                    DateTime.TryParse(contract.CONTRACT_START.ToString().Trim(), out contract_start);
                    DateTime.TryParse(contract.CONTRACT_END.ToString().Trim(), out contract_end);
                }
                catch
                {
                    errors.AppendLine("Выделите запись, которую требуется изменить!");
                    MessageBox.Show(errors.ToString());
                    errors.Clear();
                    return;
                }
                if (string.IsNullOrWhiteSpace(CONTRACT_ID.Text.Trim()))
                {
                    errors.AppendLine("Выберите ID контракта!");
                }
                if (string.IsNullOrWhiteSpace(DATE.Text.Trim()))
                {
                    errors.AppendLine("Введите дату ДТП!");
                }
                DateTime date = new DateTime();
                DateTime.TryParse(DATE.Text.Trim(), out date);
                if ((date.Date < contract_start.Date && date.Date < contract_end.Date) || (date.Date > contract_start.Date && date.Date > contract_end.Date))
                {
                    errors.AppendLine("Выбранная дата не входит в период аренды, указанного в контракте!");
                }
                if (string.IsNullOrWhiteSpace(DAMAGE_COST.Text.Trim()))
                {
                    errors.AppendLine("Введите величину ущерба!");
                }
                if (string.IsNullOrWhiteSpace(DAMAGE_DESCRIPTION.Text.Trim()))
                {
                    errors.AppendLine("Введите описание повреждения!");
                }
                if (currentAccident == null)
                {
                    errors.AppendLine("Выделите запись для редактирования!");
                }
                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    errors.Clear();
                    return;
                }
                currentAccident.ID = Convert.ToInt32(ID.Text.Trim());
                currentAccident.CONTRACT_ID = Convert.ToInt32(CONTRACT_ID.Text.Trim());
                currentAccident.DATE = Convert.ToDateTime(DATE.Text.Trim());
                currentAccident.DAMAGE_COST = Convert.ToInt32(DAMAGE_COST.Text.Trim());
                currentAccident.DAMAGE_DESCRIPTION = DAMAGE_DESCRIPTION.Text.Trim();
                if (currentAccident != null)
                {
                    try
                    {
                        CAR_RENTEntities.GetContext().SaveChanges();
                        DGridAccidents.ItemsSource = CAR_RENTEntities.GetContext().ACCIDENTS.ToList();
                        Clear();
                        MessageBox.Show("Данные успешно обновлены!");
                    }
                    catch
                    {
                        MessageBox.Show("Некорректный ID контракта!");
                    }

                }

            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                {
                    foreach (DbValidationError err in validationError.ValidationErrors)
                    {
                        MessageBox.Show(err.ErrorMessage + " ");

                    }
                }
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ID.Text.Equals(""))
                {
                    MessageBox.Show("Выделите запись, которую требуется удалить!");

                }
                ACCIDENT currentAccident = CAR_RENTEntities.GetContext().ACCIDENTS.Where(a => a.ID.ToString().Trim() == ID.Text.ToString().Trim()).FirstOrDefault();
                if (currentAccident != null)
                {
                    CAR_RENTEntities.GetContext().ACCIDENTS.Remove(currentAccident);
                    try
                    {
                        CAR_RENTEntities.GetContext().SaveChanges();
                        DGridAccidents.ItemsSource = CAR_RENTEntities.GetContext().ACCIDENTS.ToList();
                        Clear();
                        MessageBox.Show("Запись удалена!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (currentAccident == null && string.IsNullOrEmpty(ID.Text.Trim()) == false)
                {
                    MessageBox.Show("Такого акта ДТП не существует!");
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
