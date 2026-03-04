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
    /// Логика взаимодействия для HistoryPage.xaml
    /// </summary>
    public partial class HistoryPage : Page
    {
        List<assembly> ItemsHistory = new List<assembly>();
        public HistoryPage()
        {
            InitializeComponent();

            ItemsHistory = Core.ContextKIP.assembly.ToList();
            History_LB.ItemsSource = ItemsHistory;
        }

        private void goback_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void mouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedSborka = History_LB.SelectedItem as assembly;
            var lstOfComponents = Core.ContextKIP.partassembly.Where(x => x.assemblyid == selectedSborka.id).ToList();

            string mainMessage = $"============================================\n";
            double total = 0;

            foreach (var item in lstOfComponents) 
            {
                total += Convert.ToDouble(item.basepart.price);
                mainMessage += $"{item.basepart.PrintInfo}\n============================================\n";
            }

            mainMessage += $"\nСтоимость: {total}₽";
            MessageBox.Show(mainMessage, "Мини-справочка", MessageBoxButton.OK);
        }
    }
}
