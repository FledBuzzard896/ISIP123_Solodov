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
using System.Windows.Shapes;
using ShutAndKing.Classes;

namespace ShutAndKing.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для AddReview_Dialog.xaml
    /// </summary>
    public partial class AddReview_Dialog : Window
    {
        Books book;
        public AddReview_Dialog(Books inputObject)
        {
            InitializeComponent();
            book = inputObject;
            Loaded += PageLoaded;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ReviewTBox.Text)) 
            {
                MessageBox.Show("Отзыв не может быть пустым", "Отказ", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            if (ReviewRating.SelectedItem == null) 
            {
                MessageBox.Show("Поставьте книге оценку", "Отказ", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            UserReviews newReview = new UserReviews()
            {
                UserID = User.ID,
                BookID = book.ID,
                Comment = ReviewTBox.Text.Trim(),
                Rating = Convert.ToInt32(ReviewRating.SelectedItem.ToString()),
                DateOfReview = DateTime.Now,
                Status = "Активен"
            };
            Core.ContextKIP_Local.UserReviews.Add(newReview);
            Core.ContextKIP_Local.SaveChanges();

            this.DialogResult = true;
        }

        private void PageLoaded(object sender, RoutedEventArgs e) 
        {
            // Загрузка рейтинга
            for (int num = 1; num < 11; num++) 
            {
                ReviewRating.Items.Add(num);
            }
        }
    }
}
