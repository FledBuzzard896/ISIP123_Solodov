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
    /// Логика взаимодействия для ShoppingCartPage.xaml
    /// </summary>
    public partial class ShoppingCartPage : Page
    {
        public ShoppingCartPage()
        {
            InitializeComponent();

            List<Products> products = Core.Context.Products.ToList();

            if (shopCart.ak > 0)
            {
                var res = products.Find(x => x.ProductName == "Пушка AK-47 гад дэмн");
                text += $"Пушка AK-47 гад дэмн. \n\tКоличество: {shopCart.ak}. Цена: {res.Price} * {shopCart.ak} = {res.Price * shopCart.ak}₽\n";
                totalPrice += Convert.ToDouble(res.Price * shopCart.ak);
            }
            if (shopCart.belka > 0)
            {
                var res = products.Find(x => x.ProductName == "Водка Белочка: Я пришла! 40%, 500мл");
                text += $"Водка Белочка: Я пришла! 40%, 500мл. \n\tКоличество: {shopCart.belka}. Цена: {res.Price} * {shopCart.belka} = {res.Price * shopCart.belka}₽\n";
                totalPrice += Convert.ToDouble(res.Price * shopCart.belka);
            }
            if (shopCart.maus > 0)
            {
                var res = products.Find(x => x.ProductName == "Maus — легендарный сверхтяжёлый танк Второй мировой, 188 тонн");
                text += $"Maus — легендарный сверхтяжёлый танк Второй мировой, 188 тонн. \n\tКоличество: {shopCart.maus}. Цена: {res.Price} * {shopCart.maus} = {res.Price * shopCart.maus}₽\n";
                totalPrice += Convert.ToDouble(res.Price * shopCart.maus);
            }
            if (shopCart.obama > 0)
            {
                var res = products.Find(x => x.ProductName == "Надувной Бардак Обама в полный рост");
                text += $"Надувной Бардак Обама в полный рост. \n\tКоличество: {shopCart.obama}. Цена: {res.Price} * {shopCart.obama} = {res.Price * shopCart.obama}₽\n";
                totalPrice += Convert.ToDouble(res.Price * shopCart.obama);
            }
            if (shopCart.oreshk > 0)
            {
                var res = products.Find(x => x.ProductName == "Боевая машина поддержки танков (БМПТ) «Орешник»");
                text += $"Боевая машина поддержки танков (БМПТ) «Орешник». \n\tКоличество: {shopCart.oreshk}. Цена: {res.Price} * {shopCart.oreshk} = {res.Price * shopCart.oreshk}₽\n";
                totalPrice += Convert.ToDouble(res.Price * shopCart.oreshk);
            }
            if (shopCart.soyz > 0)
            {
                var res = products.Find(x => x.ProductName == "Микрофон студийный СОЮЗ 017 TUBE");
                text += $"Микрофон студийный СОЮЗ 017 TUBE. \n\tКоличество: {shopCart.soyz}. Цена: {res.Price} * {shopCart.soyz} = {res.Price * shopCart.soyz}₽\n";
                totalPrice += Convert.ToDouble(res.Price * shopCart.soyz);
            }
            if (shopCart.trump > 0)
            {
                var res = products.Find(x => x.ProductName == "Надувной Дональд Трамп в полный рост");
                text += $"Надувной Дональд Трамп в полный рост. \n\tКоличество: {shopCart.trump}. Цена: {res.Price} * {shopCart.trump} = {res.Price * shopCart.trump}₽\n";
                totalPrice += Convert.ToDouble(res.Price * shopCart.trump);
            }
            if (shopCart.ushanka > 0)
            {
                var res = products.Find(x => x.ProductName == "Шапка ушанка енот");
                text += $"Шапка ушанка енот. \n\tКоличество: {shopCart.ushanka}. Цена: {res.Price} * {shopCart.ushanka} = {res.Price * shopCart.ushanka}₽\n";
                totalPrice += Convert.ToDouble(res.Price * shopCart.ushanka);
            }
            if (shopCart.whouse > 0)
            {
                var res = products.Find(x => x.ProductName == "Бля Белый Дом");
                text += $"Бля Белый Дом. \n\tКоличество: {shopCart.whouse}. Цена: {res.Price} * {shopCart.whouse} = {res.Price * shopCart.whouse}₽\n";
                totalPrice += Convert.ToDouble(res.Price * shopCart.whouse);
            }

            if (text == "")
            {
                main.Text = "ну ты ничего не выбрал, вернись назад бро";
            }
            else 
            {
                main.Text = text;
                finalPrice.Text = $"К оплате: {totalPrice}₽";
            }
                
        }

        shopCart cart = new shopCart();
        string text = "";
        double totalPrice = 0;

        private void TakeOrder_Click(object sender, RoutedEventArgs e)
        {
            if (main.Text != "ну ты ничего не выбрал, вернись назад бро") 
            {
                TakeOrderPage page = new TakeOrderPage(totalPrice);
                NavigationService.Navigate(page);
            }
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            shopCart.ak = 0;
            shopCart.belka = 0;
            shopCart.maus = 0;
            shopCart.obama = 0;
            shopCart.oreshk = 0;
            shopCart.soyz = 0;
            shopCart.trump = 0;
            shopCart.ushanka = 0;
            shopCart.whouse = 0;

            NavigationService.GoBack();
        }
    }
}
