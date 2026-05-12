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
    /// Логика взаимодействия для ChangePassword_Dialog.xaml
    /// </summary>
    public partial class ChangePassword_Dialog : Window
    {
        private int _UserID;

        public ChangePassword_Dialog(int inputID)
        {
            InitializeComponent();
            _UserID = inputID;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            var ans = MessageBox.Show("Закрыть окно смены пароля?", "Промежуточный процесс", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (ans == MessageBoxResult.Yes)
            {
                this.DialogResult = false;
                this.Close();
            }
        }

        private void ChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(firstPassword.Text) || string.IsNullOrWhiteSpace(secondPassword.Text))
            {
                MessageBox.Show("Заполните оба поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            if (firstPassword.Text.Trim() != secondPassword.Text.Trim())
            {
                MessageBox.Show("Пароли не совпадают!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            Users _User = Core.ContextKIP.Users.FirstOrDefault(u => u.ID == _UserID);
            if (_User is null)
            {
                MessageBox.Show("Пользователь не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            else
            {
                _User.Password = secondPassword.Text.Trim();
                Core.ContextKIP.SaveChanges();

                this.DialogResult = true;
                this.Close();
            }
        }
    }
}
