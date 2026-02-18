using Microsoft.Win32;
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
    /// Логика взаимодействия для KinoAfishaPage.xaml
    /// </summary>
    public partial class KinoAfishaPage : Page
    {
        Films _movie;
        public KinoAfishaPage(Films movie)
        {
            InitializeComponent();

            _movie = movie;

            this.Loaded += MyPage_Loaded;
        }

        private void DateButton_Click(object sender, RoutedEventArgs e)
        {
            isEmptyLB.Text = null;
            Sessions_LB.Visibility = Visibility.Hidden;

            var btn = sender as Button;

            List<Afisha> afishas = Core.ContextKIP.Afisha
                .Where(x => x.FilmID == _movie.ID && x.DateOfSession.Day.ToString() == btn.Content.ToString())
                .ToList();

            Sessions_LB.ItemsSource = afishas;

            if (afishas.Count == 0)
            {
                isEmptyLB.Text = "Сеансов нет.";
                Sessions_LB.Visibility = Visibility.Hidden;
            }
            else
            {
                Sessions_LB.Visibility = Visibility.Visible; // Не забудьте включить видимость!

            }
        }

        private void TimeButton_Click(object sender, RoutedEventArgs e) 
        {
            if (UserClass.IsLogged)
            {
                // NavigationServise.Navigate(страница)
            }
            else 
            {
                MessageBox.Show("Для перехода на страницу оформления билетов, вы должны зарегистрироваться или войти в аккаунт.", "Бык тупогуб", MessageBoxButton.OK, MessageBoxImage.Information);

                //Page next = new СтраницаМестИБилетов();

                //var dialog = new RegOrLog(next);
                //dialog.ShowDialog();
            }
        }

        private void MyPage_Loaded(object sender, RoutedEventArgs e) 
        {

            // Даты на неделю вперед
            btnDay1.Content = 10;
            btnDay2.Content = Convert.ToInt32(btnDay1.Content) + 1;
            btnDay3.Content = Convert.ToInt32(btnDay1.Content) + 2;
            btnDay4.Content = Convert.ToInt32(btnDay1.Content) + 3;
            btnDay5.Content = Convert.ToInt32(btnDay1.Content) + 4;
            btnDay6.Content = Convert.ToInt32(btnDay1.Content) + 5;
            btnDay7.Content = Convert.ToInt32(btnDay1.Content) + 6;
        }

        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
