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

namespace ISIP123_Solodov_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double price = 1000;
        bool isPresent = false;
        bool isDelivery= false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (count != null)
            {
                Slider sldr = sender as Slider;
                count.Text = $"{ sldr.Value} шт.";

                price = 1000 * sldr.Value;

                if (isPresent) { price += 100; }
                if (isDelivery) { price += 200; }

                total.Text = $"Итого: {price}₽";
            }

        }
        private void box1_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb.IsChecked == true)
            {
                price += 100;
                isPresent = true;
                total.Text = $"Итого: {price}₽";
            }
            else 
            {
                price -= 100;
                isPresent = false;
                total.Text = $"Итого: {price}₽";
            }
        }

        private void box2_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb.IsChecked == true)
            {
                price += 200;
                isDelivery = true;
                total.Text = $"Итого: {price}₽";
            }
            else if (cb.IsChecked == false)
            {
                price -= 200;
                isDelivery = false;
                total.Text = $"Итого: {price}₽";
            }
        }

        private void TextBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text[0]);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text == "" || phone.Text == "" || (btn1.IsChecked == false && btn2.IsChecked == false && btn3.IsChecked == false))
            {
                MessageBox.Show("Заполните поля!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else 
            {
                string pay_choice;
                string additionally = "";

                if (btn1.IsChecked == true)
                {
                    pay_choice = btn1.Content.ToString();
                }
                else if (btn2.IsChecked == true)
                {
                    pay_choice = btn2.Content.ToString();
                }
                else 
                {
                    pay_choice = btn3.Content.ToString();
                }

                if (isPresent) { additionally += "\n" + box1.Content.ToString(); }
                if (isDelivery) { additionally += "\n" + box2.Content.ToString(); }

                MessageBox.Show($"Вы: {name.Text}\nТелефон: {phone.Text}\nСпособ оплаты: {pay_choice}\nДополнительно: {additionally}\nКоличество: {count.Text}\nИтог: {price} руб.");
            }
        }
    }
}
