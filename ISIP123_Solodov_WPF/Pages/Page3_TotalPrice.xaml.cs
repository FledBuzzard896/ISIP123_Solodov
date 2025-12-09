using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для Page3_TotalPrice.xaml
    /// </summary>
    public partial class Page3_TotalPrice : Page
    {
        private string _adds_comment;
        private double _price;

        public Page3_TotalPrice()
        {
            InitializeComponent();
            _price = MyCar.price;
            _adds_comment = MyCar.adds_comment;

            
            /// Добавление точек после 3 цифр в цене
            string s_price = "";
            int count = 0;

            for (int i = _price.ToString().Length; i != 0; i--) 
            {
                count++;
                s_price += _price.ToString()[i-1];

                if (count % 3 == 0) 
                {
                    s_price += ".";
                }
            }

            /// Переворот числа
            char[] chars = s_price.ToCharArray();
            Array.Reverse(chars);
            s_price = new string(chars);

            /// Вывод чека
            complectation.Text = MyCar.PrintInfo();
            if (_adds_comment == "") 
            {
                complectation.Text = complectation.Text.Replace("Ваш комментарий: ", "");
            }
            total.Text = $"Итоговая стоимость: {s_price}₽";
        }

        private void ToNext_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoForward)
            {
                NavigationService.GoForward();
            }
            else 
            {
                Page4_Credit page4 = new Page4_Credit();
                NavigationService.Navigate(page4);
            }
        }

        private void ToBack_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService?.CanGoBack == true)
            {
                NavigationService.GoBack();
            }
        }
    }
}
