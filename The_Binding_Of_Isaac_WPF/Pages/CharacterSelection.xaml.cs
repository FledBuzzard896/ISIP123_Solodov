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

using ISIP123_Solodov.Model;

namespace The_Binding_Of_Isaac_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для CharacterSelection.xaml
    /// </summary>
    public partial class CharacterSelection : Page
    {
        public CharacterSelection()
        {
            InitializeComponent();
        }

        private void MagdalineBtn_Click(object sender, RoutedEventArgs e)
        {
            Isaac.SetStats(80, 3.5, 0.05, "Ням-сердце");
            NavigationService.Navigate(new GenerateFloor());
        }

        private void IsaacBtn_Click(object sender, RoutedEventArgs e)
        {
            Isaac.SetStats(60, 3.5, 0.1, "Девственные слёзы");
            NavigationService.Navigate(new GenerateFloor());
        }

        private void KainBtm_Click(object sender, RoutedEventArgs e)
        {
            Isaac.SetStats(40, 3.5 * 1.2, 0.25, "Счастливая нога");
            NavigationService.Navigate(new GenerateFloor());
        }
    }
}
