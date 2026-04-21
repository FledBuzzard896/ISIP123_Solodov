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
            if (DateCalendar.SelectedDate == null) { MessageBox.Show("Выберите дату для записи!", "Отказ", MessageBoxButton.OK, MessageBoxImage.Stop); return; }
            if (DateCalendar.SelectedDate.Value.Date < DateTime.Now.Date) { MessageBox.Show("Вы не можете забронировать прошедшее число!", "Отказ", MessageBoxButton.OK, MessageBoxImage.Stop); return; }
            if (TimeCalendar.SelectedItem == null) { MessageBox.Show("Заполните время приёма!", "Отказ", MessageBoxButton.OK, MessageBoxImage.Stop); return; }

            // Берем объект который выбрали кнопкой
            var serviceName = (sender as Button).Tag as string;
            ServiceTypes service = Core.ContextKIP.ServiceTypes.FirstOrDefault(x => x.ServiceName == serviceName);

            // Соедняем дату + время
            DateTime date = DateCalendar.SelectedDate.Value;
            string timeString = (TimeCalendar.SelectedItem as ComboBoxItem).Content.ToString();
            TimeSpan time = TimeSpan.Parse(timeString);
            DateTime fullDateTime = date.Date.Add(time);

            RecordInformation dialog = new RecordInformation(_master, fullDateTime.ToString(), service);

            if (dialog.ShowDialog() == true) 
            {
                MessageBox.Show("Запись подтверждена!", "Ладно, проходи", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.GoBack();
            }
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
                        item.Foreground = Brushes.DarkRed;
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
                                    time.Foreground = Brushes.DarkRed;
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

            DateCalendar.SelectedDate = null;
            TimeCalendar.SelectedItem = null;

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
