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
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(CONTRACT_ID.Text))
            {
                errors.AppendLine("Введите ID контракта!");
            }
            if (string.IsNullOrWhiteSpace(DATE.Text))
            {
                errors.AppendLine("Введите дату ДТП!");
            }
            if (string.IsNullOrWhiteSpace(DAMAGE_COST.Text))
            {
                errors.AppendLine("Введите величину ущерба!");
            }
            if (string.IsNullOrWhiteSpace(DAMAGE_DESCRIPTION.Text))
            {
                errors.AppendLine("Введите описание повреждения!");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            ACCIDENT currentAccident=new ACCIDENT();
            currentAccident.ID = Convert.ToInt32(ID.Text);
            currentAccident.CONTRACT_ID = Convert.ToInt32(CONTRACT_ID.Text);
            currentAccident.DATE = Convert.ToDateTime(DATE.Text);
            currentAccident.DAMAGE_COST = Convert.ToInt32(DAMAGE_COST.Text);
            currentAccident.DAMAGE_DESCRIPTION = DAMAGE_DESCRIPTION.Text;
            CAR_RENTEntities.GetContext().ACCIDENTS.Add(currentAccident); 
            try
            {
                CAR_RENTEntities.GetContext().SaveChanges();
                DGridAccidents.ItemsSource = CAR_RENTEntities.GetContext().ACCIDENTS.ToList();
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
            if (string.IsNullOrWhiteSpace(CONTRACT_ID.Text))
            {
                errors.AppendLine("Введите ID контракта!");
            }
            if (string.IsNullOrWhiteSpace(DATE.Text))
            {
                errors.AppendLine("Введите дату ДТП!");
            }
            if (string.IsNullOrWhiteSpace(DAMAGE_COST.Text))
            {
                errors.AppendLine("Введите величину ущерба!");
            }
            if (string.IsNullOrWhiteSpace(DAMAGE_DESCRIPTION.Text))
            {
                errors.AppendLine("Введите описание повреждения!");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            try
            {
                ACCIDENT currentAccident =CAR_RENTEntities.GetContext().ACCIDENTS.Where(a=>a.ID.ToString() == ID.Text.ToString()).FirstOrDefault();
                currentAccident.ID = Convert.ToInt32(ID.Text);
                currentAccident.CONTRACT_ID = Convert.ToInt32(CONTRACT_ID.Text);
                currentAccident.DATE = Convert.ToDateTime(DATE.Text);
                currentAccident.DAMAGE_COST = Convert.ToInt32(DAMAGE_COST.Text);
                currentAccident.DAMAGE_DESCRIPTION = DAMAGE_DESCRIPTION.Text;
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
                //MessageBox.Show(ex.Message);
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
            if (ID.Text.Equals(""))
            {
                MessageBox.Show("Выделите запись, которую требуется удалить!");

            }
            ACCIDENT currentAccident = CAR_RENTEntities.GetContext().ACCIDENTS.Where(a => a.ID.ToString() == ID.Text.ToString()).FirstOrDefault();
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
            else if (currentAccident == null && string.IsNullOrEmpty(ID.Text) == false)
            {
                MessageBox.Show("Такого акта ДТП не существует!");
            }
        }


    }
}
