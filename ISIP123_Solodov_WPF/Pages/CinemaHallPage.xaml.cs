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
    /// Логика взаимодействия для CinemaHallPage.xaml
    /// </summary>
    public partial class CinemaHallPage : Page
    {
        string session;
        List<string> selectedSeats = new List<string>();
        List<CheckBox> allSeats = new List<CheckBox>();

        public CinemaHallPage(string inputSession)
        {
            InitializeComponent();

            session = inputSession;

            this.Loaded += MyPage_Loaded;
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            selectedSeats.Clear();

            foreach (CheckBox elem in allSeats)
            {
                if (elem.IsChecked == true)
                {
                    selectedSeats.Add(elem.Name);
                }
            }

            // Показываем кнопку, если есть хоть одно выбранное место
            PaymentBtn.Visibility = selectedSeats.Count > 0 ? Visibility.Visible : Visibility.Hidden;
        }

        private void PaymentBtn_Click(object sender, RoutedEventArgs e) 
        {
            SessionClass.UsedSeats = selectedSeats;
            PaymentPage paymentPage = new PaymentPage();
            NavigationService.Navigate(paymentPage);
        }

        private void MyPage_Loaded(object sender, RoutedEventArgs e) 
        {
            PaymentBtn.Visibility = Visibility.Hidden;
            SessionClass.ClearSeats();
            allSeats.Clear();
            // Все стулья в одном листе
            foreach (CheckBox elem in Row1.Children.OfType<CheckBox>()) { allSeats.Add(elem); elem.IsChecked = false; }
            foreach (CheckBox elem in Row2.Children.OfType<CheckBox>()) { allSeats.Add(elem); elem.IsChecked = false; }
        }

        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            SessionClass.ClearAfisha();
            NavigationService.GoBack();
        }
    }
}
