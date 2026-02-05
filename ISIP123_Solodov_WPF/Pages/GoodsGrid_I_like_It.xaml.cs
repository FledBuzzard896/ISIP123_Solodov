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

using ISIP123_Solodov_WPF;

namespace ISIP123_Solodov_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для GoodsGrid_I_like_It.xaml
    /// </summary>
    public partial class GoodsGrid_I_like_It : Page
    {
        public GoodsGrid_I_like_It()
        {
            InitializeComponent();

            List<Products> items = Core.Context.Products.ToList(); // лист с итемами из бд
            Products_LB.ItemsSource = items;
        }

        string id;

        private void Products_LB_MouseDoubleClick(object sender, MouseButtonEventArgs e) 
        {
            mainPanel.Visibility = Visibility.Visible;
            descriptPanel.Visibility = Visibility.Visible;
            DeleteItem.Visibility = Visibility.Collapsed;

            //mainPic.Source = ImgURL;
            Products selectedProduct = Products_LB.SelectedItem as Products;

            //mainPic.Source = new BitmapImage(new Uri(selectedProduct.ImgURL, UriKind.Absolute));
            descript.Text = selectedProduct.ProductName;
            price.Text = Convert.ToString(selectedProduct.Price);

            id = selectedProduct.ProductName;

        }
        private void MyImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mainPanel.Visibility = Visibility.Visible;
            descriptPanel.Visibility = Visibility.Visible;
            DeleteItem.Visibility = Visibility.Collapsed;

            if (sender is Image clickedImage)
            {
                mainPic.Source = clickedImage.Source;
            }

            switch (mainPic.Source.ToString()) 
            {
                case "/Images/AK-47.jpg":

                    descript.Text = "Пушка AK-47 гад дэмн";
                    price.Text = "40790,00 ₽";
                    id = "ak";

                    if (shopCart.ak > 0) { DeleteItem.Visibility = Visibility.Visible; DeleteItem.Content = $"Удалить ({shopCart.ak})"; }

                    break;

                case "pack://application:,,,/Images/belka.jpg":

                    descript.Text = "Водка Белочка: Я пришла! 40%, 500мл";
                    price.Text = "409,00 ₽";
                    id = "belka";

                    if (shopCart.ak > 0) { DeleteItem.Visibility = Visibility.Visible; DeleteItem.Content = $"Удалить ({shopCart.ak})"; }

                    break;

                case "pack://application:,,,/Images/Maus.jpg":

                    descript.Text = "Maus — легендарный сверхтяжёлый танк \nВторой мировой, 188 тонн";
                    price.Text = "2.146.011.560,00 ₽";
                    id = "maus";

                    if (shopCart.maus > 0) { DeleteItem.Visibility = Visibility.Visible; DeleteItem.Content = $"Удалить ({shopCart.maus})"; }

                    break;
                
                case "pack://application:,,,/Images/Obama.jpg":

                    descript.Text = "Надувной Бардак Обама в полный рост";
                    price.Text = "123.456,00 ₽";
                    id = "obama";

                    if (shopCart.obama > 0) { DeleteItem.Visibility = Visibility.Visible; DeleteItem.Content = $"Удалить ({shopCart.obama})"; }

                    break;

                case "pack://application:,,,/Images/oreshki.jpg":

                    descript.Text = "Боевая машина поддержки танков \n(БМПТ) «Орешник»";
                    price.Text = "613.146.160,00 ₽";
                    id = "oreshk";

                    if (shopCart.oreshk > 0) { DeleteItem.Visibility = Visibility.Visible; DeleteItem.Content = $"Удалить ({shopCart.oreshk})"; }

                    break;

                case "pack://application:,,,/Images/soyz.jpg":

                    descript.Text = "Микрофон студийный СОЮЗ 017 TUBE";
                    price.Text = "419.990,00 ₽";
                    id = "soyz";

                    if (shopCart.soyz > 0) { DeleteItem.Visibility = Visibility.Visible; DeleteItem.Content = $"Удалить ({shopCart.soyz})"; }

                    break;

                case "pack://application:,,,/Images/Trump.jpg":

                    descript.Text = "Надувной Дональд Трамп в полный рост";
                    price.Text = "148.852,67 ₽";
                    id = "trump";

                    if (shopCart.trump > 0) { DeleteItem.Visibility = Visibility.Visible; DeleteItem.Content = $"Удалить ({shopCart.trump})"; }

                    break;

                case "pack://application:,,,/Images/ushanka.jpg":

                    descript.Text = "Шапка ушанка енот";
                    price.Text = "5260,00 ₽";
                    id = "ushanka";

                    if (shopCart.ushanka > 0) { DeleteItem.Visibility = Visibility.Visible; DeleteItem.Content = $"Удалить ({shopCart.ushanka})"; }

                    break;

                case "pack://application:,,,/Images/WhiteHouse.jpg":

                    descript.Text = "Бля Белый Дом";
                    price.Text = "100,00 ₽";
                    id = "whouse";

                    if (shopCart.whouse > 0) { DeleteItem.Visibility = Visibility.Visible; DeleteItem.Content = $"Удалить ({shopCart.whouse})"; }

                    break;

            }
        }
        private void OpenProductCard(object sender, MouseButtonEventArgs e) 
        {
            List<Products> products = Core.Context.Products.ToList();

            string text = "Описание: ";

            foreach (var elem in products) 
            {
                if (elem.ProductName == descript.Text.Replace("\n", string.Empty)) { text += $"{elem.Description}\nКоличество: {elem.Count}"; }
            }

            MessageBox.Show(text, "Карточка товара.", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //shopCart shopCart = new shopCart();

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {

            List<Products> products = Core.Context.Products.ToList();

            DeleteItem.Visibility = Visibility.Visible;

            switch (id) 
            {
                case "Пушка AK-47 гад дэмн":
                    shopCart.ak++;

                    var res = products.Find(x => x.ProductName == "Пушка AK-47 гад дэмн");
                    if (shopCart.ak > res.Count) 
                    {
                        MessageBox.Show("Товаров на скалде больше нет", "Обосрано!", MessageBoxButton.OK, MessageBoxImage.Information);
                        shopCart.ak--;
                    }

                    DeleteItem.Content = $"Удалить ({shopCart.ak})";
                    break;

                case "Водка Белочка: Я пришла! 40%, 500мл":
                    shopCart.belka++;

                    res = products.Find(x => x.ProductName == "Водка Белочка: Я пришла! 40%, 500мл");
                    if (shopCart.belka > res.Count)
                    {
                        MessageBox.Show("Товаров на скалде больше нет", "Обосрано!", MessageBoxButton.OK, MessageBoxImage.Information);
                        shopCart.belka--;
                    }

                    DeleteItem.Content = $"Удалить ({shopCart.belka})";
                    break;

                case "Maus — легендарный сверхтяжёлый танк Второй мировой, 188 тонн":
                    shopCart.maus++;

                    res = products.Find(x => x.ProductName == "Maus — легендарный сверхтяжёлый танк Второй мировой, 188 тонн");
                    if (shopCart.maus > res.Count)
                    {
                        MessageBox.Show("Товаров на скалде больше нет", "Обосрано!", MessageBoxButton.OK, MessageBoxImage.Information);
                        shopCart.maus--;
                    }

                    DeleteItem.Content = $"Удалить ({shopCart.maus})";
                    break;

                case "Надувной Бардак Обама в полный рост":
                    shopCart.obama++;

                    res = products.Find(x => x.ProductName == "Надувной Бардак Обама в полный рост");
                    if (shopCart.obama > res.Count)
                    {
                        MessageBox.Show("Товаров на скалде больше нет", "Обосрано!", MessageBoxButton.OK, MessageBoxImage.Information);
                        shopCart.obama--;
                    }

                    DeleteItem.Content = $"Удалить ({shopCart.obama})";
                    break;

                case "Боевая машина поддержки танков (БМПТ) «Орешник»":
                    shopCart.oreshk++;

                    res = products.Find(x => x.ProductName == "Боевая машина поддержки танков (БМПТ) «Орешник»");
                    if (shopCart.oreshk > res.Count)
                    {
                        MessageBox.Show("Товаров на скалде больше нет", "Обосрано!", MessageBoxButton.OK, MessageBoxImage.Information);
                        shopCart.oreshk--;
                    }

                    DeleteItem.Content = $"Удалить ({shopCart.oreshk})";
                    break;

                case "Микрофон студийный СОЮЗ 017 TUBE":
                    shopCart.soyz++;

                    res = products.Find(x => x.ProductName == "Микрофон студийный СОЮЗ 017 TUBE");
                    if (shopCart.soyz > res.Count)
                    {
                        MessageBox.Show("Товаров на скалде больше нет", "Обосрано!", MessageBoxButton.OK, MessageBoxImage.Information);
                        shopCart.soyz--;
                    }

                    DeleteItem.Content = $"Удалить ({shopCart.soyz})";
                    break;

                case "Надувной Дональд Трамп в полный рост":
                    shopCart.trump++;
                    
                    res = products.Find(x => x.ProductName == "Надувной Дональд Трамп в полный рост");
                    if (shopCart.trump > res.Count)
                    {
                        MessageBox.Show("Товаров на скалде больше нет", "Обосрано!", MessageBoxButton.OK, MessageBoxImage.Information);
                        shopCart.trump--;
                    }

                    DeleteItem.Content = $"Удалить ({shopCart.trump})";
                    break;

                case "Шапка ушанка енот":
                    shopCart.ushanka++;

                    res = products.Find(x => x.ProductName == "Шапка ушанка енот");
                    if (shopCart.ushanka > res.Count)
                    {
                        MessageBox.Show("Товаров на скалде больше нет", "Обосрано!", MessageBoxButton.OK, MessageBoxImage.Information);
                        shopCart.ushanka--;
                    }

                    DeleteItem.Content = $"Удалить ({shopCart.ushanka})";
                    break;

                case "Бля Белый Дом":
                    shopCart.whouse++;

                    res = products.Find(x => x.ProductName == "Бля Белый Дом");
                    if (shopCart.whouse > res.Count)
                    {
                        MessageBox.Show("Товаров на скалде больше нет", "Обосрано!", MessageBoxButton.OK, MessageBoxImage.Information);
                        shopCart.whouse--;
                    }

                    DeleteItem.Content = $"Удалить ({shopCart.whouse})";
                    break;
            }
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            switch (id)
            {
                case "Пушка AK-47 гад дэмн": 
                    shopCart.ak--;
                    if (shopCart.ak == 0)
                    {
                        DeleteItem.Visibility = Visibility.Collapsed;
                    }
                    else 
                    {
                        DeleteItem.Content = $"Удалить ({shopCart.ak})";
                    }
                    break;

                case "Водка Белочка: Я пришла! 40%, 500мл":
                    shopCart.belka--;
                    if (shopCart.belka == 0)
                    {
                        DeleteItem.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        DeleteItem.Content = $"Удалить ({shopCart.belka})";
                    }
                    break; 

                case "Maus — легендарный сверхтяжёлый танк Второй мировой, 188 тонн":
                    shopCart.maus--;
                    if (shopCart.maus == 0)
                    {
                        DeleteItem.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        DeleteItem.Content = $"Удалить ({shopCart.maus})";
                    }
                    break;

                case "Надувной Бардак Обама в полный рост":
                    shopCart.obama--;
                    if (shopCart.obama == 0)
                    {
                        DeleteItem.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        DeleteItem.Content = $"Удалить ({shopCart.obama})";
                    }
                    break;

                case "Боевая машина поддержки танков (БМПТ) «Орешник»":
                    shopCart.oreshk--;
                    if (shopCart.oreshk == 0)
                    {
                        DeleteItem.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        DeleteItem.Content = $"Удалить ({shopCart.oreshk})";
                    }
                    break;

                case "Микрофон студийный СОЮЗ 017 TUBE":
                    shopCart.soyz--;
                    if (shopCart.soyz == 0)
                    {
                        DeleteItem.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        DeleteItem.Content = $"Удалить ({shopCart.soyz})";
                    }
                    break; 

                case "Надувной Дональд Трамп в полный рост":
                    shopCart.trump--;
                    if (shopCart.trump == 0)
                    {
                        DeleteItem.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        DeleteItem.Content = $"Удалить ({shopCart.trump})";
                    }
                    break;

                case "Шапка ушанка енот":
                    shopCart.ushanka--;
                    if (shopCart.ushanka == 0)
                    {
                        DeleteItem.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        DeleteItem.Content = $"Удалить ({shopCart.ushanka})";
                    }
                    break;

                case "Бля Белый Дом":
                    shopCart.whouse--;
                    if (shopCart.whouse == 0)
                    {
                        DeleteItem.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        DeleteItem.Content = $"Удалить ({shopCart.whouse})";
                    }
                    break;
            }
        }

        private void GoToCart_Click(object sender, RoutedEventArgs e)
        {
            
            ShoppingCartPage page = new ShoppingCartPage();
            NavigationService.Navigate(page);
        }
    }
}
