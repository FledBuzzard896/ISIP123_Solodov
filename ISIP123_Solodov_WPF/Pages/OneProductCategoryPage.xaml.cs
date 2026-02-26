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
    /// Логика взаимодействия для OneProductCategoryPage.xaml
    /// </summary>
    public partial class OneProductCategoryPage : Page
    {
        string title;
        public OneProductCategoryPage(string inputTitle)
        {
            InitializeComponent();

            title = inputTitle;

            Loaded += MyPageLoaded;
        }

        private void MyPageLoaded(object sender, RoutedEventArgs e) 
        {
            switch (title)
            {
                case "Процессоры":
                    var cpusID = Core.ContextHOME.cpu.ToList().Select(c => c.id).ToList();
                    List<basepart> Products = Core.ContextHOME.basepart.Where(x => cpusID.Contains(x.id)).ToList();
                    Products_LB.ItemsSource = Products;
                    break;

                case "Видеокарты":
                    break;

                case "Оперативная память":
                    break;

                case "Материнская плата":
                    break;

                case "Корпуса":
                    break;

                case "Блок питания":
                    break;

                case "Кулер для ЦП":
                    break;

                case "Жесткий диск/SSD":
                    break;
            }
        }
    }
}
