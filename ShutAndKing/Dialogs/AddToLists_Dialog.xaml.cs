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

namespace ShutAndKing
{
    /// <summary>
    /// Логика взаимодействия для AddToLists_Dialog.xaml
    /// </summary>
    public partial class AddToLists_Dialog : Window
    {
        private Books _thisBook;
        public AddToLists_Dialog(Books inputBook)
        {
            InitializeComponent();
            Loaded += PageLoaded;

            _thisBook = inputBook;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var dataList = lists.ItemsSource as IEnumerable<dynamic>;
            bool isAnyChecked = dataList.Any(x => x.IsSelected == true);

            if (isAnyChecked)
            {
                var thisCategory = dataList.First(x => x.IsSelected == true);
                var newLine = new UserReadingList
                {
                    UserID = User.ID,
                    BookID = _thisBook.ID,
                    SectionID = thisCategory.SectionID,
                };
                Core.ContextKIP.UserReadingList.Add(newLine);
                Core.ContextKIP.SaveChanges();
                this.DialogResult = true;
            }
            else 
            {
                MessageBox.Show("Вы не выбрали ни одну категорию!", "Не мороси", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            var existingSectionTitles = Core.ContextKIP.UserReadingList
                .Where(x => x.UserID == User.ID && x.BookID == _thisBook.ID)
                .Select(x => x.ReadingListSection.Title)
                .ToList();

            var RL_Titles = Core.ContextKIP.ReadingListSection.ToList()
                .Select(x => new ReadingListViewModel
                {
                    Title = x.Title,
                    IsSelected = Core.ContextKIP.UserReadingList.Any(u => u.UserID == User.ID && u.ReadingListSection.Title == x.Title && u.BookID == _thisBook.ID)
                }).ToList();

            lists.ItemsSource = RL_Titles;
        }
    }
}
