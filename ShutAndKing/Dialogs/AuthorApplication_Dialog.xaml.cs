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
    /// Логика взаимодействия для AuthorApplication_Dialog.xaml
    /// </summary>
    public partial class AuthorApplication_Dialog : Window
    {
        public AuthorApplication_Dialog()
        {
            InitializeComponent();
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
                MessageBox.Show("Заполните причину, почему мы должны выдать вам роль Автора :)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            var newApplication = new AuthorApplication()
            {
                UserID = User.ID,
                Reason = reasonTBox.Text.Trim()
            };
    
            Core.ContextKIP_Local.AuthorApplication.Add(newApplication);
            Core.ContextKIP_Local.SaveChanges();

            MessageBox.Show("Заявление успешно отправлено!", "Выполнено", MessageBoxButton.OK, MessageBoxImage.Information);
            this.DialogResult = true;
        }
    }
}
