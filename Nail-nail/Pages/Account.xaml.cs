using Nail_nail.Classes;
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

namespace Nail_nail.Pages
{
    /// <summary>
    /// Логика взаимодействия для Account.xaml
    /// </summary>
    public partial class Account : Page
    {
        string _products = "";
        public Account()
        {

            InitializeComponent();
            DataContext = IUser.AppUser;

        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            // загрузка данных из бд
            // OrderHistory --> Orders + OrderItems
            // RecordHistory --> Appointments 

            var orders = Core.ContextHOME.Orders.Where(x => x.UserID == IUser.AppUser.UserID);
            var appointmets = Core.ContextHOME.Appointments.Where(x => x.ClientID == IUser.AppUser.UserID);

            var orderProducts = Core.ContextHOME.OrderItems.Where(x => x.OrderID == )
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
