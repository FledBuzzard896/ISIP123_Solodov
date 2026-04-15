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

        }
        private void BackBtn_Click(object sender, RoutedEventArgs e) { NavigationService.GoBack(); }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            master_name.Text = $"МАСТЕР: {_master.FullName}";

            var servicesOfMaster = Core.ContextHOME.MasterServices.Where(x => x.MasterID == _master.ID).ToList();
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
