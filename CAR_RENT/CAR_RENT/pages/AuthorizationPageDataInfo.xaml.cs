using CAR_RENT.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace CAR_RENT.pages
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPageDataInfo.xaml
    /// </summary>
    public partial class AuthorizationPageDataInfo : Page
    {
        public AuthorizationPageDataInfo()
        {
            InitializeComponent();
            telephone.PreviewTextInput += new TextCompositionEventHandler(telephoneTextInput);
            passportID.PreviewTextInput += new TextCompositionEventHandler(passportTextInput);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            using (CAR_RENTEntities db = new CAR_RENTEntities())
            { 
                CLIENT client = new CLIENT();
                client.LOGIN = AuthorizationPage.Log.ToString();
                client.PASSWORD = AuthorizationPage.Pass.ToString();
                client.SURNAME = AuthorizationPage.Surn.ToString();
                client.NAME = AuthorizationPage.N.ToString();
                client.PATRONYMIC = AuthorizationPage.Patron.ToString();
                client.BDAY = AuthorizationPage.Date;
                client.TELEPHONE=telephone.Text;
                client.ADRESS=adress.Text;
                client.PASSPORT_SERIES = passportSeries.Text;
                try
                {
                    client.PASSPORT_ID = Int32.Parse(passportID.Text);
                }
                catch
                {
                    MessageBox.Show("Необходимо заполнить все поля!");
                }
                client.DRIVER_LICENSE_ID=licenseID.Text;
                client.DRIVING_EXPERIENCE=experience.Text;
               
                if (telephDone.Visibility==Visibility.Visible 
                    && adressDone.Visibility==Visibility.Visible
                    &&seriesDone.Visibility==Visibility.Visible
                    &&passportDone.Visibility==Visibility.Visible
                    &&numberDone.Visibility==Visibility.Visible
                    &&numDone.Visibility==Visibility.Visible
                    &&passportID.Text!="") 
                {
                    db.CLIENTS.Add(client);
                    db.SaveChanges();
                    App.currentClient = client;
                    AuthorizattionIsDone authorizattionIsDone = new AuthorizattionIsDone();
                    NavigationService.Navigate(authorizattionIsDone);
                }
                else
                {
                    if (passportID.Text != "")
                    {
                        MessageBox.Show("Необходимо заполнить все поля!");
                    }
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void telephone_SelectionChanged(object sender, RoutedEventArgs e)
        {
            string patternTelephone = @"^\+375 \((25|29|33|44)\) [0-9]{3}-[0-9]{2}-[0-9]{2}$";
            string teleph = telephone.Text;
            if (Regex.IsMatch(teleph, patternTelephone, RegexOptions.IgnoreCase))
            {
                telephDone.Visibility = Visibility.Visible;
            }
            else return;
        }
        void telephoneTextInput(object sender, TextCompositionEventArgs e)
        {
          if (!Char.IsControl(e.Text, 0) && !Char.IsDigit(e.Text, 0) && !Char.IsWhiteSpace(e.Text, 0))
          {
                e.Handled = true; //не обрабатывать введеный символ
          }
        }

        private void aldress_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (adress.Text!="")
            {
                adressDone.Visibility = Visibility.Visible;
            }
            else
            {
                adressDone.Visibility = Visibility.Hidden;
            }
        }

        private void passportSeries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (passportSeries.SelectedIndex != -1)
            {
                seriesDone.Visibility = Visibility.Visible;
            }
            else
            {
                seriesDone.Visibility = Visibility.Hidden;
            }
        }
        void passportTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true; //не обрабатывать введеный символ
            }
        }

        private void passportID_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if(passportID.Text.Length==7)
            {
                passportDone.Visibility = Visibility.Visible;
            }
            else
            {
                passportDone.Visibility = Visibility.Hidden;
            }
        }

        private void licenseID_SelectionChanged(object sender, RoutedEventArgs e)
        {

            if (licenseID.Text != "")
            {
                numberDone.Visibility = Visibility.Visible;
            }
            else
            {
                numberDone.Visibility = Visibility.Hidden;
            }
        }

        private void experience_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (experience.Text != "")
            {
                numDone.Visibility = Visibility.Visible;
            }
            else
            {
                numDone.Visibility = Visibility.Hidden;
            }
        }
    }
}
