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
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace ShutAndKing.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageOfBook.xaml
    /// </summary>
    public partial class PageOfBook : Page
    {
        private Books book = null;

        public PageOfBook(Books inputBook)
        {
            InitializeComponent();
            book = inputBook;
            Loaded += PageLoaded;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void FreezeBook_Click(object sender, RoutedEventArgs e)
        {
            book.Status = "Заморожен";
            Core.ContextHOME.SaveChanges();
        }

        private void FreezeReview_Click(object sender, RoutedEventArgs e)
        {
            if (User.RoleID != 3) {
                MessageBox.Show("Вы не являетесь Администратором", "Отказано!", MessageBoxButton.OK, MessageBoxImage.Stop); 
                return;
            }

            var button = sender as Button;
            if (button?.Tag is UserReviews review)
            {
                Core.ContextHOME.UserReviews.Remove(review);
                Core.ContextHOME.SaveChanges();

                MessageBox.Show("Отзыв заморожен (удалён навсегда, вы его больше никогда не увидите, прощай, земля тебе пуховик)", "Операция выполнена", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Не удалось определить отзыв", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Stop);
            }

        }

        private void BookComplaint_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(book_complaint.Text))
            {
                MessageBox.Show("Причина жалобы не заполнена!", "Отказ", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            Complaints newComplaint = new Complaints
            {
                UserID = User.ID,
                BookID = book.ID,
                ReviewID = null,
                AuthorID = null,
                Reason = book_complaint.Text.Trim()
            };
            Core.ContextHOME.Complaints.Add(newComplaint);
            Core.ContextHOME.SaveChanges();

            MessageBox.Show("Жалоба на книгу отправлена администратору.", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            book_complaint.Clear();
        }

        private void AuthorComplaint_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(author_complaint.Text))
            {
                MessageBox.Show("Причина жалобы не заполнена!", "Отказ", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            Complaints newComplaint = new Complaints
            {
                UserID = User.ID,
                BookID = null,
                ReviewID = null,
                AuthorID = Convert.ToInt32(Core.ContextHOME.Users.First(x => x.ID == book.AuthorID)),
                Reason = author_complaint.Text.Trim()
            };

            MessageBox.Show("Жалоба на автора отправлена администратору.", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            book_complaint.Clear();
        }

        private void ReviewComplaint_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button?.Tag is UserReviews review)
            {
                ReviewComplaint_Dialog dialog = new ReviewComplaint_Dialog(review);
                dialog.ShowDialog();
            }
        }


        private void AddToList_Click(object sender, RoutedEventArgs e)
        {
            AddToLists_Dialog dialog = new AddToLists_Dialog(book);
            if (dialog.ShowDialog() == true)
            {
                MessageBox.Show("Книга добавлена", "Approve", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            // Заголовок страницы
            top_TB.Text = $"Информация о {book.Title}";

            // Обложка
            //if (!string.IsNullOrEmpty(book.CoverPath))
            //{
            //    book_cover.Source = new BitmapImage(new Uri(book.CoverPath, UriKind.RelativeOrAbsolute));
            //}

            // Общая информация
            book_name.Text = book.Title;
            book_descript.Text = book.Description;
            book_text.Text = book.Text;

            // Рейтинг
            var ratingList = Core.ContextHOME.UserReviews.Where(x => x.BookID == book.ID).ToList();
            if (ratingList.Any())
            {
                double avgRating = ratingList.Average(x => x.Rating);
                book_rating.Text = avgRating.ToString("0.0");
            }
            else book_rating.Text = "Нет оценок";

            // Автор
            var author = Core.ContextHOME.Users.First(x => x.ID == book.AuthorID);
            book_author.Text = author.Name;

            // Отзывы
            var usersReviews = Core.ContextHOME.UserReviews
                .Include(x => x.Users) // загружаем автора отзыва
                .Where(x => x.BookID == book.ID)
                .ToList();

            Reviews_LB.ItemsSource = usersReviews;

            // Жанры (оставлено для вас)
            // ...

            // Проверка роли администратора
            if (User.RoleID == 3)
            {
                freezeBookBtn.Visibility = Visibility.Visible;
            }
        }
    }
}
