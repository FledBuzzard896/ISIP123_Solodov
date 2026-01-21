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
    /// Логика взаимодействия для Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
        }
        private Line _line;
        private SolidColorBrush color = Brushes.Red;
        private void DrawingCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                _line = new Line();
                _line.Stroke = color;
                _line.StrokeThickness = 2;
                Point p = e.GetPosition(DriwingCanvas);
                _line.X1 = p.X;
                _line.Y1 = p.Y;
                _line.X2 = p.X;
                _line.Y2 = p.Y;
                DriwingCanvas.Children.Add(_line);
            }
        }

        private void DrawingCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && _line != null)
            {
                Point finalPoint = e.GetPosition(DriwingCanvas);
                _line.X2 = finalPoint.X;
                _line.Y2 = finalPoint.Y;
            }
        }

        private void DrawingCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _line = null;
        }

        private void red_Click(object sender, RoutedEventArgs e)
        {
            color = Brushes.Red;
        }

        private void green_Click(object sender, RoutedEventArgs e)
        {
            color = Brushes.Green;
        }

        private void blue_Click(object sender, RoutedEventArgs e)
        {
            color = Brushes.Blue;
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            DriwingCanvas.Children.Clear();
        }
        private void exit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
