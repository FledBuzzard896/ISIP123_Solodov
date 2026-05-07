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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShutAndKing.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void AccountBtn_Click(object sender, RoutedEventArgs e)
        {
            // Навигация на страницу профиля
        }
        private void BooksCatalogBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new BooksCatalog());
        }
        private void BooksListsBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new BooksLists());
        }
        private void AdministrationBtn_Click(object sender, RoutedEventArgs e)
        {
            if (User.RoleID == 3)
            {
                MainFrame.NavigationService.Navigate(new AdministrationPage());
            }
            else MessageBox.Show("Тебе тут делать нечего.", "Отказано", MessageBoxButton.OK, MessageBoxImage.Stop);
        }
        private void AuthorPageBtn_Click(object sender, RoutedEventArgs e)
        {

        }
        private void GreenFlagBtn_Click(object sender, RoutedEventArgs e)
        {

        }
        private void RedFlagBtn_Click(object sender, RoutedEventArgs e)
        {

        }
        
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            var answer = MessageBox.Show("Вы перепишите на меня вашу нынешнюю/будущую квартиру?", "Доп. проверка", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (answer == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
