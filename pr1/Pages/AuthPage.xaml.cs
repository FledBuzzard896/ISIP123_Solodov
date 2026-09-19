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
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void entryBtn_Click(object sender, RoutedEventArgs e)
        {
            var acc = Core.Context.Users_.FirstOrDefault(x => x.Login == login.Text);
            if (acc == null)
            {
                MessageBox.Show("не найдено");
            }
            else 
            {
                Core.CurrentUser = acc;
                NavigationService.Navigate(new MainPage());
            }
        }

        private void regBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegPage());
        }

        private void entryGuestBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }
    }
}
