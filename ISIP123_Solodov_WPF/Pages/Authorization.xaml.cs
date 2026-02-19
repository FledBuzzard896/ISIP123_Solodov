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
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        Page page = null;
        public Authorization(Page inputPage)
        {
            page = inputPage;

            InitializeComponent();
        }

        private void enter_Click(object sender, RoutedEventArgs e)
        {
            List<Users> data = Core.ContextKIP.Users.ToList();

            //login.Text = data[0].Login;
            //password.Text = data[0].Password;

            foreach (Users user in data)
            {
                if (user.Login == login.Text)
                {
                    if (user.Password == password.Text)
                    {
                        UserClass.FIO = user.Fullname;
                        UserClass.Birthday = (DateTime)user.Birthday;
                        UserClass.Login = user.Login;
                        UserClass.Password = user.Password;

                        UserClass.IsLogged = true;

                        if (UserClass.IsNextPageIsProfile) 
                        {
                            page = new ProfilePage();
                        }

                        // next page
                        NavigationService.Navigate(page);
                        return;
                    }
                }
            }
            MessageBox.Show("Аккаунт не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            Registration regPage = new Registration(page);
            NavigationService.Navigate(regPage);
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            var movies = Core.ContextKIP.Films.ToList();
            DeletePathGorImages(movies);

            NavigationService.GoBack();
        }

        private void DeletePathGorImages(List<Films> movies)
        {
            foreach (var elem in movies) { elem.Cover = elem.Cover.Substring(7); }
        }
    }
}
