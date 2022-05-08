using CAR_RENT.models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CAR_RENT
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static CLIENT currentClient { get; set; }
        public static CLIENT admin { get; set; }
        public App()
        {
            App.currentClient = new CLIENT();
        }
    }
}
