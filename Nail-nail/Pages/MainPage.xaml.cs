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
using Nail_nail.Classes;

namespace Nail_nail.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            Loaded += PageLoaded;
        }

        private void Shop_ButtonClick(object sender, RoutedEventArgs e)
        {
            if (!IUser.AppUser.isAuthorizated) 
            {
                MessageBox.Show("Войдите в аккаунт", "Отказано в доступе", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            // Навигация на страницу
        }
        private void Account_ButtonClick(object sender, RoutedEventArgs e)
        {
            if (!IUser.AppUser.isAuthorizated) NavigationService.Navigate(new Authorization());
            else NavigationService.Navigate(new Account());
        }
        private void SignUp_ButtonClick(object sender, RoutedEventArgs e) 
        {
            
        }
        private void PageLoaded(object sender, RoutedEventArgs e) 
        {
            if (IUser.AppUser.isAuthorizated)
            {
                AccountLogIn.Visibility = Visibility.Collapsed;
                Account.Visibility = Visibility.Visible;
            }
            else 
            {
                AccountLogIn.Visibility = Visibility.Visible;
                Account.Visibility = Visibility.Collapsed;
            }
        }
    }
}
