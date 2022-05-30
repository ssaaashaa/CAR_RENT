using CAR_RENT.models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CAR_RENT.pagesForAdmin
{
    /// <summary>
    /// Логика взаимодействия для clientPage.xaml
    /// </summary>
    public partial class clientsPage : Page
    {
       
        public clientsPage()
        {
            InitializeComponent();
            try
            {
                DGridClients.ItemsSource = CAR_RENTEntities.GetContext().CLIENTS.ToList();
               
                telephNum.PreviewTextInput += new TextCompositionEventHandler(numbersSearch);
            }
            catch { }

        }
      
        void numbersSearch(object sender, TextCompositionEventArgs e)
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


        
      


        private void DGridClients_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                CLIENT selectedClient = new CLIENT();
                selectedClient = DGridClients.SelectedItem as CLIENT;
                if (selectedClient != null)
                {
                    LOGIN.Text = selectedClient.LOGIN.Trim();
                    PASSWORD.Text = selectedClient.PASSWORD.Trim();
                    SURNAME.Text = selectedClient.SURNAME.Trim();
                    NAME.Text = selectedClient.NAME.Trim();
                    PATRONYMIC.Text = selectedClient.PATRONYMIC.Trim();
                    BDAY.Text = selectedClient.BDAY.ToString().Remove(10).Trim();
                    PASSPORT.Text = selectedClient.PASSPORT.Trim();
                    LICENSE_ID.Text = selectedClient.DRIVER_LICENSE_ID.Trim();
                    EXPERIENCE.Text = selectedClient.DRIVING_EXPERIENCE.Trim();
                    TELEPHONE.Text = selectedClient.TELEPHONE.Trim();
                    TYPE.Text = selectedClient.USER_TYPE.ToString().Trim();
                    ID.Text = selectedClient.ID.ToString().Trim();
                }
                else
                {
                    MessageBox.Show("Запись не выбрана!");
                }
            }
            catch { }

        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string patternTelephone = @"^\+375 \((25|29|33|44)\) [0-9]{3}-[0-9]{2}-[0-9]{2}$";
                if (!Regex.IsMatch(telephNum.Text.Trim(), patternTelephone))
                {
                    MessageBox.Show("Введите корректный номер телефона!");
                    return;
                }
                for (int i = 0; i < DGridClients.Items.Count; i++)
                {
                    string param = telephNum.Text.Trim();
                    DGridClients.ScrollIntoView(DGridClients.Items[i]);
                    DataGridRow row = (DataGridRow)DGridClients.ItemContainerGenerator.ContainerFromIndex(i);
                    TextBlock cellContentTeleph = DGridClients.Columns[7].GetCellContent(row) as TextBlock;
                    
                    if (cellContentTeleph != null && cellContentTeleph.Text.ToLower().Trim().Equals(param.ToLower()))
                        
                    {
                        object item = DGridClients.Items[i];
                        DGridClients.SelectedItem = item;
                        DGridClients.ScrollIntoView(item);
                        row.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                        CLIENT selectedClient = DGridClients.Items[i] as CLIENT;
                        if(selectedClient != null)
                        {
                            LOGIN.Text = selectedClient.LOGIN.Trim();
                            PASSWORD.Text = selectedClient.PASSWORD.Trim();
                            SURNAME.Text = selectedClient.SURNAME.Trim();
                            NAME.Text = selectedClient.NAME.Trim();
                            PATRONYMIC.Text = selectedClient.PATRONYMIC.Trim();
                            BDAY.Text = selectedClient.BDAY.ToString().Remove(10).Trim();
                            PASSPORT.Text = selectedClient.PASSPORT.Trim();
                            LICENSE_ID.Text = selectedClient.DRIVER_LICENSE_ID.Trim();
                            EXPERIENCE.Text = selectedClient.DRIVING_EXPERIENCE.Trim();
                            TELEPHONE.Text = selectedClient.TELEPHONE.Trim();
                            TYPE.Text = selectedClient.USER_TYPE.ToString().Trim();
                            ID.Text = selectedClient.ID.ToString().Trim();
                            break;
                        }
                        
                    }
                    else
                    {
                     
                        LOGIN.Text=null;
                        PASSWORD.Text=null; 
                        SURNAME.Text=null;
                        NAME.Text=null;
                        PATRONYMIC.Text=null;
                        BDAY.Text=null;
                        PASSPORT.Text=null;
                        LICENSE_ID.Text=null;
                        EXPERIENCE.Text=null;
                        TELEPHONE.Text=null;
                        TYPE.Text=null;
                        ID.Text=null;

                    }
                }
            }
            catch { }

        }

    }
}
