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
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void page2_Click(object sender, RoutedEventArgs e)
        {
            Page2 page2 = new Page2();
            NavigationService.Navigate(page2);
        }

        private void page3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void page4_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
