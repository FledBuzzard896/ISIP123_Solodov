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
                _allBooks = Core.ContextKIP.Books.ToList();
            }
            else
            {
                _allBooks = Core.ContextKIP.Books
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

        private void ApplyFiltersAndSort()
        {
            if (_allBooks == null) return;

            IEnumerable<Books> query = _allBooks;

            //// --- Фильтрация ---
            //if (_currentFilter != "Все")
            //{
            //    // Определяем, что выбрано: тип или производитель (по суффиксу в скобках)
            //    if (_currentFilter.EndsWith("(тип)"))
            //    {
            //        string typeName = _currentFilter.Replace("(тип)", "").Trim();
            //        query = query.Where(p => p.ProductTypes.TypeName == typeName);
            //    }
            //    else if (_currentFilter.EndsWith("(производитель)"))
            //    {
            //        string manufName = _currentFilter.Replace("(производитель)", "").Trim();
            //        query = query.Where(p => p.Manufacturers.ManufacturerName == manufName);
            //    }
            //}

            //// --- Сортировка ---
            //switch (_currentSort)
            //{
            //    case "Рейтинг (убывание)":
            //        query = query.OrderByDescending(p => p.Rating);
            //        break;
            //    case "Рейтинг (возрастание)":
            //        query = query.OrderBy(p => p.Rating);
            //        break;
            //    default:
            //        query = query.OrderBy(p => p.ProductName);
            //        break;
            //}

            // Применяем к UI
            //Assignment(query.ToList());
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

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            // List<Books> booksList = Core.ContextKIP.Books.ToList();
            // var new_bookList = booksList
            //     .Select(x => new {
            //         BookObject = x,
            //         Title = x.Title,
            //         Name = x.Users.Name,
            //     }).ToList();
            Books_LB.ItemsSource = Core.ContextKIP.Books.Include(b => b.Users).ToList();
        }
    }
}
