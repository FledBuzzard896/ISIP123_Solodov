using Nail_nail.Classes;
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
using System.Windows.Shapes;

namespace Nail_nail
{
    /// <summary>
    /// Логика взаимодействия для RecordInformation.xaml
    /// </summary>
    public partial class RecordInformation : Window
    {
        private string _payment = "";
        private string _appointmentDate = "";
        private Users _master;
        private ServiceTypes _service;
        public RecordInformation(Users master, string appoitmentDate, ServiceTypes service)
        {
            InitializeComponent();

            masterTB.Text = $"ФИО Мастера: {master.FullName}";
            timeTB.Text = $"Дата: {appoitmentDate}";
            serviceTB.Text = $"Услуга: {service.ServiceName}";
            priceTB.Text = $"Стоимость: {service.Price}₽";

            _master = master;
            _service = service;
            _appointmentDate = appoitmentDate;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            // Подсчет тыкнутых кнопочек
            int count_rbox = 0;
            if (sbp_method.IsChecked == true || mir_method.IsChecked == true) count_rbox++;

            // Если какая-та кнопочка не тыкнута --> отказ
            if (count_rbox < 1) return;
            // Иначе:
            if (sbp_method.IsChecked == true) _payment = "Online";
            else if (cash_method.IsChecked == true) _payment = "Cash";
            else _payment = "Card";

            // Создание заказа
            var newApp = new Appointments
            {
                ClientID = IUser.AppUser.UserID,
                MasterID = _master.ID,
                ServiceTypeID = _service.ID,
                AppointmentDateTime = Convert.ToDateTime(_appointmentDate),
                TotalPrice = _service.Price,
                PaymentMethod = _payment,
                Comment = commentTB.Text,
                IsCompleted = false,
                IsCancelled = false,
                CreatedAt = DateTime.Now, 
            };

            Core.ContextKIP.Appointments.Add(newApp);
            Core.ContextKIP.SaveChanges();

            this.DialogResult = true;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e) { this.Close(); }
    }
}
