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

using ISIP123_Solodov_WPF;

namespace ISIP123_Solodov_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        Page nextPage = null;
        public Registration(Page inputPage)
        {
            nextPage = inputPage;

            InitializeComponent();
        }

        private void enterInfo_Click(object sender, RoutedEventArgs e)
        {
            DateTime? selectedDate = birthday.SelectedDate;

            if (login.Text != "" && password.Text != "" && password_2.Text != "" && fio.Text != "" && password.Text == password_2.Text && selectedDate.HasValue) 
            {
                Users newUser = new Users 
                {
                    Login = login.Text,
                    Password = password.Text,
                    Fullname = fio.Text,
                    Birthday = selectedDate.Value,
                };

                Core.ContextHOME.Users.Add(newUser);
                Core.ContextHOME.SaveChanges();

                UserClass.Login = login.Text;
                UserClass.Password = password.Text;
                UserClass.FIO = fio.Text;
                UserClass.Birthday = selectedDate.Value;

                UserClass.IsLogged = true;

                MessageBoxResult res = MessageBox.Show("Вы были успешно зарагистрированы!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                if (res == MessageBoxResult.OK) 
                {
                    NavigationService.Navigate(nextPage);
                }
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
