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

namespace ISIP123_Solodov_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для CategoriesPage.xaml
    /// </summary>
    public partial class CategoriesPage : Page
    {
        List<string> categories = new List<string> { "Процессоры", "Видеокарты", "Оперативная память", "Материнская плата", "Корпуса", "Блок питания", "Кулер для ЦП", "Жесткий диск/SSD" };
        public CategoriesPage()
        {
            InitializeComponent();

            Categories_LB.ItemsSource = categories;
        }

        void Category_Click(object sender, RoutedEventArgs e) 
        {
            Button btn = sender as Button;
            string title = btn.Content.ToString();

            OneProductCategoryPage page = new OneProductCategoryPage(title);
            NavigationService.Navigate(page);
        }
    }
}
