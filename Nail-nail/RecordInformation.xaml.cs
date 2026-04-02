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
        private string _services = "";
        private string _payment = "";
        public RecordInformation()
        {
            InitializeComponent();
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            // Подсчет тыкнутых кнопочек
            int count_chbox = 0;
            int count_rbox = 0;
            foreach (var chBox in Servises_LB.Items.OfType<CheckBox>()) 
            {
                if (chBox.IsChecked == true) 
                {
                    count_chbox++; 
                    break;
                }   
            }
            if (sbp_method.IsChecked == true || mir_method.IsChecked == true) count_rbox++;

            // Если какая-та кнопочка не тыкнута --> отказ
            if (count_rbox < 1 && count_chbox < 1) return;

            // Иначе:
            foreach (var chBox in Servises_LB.Items.OfType<CheckBox>())
            {
                if (chBox.IsChecked == true) { _services += $" - {chBox.Content}\n"; }
            }
            if (sbp_method.IsChecked == true) _payment = "СБП";
            else _payment = "Карта МИР";

            // Создание заказа
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e) { this.Close(); }
    }
}
