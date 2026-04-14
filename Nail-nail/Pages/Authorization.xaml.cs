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
                    // Запись пользователя в класс
                    RegUser(LoginTBox.Text, PasswordTBox.Password, NameTBox.Text, PhoneTBox.Text);

                    // Запись данных в БД
                    var newUser = new Users() 
                    {
                        Login = LoginTBox.Text,
                        Password = PasswordTBox.Password,
                        FullName = NameTBox.Text,
                        PhoneNumber = PhoneTBox.Text,
                        Role = 1,
                        CreatedAt = DateTime.Now,
                        Cover = null,
                    };
                    Core.ContextHOME.Users.Add(newUser);
                    Core.ContextHOME.SaveChanges();

                    NavigationService.Navigate(new MainPage());
                }
                else MessageBox.Show("Заполните все поля!", "Отказ", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else 
            {
                if (LoginTBox.Text != "" && PasswordTBox.Password != "") 
                {
                    var users = Core.ContextHOME.Users.Where(x => x.Role == 1).ToList();
                    if (users.Any(x => x.Login == LoginTBox.Text)) 
                    {
                        if (users.Any(x => x.Password == PasswordTBox.Password))
                        {
                            // Запись пользователя из БД
                            var user = users.First(x => x.Login == LoginTBox.Text);
                            RegUser(user.Login, user.Password, user.FullName, user.PhoneNumber);

                            NavigationService.Navigate(new MainPage());
                        }
                        else MessageBox.Show("Неправильный логин или пароль!", "Отказ", MessageBoxButton.OK, MessageBoxImage.Stop);
                    }
                    else MessageBox.Show("Неправильный логин или пароль!", "Отказ", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
                else MessageBox.Show("Заполните все поля!", "Отказ", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        private void RegUser(string login, string password, string fullname, string phonenum) 
        {
            IUser.AppUser.Login = login;
            IUser.AppUser.Password = password;
            IUser.AppUser.FullName = fullname;
            IUser.AppUser.PhoneNumber = phonenum;
            IUser.AppUser.isAuthorizated = true;
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
