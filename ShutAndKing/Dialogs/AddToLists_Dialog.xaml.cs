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
            // Получаем список моделей с правильным типом
            var dataList = lists.ItemsSource as List<ReadingListViewModel>;
            if (dataList == null) return;

            var selectedCategory = dataList.FirstOrDefault(x => x.IsSelected);
            if (selectedCategory != null)
            {
                // Проверяем, не добавлена ли уже книга в этот раздел (защита от дубликатов)
                bool alreadyExists = Core.ContextKIP.UserReadingList
                    .Any(ur => ur.UserID == User.ID && ur.BookID == _thisBook.ID && ur.SectionID == selectedCategory.SectionID);

                if (alreadyExists)
                {
                    MessageBox.Show("Эта книга уже находится в выбранном списке.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var flag = Core.ContextKIP.UserReadingList.FirstOrDefault(x => x.UserID == User.ID && x.BookID == _thisBook.ID);
                if (flag != null)
                {
                    // Изменяем
                    flag.SectionID = selectedCategory.SectionID;
                }
                else 
                {
                    // Добавляем
                    var newLine = new UserReadingList
                    {
                        UserID = User.ID,
                        BookID = _thisBook.ID,
                        SectionID = selectedCategory.SectionID,
                    };
                    Core.ContextKIP.UserReadingList.Add(newLine);
                }
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
            // Получаем ID разделов, в которых книга уже находится
            var existingSectionIDs = Core.ContextKIP.UserReadingList
                .Where(x => x.UserID == User.ID && x.BookID == _thisBook.ID)
                .Select(x => x.SectionID)
                .ToList();

            // Создаём список моделей для отображения с проставленными флагами IsSelected
            var categories = Core.ContextKIP.ReadingListSection.ToList()
                .Select(x => new ReadingListViewModel
                {
                    Title = x.Title,
                    SectionID = x.ID,
                    IsSelected = existingSectionIDs.Contains(x.ID)   // если уже добавлена – ставим галочку
                }).ToList();

            lists.ItemsSource = categories;
        }
    }
}
