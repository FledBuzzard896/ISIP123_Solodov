using ShutAndKing.Classes;
using ShutAndKing.DB_Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                Email_StackPanel.Visibility = Visibility.Visible;

                ChangeTBlock.Text = "Если у вас уже есть аккаунт: ";
                ChangeBtn.Content = "войти";

                RegOrLogBtn.Content = "Зарегестрироваться";
            }
            else
            {
                FIO_StackPanel.Visibility = Visibility.Collapsed;
                Email_StackPanel.Visibility = Visibility.Collapsed;

                ChangeTBlock.Text = "Если у вас нет аккаунта: ";
                ChangeBtn.Content = "зарегистрироваться";

                RegOrLogBtn.Content = "Войти";
            }
        }
        private void RegOrLogBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_isChange)
            {
                if (LoginTBox.Text != "" && PasswordTBox.Password != "" && NameTBox.Text != "" && EmailTBox.Text != "")
                {
                    if (Core.ContextKIP_Local.Users.FirstOrDefault(x => x.Login == LoginTBox.Text) is null)
                    {
                        if (EmailTBox.Text.Contains("@") && EmailTBox.Text.Contains("."))
                        {
                            var newUser = new Users()
                            {
                                Login = LoginTBox.Text,
                                Password = PasswordTBox.Password,
                                Name = NameTBox.Text,
                                Email = EmailTBox.Text,
                                RegisteredOn = DateTime.Now,
                                RoleID = 1,
                                Status = "Активен",
                            };
                            Core.ContextKIP_Local.Users.Add(newUser);
                            Core.ContextKIP_Local.SaveChanges();

                            SetUser(newUser.ID, newUser.Login, newUser.Password, newUser.Name, newUser.Email, (DateTime)newUser.RegisteredOn, newUser.RoleID, newUser.Status);

                            MessageBox.Show("Пользователь успешно зарегистрирован!", "Ладно, проходи", MessageBoxButton.OK, MessageBoxImage.Information);
                            NavigationService.Navigate(new MainPage());
                        }
                        else MessageBox.Show("Неправильный формат почты!", "Опа, ошибочная", MessageBoxButton.OK, MessageBoxImage.Stop);
                    }
                    else MessageBox.Show("Данный логин уже используется!", "Опа, ошибочная", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
                else MessageBox.Show("Заполните все поля!", "Опа, ошибочная", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else
            {
                if (LoginTBox.Text != "" && PasswordTBox.Password != "")
                {
                    var Users = Core.ContextKIP_Local.Users.ToList();
                    if (Users.Any(x => x.Login == LoginTBox.Text))
                    {
                        var User = Users.First(x => x.Login == LoginTBox.Text);
                        if (User.Password == PasswordTBox.Password)
                        {
                            SetUser(User.ID, User.Login, User.Password, User.Name, User.Email, (DateTime)User.RegisteredOn, User.RoleID, User.Status);

                            MessageBox.Show("Вы вошли в систему!", "Ладно, проходи", MessageBoxButton.OK, MessageBoxImage.Information);
                            NavigationService.Navigate(new MainPage());
                        }
                        else MessageBox.Show("Неправильный логин или пароль!", "Опа, ошибочная", MessageBoxButton.OK, MessageBoxImage.Stop);
                    }
                    else MessageBox.Show("Неправильный логин или пароль!", "Опа, ошибочная", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
                else MessageBox.Show("Заполните все поля!", "Отказ", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            var answer = MessageBox.Show("Вы перепишите на меня вашу нынешнюю/будущую квартиру?", "Доп. проверка", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (answer == MessageBoxResult.Yes) 
            {
                Application.Current.Shutdown();
            }
        }
        
        private void SetUser(int id, string login, string password, string fullname, string email, DateTime registeredOn, int roleID,  string status)
        {
            User.ID = id;
            User.Login = login;
            User.Password = password;
            User.Name = fullname;
            User.Email = email;
            User.RegisteredOn = registeredOn;
            User.RoleID = roleID;
            User.Status = status;
        }
    }
}
