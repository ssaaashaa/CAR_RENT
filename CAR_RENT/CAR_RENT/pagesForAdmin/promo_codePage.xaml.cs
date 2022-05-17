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
            DGridPromocode.ItemsSource = CAR_RENTEntities.GetContext().PROMO_CODE.ToList();
            DISCOUNT_AMOUNT.PreviewTextInput += new TextCompositionEventHandler(discount_amountTextInput);
        }
        public void Clear()
        {
            PROMO_CODE.Clear();
            DISCOUNT_AMOUNT.Clear();
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(PROMO_CODE.Text))
            {
                errors.AppendLine("Введите промокод!");
            }
            if (string.IsNullOrWhiteSpace(DISCOUNT_AMOUNT.Text.ToString()))
            {
                errors.AppendLine("Введите величину скидки!");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            PROMO_CODE currentPromo_code = new PROMO_CODE();
            currentPromo_code.PROMO_CODE1 = PROMO_CODE.Text;
            currentPromo_code.DISCOUNT_AMOUNT = DISCOUNT_AMOUNT.Text;
            CAR_RENTEntities.GetContext().PROMO_CODE.Add(currentPromo_code);
            try
            {
                CAR_RENTEntities.GetContext().SaveChanges();
                DGridPromocode.ItemsSource = CAR_RENTEntities.GetContext().PROMO_CODE.ToList();
                Clear();
                MessageBox.Show("Данные успешно добавлены!");
            }
            catch 
            {
                MessageBox.Show("Такая запись уже существует!");
            }
        }  
        void discount_amountTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true; //не обрабатывать введеный символ
            }
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (PROMO_CODE.Text.Equals(""))
            {
                MessageBox.Show("Проверьте введенные данные либо выделите запись, которую требуется изменить!");
            }
            try
            {
                PROMO_CODE currentPromocode = CAR_RENTEntities.GetContext().PROMO_CODE.Where(u => u.PROMO_CODE1 == PROMO_CODE.Text).FirstOrDefault();
                currentPromocode.PROMO_CODE1 = PROMO_CODE.Text;
                currentPromocode.DISCOUNT_AMOUNT = DISCOUNT_AMOUNT.Text;
                if (currentPromocode != null)
                {
                    try
                    {
                        CAR_RENTEntities.GetContext().SaveChanges();
                        DGridPromocode.ItemsSource = CAR_RENTEntities.GetContext().PROMO_CODE.ToList();
                        Clear();
                        MessageBox.Show("Данные успешно обновлены!");
                    }
                    catch
                    {
                        MessageBox.Show("Необходимо выбрать запись для редактирования!");
                    }
                }

            }
            catch
            {
                if (string.IsNullOrEmpty(PROMO_CODE.Text) == false)
                {
                    MessageBox.Show("Такого промокода не существует!");
                }
            }
        }
       
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (PROMO_CODE.Text.Equals(""))
            {
                MessageBox.Show("Проверьте введенные данные либо выделите запись, которую требуется удалить!");
            }
                PROMO_CODE currentPromocode = CAR_RENTEntities.GetContext().PROMO_CODE.Where(p => p.PROMO_CODE1 == PROMO_CODE.Text).FirstOrDefault();
                if (currentPromocode != null)
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
                else if(currentPromocode == null&&string.IsNullOrEmpty(PROMO_CODE.Text)==false)
                {
                    MessageBox.Show("Такого промокода не существует!");
                }           
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
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
    }
}
