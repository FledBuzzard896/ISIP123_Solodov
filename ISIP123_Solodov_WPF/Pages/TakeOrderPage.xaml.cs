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

namespace ISIP123_Solodov_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для TakeOrderPage.xaml
    /// </summary>
    public partial class TakeOrderPage : Page
    {
        public TakeOrderPage(double price)
        {
            InitializeComponent();

            double _price = price;

            final.Text += _price.ToString() + "₽";
        }

        private void payBtn_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text != "" && mail.Text != "" && adress.Text != "")
            {
                Users newUser = null;
                try 
                {
                    newUser = Core.Context.Users.First(u => u.Name == name.Text);
                } catch 
                {
                    // Пользователь
                    newUser = new Users
                    {
                        Name = name.Text,
                    };

                    Core.Context.Users.Add(newUser);
                    Core.Context.SaveChanges();
                } 
               

                // Заказ
                Orders newOrder = new Orders
                {
                    UserID = newUser.ID,
                    OrderDate = DateTime.Now,
                };

                Core.Context.Orders.Add(newOrder);
                Core.Context.SaveChanges();

                if (shopCart.ak > 0)
                {
                    Products pr = Core.Context.Products.First(p => p.ProductName == "Пушка AK-47 гад дэмн");
                    pr.Count -= shopCart.ak;

                    ProductList lst = new ProductList
                    {
                        ProductID = pr.ID,
                        OrderID = newOrder.ID,
                        Quantity = shopCart.ak,
                    };

                    Core.Context.ProductList.Add(lst);
                    Core.Context.SaveChanges();
                }

                if (shopCart.belka > 0)
                {
                    Products pr = Core.Context.Products.First(p => p.ProductName == "Водка Белочка: Я пришла! 40%, 500мл");
                    pr.Count -= shopCart.belka;

                    ProductList lst = new ProductList
                    {
                        ProductID = pr.ID,
                        OrderID = newOrder.ID,
                        Quantity = shopCart.belka,
                    };

                    Core.Context.ProductList.Add(lst);
                    Core.Context.SaveChanges();
                }

                if (shopCart.maus > 0)
                {
                    Products pr = Core.Context.Products.First(p => p.ProductName == "Maus — легендарный сверхтяжёлый танк Второй мировой, 188 тонн");
                    pr.Count -= shopCart.maus;

                    ProductList lst = new ProductList
                    {
                        ProductID = pr.ID,
                        OrderID = newOrder.ID,
                        Quantity = shopCart.maus,
                    };

                    Core.Context.ProductList.Add(lst);
                    Core.Context.SaveChanges();
                }

                if (shopCart.obama > 0)
                {
                    Products pr = Core.Context.Products.First(p => p.ProductName == "Надувной Бардак Обама в полный рост");
                    pr.Count -= shopCart.obama;

                    ProductList lst = new ProductList
                    {
                        ProductID = pr.ID,
                        OrderID = newOrder.ID,
                        Quantity = shopCart.obama,
                    };

                    Core.Context.ProductList.Add(lst);
                    Core.Context.SaveChanges();
                }

                if (shopCart.oreshk > 0)
                {
                    Products pr = Core.Context.Products.First(p => p.ProductName == "Боевая машина поддержки танков (БМПТ) «Орешник»");
                    pr.Count -= shopCart.oreshk;

                    ProductList lst = new ProductList
                    {
                        ProductID = pr.ID,
                        OrderID = newOrder.ID,
                        Quantity = shopCart.oreshk,
                    };

                    Core.Context.ProductList.Add(lst);
                    Core.Context.SaveChanges();
                }

                if (shopCart.soyz > 0)
                {
                    Products pr = Core.Context.Products.First(p => p.ProductName == "Микрофон студийный СОЮЗ 017 TUBE");
                    pr.Count -= shopCart.soyz;

                    ProductList lst = new ProductList
                    {
                        ProductID = pr.ID,
                        OrderID = newOrder.ID,
                        Quantity = shopCart.soyz,
                    };

                    Core.Context.ProductList.Add(lst);
                    Core.Context.SaveChanges();
                }

                if (shopCart.trump > 0)
                {
                    Products pr = Core.Context.Products.First(p => p.ProductName == "Надувной Дональд Трамп в полный рост");
                    pr.Count -= shopCart.trump;

                    ProductList lst = new ProductList
                    {
                        ProductID = pr.ID,
                        OrderID = newOrder.ID,
                        Quantity = shopCart.trump,
                    };

                    Core.Context.ProductList.Add(lst);
                    Core.Context.SaveChanges();
                }

                if (shopCart.ushanka > 0)
                {
                    Products pr = Core.Context.Products.First(p => p.ProductName == "Шапка ушанка енот");
                    pr.Count -= shopCart.ushanka;

                    ProductList lst = new ProductList
                    {
                        ProductID = pr.ID,
                        OrderID = newOrder.ID,
                        Quantity = shopCart.ushanka,
                    };

                    Core.Context.ProductList.Add(lst);
                    Core.Context.SaveChanges();
                }

                if (shopCart.whouse > 0)
                {
                    Products pr = Core.Context.Products.First(p => p.ProductName == "Бля Белый Дом");
                    pr.Count -= shopCart.whouse;

                    ProductList lst = new ProductList
                    {
                        ProductID = pr.ID,
                        OrderID = newOrder.ID,
                        Quantity = shopCart.whouse,
                    };

                    Core.Context.ProductList.Add(lst);
                    Core.Context.SaveChanges();
                }
                MessageBox.Show("Доставка уже в пути!", "Вас заскамили", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Application.Current.Shutdown();
            }
            else 
            {
                MessageBox.Show("Заполни все поля!!!", "Attention!!!", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }
    }
}
