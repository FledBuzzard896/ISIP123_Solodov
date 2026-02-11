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
    /// Логика взаимодействия для Kinopoisk.xaml
    /// </summary>
    public partial class Kinopoisk : Page
    {
        public Kinopoisk()
        {
            InitializeComponent();

            List<Films> movies = Core.ContextHome.Films.ToList(); // лист с фильмами из БД

            foreach (var elem in movies) { elem.Cover = "/Image/" + elem.Cover; }

            Films_LB.ItemsSource = movies;
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) 
        {
            // sender — это Image, его DataContext — объект Films
            if (sender is FrameworkElement element && element.DataContext is Films movie)
            {
                FilmPage page = new FilmPage(movie.ID);
                NavigationService?.Navigate(page);

                // Останавливаем всплытие события, чтобы оно не дошло до ListBox
                e.Handled = true;
            }
        }
    }
}
