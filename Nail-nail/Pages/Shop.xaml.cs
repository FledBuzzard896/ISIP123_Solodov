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
    /// Логика взаимодействия для Shop.xaml
    /// </summary>
    public partial class Shop : Page
    {
        private List<Products> _allProducts;        // все товары после поиска (без фильтров и сортировки)
        private string _currentFilter = "Все";      // выбранный фильтр (текст из ComboBox)
        private string _currentSort = "Без сортировки";

        public Shop()
        {
            Loaded += PageLoaded;
            InitializeComponent();
        }

        private void Account_ButtonClick(object sender, RoutedEventArgs e)
        {
            if (!IUser.AppUser.isAuthorizated) NavigationService.Navigate(new Authorization());
            else NavigationService.Navigate(new Account());
        }
        private void SignUp_ButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                var master = button.Tag as Users;
                if (master != null)
                {
                    if (IUser.AppUser.isAuthorizated) NavigationService.Navigate(new RecordPage(master));
                    else MessageBox.Show("Войдите в аккаунт.", "Отказано", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            }
        }
        private void Records_ButtonClick(object sender, RoutedEventArgs e) 
        {
            NavigationService.Navigate(new MainPage());
        }
        private void Enter_Click(object sender, KeyEventArgs e) 
        {
            if (e.Key != Key.Enter) return;

            string searchQuery = searchTB.Text.Trim();
            if (string.IsNullOrEmpty(searchQuery))
            {
                _allProducts = Core.ContextKIP.Products.ToList();
            }
            else
            {
                _allProducts = Core.ContextKIP.Products
                    .Where(p => p.ProductName.Contains(searchQuery))
                    .ToList();
            }
            ApplyFiltersAndSort();
        }
        private void BuyDeleteProduct_Click(object sender, RoutedEventArgs e) 
        {
            Button button = sender as Button;
            Products product = button?.Tag as Products;
            if (product == null) return;

            var cart = IUser.AppUser.ProductsInCart;
            if (button.Content.ToString() == "Купить")
            {
                button.Background = Brushes.DarkRed;
                button.Content = $"Удалить";
                cart.Add(product);
            }
            else
            {
                button.Background = Brushes.LightGray;
                button.Content = "Купить";
                cart.Remove(product);
            }
        }

        private void filter_box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _currentFilter = filter_box.SelectedItem?.ToString() ?? "Все";
            ApplyFiltersAndSort();
        }
        private void sorting_box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _currentSort = sorting_box.SelectedItem?.ToString() ?? "Без сортировки";
            ApplyFiltersAndSort();
        }

        private void ApplyFiltersAndSort()
        {
            if (_allProducts == null) return;

            IEnumerable<Products> query = _allProducts;

            // --- Фильтрация ---
            if (_currentFilter != "Все")
            {
                // Определяем, что выбрано: тип или производитель (по суффиксу в скобках)
                if (_currentFilter.EndsWith("(тип)"))
                {
                    string typeName = _currentFilter.Replace("(тип)", "").Trim();
                    query = query.Where(p => p.ProductTypes.TypeName == typeName);
                }
                else if (_currentFilter.EndsWith("(производитель)"))
                {
                    string manufName = _currentFilter.Replace("(производитель)", "").Trim();
                    query = query.Where(p => p.Manufacturers.ManufacturerName == manufName);
                }
            }

            // --- Сортировка ---
            switch (_currentSort)
            {
                case "Рейтинг (убывание)":
                    query = query.OrderByDescending(p => p.Rating);
                    break;
                case "Рейтинг (возрастание)":
                    query = query.OrderBy(p => p.Rating);
                    break;
                default:
                    query = query.OrderBy(p => p.ProductName);
                    break;
            }

            // Применяем к UI
            Assignment(query.ToList());
        }
        private void Assignment(List<Products> inputLst) 
        {
            // Нужная выборка
            var Products = inputLst
                .Select(x => new {
                    ProductObject = x,
                    ProductName = x.ProductName,
                    Rating = x.Rating,
                    Description = x.Description,
                    TypeName = x.ProductTypes.TypeName,
                    ManufacturerName = x.Manufacturers.ManufacturerName,
                    RawPrice = x.Price,
                    DiscountPercent = x.DiscountPercent,
                }).ToList()
                .Select(x => new {
                    x.ProductObject,
                    x.ProductName,
                    x.Rating,
                    x.Description,
                    x.TypeName,
                    x.ManufacturerName,
                    Price = Math.Round(x.RawPrice - (x.RawPrice * x.DiscountPercent / 100), 2), // Расчет цены со скидкой
                    x.DiscountPercent,
                    // Флаг для скидки
                    IsHighDiscount = x.DiscountPercent > 15
                }).ToList();

            ProductsLB.ItemsSource = Products;
        }
        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            if (IUser.AppUser.isAuthorizated)
            {
                AccountLogIn.Visibility = Visibility.Collapsed;
                Account.Visibility = Visibility.Visible;
            }
            else
            {
                AccountLogIn.Visibility = Visibility.Visible;
                Account.Visibility = Visibility.Collapsed;
            }

            _allProducts = Core.ContextKIP.Products.ToList();
            ApplyFiltersAndSort();

            List<string> filterItems = new List<string> { "Все" };
            foreach (var type in Core.ContextKIP.ProductTypes.ToList())
                filterItems.Add($"{type.TypeName} (тип)");
            foreach (var manuf in Core.ContextKIP.Manufacturers.ToList())
                filterItems.Add($"{manuf.ManufacturerName} (производитель)");
            filter_box.ItemsSource = filterItems;

            sorting_box.ItemsSource = new List<string>
            {
                "Без сортировки",
                "Рейтинг (убывание)",
                "Рейтинг (возрастание)"
            };
        }

        private void GoToCart_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Cart());
        }
    }
}
