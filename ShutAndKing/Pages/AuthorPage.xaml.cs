using ShutAndKing.Classes;
using ShutAndKing.DB_Models;
using ShutAndKing.Dialogs;
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
    /// Логика взаимодействия для AuthorPage.xaml
    /// </summary>
    public partial class AuthorPage : Page
    {
        public AuthorPage()
        {
            InitializeComponent();

            Loaded += PageLoaded;
        }

        private void AddOrEditBookBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button.Tag is Books book)
            {
                NavigationService.Navigate(new WorkWithBookPage(book));
            }
            else
            {
                NavigationService.Navigate(new WorkWithBookPage(null));
            }
        }

        private void UnFrostingBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button.Tag is Books book)
            {
                DefrostingApplication_Dialog dialog = new DefrostingApplication_Dialog(-1, book.ID);
                dialog.ShowDialog();
            }
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            var publishedBooks = Core.ContextHOME.Books.Where(x => x.AuthorID == User.ID && x.Status != "Заморожен").ToList();
            var frozenBooks = Core.ContextHOME.Books.Where(x => x.AuthorID == User.ID && x.Status == "Заморожена").ToList();

            PublishedBooks_LB.ItemsSource = publishedBooks;
            FrozenBooks_LB.ItemsSource = frozenBooks;
        }
    }
}
