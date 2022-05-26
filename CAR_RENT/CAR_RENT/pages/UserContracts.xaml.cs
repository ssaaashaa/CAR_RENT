using CAR_RENT.models;
using CAR_RENT.userControls;
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

namespace CAR_RENT.pages
{
    /// <summary>
    /// Логика взаимодействия для UserAccount.xaml
    /// </summary>
    public partial class UserContracts : Page
    {
        public UserContracts()
        {
            InitializeComponent();
            try
            {
            using (CAR_RENTEntities db = new CAR_RENTEntities())
            {
                var historyContracts = from cars in db.CARS
                                       join contracts in db.CONTRACTS
                                       on cars.ID equals contracts.CAR_ID
                                       join modelss in db.MODEL_INFO
                                       on cars.MODEL equals modelss.ID
                                       where contracts.CLIENT_ID.ToString() == App.currentClient.ID.ToString()
                                       orderby contracts.ID descending
                                       select new
                                       {
                                           ContractId = contracts.ID,
                                           ContractStart = contracts.CONTRACT_START,
                                           ContractEnd = contracts.CONTRACT_END,
                                           Car = modelss.BREND + " " + modelss.MODEL,
                                           RegisterNum = cars.REGISTRATION_NUMBER,
                                           Status = contracts.CONTRACT_STATUS,
                                           TotalCost = contracts.TOTAL_COST + " BYN"
                                       };
                                           
                foreach (var historyContract in historyContracts)
                {

                    var buf = new HistoryContract(historyContract.ContractId.ToString(), historyContract.ContractStart.ToString().Remove(10), historyContract.ContractEnd.ToString().Remove(10),
                        historyContract.Car, historyContract.RegisterNum, historyContract.Status, historyContract.TotalCost.ToString());
                    buf.Margin = new Thickness(15);                    
                    buf.Width = 1200;
                    buf.Height = 150;
                    HISTORY_CONTRACTS.Children.Add(buf);
                }

            }
            }
            catch { }
        }
    }
}
