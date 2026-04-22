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
    /// Логика взаимодействия для Cart.xaml
    /// </summary>
    public partial class Cart : Page
    {
        public Cart()
        {
            InitializeComponent();
            Loaded += PageLoaded;
        }

        private void PaymentBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            var Products = IUser.AppUser.ProductsInCart
                .Select(x => new
                {
                    ProductName = x.ProductName,
                    Rating = x.Rating,
                    Description = x.Description,
                    TypeName = x.ProductTypes.TypeName,
                    ManufacturerName = x.Manufacturers.ManufacturerName,
                    RawPrice = x.Price,
                    DiscountPercent = x.DiscountPercent,
                }).ToList()
                .Select(x => new
                {
                    x.ProductName,
                    x.Rating,
                    x.Description,
                    x.TypeName,
                    x.ManufacturerName,
                    Price = Math.Round(x.RawPrice - (x.RawPrice * x.DiscountPercent / 100), 2), // Расчет цены со скидкой
                    x.DiscountPercent,
                    IsHighDiscount = x.DiscountPercent > 15
                }).ToList();

            ShoppingCart_LB.ItemsSource = Products;
        }
    }
}
