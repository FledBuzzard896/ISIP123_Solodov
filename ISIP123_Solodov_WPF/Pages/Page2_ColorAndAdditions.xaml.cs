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
    /// Логика взаимодействия для Page2_ColorAndAdditions.xaml
    /// </summary>
    public partial class Page2_ColorAndAdditions : Page
    { 

        private string _color = "";
        private string _adds;
        private string _adds_comment;
        private string _model;
        private string _engine;
        private double _price;

        double copy_price = 0;

        public Page2_ColorAndAdditions(double price, string model, string engine ) 
        {
            InitializeComponent();
            _model = model;
            _engine = engine;
            _price = price;

            color_square.Foreground = Brushes.Gray;
            copy_price = _price;
        }

        private void ColorRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radio = sender as RadioButton;

            //comment.Text = _engine;

            switch (radio.Content.ToString()) 
            {
                case "Бордовый": color_square.Foreground = new SolidColorBrush(Color.FromRgb(155, 45, 48)); break;
                case "Бежевый": color_square.Foreground = new SolidColorBrush(Color.FromRgb(255, 228, 181)); break;
                case "Черный": color_square.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0)); break;
                case "Голубой": color_square.Foreground = new SolidColorBrush(Color.FromRgb(120, 219, 226)); break;
                case "Коричневый": color_square.Foreground = new SolidColorBrush(Color.FromRgb(117, 92, 72)); break;
            }
            _color = radio.Content.ToString();
        }

        private void AddsBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            
            _adds += "\n" + checkBox.Content;
        }

        private void AddsBox_UnChecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            string toRemove = checkBox.Content.ToString();
            int index = _adds.IndexOf(toRemove);

            if (index != -1)
            {
                _adds = _adds.Remove(index, toRemove.Length).Trim();
                // Убираем лишние пробелы (двойные пробелы) и двойные переносы строки
                _adds = _adds.Replace("  ", " ");
                _adds = _adds.Replace("\n\n", "\n");
            }
        }

        private void ToBack_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService?.CanGoBack == true)
            {
                NavigationService.GoBack();
            }
        }

        private void ToNext_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoForward)
            {
                NavigationService.GoForward();
            }
            else
            {
                if (_color == "")
                {
                    MessageBox.Show("Выберите цвет автомобиля!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string[] adds = _adds.Split('\n');


                foreach (string add in adds)
                {
                    switch (add)
                    {
                        case "Магнитола (+12 999₽)": _price += 12999; break;
                        case "Салон (кожа) (+97 459₽)": _price += 97459; break;
                        case "Хром. дет. (+342 000₽)": _price += 342000; break;
                        case "Вонючка (+150 ₽)": _price += 150; break;
                    }
                }

                _adds_comment = comment.Text;
                Page3_TotalPrice page3 = new Page3_TotalPrice(_price, _model, _engine, _color, _adds, _adds_comment);
                _price = copy_price;
                NavigationService.Navigate(page3);
            }
        }
    }
}
