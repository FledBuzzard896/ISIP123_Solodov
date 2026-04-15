using Nail_nail.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public Account()
        {

            InitializeComponent();
            DataContext = IUser.AppUser;

            Loaded += PageLoaded;
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            // загрузка данных из бд
            // OrderHistory --> Orders + OrderItems
            // RecordHistory --> Appointments 

            var orders = Core.ContextHOME.Orders.Where(x => x.UserID == IUser.AppUser.UserID).ToList();
            var appointmets = Core.ContextHOME.Appointments.Where(x => x.ClientID == IUser.AppUser.UserID).ToList();

            OrderHistory.ItemsSource = orders
                .Select(x => new
                {
                    OrderDateTime = x.OrderDateTime,
                    DeliveryDate = x.DeliveryDate,
                    PaymentMethod = x.PaymentMethod,
                    IsIssued = x.IsIssued,
                    Products = x.OrderItems.Select(y => $"{y.Products.ProductName} x {y.Quantity} ({y.PriceAtOrder}₽)"),
                    TotalPrice = x.TotalPrice,
                    Comment = x.Comment,
                });

            RecordHistory.ItemsSource = appointmets
                .Select(x => new
                {
                    Master = Core.ContextHOME.Users.First(y => y.ID == x.MasterID).FullName,
                    Service = Core.ContextHOME.ServiceTypes.First(y => y.ID == x.ServiceTypeID).ServiceName,
                    AppointmentDateTime = x.AppointmentDateTime,
                    PaymentMethod = x.PaymentMethod,
                    Comment = x.Comment,
                    TotalPrice = x.TotalPrice,
                    IsCompleted = x.IsCompleted,
                    IsCancelled = x.IsCancelled,
                });
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
