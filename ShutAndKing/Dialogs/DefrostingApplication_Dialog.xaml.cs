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
    /// Логика взаимодействия для DefrostingApplication_Dialog.xaml
    /// </summary>
    public partial class DefrostingApplication_Dialog : Window
    {
        private int accID;
        private int bookID;

        public DefrostingApplication_Dialog(int inputAccountID, int inputBookID)
        {
            InitializeComponent();

            accID = inputAccountID;
            bookID = inputBookID;

            Loaded += PageLoaded;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void SendReasonBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(reasonTBox.Text))
            {
                MessageBox.Show("Заполните причину", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            var newDefrostApplication = new DefrostingApplication()
            {
                UserID = User.ID,
                AccountID = accID,
                BookID = bookID,
                Reason = reasonTBox.Text.Trim()
            };
    
            Core.ContextKIP.DefrostingApplication.Add(newDefrostApplication);
            Core.ContextKIP.SaveChanges();

            MessageBox.Show("Заявление успешно отправлено!", "Выполнено", MessageBoxButton.OK, MessageBoxImage.Information);
            this.DialogResult = true;
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            if (accID > bookID)
            {
                applicationTB.Text = "Заявление на разморозку Аккаунта";
            }
            else
            {
                applicationTB.Text = "Заявление на разморозку Книги";
            }

        }
    }
}
