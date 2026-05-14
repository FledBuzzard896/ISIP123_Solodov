using ShutAndKing.Classes;
using ShutAndKing.DB_Models;
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
using Microsoft.EntityFrameworkCore;

namespace ShutAndKing.Pages
{
    /// <summary>
    /// Логика взаимодействия для BooksCatalog.xaml
    /// </summary>
    public partial class BooksCatalog : Page
    {
        private List<Books> _allBooks;              // все товары после поиска (без фильтров и сортировки)
        private string _currentFilter = "Все";      // выбранный фильтр (текст из ComboBox)
        private string _currentSort = "Без сортировки";

        public BooksCatalog()
        {
            InitializeComponent();
            Loaded += PageLoaded;
        }

        private void Enter_Click(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            string searchQuery = searchTB.Text.Trim();
            if (string.IsNullOrEmpty(searchQuery))
            {
                _allBooks = Core.ContextHOME.Books.ToList();
            }
            else
            {
                _allBooks = Core.ContextHOME.Books
                    .Where(p => p.Title.Contains(searchQuery) || p.Users.Name.Contains(searchQuery))
                    .ToList();
            }
            ApplyFiltersAndSort();
        }


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

        private double GetAverageRating(Books book)
        {
            // Если отзывы не загружены или их нет – возвращаем 0
            if (book.UserReviews == null || !book.UserReviews.Any())
                return 0;

            return book.UserReviews.Average(r => r.Rating);
        }

        private void ApplyFiltersAndSort()
        {
            if (_allBooks == null) return;

            IEnumerable<Books> query = _allBooks;

            // --- Фильтрация ---
            if (_currentFilter != "Все")
            {
                query = query.Where(book => book.Genres.Any(g => g.Title == _currentFilter)).ToList();
            }

            // Сортировка
            switch (_currentSort)
            {
                case "Название (убывание)":
                    query = query.OrderByDescending(b => b.Title, StringComparer.CurrentCultureIgnoreCase);
                    break;
                case "Название (возрастание)":
                    query = query.OrderBy(b => b.Title, StringComparer.CurrentCultureIgnoreCase);
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
            Books_LB.ItemsSource = query;
        }

        private void AddToList_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button?.Tag is Books book)
            {
                AddToLists_Dialog dialog = new AddToLists_Dialog(book);
                dialog.ShowDialog();
            }
        }

        private void Books_LB_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Books selectedBook = Books_LB.SelectedItem as Books;
            if (selectedBook == null) return;

            PageOfBook page = new PageOfBook(selectedBook);
            NavigationService.Navigate(page);
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            Books_LB.ItemsSource = Core.ContextHOME.Books.Include(b => b.Users).ToList();
            _allBooks = Core.ContextHOME.Books.ToList();

            // Жанры
            List<string> filterItems = new List<string> { "Все" };
            foreach (var genre in Core.ContextHOME.Genres.ToList())
                filterItems.Add($"{genre.Title}");
            filter_box.ItemsSource = filterItems;

            // Сортиров4ка
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
