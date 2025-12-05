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
        private string _color;
        private string _adds;
        private string _model;
        private string _engine;
        private double _price;
        public Page2_ColorAndAdditions(double price, string model, string engine ) 
        {
            InitializeComponent();
            _model = model;
            _engine = engine;
            _price = price;
        }

        private void ColorRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radio = sender as RadioButton;

            switch (radio.Content.ToString()) 
            {
                case "Бордовый": color_square.Foreground = new SolidColorBrush(Color.FromRgb(155, 45, 48)); break;
                case "Бежевый": color_square.Foreground = new SolidColorBrush(Color.FromRgb(255, 228, 181)); break;
                case "Черный": color_square.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0)); break;
                case "Голубой": color_square.Foreground = new SolidColorBrush(Color.FromRgb(120, 219, 226)); break;
                case "Коричневый": color_square.Foreground = new SolidColorBrush(Color.FromRgb(117, 92, 72)); break;
            }
        }

        private void AddsBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            
            _adds += "\n" + checkBox.Content;

            comment.Text = _adds;
        }

        private void AddsBox_UnChecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            string toRemove = checkBox.Content.ToString();
            int index = _adds.IndexOf(toRemove);

            if (index != -1)
            {
                _adds = _adds.Remove(index, toRemove.Length).Trim();
                // Убираем лишние пробелы (двойные пробелы)
                _adds = _adds.Replace("  ", " ");
            }

            comment.Text = _adds;
        }
    }
}
