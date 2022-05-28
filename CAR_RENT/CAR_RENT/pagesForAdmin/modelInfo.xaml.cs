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
