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

            Loaded += Kinopoisk_Loaded;
        }

        private void MouseDoubleClick(object sender, MouseButtonEventArgs e) 
        {
            var selectFilm = Films_LB.SelectedItem as Films;

            if (selectFilm == null) return;

            FilmPage page = new FilmPage(selectFilm);

            NavigationService.Navigate(page);
        }

        private void SearchBtnClick(object sender, RoutedEventArgs e)
        {
            if (searchBox.Text != "")
            {
                List<Films> lst = Core.ContextHOME.Films.Where(x => x.Name.StartsWith(searchBox.Text)).ToList();

                if (lst.Count > 0)
                {
                    Films_LB.ItemsSource = lst;
                }
                else
                {
                    MessageBox.Show("Фильма/Сериала не найдено!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else 
            {
                Films_LB.ItemsSource = Core.ContextHOME.Films.ToList();
            }
        }

        private void ProfileBtnClick(object sender, RoutedEventArgs e)
        {
            if (UserClass.IsLogged == false)
            {
                UserClass.IsNextPageIsProfile = true;

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

        private void MyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            ComboBoxItem selectedItem = comboBox.SelectedItem as ComboBoxItem;

            List<Films> sortedLst = (List<Films>)Films_LB.ItemsSource;

            switch (selectedItem.Content.ToString()) 
            {
                case "По возрастанию":
                    sortedLst = sortedLst.OrderBy(f => f.Name).ToList();
                    Films_LB.ItemsSource = sortedLst;
                    break;

                case "По убыванию":
                    sortedLst = sortedLst.OrderByDescending(f => f.Name).ToList();
                    Films_LB.ItemsSource = sortedLst;
                    break;

                case "Рейтинг (↑)":
                    sortedLst = sortedLst.OrderBy(r => r.Rating).ToList();
                    Films_LB.ItemsSource = sortedLst;
                    break;

                case "Рейтинг (↓)":
                    sortedLst = sortedLst.OrderByDescending(r => r.Rating).ToList();
                    Films_LB.ItemsSource = sortedLst;
                    break;
            }
        }


        private void Kinopoisk_Loaded(object sender, RoutedEventArgs e)
        {
            List<Films> movies = Core.ContextHOME.Films.ToList();
            foreach (var elem in movies) { elem.Cover = "/Image/" + elem.Cover; }
            Films_LB.ItemsSource = movies;
        }
    }
}
