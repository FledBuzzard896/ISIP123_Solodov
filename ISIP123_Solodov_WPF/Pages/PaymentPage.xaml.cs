using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
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
    /// Логика взаимодействия для PaymentPage.xaml
    /// </summary>
    public partial class PaymentPage : Page
    {
        public PaymentPage()
        {
            InitializeComponent();

            this.Loaded += MyPage_Loaded;
        }

        private void pay_Click(object sender, RoutedEventArgs e)
        {

        }

        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            name.Text = null;
            day.Text = null;
            time.Text = null;
            hall.Text = null;
            type.Text = null;
            seats.Text = null;
            price.Text = null;

            NavigationService.GoBack();
        }

        private void MyPage_Loaded(object sender, RoutedEventArgs e)
        {
            var film = Core.ContextHOME.Films.FirstOrDefault(x => x.ID == SessionClass.MovieAfisha.FilmID);
            name.Text = "Название фильма: " + film.Name;

            day.Text = "Дата сеанса: " + SessionClass.MovieAfisha.DateOfSession.Date.ToString();

            time.Text = "Время сеанса: " + SessionClass.MovieAfisha.DateOfSession.TimeOfDay.ToString();

            hall.Text = "Зал: " + SessionClass.MovieAfisha.HallNumber.ToString();

            var ttt = Core.ContextHOME.CinemaHallType.FirstOrDefault(x => x.ID == SessionClass.MovieAfisha.HallTypeID);
            type.Text = "Тип зала: " +  ttt.Type;

            int count = 0;
            string newStrElem = null;
            foreach (var elem in SessionClass.UsedSeats) 
            {
                count++;

                newStrElem = elem.Replace("row", "Ряд ");
                newStrElem = newStrElem.Replace("_sit", " Место ");

                seats.Text += newStrElem + "   ";
            }

            price.Text = "Цена: " +  (565 * count).ToString();
        }
    }
}
