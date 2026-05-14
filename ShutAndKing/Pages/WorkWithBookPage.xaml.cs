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
    /// Логика взаимодействия для WorkWithBookPage.xaml
    /// </summary>
    public partial class WorkWithBookPage : Page
    {
        private Books book = null;
        private List<GenresStatusModel> GenresList = new List<GenresStatusModel>();

        public WorkWithBookPage(Books inputBook)
        {
            InitializeComponent();
            this.DataContext = this;

            book = inputBook;
            Loaded += PageLoaded;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (book == null)
            {
                Books newBook = new Books()
                {
                    Title = TitleTBox.Text.Trim(),
                    Description = DescriptionTBox.Text.Trim(),
                    PathToCover = "",
                    Text = TextTBox.Text,
                    AuthorID = User.ID,
                    Status = "Не заморожена"
                };

                foreach (var item in GenresList) 
                {
                    if (item.IsSelected) 
                    {
                        newBook.Genres.Add(Core.ContextHOME.Genres.First(x => x.Title == item.Name));
                    }
                }
                
                Core.ContextHOME.Books.Add(newBook);
                
                MessageBox.Show("Книга добавлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(TitleTBox.Text) == true && string.IsNullOrWhiteSpace(DescriptionTBox.Text) == true && string.IsNullOrWhiteSpace(TextTBox.Text) == true)
                {
                    MessageBox.Show("Все поля должны быть заполнены!", "Отказ", MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }

                book.Title = TitleTBox.Text;
                book.Description = DescriptionTBox.Text;
                book.Text = TextTBox.Text;
                MessageBox.Show("Книга изменена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Core.ContextHOME.SaveChanges();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (book == null)
            {
                MessageBox.Show("Книга не найдена!", "Ошибка удаления книги", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else
            {
                var ans = MessageBox.Show($"Вы точно хотите удалить книгу: {book.Title}", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (ans == MessageBoxResult.Yes)
                {
                    Core.ContextHOME.Books.Remove(book);
                    Core.ContextHOME.SaveChanges();
                }
                return;
            }
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            var listOfGenres = Core.ContextHOME.Genres.ToList();

            if (book != null)
            {
                TitleTBox.Text = book.Title;
                DescriptionTBox.Text = book.Description;
                TextTBox.Text = book.Text;

                //foreach (var elem in listOfGenres) 
                //{
                //    GenresList.Add(new GenresStatusModel { Name = elem.Title, IsSelected = false });
                //}

                //foreach (var )
                //GenreBox.ItemsSource = GenresList;
            }

            foreach (var elem in listOfGenres)
            {
                GenresList.Add(new GenresStatusModel { Name = elem.Title, IsSelected = false });
            }
            GenreBox.ItemsSource = GenresList;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
