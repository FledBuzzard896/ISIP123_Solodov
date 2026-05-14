using ShutAndKing.Classes;
using ShutAndKing.DB_Models;
using ShutAndKing.Dialogs;
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

namespace ShutAndKing.Pages
{
    /// <summary>
    /// Логика взаимодействия для BooksLists.xaml
    /// </summary>
    public partial class BooksLists : Page
    {
        private IEnumerable<Books> _allBooks;
        private List<Books> _booksInSection;
        private string _currentFilter = "Все";
        private string _currentSort = "Без сортировки";
        private string _currentSectionTitle = "Читаю"; // по умолчанию

        public BooksLists()
        {
            InitializeComponent();
            Loaded += PageLoaded;
        }


        #region Переключение списков книг
        private void trashBtn_Click(object sender, RoutedEventArgs e)
        {
            trashBtn.IsEnabled = false;
            plansBtn.IsEnabled = true;
            readingBtn.IsEnabled = true;
            alreadyReadBtn.IsEnabled = true;
            _currentSectionTitle = "Заброшено";

            RefreshCurrentCategory();
        }
        private void plansBtn_Click(object sender, RoutedEventArgs e)
        {
            trashBtn.IsEnabled = true;
            plansBtn.IsEnabled = false;
            readingBtn.IsEnabled = true;
            alreadyReadBtn.IsEnabled = true;
            _currentSectionTitle = "В планах";

            RefreshCurrentCategory();
        }
        private void readingBtn_CLick(object sender, RoutedEventArgs e)
        {
            trashBtn.IsEnabled = true;
            plansBtn.IsEnabled = true;
            readingBtn.IsEnabled = false;
            alreadyReadBtn.IsEnabled = true;
            _currentSectionTitle = "Читаю";

            RefreshCurrentCategory();
        }
        private void alreadyReadBtn_Click(object sender, RoutedEventArgs e)
        {
            trashBtn.IsEnabled = true;
            plansBtn.IsEnabled = true;
            readingBtn.IsEnabled = true;
            alreadyReadBtn.IsEnabled = false;
            _currentSectionTitle = "Прочитано";

            RefreshCurrentCategory();
        }
        private void RefreshCurrentCategory()
        {
            _allBooks = Core.ContextHOME.UserReadingList
                .Where(x => x.UserID == User.ID && x.ReadingListSection.Title == _currentSectionTitle)
                .Select(x => x.Books)
                .ToList();
            BooksInLists_LB.ItemsSource = _allBooks;

            _booksInSection = _allBooks.ToList();
        }
        #endregion


        private void ChangeList_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn?.Tag is Books book)
            {
                ChangeListForBook_Dialog dialog = new ChangeListForBook_Dialog(book);

                if (dialog.ShowDialog() == true) 
                {
                    RefreshCurrentCategory();
                }
            }
            else
            {
                MessageBox.Show("Не удалось получить книгу", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }
        private void Enter_Click(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            string searchQuery = searchTBox.Text.Trim();
            if (string.IsNullOrEmpty(searchQuery))
            {
                _allBooks = _booksInSection;
            }
            else
            {
                _allBooks = _booksInSection
                    .Where(x => x.Title.Contains(searchQuery) || x.Users.Name.Contains(searchQuery))
                    .ToList();
            }
            ApplyFiltersAndSort();
        }


        #region Изменение параметров "Сортировки" и "Фильтрации"
        private void filter_box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _currentFilter = filter_box.SelectedItem?.ToString() ?? "Все";
            ApplyFiltersAndSort();
        }
        private void sorting_box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _currentSort = sorting_box.SelectedItem?.ToString() ?? "Без сортировки";
            ApplyFiltersAndSort();
        }
        private void ApplyFiltersAndSort()
        {
            if (_allBooks == null) return;

            var query = _allBooks.AsEnumerable();

            // --- Фильтрация ---
            if (_currentFilter != "Все")
            {
                query = query.Where(book => book.Genres.Any(g => g.Title == _currentFilter)).ToList();
            }

            // Сортировка
            switch (_currentSort)
            {
                case "Название (убывание)":
                    query = query.OrderByDescending(b => b.Title);
                    break;
                case "Название (возрастание)":
                    query = query.OrderBy(b => b.Title);
                    break;
                case "Оценка (убывание)":
                    query = query.OrderByDescending(b => GetAverageRating(b));
                    break;
                case "Оценка (возрастание)":
                    query = query.OrderBy(b => GetAverageRating(b));
                    break;
                default:
                    query = query.OrderBy(b => b.Title);
                    break;
            }

            // Применяем к UI
            BooksInLists_LB.ItemsSource = query;
        }
        private double GetAverageRating(Books book)
        {
            // Если отзывы не загружены или их нет – возвращаем 0
            if (book.UserReviews == null || !book.UserReviews.Any())
                return 0;

            return book.UserReviews.Average(r => r.Rating);
        }
        #endregion


        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            _allBooks = Core.ContextHOME.UserReadingList
                .Where(x => x.UserID == User.ID && x.ReadingListSection.Title == "Читаю")
                .Select(x => x.Books)   // сразу берём объекты книг
                .ToList();
            BooksInLists_LB.ItemsSource = _allBooks;

            List<string> filterItems = new List<string> { "Все" };
            foreach (var genre in Core.ContextHOME.Genres.ToList())
                filterItems.Add($"{genre.Title}");
            filter_box.ItemsSource = filterItems;

            sorting_box.ItemsSource = new List<string>
            {
                "Без сортировки",
                "Название (убывание)",
                "Навзание (возрастание)",
                "Оценка (убывание)",
                "Оценка (возрастание)"
            };
        }
    }
}
