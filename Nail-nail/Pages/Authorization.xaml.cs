using Nail_nail.Classes;
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

namespace Nail_nail.Pages
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        private bool _isChange = false;
        public Authorization()
        {
            InitializeComponent();
        }

        private void ChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            _isChange = !_isChange;

            if (_isChange)
            {
                FIO_StackPanel.Visibility = Visibility.Visible;
                Phone_StackPanel.Visibility = Visibility.Visible;

                ChangeTBlock.Text = "Если у вас уже есть аккаунт: ";
                ChangeBtn.Content = "войти";

                RegOrLogBtn.Content = "Зарегестрироваться";
            }
            else 
            {
                FIO_StackPanel.Visibility = Visibility.Collapsed;
                Phone_StackPanel.Visibility = Visibility.Collapsed;

                ChangeTBlock.Text = "Если у вас нет аккаунта: ";
                ChangeBtn.Content = "зарегистрироваться";

                RegOrLogBtn.Content = "Войти";
            }
        }
        private void RegOrLogBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_isChange)
            {
                if (LoginTBox.Text != "" && PasswordTBox.Password != "" && NameTBox.Text != "" && PhoneTBox.Text != "")
                {
                    User.Login = LoginTBox.Text;
                    User.Password = PasswordTBox.Password;
                    User.FullName = NameTBox.Text;
                    User.PhoneNumber = PhoneTBox.Text;
                    User.isAuthorizated = true;

                    // Запись данных в БД
                    NavigationService.Navigate(new MainPage());
                }
                else MessageBox.Show("Заполните все поля!", "Отказ", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else 
            {
                if (LoginTBox.Text != "" && PasswordTBox.Password != "") 
                {
                    // Проверка на аккаунты
                    // Если да -> норм
                    // Если нет -> messagebox
                }
                else MessageBox.Show("Заполните все поля!", "Отказ", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        private static bool IsTextAllowed(string text)
        {
            return text.All(char.IsDigit);
        }
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
