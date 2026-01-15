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
using System.Windows.Shapes;

namespace ISIP123_Solodov_WPF
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private readonly MainWindow _mainWindow;
        public SettingsWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;

            WidthSlider.Value = _mainWindow.Width;
            HeightSlider.Value = _mainWindow.Height;
        }


        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _mainWindow.Width = WidthSlider.Value;
        }

        private void another_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _mainWindow.Height = HeightSlider.Value;
        }

        private void white_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.Background = Brushes.White;
        }

        private void blue_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.Background  = Brushes.DarkCyan;
        }

        private void red_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.Background = Brushes.Red;
        }

        private void green_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.Background = Brushes.Green;
        }
    }
}
