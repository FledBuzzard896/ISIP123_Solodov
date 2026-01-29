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
                text += $"Пушка AK-47 гад дэмн. Количество: {shopCart.ak}. Цена: {res.Price} * {shopCart.ak} = {res.Price * shopCart.ak}₽\n";
            }
            if (shopCart.belka > 0)
            {
                var res = products.Find(x => x.ProductName == "Водка Белочка: Я пришла! 40%, 500мл");
                text += $"Водка Белочка: Я пришла! 40%, 500мл. Количество: {shopCart.belka}. Цена: {res.Price} * {shopCart.belka} = {res.Price * shopCart.belka}₽\n";
            }
            if (shopCart.maus > 0)
            {
                var res = products.Find(x => x.ProductName == "Maus — легендарный сверхтяжёлый танк Второй мировой, 188 тонн");
                text += $"Maus — легендарный сверхтяжёлый танк Второй мировой, 188 тонн. Количество: {shopCart.maus}. Цена: {res.Price} * {shopCart.maus} = {res.Price * shopCart.maus}₽\n";
            }
            if (shopCart.obama > 0)
            {
                var res = products.Find(x => x.ProductName == "Надувной Бардак Обама в полный рост");
                text += $"Надувной Бардак Обама в полный рост. Количество: {shopCart.obama}. Цена: {res.Price} * {shopCart.obama} = {res.Price * shopCart.obama}₽\n";
            }
            if (shopCart.oreshk > 0)
            {
                var res = products.Find(x => x.ProductName == "Боевая машина поддержки танков (БМПТ) «Орешник»");
                text += $"Боевая машина поддержки танков (БМПТ) «Орешник». Количество: {shopCart.oreshk}. Цена: {res.Price} * {shopCart.oreshk} = {res.Price * shopCart.oreshk}₽\n";
            }
            if (shopCart.soyz > 0)
            {
                var res = products.Find(x => x.ProductName == "Микрофон студийный СОЮЗ 017 TUBE");
                text += $"Микрофон студийный СОЮЗ 017 TUBE. Количество: {shopCart.soyz}. Цена: {res.Price} * {shopCart.soyz} = {res.Price * shopCart.soyz}₽\n";
            }
            if (shopCart.trump > 0)
            {
                var res = products.Find(x => x.ProductName == "Надувной Дональд Трамп в полный рост");
                text += $"Надувной Дональд Трамп в полный рост. Количество: {shopCart.trump}. Цена: {res.Price} * {shopCart.trump} = {res.Price * shopCart.trump}₽\n";
            }
            if (shopCart.ushanka > 0)
            {
                var res = products.Find(x => x.ProductName == "Шапка ушанка енот");
                text += $"Шапка ушанка енот. Количество: {shopCart.ushanka}. Цена: {res.Price} * {shopCart.ushanka} = {res.Price * shopCart.ushanka}₽\n";
            }
            if (shopCart.whouse > 0)
            {
                var res = products.Find(x => x.ProductName == "Бля Белый Дом");
                text += $"Бля Белый Дом. Количество: {shopCart.ushanka}. Цена: {res.Price} * {shopCart.ushanka} = {res.Price * shopCart.ushanka}₽\n";
            }

            if (text == "")
            {
                main.Text = "ну ты ничего не выбрал, вернись назад бро";
            }
            else 
            {
                main.Text = text;
            }
                
        }

        shopCart cart = new shopCart();
        string text = "";

        private void TakeOrder_Click(object sender, RoutedEventArgs e)
        {

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
