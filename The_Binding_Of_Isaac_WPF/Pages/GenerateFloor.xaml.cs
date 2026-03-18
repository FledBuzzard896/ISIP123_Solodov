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
    /// Логика взаимодействия для GenerateFloor.xaml
    /// </summary>
    public partial class GenerateFloor : Page
    {
        GameRandom gameRandom = new GameRandom();
        public GenerateFloor()
        {
            InitializeComponent();

            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("/Images/Isaac.png", UriKind.Relative);
            bitmap.EndInit();
            Isaac.Source = bitmap;

            int rn = gameRandom.RandomNightmare(Model.Isaac.listOfNightmares);
            string path = Model.Isaac.listOfNightmares[rn];
            Model.Isaac.listOfNightmares.Remove(path);

            bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(path, UriKind.Relative);
            bitmap.EndInit();
            IsaacNightmare.Source = bitmap;
        }

        private void generateFloor_Click(object sender, RoutedEventArgs e)
        {
            if (Model.Isaac.floorsLeft > 0)
            {
                // Генерируем комнаты на этаж
                Model.Floor.THIS_ROOM = 1;
                Model.Floor.ALL_ROOMS = gameRandom.GenerateRooms();

                NavigationService.Navigate(new MenuNeutralRoom());
                Model.Isaac.floorsLeft -= 1;
            }
            else 
            {
                Model.Floor.IS_FINAL_BOSS = true;
                NavigationService.Navigate(new MenuNeutralRoom());
            }
        }
    }
}
