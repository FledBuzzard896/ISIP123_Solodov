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
    /// Логика взаимодействия для FilmPage.xaml
    /// </summary>
    public partial class FilmPage : Page
    {
        private Films _movie; // Выбранный фильм
        private string res;   // Жанры фильма 

        /// <summary>
        /// От сюда берем Binding
        /// </summary>
        public string year => $"Год производства: {_movie.PremiereDate}";
        public string rating => $"Рейтинг: {_movie.Rating}";
        public string genres => $"Жанр: {res}";
        public string name => _movie.Name ;
        public string description => _movie.Description ;
        public string cover => $"/Images/{_movie.Cover}";

        public FilmPage(int movieID)
        {
            InitializeComponent();

            _movie = Core.ContextHome.Films.Find(movieID);

            this.Loaded += MyPage_Loaded;
        }


        private void MyPage_Loaded(object sender, RoutedEventArgs e) 
        {
            List<FilmGenres> lstBunch = Core.ContextHome.FilmGenres.ToList(); // Получаем список жанров с привязкой к фильмам
            List<Genres> lstGenres = Core.ContextHome.Genres.ToList(); // Получаем общий список жанров

            foreach (var str in lstBunch)
            {
                if (str.FilmID == _movie.ID)
                {
                    Genres g = lstGenres.Find(x => x.ID == str.GenreID);

                    res += $"{g.Name}, ";
                }
            }
        }


    }
}
