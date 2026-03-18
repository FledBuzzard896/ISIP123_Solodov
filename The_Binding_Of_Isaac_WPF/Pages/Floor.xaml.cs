using The_Binding_Of_Isaac_WPF.Model;
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

namespace The_Binding_Of_Isaac_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для Floor.xaml
    /// </summary>
    public partial class Floor : Page
    {
        public Floor()
        {
            InitializeComponent();

            ToolBar.ItemsSource = Isaac.inventory;

            hpBar.Text = Isaac.Hp.ToString();
            dmgBar.Text = Isaac.Damage.ToString();
            dfncBar.Text = Isaac.Defence.ToString();
        }

        private void PozitiveBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void NegativeBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
