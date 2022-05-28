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

namespace CAR_RENT.userControls
{
    /// <summary>
    /// Логика взаимодействия для PopUp.xaml
    /// </summary>
    public partial class PopUp : UserControl
    {
        public PopUp()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty TextDependencyProperty;
        static PopUp()
        {
            TextDependencyProperty = DependencyProperty.Register("TextProperty", typeof(string), typeof(PopUp));

        }
        public string TextProperty
        {
            get { return (string)GetValue(TextDependencyProperty); }
            set { SetValue(TextDependencyProperty, value); }
        }
    }
}
