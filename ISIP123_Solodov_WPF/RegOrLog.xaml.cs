using ISIP123_Solodov_WPF.Pages;
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

namespace ISIP123_Solodov_WPF
{
    /// <summary>
    /// Логика взаимодействия для RegOrLog.xaml
    /// </summary>
    public partial class RegOrLog : Window
    {
        public RegOrLog()
        {
            InitializeComponent();
        }

        private void autho_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow main)
            {
                main.mainFrame.Navigate(new Authorization());
            }
            this.Close();
        }

        private void registr_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow main)
            {
                main.mainFrame.Navigate(new Registration());
            }
            this.Close();
        }
    }
}
