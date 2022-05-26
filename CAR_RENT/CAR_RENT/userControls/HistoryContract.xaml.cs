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
    /// Логика взаимодействия для HistoryContract.xaml
    /// </summary>
    public partial class HistoryContract : UserControl
    {
        public HistoryContract(string ContractId, string ContractStart, string ContractEnd, string Car, string RegisterNum, string Status, string TotalCost)
        {
            InitializeComponent();
            try
            {
                CONTRACT_ID.Text += ContractId;
                CONTRACT_START.Text = ContractStart;
                CONTRACT_END.Text = ContractEnd;
                CAR.Text = Car;
                REGISTRATION_NUMBER.Text = RegisterNum;
                STATUS.Text = Status;
                TOTAL_COST.Text = TotalCost;
            }
            catch { }
        }
    }
}
