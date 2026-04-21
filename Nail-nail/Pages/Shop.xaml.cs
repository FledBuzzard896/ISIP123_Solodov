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
    /// Логика взаимодействия для Shop.xaml
    /// </summary>
    public partial class Shop : Page
    {
        public Shop()
        {
            Loaded += PageLoaded;
            InitializeComponent();
        }

        private void Account_ButtonClick(object sender, RoutedEventArgs e)
        {
            if (!IUser.AppUser.isAuthorizated) NavigationService.Navigate(new Authorization());
            else NavigationService.Navigate(new Account());
        }
        private void SignUp_ButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                var master = button.Tag as Users;
                if (master != null)
                {
                    if (IUser.AppUser.isAuthorizated) NavigationService.Navigate(new RecordPage(master));
                    else MessageBox.Show("Войдите в аккаунт.", "Отказано", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            }
        }

        private void Records_ButtonClick(object sender, RoutedEventArgs e) 
        {
            NavigationService.Navigate(new MainPage());
        }

        private void Enter_Click(object sender, KeyEventArgs e) 
        {
            if (e.Key != Key.Enter) { return; }

            // Поиск + сортировка + фильтрация
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
