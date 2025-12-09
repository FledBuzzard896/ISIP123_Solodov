using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
    /// Логика взаимодействия для Page4_Credit.xaml
    /// </summary>
    public partial class Page4_Credit : Page
    {
        double _price;
        double _procent;
        int _srok;
        public Page4_Credit()
        {
            InitializeComponent();

            _price = MyCar.price;
        }

        private void Procent_TextChanged(object sender, TextChangedEventArgs e)
        {
            string str = Procent.Text;

            if (str != "")
            {
                if (str.Length < 3 && str.Contains(" ") == false) 
                {
                    _procent = Convert.ToDouble(Procent.Text);

                    if (str.Length > 0 && Srok.Text.Length > 0)
                    {
                        CalculateCredit(_price, _procent, _srok);
                    }
                }
            }
            
        }

        private void Srok_TextChanged(object sender, TextChangedEventArgs e)
        {
            string str = Srok.Text;

            if (str != "") 
            {
                if ((12 <= Convert.ToInt32(str) && Convert.ToInt32(str) <= 96) && str.Contains(" ") == false)
                {
                    _srok = Convert.ToInt32(str);

                    if (Procent.Text.Length > 0 && str.Length > 0)
                    {
                        CalculateCredit(_price, _procent, _srok);
                    }
                }
            }

        }

        private void CalculateCredit(double price, double procent, int period)
        {
            double STAVKA = 8;

            double sumOfCredit = price - (price / 100 * procent);
            double MONTH_STAVKA = STAVKA / 100 / 12;
            double monthPayment = Math.Round((sumOfCredit * (MONTH_STAVKA * Math.Pow((1 + MONTH_STAVKA), _srok)) / (Math.Pow((1 + MONTH_STAVKA), _srok) - 1)), 2);

            SumOfFirstPayment.Text = $"Сумма первоначального взноса: {(price / 100 * procent)}₽";
            CreditSum.Text = $"Сумма, берущаяся в кредит: {sumOfCredit}₽";
            Payment.Text = $"Ориентировочный ежемесячный платёж: {monthPayment}₽";
        }

        private void ToNext_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoForward)
            {
                NavigationService.GoForward();
            }
            else
            {
                Page5_EndForm page5 = new Page5_EndForm();
                NavigationService.Navigate(page5);
            }
        }

        private void ToBack_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService?.CanGoBack == true)
            {
                NavigationService.GoBack();
            }
        }

        private void TextBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text[0]);
        }
    }
}
