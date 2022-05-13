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
    /// Логика взаимодействия для LimitedInputUserControl.xaml
    /// </summary>
    public partial class LimitedInputUserControl : UserControl
    {
        public LimitedInputUserControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        public static DependencyProperty TitleDependencyProperty;
        public static DependencyProperty MaxLengthDependencyProperty;

        static LimitedInputUserControl()
        {
            TitleDependencyProperty = DependencyProperty.Register("Title", typeof(string), typeof(LimitedInputUserControl));
            MaxLengthDependencyProperty = DependencyProperty.Register("MaxLength", typeof(int), typeof(LimitedInputUserControl));
        }
        public string Title
        {
            get { return (string)GetValue(TitleDependencyProperty); }
            set { SetValue(TitleDependencyProperty, value); }
        }
        public int MaxLength
        {
            get { return (int)GetValue(MaxLengthDependencyProperty); }
            set { SetValue(MaxLengthDependencyProperty, value); }
        }
    }
}
