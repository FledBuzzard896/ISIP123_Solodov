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

namespace ShutAndKing.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для ChangeListForBook_Dialog.xaml
    /// </summary>
    public partial class ChangeListForBook_Dialog : Window
    {
        Books book = null;
        public ChangeListForBook_Dialog(Books inputBook)
        {
            InitializeComponent();

            book = inputBook;
            Loaded += PageLoaded;
        }

        private void ChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            ReadingListSection toLst = To_ComboBox.SelectedItem as ReadingListSection;

            // Обработка нулевого значения
            if (toLst == null) { MessageBox.Show("Выберите список, куда хотите переместить книгу.", "Отказано", MessageBoxButton.OK, MessageBoxImage.Stop); return; }

            var bookLine = Core.ContextHOME.UserReadingList.FirstOrDefault(x => x.UserID == User.ID && x.BookID == book.ID);
            bookLine.SectionID = toLst.ID;
            Core.ContextHOME.SaveChanges();

            MessageBox.Show("Книга перемещена в другой список", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            this.DialogResult = true;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            var userReadingEntry = Core.ContextHOME.UserReadingList.Where(x => x.UserID == User.ID && x.BookID == book.ID).ToList();
            var elem = userReadingEntry[0];

            From_ComboBox.SelectedValuePath = "ReadingListSection.Title";

            if (userReadingEntry != null)
            {
                From_ComboBox.ItemsSource = userReadingEntry;
                From_ComboBox.SelectedValue = elem.ReadingListSection.Title;
                From_ComboBox.DisplayMemberPath = "ReadingListSection.Title";
            }

            var sections = Core.ContextHOME.ReadingListSection.Where(x => x.Title != elem.ReadingListSection.Title).ToList();
            To_ComboBox.ItemsSource = sections;
            To_ComboBox.DisplayMemberPath = "Title";
        }
    }
}
