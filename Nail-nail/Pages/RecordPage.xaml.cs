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

namespace Nail_nail.Pages
{
    /// <summary>
    /// Логика взаимодействия для RecordPage.xaml
    /// </summary>
    public partial class RecordPage : Page
    {
        private Users _master;
        public RecordPage(Users inputMaster)
        {
            InitializeComponent();
            _master = inputMaster;
            Loaded += PageLoaded;
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToDateTime(DateCalendar.Text).Date < DateTime.Now.Date) { MessageBox.Show("Вы не можете забронировать прошедшее число!", "Отказ", MessageBoxButton.OK, MessageBoxImage.Stop); }
            if (TimeCalendar.Text == null) { MessageBox.Show("Заполните время приёма!", "Отказ", MessageBoxButton.OK, MessageBoxImage.Stop); }

            // Сохранение данных в БД
        }
        private void BackBtn_Click(object sender, RoutedEventArgs e) { NavigationService.GoBack(); }

        private void DateCalendar_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = DateCalendar.SelectedDate;
            if (selectedDate.HasValue)
            {
                if (selectedDate < DateTime.Now.Date)
                {
                    foreach (ComboBoxItem item in TimeCalendar.Items)
                    {
                        item.IsEnabled = false;
                        item.Foreground = Brushes.LightGray;
                    }
                }
                else
                {
                    foreach (ComboBoxItem item in TimeCalendar.Items)
                    {
                        item.IsEnabled = true;
                        item.Foreground = Brushes.Black;
                    }
                    DateTime date = selectedDate.Value.Date;

                    // Проверка на то, какое время и когда свободно
                    var MasterAppointments = Core.ContextKIP.Appointments.Where(x => x.MasterID == _master.ID).ToList();
                    foreach (var appointment in MasterAppointments)
                    {
                        if (appointment.AppointmentDateTime.Date != date) { continue; }
                        if (appointment.IsCancelled == true || appointment.IsCompleted == true) { continue; }

                        // Рассчитываем интервал занятости
                        TimeSpan start = appointment.AppointmentDateTime.TimeOfDay;
                        int duration = Convert.ToInt32(appointment.ServiceTypes.DurationMinutes);
                        TimeSpan end = start.Add(TimeSpan.FromMinutes(duration));

                        foreach (ComboBoxItem time in TimeCalendar.Items)
                        {
                            if (TimeSpan.TryParse(time.Content.ToString(), out TimeSpan slotTime))
                            {
                                if (slotTime >= start && slotTime <= end)
                                {
                                    time.IsEnabled = false;
                                    time.Foreground = Brushes.LightGray;
                                }
                            }
                        }
                    }
                }
            }
        }
        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            master_name.Text = $"МАСТЕР: {_master.FullName}";

            var servicesOfMaster = Core.ContextKIP.MasterServices.Where(x => x.MasterID == _master.ID).ToList();
            Services_LB.ItemsSource = servicesOfMaster
                .Select(x => new
                {
                    ServiceName = x.ServiceTypes.ServiceName,
                    DurationMinutes = x.ServiceTypes.DurationMinutes,
                    Price = x.ServiceTypes.Price,
                });
        }
    }
}
