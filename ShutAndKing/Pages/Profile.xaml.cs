using ShutAndKing.Classes;
using ShutAndKing.Dialogs;
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

namespace ShutAndKing.Pages
{
    /// <summary>
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : Page
    {
        public Profile()
        {
            InitializeComponent();
            Loaded += PageLoaded;
        }

        private void AuthorApplicationBtn_Click(object sender, RoutedEventArgs e)
        {
            AuthorApplication_Dialog dialog = new AuthorApplication_Dialog();
            if (dialog != null)
            {
                dialog.ShowDialog();
            }
        }

        private void UnFreezeBtn_Click(object sender, RoutedEventArgs e)
        {
            DefrostingApplication_Dialog dialog = new DefrostingApplication_Dialog(User.ID, -1);
            if (dialog != null)
            {
                dialog.ShowDialog();
            }
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            nameTB.Text = User.Name;
            loginTB.Text = User.Login;
            mailTB.Text = User.Email;
            roleTB.Text = Core.ContextKIP.Roles.FirstOrDefault(x => x.ID == User.ID).Title;

            isFrozen.Text = User.Status;
            if (User.Status == "Заморожен") UnFreezeBtn.Visibility = Visibility.Visible;

            var reviews = Core.ContextKIP.UserReviews.Where(x => x.UserID == User.ID).ToList();
            Reviews_LB.ItemsSource = reviews;
        }
    }
}
