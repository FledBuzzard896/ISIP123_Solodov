using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                int fDig, sDig, finalID;
                MatchCollection matches = Regex.Matches(elem.Name, @"\d+");
                fDig = Convert.ToInt32(matches[0].Value);
                sDig = Convert.ToInt32(matches[1].Value);

                if (fDig == 1)
                {
                    finalID = sDig;
                }
                else
                {
                    finalID = 10 + sDig;
                }

                if (elem.IsChecked == true && Core.ContextKIP.AfishaSit.ToList().Contains(Core.ContextKIP.AfishaSit.FirstOrDefault(x => x.SitID == finalID && x.isTaken == true)) == false)
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


            List<AfishaSit> UsedSeatsLst = Core.ContextKIP.AfishaSit.Where(x => x.AfishaID == SessionClass.MovieAfisha.ID).ToList();

            foreach (var elem in UsedSeatsLst) 
            {
                foreach (CheckBox sit in Row1.Children.OfType<CheckBox>()) 
                {
                    if (Convert.ToInt32(sit.Name.Replace("row1_sit", "")) == elem.SitID && elem.isTaken == true) 
                    {
                        sit.IsChecked = true;
                        sit.IsEnabled = false;
                    }
                }
                foreach (CheckBox sit in Row2.Children.OfType<CheckBox>())
                {
                    if (Convert.ToInt32(sit.Name.Replace("row2_sit", "")) + 10 == elem.SitID && elem.isTaken == true)
                    {
                        sit.IsChecked = true;
                        sit.IsEnabled = false;
                    }
                }
            }
        }

        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            KinoAfishaPage p = new KinoAfishaPage(Core.ContextKIP.Films.First(x => x.ID == SessionClass.MovieAfisha.FilmID));
            SessionClass.ClearAfisha();
            NavigationService.Navigate(p);
        }
    }
}
