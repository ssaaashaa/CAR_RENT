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
    /// Логика взаимодействия для UserInfoPage.xaml
    /// </summary>
    public partial class UserInfoPage : Page
    {
        private CLIENT currentClient=new CLIENT();
        public UserInfoPage()
        {
            InitializeComponent();
            login.Text = App.currentClient.LOGIN;
            password.Text = App.currentClient.PASSWORD;
            surname.Text = App.currentClient.SURNAME;
            name.Text = App.currentClient.NAME;
            patronymic.Text = App.currentClient.PATRONYMIC;
            bday.Text = App.currentClient.BDAY.ToString();
            telephone.Text = App.currentClient.TELEPHONE;
            adress.Text = App.currentClient.ADRESS;
            passportSeries.Text = App.currentClient.PASSPORT_SERIES;
            passport_id.Text = App.currentClient.PASSPORT_ID.ToString();
            license_id.Text=App.currentClient.DRIVER_LICENSE_ID.ToString();
            experience.Text = App.currentClient.DRIVING_EXPERIENCE;
            passport_id.PreviewTextInput += new TextCompositionEventHandler(passportTextInput);
            license_id.PreviewTextInput += new TextCompositionEventHandler(lettersAndNumbers);
            experience.PreviewTextInput += new TextCompositionEventHandler(lettersAndNumbers);
            surname.PreviewTextInput += new TextCompositionEventHandler(letters);
            name.PreviewTextInput += new TextCompositionEventHandler(letters);
            patronymic.PreviewTextInput += new TextCompositionEventHandler(letters);

        }

        private void login_GotFocus(object sender, RoutedEventArgs e)
        {
            loginToolTip.Visibility = Visibility.Visible;
        }

        private void login_LostFocus(object sender, RoutedEventArgs e)
        {
            loginToolTip.Visibility = Visibility.Hidden;
        }

        private void password_GotFocus(object sender, RoutedEventArgs e)
        {
            passwordToolTip.Visibility = Visibility.Visible;
        }

        private void password_LostFocus(object sender, RoutedEventArgs e)
        {
            passwordToolTip.Visibility= Visibility.Hidden;  
        }

        private void passwordConf_GotFocus(object sender, RoutedEventArgs e)
        {
            passwordConfToolTip.Visibility = Visibility.Visible;
        }

        private void passwordConf_LostFocus(object sender, RoutedEventArgs e)
        {
            passwordConfToolTip.Visibility = Visibility.Hidden;
        }

        private void surname_GotFocus(object sender, RoutedEventArgs e)
        {
            surnameToolTip.Visibility = Visibility.Visible; 
        }

        private void surname_LostFocus(object sender, RoutedEventArgs e)
        {
            surnameToolTip.Visibility = Visibility.Hidden;
        }

        private void name_GotFocus(object sender, RoutedEventArgs e)
        {
            nameToolTip.Visibility = Visibility.Visible;
        }

        private void name_LostFocus(object sender, RoutedEventArgs e)
        {
            nameToolTip.Visibility=Visibility.Hidden;   
        }

        private void patronymic_GotFocus(object sender, RoutedEventArgs e)
        {
            patronymicToolTip.Visibility = Visibility.Visible;
        }

        private void patronymic_LostFocus(object sender, RoutedEventArgs e)
        {
            patronymicToolTip.Visibility=Visibility.Hidden;
        }
        private void bday_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

            DateTime date = new DateTime();
            DateTime.TryParse(bday.Text, out date);
            DateTime today = DateTime.Today;
            if ((today.Year - date.Year) <= 18)
            {
                MessageBox.Show("Извините! Вам нет 18 лет! Мы не сможем предоставить вам автомобиль!");                
            }
            //this.NavigationService.GoBack();
        }
        void passportTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true; //не обрабатывать введеный символ
            }
        }

        private void license_id_GotFocus(object sender, RoutedEventArgs e)
        {
            licenseToolTip.Visibility = Visibility.Visible;
        }

        private void license_id_LostFocus(object sender, RoutedEventArgs e)
        {
            licenseToolTip.Visibility=Visibility.Hidden;
        }
        void lettersAndNumbers(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetterOrDigit(e.Text, 0))
            {
                e.Handled = true; //не обрабатывать введеный символ
            }
        }
        void letters(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0))
            {
                e.Handled = true; //не обрабатывать введеный символ
            }
        }
    }
}
