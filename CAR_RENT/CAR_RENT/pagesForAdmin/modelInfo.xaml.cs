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
            Load();
        }
        private void Load()
        {
            CAR_RENTEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(entry => entry.Reload());
            DGridModelInfo.ItemsSource = CAR_RENTEntities.GetContext().MODEL_INFO.ToList();

        }

        private void DGridModelInfo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MODEL_INFO selectedModel = new MODEL_INFO();
            selectedModel = DGridModelInfo.SelectedItem as MODEL_INFO;
            MODEL.Text = selectedModel.MODEL;
            YEAR_OF_ISSUE.Text = selectedModel.YEAR_OF_ISSUE.ToString().Remove(0,6).Remove(4);
            BODY_TYPE.Text = selectedModel.BODY_TYPE;
            ENGINE_CAPACITY.Text = selectedModel.ENGINE_CAPACITY;
            ENGINE_TYPE.Text = selectedModel.ENGINE_TYPE;
            TRANSMISSION.Text = selectedModel.TRANSMISSION;
            EQUIPMENT.Text = selectedModel.EQUIPMENT;
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MODEL_INFO currentModel = CAR_RENTEntities.GetContext().MODEL_INFO.Where(u => u.MODEL == MODEL.Text).Single();
                if (currentModel != null)
                {
                    CAR_RENTEntities.GetContext().MODEL_INFO.Remove(currentModel);
                    try
                    {
                        CAR_RENTEntities.GetContext().SaveChanges();
                        MessageBox.Show("Запись удалена!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    Load();

                }
               
            }
            catch 
            {
                MessageBox.Show("Для удаления записи необходимо выделить её!");
            }
        }
    }
}
