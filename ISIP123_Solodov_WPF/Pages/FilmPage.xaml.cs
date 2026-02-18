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
    public partial class FilmPage : Page
    {
        public Films movie { get; set; }   // Выбранный фильм
        public string res;                 // Жанры фильма 

        public FilmPage(Films movie)
        {
            InitializeComponent();

            this.movie = movie;
            this.movie.Cover = "/Image/" + this.movie.Cover;

            DataContext = this.movie;

            this.Loaded += MyPage_Loaded;
        }


        private void MyPage_Loaded(object sender, RoutedEventArgs e) 
        {
            List<FilmGenres> lstBunch = Core.ContextHOME.FilmGenres.ToList();    // Получаем список жанров с привязкой к фильмам
            List<Genres> lstGenres = Core.ContextHOME.Genres.ToList();           // Получаем общий список жанров

            res += "Жанры: ";

            foreach (var str in lstBunch)
            {
                if (str.FilmID == movie.ID)
                {
                    Genres g = lstGenres.Find(x => x.ID == str.GenreID);

                    res += $"{g.Name}, ";
                }
            }
            genryTextyGoggy.Text = res.Substring(0, res.Length-2);
        }

        private void BuyTicket_Click(object sender, RoutedEventArgs e)
        {
            KinoAfishaPage page = new KinoAfishaPage(movie);
            NavigationService.Navigate(page);
        }

        private void Profile_CLick(object sender, RoutedEventArgs e) 
        {
            if (UserClass.IsLogged == false)
            {
                Page next = new ProfilePage();

                var dialog = new RegOrLog(next);
                dialog.ShowDialog();
            }
            else
            {
                ProfilePage page = new ProfilePage();
                NavigationService.Navigate(page);
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            //Kinopoisk back_page = new Kinopoisk();
            //NavigationService.Navigate(back_page);
            DeletePathGorImages(movie);
            NavigationService.GoBack();
        }

        private void DeletePathGorImages(Films movie)
        {
           movie.Cover = movie.Cover.Substring(7);
        }
    }
}
