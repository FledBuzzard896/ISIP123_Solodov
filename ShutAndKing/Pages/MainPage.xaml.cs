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
            Loaded += PageLoaded;
        }

        private void AccountBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Profile());
        }
        private void BooksCatalogBtn_Click(object sender, RoutedEventArgs e)
        {
            if (User.Status == "Заморожен") 
            {
                MessageBox.Show("Для того, чтобы порльзоваться сервисом, ваш аккаунт должен быть Активен", "Отказано в доступе", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            MainFrame.NavigationService.Navigate(new BooksCatalog());
        }
        private void BooksListsBtn_Click(object sender, RoutedEventArgs e)
        {
            if (User.Status == "Заморожен")
            {
                MessageBox.Show("Для того, чтобы порльзоваться сервисом, ваш аккаунт должен быть Активен", "Отказано в доступе", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            MainFrame.NavigationService.Navigate(new BooksLists());
        }
        private void AdministrationBtn_Click(object sender, RoutedEventArgs e)
        {
            if (User.Status == "Заморожен")
            {
                MessageBox.Show("Для того, чтобы порльзоваться сервисом, ваш аккаунт должен быть Активен", "Отказано в доступе", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            if (User.RoleID == 3)
            {
                MainFrame.NavigationService.Navigate(new AdministrationPage());
            }
            else MessageBox.Show("Тебе тут делать нечего.", "Отказано", MessageBoxButton.OK, MessageBoxImage.Stop);
        }
        private void AuthorPageBtn_Click(object sender, RoutedEventArgs e)
        {
            if (User.Status == "Заморожен")
            {
                MessageBox.Show("Для того, чтобы порльзоваться сервисом, ваш аккаунт должен быть Активен", "Отказано в доступе", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            if (User.RoleID == 2)
            {
                MainFrame.NavigationService.Navigate(new AuthorPage());
            }
            else MessageBox.Show("Вы не являетесь автором, но вы можете им стать, если подадите заявку на авторство.", "Отказано", MessageBoxButton.OK, MessageBoxImage.Stop);
        }
        private void GreenFlagBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ваш аккаунт не заморожен", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void RedFlagBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ваш аккаунт заморожен, вы можете опровергнуть свою заморозку в специальной форме.", "Информация", MessageBoxButton.OK, MessageBoxImage.Stop);
        }
        
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            var answer = MessageBox.Show("Вы перепишите на меня вашу нынешнюю/будущую квартиру?", "Доп. проверка", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (answer == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void PageLoaded(object sender, RoutedEventArgs e) 
        {
            if (User.Status == "Заморожен") 
            {
                GreenFlagBtn.Visibility = Visibility.Collapsed;
                RedFlagBtn.Visibility = Visibility.Visible;
            }
        }
    }
}
