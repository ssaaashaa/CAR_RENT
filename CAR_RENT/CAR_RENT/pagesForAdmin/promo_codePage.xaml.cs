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
    /// Логика взаимодействия для promo_codePage.xaml
    /// </summary>
    public partial class promo_codePage : Page
    {
        public promo_codePage()
        {
            InitializeComponent();
            try
            {
                DGridPromocode.ItemsSource = CAR_RENTEntities.GetContext().PROMO_CODE.ToList();
                DISCOUNT_AMOUNT.PreviewTextInput += new TextCompositionEventHandler(numbers);
                PROMO_CODE.PreviewTextInput += new TextCompositionEventHandler(lettersAndNumbers);

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
        void lettersAndNumbers(object sender, TextCompositionEventArgs e)
        {
            try
            {
                if (!Char.IsLetterOrDigit(e.Text, 0))
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
        public void Clear()
        {
            try
            {
                PROMO_CODE.Clear();
                DISCOUNT_AMOUNT.Clear();
            }
            catch { }
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
          
            try
            {
                StringBuilder errors = new StringBuilder();
                if(string.IsNullOrWhiteSpace(PROMO_CODE.Text.Trim())
                    && string.IsNullOrWhiteSpace(DISCOUNT_AMOUNT.Text.ToString().Trim()))
                {
                    errors.AppendLine("Необходимо заполнить все поля!");
                    MessageBox.Show(errors.ToString());
                    errors.Clear();
                    return;
                }
                if (string.IsNullOrWhiteSpace(PROMO_CODE.Text.Trim()))
                {
                    errors.AppendLine("Введите промокод!");
                }
                if (string.IsNullOrWhiteSpace(DISCOUNT_AMOUNT.Text.ToString().Trim()))
                {
                    errors.AppendLine("Введите величину скидки!");
                }
                PROMO_CODE currentPromocode = CAR_RENTEntities.GetContext().PROMO_CODE.Where(u => u.PROMO_CODE1.Trim() == PROMO_CODE.Text.Trim()).FirstOrDefault();
                if(currentPromocode != null)
                {
                    errors.AppendLine("Такой промокод уже существует!");
                }
                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    errors.Clear();
                    return;
                }
                PROMO_CODE currentPromo_code = new PROMO_CODE();
                currentPromo_code.PROMO_CODE1 = PROMO_CODE.Text.Trim();
                currentPromo_code.DISCOUNT_AMOUNT = DISCOUNT_AMOUNT.Text.Trim();
                CAR_RENTEntities.GetContext().PROMO_CODE.Add(currentPromo_code);
                CAR_RENTEntities.GetContext().SaveChanges();
                DGridPromocode.ItemsSource = CAR_RENTEntities.GetContext().PROMO_CODE.ToList();
                Clear();
                MessageBox.Show("Данные успешно добавлены!");
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
                if (PROMO_CODE.Text.Equals(""))
                {
                    MessageBox.Show("Выделите запись, которую требуется удалить!");
                }
                PROMO_CODE currentPromocode = CAR_RENTEntities.GetContext().PROMO_CODE.Where(p => p.PROMO_CODE1.Trim() == PROMO_CODE.Text.Trim()).FirstOrDefault();
                var contract = CAR_RENTEntities.GetContext().CONTRACTS.Where(c => c.PROMO_CODE.Trim() == PROMO_CODE.Text.Trim()).ToList();
                if (currentPromocode != null && contract.Count==0)
                {
                    CAR_RENTEntities.GetContext().PROMO_CODE.Remove(currentPromocode);
                    try
                    {
                        CAR_RENTEntities.GetContext().SaveChanges();
                        DGridPromocode.ItemsSource = CAR_RENTEntities.GetContext().PROMO_CODE.ToList();
                        Clear();
                        MessageBox.Show("Запись удалена!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                if(currentPromocode != null && contract.Count>=1)
                {
                    MessageBox.Show("Удаление промокода и и нформации о нем невозможно!");

                }
                else if (currentPromocode == null && string.IsNullOrEmpty(PROMO_CODE.Text.Trim()) == false)
                {
                    MessageBox.Show("Такого промокода не существует!");
                }
            }
            catch { }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                PROMO_CODE promo_code = new PROMO_CODE();
                promo_code = DGridPromocode.SelectedItem as PROMO_CODE;
                if (promo_code != null)
                {
                    PROMO_CODE.Text = promo_code.PROMO_CODE1.ToString();
                    DISCOUNT_AMOUNT.Text = promo_code.DISCOUNT_AMOUNT.ToString();
                }
                else
                {
                    MessageBox.Show("Запись не выбрана!");
                }
            }
            catch { }
           
        }
    }
}
