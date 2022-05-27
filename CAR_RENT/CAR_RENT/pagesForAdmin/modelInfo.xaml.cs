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
    /// Логика взаимодействия для modelInfo.xaml
    /// </summary>
    public partial class modelInfo : Page
    {
        public modelInfo()
        {
            InitializeComponent();
            try
            {
                DGridModelInfo.ItemsSource = CAR_RENTEntities.GetContext().MODEL_INFO.ToList();
                BREND.PreviewTextInput += new TextCompositionEventHandler(lettersAndNumbers);
                MODEL.PreviewTextInput += new TextCompositionEventHandler(lettersAndNumbers);
                YEAR_OF_ISSUE.PreviewTextInput += new TextCompositionEventHandler(date);
                ENGINE_TYPE.PreviewTextInput += new TextCompositionEventHandler(letters);
                ENGINE_CAPACITY.PreviewTextInput += new TextCompositionEventHandler(numbers);
            }
            catch { }
          
        }
        void lettersAndNumbers(object sender, TextCompositionEventArgs e)
        {
            try
            {
                if (!Char.IsLetterOrDigit(e.Text, 0) && e.Text != "-" && e.Text != "'" && e.Text != "." && e.Text != ",")
                {
                    e.Handled = true; //не обрабатывать введеный символ
                }
            }
            catch { }
        }
        private void date(object sender, TextCompositionEventArgs e)
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
        void letters(object sender, TextCompositionEventArgs e)
        {
            try
            {
                if (!Char.IsLetter(e.Text, 0) && e.Text != "-" && e.Text != "'")
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
        private void DGridModelInfo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                MODEL_INFO selectedModel = new MODEL_INFO();
                selectedModel = DGridModelInfo.SelectedItem as MODEL_INFO;
                if (selectedModel != null)
                {
                    ID.Text = selectedModel.ID.ToString().Trim();
                    MODEL.Text = selectedModel.MODEL.Trim();
                    BREND.Text = selectedModel.BREND.Trim();
                    YEAR_OF_ISSUE.Text = selectedModel.YEAR_OF_ISSUE.ToString().Remove(0, 6).Remove(4).Trim();
                    BODY_TYPE.Text = selectedModel.BODY_TYPE.Trim();
                    try
                    {
                        ENGINE_CAPACITY.Text = selectedModel.ENGINE_CAPACITY.Trim();
                    }
                    catch { }
                    ENGINE_TYPE.Text = selectedModel.ENGINE_TYPE.Trim();
                    TRANSMISSION.Text = selectedModel.TRANSMISSION.Trim();
                    EQUIPMENT.Text = selectedModel.EQUIPMENT.Trim();
                }
                else
                {
                    MessageBox.Show("Запись не выбрана!");
                }
            }
            catch
            {

            }
           
        }
    
        void Clear()
        {
            try
            {
                ID.Clear();
                MODEL.Clear();
                BREND.Clear();
                YEAR_OF_ISSUE.Clear();
                BODY_TYPE.Clear();
                ENGINE_TYPE.SelectedIndex = -1;
                ENGINE_CAPACITY.Clear();
                TRANSMISSION.SelectedIndex = -1;
                EQUIPMENT.Clear();
            }
            catch{ }
      
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                using (CAR_RENTEntities db = new CAR_RENTEntities())
                {
                    StringBuilder errors = new StringBuilder();
                    DateTime date = new DateTime();
                    DateTime.TryParse("01.01." + YEAR_OF_ISSUE.Text.Trim(), out date);
                    if (string.IsNullOrWhiteSpace(BREND.Text.Trim()))
                    {
                        errors.AppendLine("Введите марку!");
                    }

                    if (string.IsNullOrWhiteSpace(MODEL.Text.Trim()))
                    {
                        errors.AppendLine("Введите модель!");
                    }

                    if (string.IsNullOrWhiteSpace(YEAR_OF_ISSUE.Text.Trim()))
                    {
                        errors.AppendLine("Введите год выпуска!");
                    }

                    if (string.IsNullOrWhiteSpace(BODY_TYPE.Text.Trim()))
                    {
                        errors.AppendLine("Введите тип кузова!");
                    }

                    if (string.IsNullOrWhiteSpace(ENGINE_TYPE.Text.Trim()))
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
                    currentModel_info.BREND = BREND.Text.Trim();
                    currentModel_info.MODEL = MODEL.Text.Trim();
                    currentModel_info.YEAR_OF_ISSUE = date;
                    currentModel_info.BODY_TYPE = BODY_TYPE.Text.Trim();
                    currentModel_info.ENGINE_CAPACITY = ENGINE_CAPACITY.Text.Trim();
                    currentModel_info.ENGINE_TYPE = ENGINE_TYPE.Text.Trim();
                    currentModel_info.TRANSMISSION = TRANSMISSION.Text.Trim();
                    currentModel_info.EQUIPMENT = EQUIPMENT.Text.Trim();
                    CAR_RENTEntities.GetContext().MODEL_INFO.Add(currentModel_info);
                    CAR_RENTEntities.GetContext().SaveChanges();
                    DGridModelInfo.ItemsSource = CAR_RENTEntities.GetContext().MODEL_INFO.ToList();
                    Clear();
                    MessageBox.Show("Данные успешно добавлены!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                using (CAR_RENTEntities db = new CAR_RENTEntities())
                {
                    MODEL_INFO currentModel_info = CAR_RENTEntities.GetContext().MODEL_INFO.Where(m => m.ID.ToString().Trim() == ID.Text.ToString().Trim()).FirstOrDefault();
                    if (currentModel_info == null)
                    {
                        MessageBox.Show("Необходимо выбрать запись для редактирования!");
                        return;
                    }
                    StringBuilder errors = new StringBuilder();
                    DateTime date = new DateTime();
                    DateTime.TryParse("01.01." + YEAR_OF_ISSUE.Text.Trim(), out date);

                    if (string.IsNullOrWhiteSpace(BREND.Text.Trim()))
                    {
                        errors.AppendLine("Введите марку!");
                    }

                    if (string.IsNullOrWhiteSpace(MODEL.Text.Trim()))
                    {
                        errors.AppendLine("Введите модель!");
                    }

                    if (string.IsNullOrWhiteSpace(YEAR_OF_ISSUE.Text.Trim()))
                    {
                        errors.AppendLine("Введите год выпуска!");
                    }

                    if (string.IsNullOrWhiteSpace(BODY_TYPE.Text.Trim()))
                    {
                        errors.AppendLine("Введите тип кузова!");
                    }

                    if (string.IsNullOrWhiteSpace(ENGINE_CAPACITY.Text.Trim()))
                    {
                        errors.AppendLine("Введите объем двигателя!");
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
                    currentModel_info.BREND = BREND.Text.Trim();
                    currentModel_info.MODEL = MODEL.Text.Trim();
                    currentModel_info.YEAR_OF_ISSUE = date;
                    currentModel_info.BODY_TYPE = BODY_TYPE.Text.Trim();
                    currentModel_info.ENGINE_CAPACITY = ENGINE_CAPACITY.Text.Trim();
                    currentModel_info.ENGINE_TYPE = ENGINE_TYPE.Text.Trim();
                    currentModel_info.TRANSMISSION = TRANSMISSION.Text.Trim();
                    currentModel_info.EQUIPMENT = EQUIPMENT.Text.Trim();
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
            }
            catch
            {
                if (string.IsNullOrEmpty(ID.Text.Trim()) == false)
                {
                    MessageBox.Show("Такой модели авто не существует!");
                }
            }
        } 
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MODEL.Text.Equals(""))
                {
                    MessageBox.Show("Проверьте введенные данные либо выделите запись, которую требуется удалить!");

                }
                MODEL_INFO currentModel_info = CAR_RENTEntities.GetContext().MODEL_INFO.Where(m => m.ID.ToString().Trim() == ID.Text.ToString().Trim()).FirstOrDefault();
                if (currentModel_info != null)
                {
                    CAR_RENTEntities.GetContext().MODEL_INFO.Remove(currentModel_info);
                    try
                    {
                        CAR_RENTEntities.GetContext().SaveChanges();
                        DGridModelInfo.ItemsSource = CAR_RENTEntities.GetContext().MODEL_INFO.ToList();
                        Clear();
                        MessageBox.Show("Запись удалена!");
                    }
                    catch
                    {
                        MessageBox.Show("Даннная машина зарегестрирована для проката! Удаление данных о ней невозможно!");

                    }

                }
                else if (currentModel_info == null && string.IsNullOrEmpty(ID.Text.Trim()) == false)
                {
                    MessageBox.Show("Такой модели авто не существует!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void search_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                DGridModelInfo.ItemsSource = CAR_RENTEntities.GetContext().MODEL_INFO.ToList();
                for (int i = 0; i < DGridModelInfo.Items.Count; i++)
                {
                    string param = id.Text;
                    DGridModelInfo.ScrollIntoView(DGridModelInfo.Items[i]);
                    DataGridRow row = (DataGridRow)DGridModelInfo.ItemContainerGenerator.ContainerFromIndex(i);
                    TextBlock cellContentID = DGridModelInfo.Columns[0].GetCellContent(row) as TextBlock;
                    if (cellContentID != null && cellContentID.Text.ToLower().Trim().Equals(param.ToLower()))
                    {
                        object item = DGridModelInfo.Items[i];
                        DGridModelInfo.SelectedItem = item;
                        DGridModelInfo.ScrollIntoView(item);
                        row.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                        break;
                    }
                }
            }
            catch { }
        }

    }
}
