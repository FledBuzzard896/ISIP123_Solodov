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
using System.Windows.Shapes;

namespace ShutAndKing.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для ReviewComplaint_Dialog.xaml
    /// </summary>
    public partial class ReviewComplaint_Dialog : Window
    {
        private UserReviews review = null;
        public ReviewComplaint_Dialog(UserReviews input)
        {
            InitializeComponent();
            review = input;

            top.Text = $"Жалоба на обзор: {review.ID}";
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
    
    }

        private void SendComplaintBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Complaint_TBox.Text))
            {
                MessageBox.Show("Заполните причину жалобы на Отзыв.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            var newComplaint = new Complaints
            {
                UserID = User.ID,
                BookID = null,
                ReviewID = review.ID,
                AuthorID = null,
                Reason = Complaint_TBox.Text.Trim(),
            };
    
            Core.ContextKIP.Complaints.Add(newComplaint);
            Core.ContextKIP.SaveChanges();

            MessageBox.Show("Жалоба на отзыв отправлена администратору.", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);

            this.DialogResult = true;
            this.Close();
        }
    }
}
