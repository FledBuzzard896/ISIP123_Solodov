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

        public WorkWithBookPage(Books inputBook)
        {
            InitializeComponent();

            book = inputBook;
            Loaded += PageLoaded;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            // Продумать изменение жанра книги!!!!!!!!!!!!!!!1
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
    

            Core.ContextKIP.Books.Add(newBook);
            }
            else
            {
                book.Title = TitleTBox.Text;
                book.Description = DescriptionTBox.Text;
                book.Text = TextTBox.Text;
            }
            Core.ContextKIP.SaveChanges();
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
                    Core.ContextKIP.Books.Remove(book);
                    Core.ContextKIP.SaveChanges();
                }
                else
                {
                    return;
                }
            }
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            if (book != null)
            {
                TitleTBox.Text = book.Title;
                DescriptionTBox.Text = book.Description;
                TextTBox.Text = book.Text;
            }
        }
    }
}
