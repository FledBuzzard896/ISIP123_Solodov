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

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void AccountBtn_Click(object sender, RoutedEventArgs e)
        {
            // Навигация на страницу профиля
        }

        private void BooksCatalogBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BooksListsBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AdministrationBtn_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
