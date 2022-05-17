using CAR_RENT.models;
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
    /// Логика взаимодействия для modelInfo.xaml
    /// </summary>
    public partial class modelInfo : Page
    {
        public modelInfo()
        {
            InitializeComponent();
            DGridModelInfo.ItemsSource = CAR_RENTEntities.GetContext().MODEL_INFO.ToList();
            ENGINE_CAPACITY.PreviewTextInput += new TextCompositionEventHandler(engine_capacityTextInput);
        }

        private void DGridModelInfo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MODEL_INFO selectedModel = new MODEL_INFO();
            selectedModel = DGridModelInfo.SelectedItem as MODEL_INFO;
            if (selectedModel != null)
            {
                MODEL.Text = selectedModel.MODEL;
                YEAR_OF_ISSUE.Text = selectedModel.YEAR_OF_ISSUE.ToString().Remove(0, 6).Remove(4);
                BODY_TYPE.Text = selectedModel.BODY_TYPE;
                ENGINE_CAPACITY.Text = selectedModel.ENGINE_CAPACITY;
                ENGINE_TYPE.Text = selectedModel.ENGINE_TYPE;
                TRANSMISSION.Text = selectedModel.TRANSMISSION;
                EQUIPMENT.Text = selectedModel.EQUIPMENT;
            }
            else
            {
                MessageBox.Show("Запись не выбрана!");
            }
        }
        void engine_capacityTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true; //не обрабатывать введеный символ
            }
        }
        void Clear()
        {
            MODEL.Clear();
            YEAR_OF_ISSUE.Clear();
            BODY_TYPE.Clear();
            ENGINE_TYPE.SelectedIndex = -1;
            ENGINE_CAPACITY.Clear();
            TRANSMISSION.SelectedIndex = -1;
            EQUIPMENT.Clear();
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            DateTime date = new DateTime();
            DateTime.TryParse(YEAR_OF_ISSUE.Text, out date);
            if (string.IsNullOrWhiteSpace(MODEL.Text))
            {
                errors.AppendLine("Введите модель!");
            }
            if (string.IsNullOrWhiteSpace(YEAR_OF_ISSUE.Text))
            {
                errors.AppendLine("Введите год выпуска!");
            }
            if (string.IsNullOrWhiteSpace(BODY_TYPE.Text))
            {
                errors.AppendLine("Введите тип кузова!");
            }
            if (string.IsNullOrWhiteSpace(ENGINE_TYPE.Text))
            {
                errors.AppendLine("Введите объем двигтеля!");
            }
            if (ENGINE_TYPE.SelectedIndex == -1)
            {
                errors.AppendLine("Выберите тип двигателя!");
            }
            if (TRANSMISSION.SelectedIndex == -1)
            {
                errors.AppendLine("Выберите трансмиссию!");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            MODEL_INFO currentModel_info = new MODEL_INFO();
            currentModel_info.MODEL = MODEL.Text;
            currentModel_info.YEAR_OF_ISSUE = date;
            currentModel_info.BODY_TYPE = BODY_TYPE.Text;
            currentModel_info.ENGINE_CAPACITY = ENGINE_CAPACITY.Text;
            currentModel_info.ENGINE_TYPE = ENGINE_TYPE.Text;
            currentModel_info.TRANSMISSION = TRANSMISSION.Text;
            currentModel_info.EQUIPMENT = EQUIPMENT.Text;
            CAR_RENTEntities.GetContext().MODEL_INFO.Add(currentModel_info);
            try
            {
                CAR_RENTEntities.GetContext().SaveChanges();
                DGridModelInfo.ItemsSource = CAR_RENTEntities.GetContext().MODEL_INFO.ToList();
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
            StringBuilder errors = new StringBuilder();
            DateTime date = new DateTime();
            DateTime.TryParse(YEAR_OF_ISSUE.Text, out date);
            if (string.IsNullOrWhiteSpace(MODEL.Text))
            {
                errors.AppendLine("Введите модель!");
            }
            if (string.IsNullOrWhiteSpace(YEAR_OF_ISSUE.Text))
            {
                errors.AppendLine("Введите год выпуска!");
            }
            if (string.IsNullOrWhiteSpace(BODY_TYPE.Text))
            {
                errors.AppendLine("Введите тип кузова!");
            }
            if (string.IsNullOrWhiteSpace(ENGINE_TYPE.Text))
            {
                errors.AppendLine("Введите объем двигтеля!");
            }
            if (ENGINE_TYPE.SelectedIndex == -1)
            {
                errors.AppendLine("Выберите тип двигателя!");
            }
            if (TRANSMISSION.SelectedIndex == -1)
            {
                errors.AppendLine("Выберите трансмиссию!");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show("Проверьте введенные данные либо выделите запись, которую требуется изменить!");
                MessageBox.Show(errors.ToString());
                return;
            }
            
            try
            {
                MODEL_INFO currentModel_info = CAR_RENTEntities.GetContext().MODEL_INFO.Where(m => m.MODEL == MODEL.Text).FirstOrDefault();
                currentModel_info.MODEL = MODEL.Text;
                currentModel_info.YEAR_OF_ISSUE = date;
                currentModel_info.BODY_TYPE = BODY_TYPE.Text;
                currentModel_info.ENGINE_CAPACITY = ENGINE_CAPACITY.Text;
                currentModel_info.ENGINE_TYPE = ENGINE_TYPE.Text;
                currentModel_info.TRANSMISSION = TRANSMISSION.Text;
                currentModel_info.EQUIPMENT = EQUIPMENT.Text;
                if (currentModel_info != null)
                {
                    try 
                    {
                    CAR_RENTEntities.GetContext().SaveChanges();
                    DGridModelInfo.ItemsSource = CAR_RENTEntities.GetContext().MODEL_INFO.ToList();
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
                if (string.IsNullOrEmpty(MODEL.Text) == false)
                {
                    MessageBox.Show("Такой модели авто не существует!");
                }
            }
        } 
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MODEL.Text.Equals(""))
            {
                MessageBox.Show("Проверьте введенные данные либо выделите запись, которую требуется удалить!");

            }
            MODEL_INFO currentModel_info=CAR_RENTEntities.GetContext().MODEL_INFO.Where(m=>m.MODEL==MODEL.Text).FirstOrDefault();
            if(currentModel_info != null)
            {
                CAR_RENTEntities.GetContext().MODEL_INFO.Remove(currentModel_info);
                try
                {
                    CAR_RENTEntities.GetContext().SaveChanges();
                    DGridModelInfo.ItemsSource = CAR_RENTEntities.GetContext().MODEL_INFO.ToList();
                    Clear();
                    MessageBox.Show("Запись удалена!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if(currentModel_info == null && string.IsNullOrEmpty(MODEL.Text) == false)
            {
                MessageBox.Show("Такой модели авто не существует!");
            }
        }
    }
}
