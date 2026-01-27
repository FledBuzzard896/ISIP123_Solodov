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
        }

        string id;

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
                case "pack://application:,,,/Images/AK-47.jpg":

                    descript.Text = "Пушка AK-47 гад дэмн";
                    price.Text = "40790,00 ₽";
                    id = "ak";

                    if (cart.ak > 0) { DeleteItem.Visibility = Visibility.Visible; DeleteItem.Content = $"Удалить ({cart.ak})"; }

                    break;

                case "pack://application:,,,/Images/belka.jpg":

                    descript.Text = "Водка Белочка: Я пришла! 40%, 500мл";
                    price.Text = "409,00 ₽";
                    id = "belka";

                    if (cart.ak > 0) { DeleteItem.Visibility = Visibility.Visible; DeleteItem.Content = $"Удалить ({cart.ak})"; }

                    break;

                case "pack://application:,,,/Images/Maus.jpg":

                    descript.Text = "Maus — легендарный сверхтяжёлый танк \nВторой мировой, 188 тонн";
                    price.Text = "2.146.011.560,00 ₽";
                    id = "maus";

                    if (cart.maus > 0) { DeleteItem.Visibility = Visibility.Visible; DeleteItem.Content = $"Удалить ({cart.maus})"; }

                    break;

                case "pack://application:,,,/Images/Obama.jpg":

                    descript.Text = "Надувной Бардак Обама в полный рост";
                    price.Text = "123.456,00 ₽";
                    id = "obama";

                    if (cart.obama > 0) { DeleteItem.Visibility = Visibility.Visible; DeleteItem.Content = $"Удалить ({cart.obama})"; }

                    break;

                case "pack://application:,,,/Images/oreshki.jpg":

                    descript.Text = "Боевая машина поддержки танков \n(БМПТ) «Орешник»";
                    price.Text = "613.146.160,00 ₽";
                    id = "oreshk";

                    if (cart.oreshk > 0) { DeleteItem.Visibility = Visibility.Visible; DeleteItem.Content = $"Удалить ({cart.oreshk})"; }

                    break;

                case "pack://application:,,,/Images/soyz.jpg":

                    descript.Text = "Микрофон студийный СОЮЗ 017 TUBE";
                    price.Text = "419.990,00 ₽";
                    id = "soyz";

                    if (cart.soyz > 0) { DeleteItem.Visibility = Visibility.Visible; DeleteItem.Content = $"Удалить ({cart.soyz})"; }

                    break;

                case "pack://application:,,,/Images/Trump.jpg":

                    descript.Text = "Надувной Дональд Трамп в полный рост";
                    price.Text = "148.852,67 ₽";
                    id = "trump";

                    if (cart.trump > 0) { DeleteItem.Visibility = Visibility.Visible; DeleteItem.Content = $"Удалить ({cart.trump})"; }

                    break;

                case "pack://application:,,,/Images/ushanka.jpg":

                    descript.Text = "Шапка ушанка енот";
                    price.Text = "5260,00 ₽";
                    id = "ushanka";

                    if (cart.ushanka > 0) { DeleteItem.Visibility = Visibility.Visible; DeleteItem.Content = $"Удалить ({cart.ushanka})"; }

                    break;

                case "pack://application:,,,/Images/WhiteHouse.jpg":

                    descript.Text = "Бля Белый Дом";
                    price.Text = "100,00 ₽";
                    id = "whouse";

                    if (cart.whouse > 0) { DeleteItem.Visibility = Visibility.Visible; DeleteItem.Content = $"Удалить ({cart.whouse})"; }

                    break;

            }
        }

        shopCart cart = new shopCart();

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {

            DeleteItem.Visibility = Visibility.Visible;

            switch (id) 
            {
                case "ak": cart.ak++; DeleteItem.Content = $"Удалить ({cart.ak})"; break;
                case "belka": cart.belka++; DeleteItem.Content = $"Удалить ({cart.belka})"; break;
                case "maus": cart.maus++; DeleteItem.Content = $"Удалить ({cart.maus})"; break;
                case "obama": cart.obama++; DeleteItem.Content = $"Удалить ({cart.obama})"; break;
                case "oreshk": cart.oreshk++; DeleteItem.Content = $"Удалить ({cart.oreshk})"; break;
                case "soyz": cart.soyz++; DeleteItem.Content = $"Удалить ({cart.soyz})"; break;
                case "trump": cart.trump++; DeleteItem.Content = $"Удалить ({cart.trump})"; break;
                case "ushanka": cart.ushanka++; DeleteItem.Content = $"Удалить ({cart.ushanka})"; break;
                case "whouse": cart.whouse++; DeleteItem.Content = $"Удалить ({cart.whouse})"; break;
            }
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            switch (id)
            {
                case "ak": 
                    cart.ak--;
                    if (cart.ak == 0)
                    {
                        DeleteItem.Visibility = Visibility.Collapsed;
                    }
                    else 
                    {
                        DeleteItem.Content = $"Удалить ({cart.ak})";
                    }
                    break;

                case "belka":
                    cart.belka--;
                    if (cart.belka == 0)
                    {
                        DeleteItem.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        DeleteItem.Content = $"Удалить ({cart.belka})";
                    }
                    break; 

                case "maus":
                    cart.maus--;
                    if (cart.maus == 0)
                    {
                        DeleteItem.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        DeleteItem.Content = $"Удалить ({cart.maus})";
                    }
                    break;

                case "obama":
                    cart.obama--;
                    if (cart.obama == 0)
                    {
                        DeleteItem.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        DeleteItem.Content = $"Удалить ({cart.obama})";
                    }
                    break;

                case "oreshk":
                    cart.oreshk--;
                    if (cart.oreshk == 0)
                    {
                        DeleteItem.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        DeleteItem.Content = $"Удалить ({cart.oreshk})";
                    }
                    break;

                case "soyz":
                    cart.soyz--;
                    if (cart.soyz == 0)
                    {
                        DeleteItem.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        DeleteItem.Content = $"Удалить ({cart.soyz})";
                    }
                    break; 

                case "trump":
                    cart.trump--;
                    if (cart.trump == 0)
                    {
                        DeleteItem.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        DeleteItem.Content = $"Удалить ({cart.trump})";
                    }
                    break;

                case "ushanka":
                    cart.ushanka--;
                    if (cart.ushanka == 0)
                    {
                        DeleteItem.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        DeleteItem.Content = $"Удалить ({cart.ushanka})";
                    }
                    break;

                case "whouse":
                    cart.whouse--;
                    if (cart.whouse == 0)
                    {
                        DeleteItem.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        DeleteItem.Content = $"Удалить ({cart.whouse})";
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
