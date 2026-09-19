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

namespace pr1.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            var products = Core.Context.Products_.ToList();
            products_lb.ItemsSource = products;

            if (Core.CurrentUser is null) { account.Text = "Акк: Гость"; }
            else { account.Text = $"Акк: {Core.CurrentUser.FirstName}"; }
        }
    }
}
