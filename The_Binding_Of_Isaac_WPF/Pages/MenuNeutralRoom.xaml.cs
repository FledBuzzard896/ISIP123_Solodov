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
    /// Логика взаимодействия для MenuNeutralRoom.xaml
    /// </summary>
    public partial class MenuNeutralRoom : Page
    {
        int THIS_ROOM = 0;
        int ALL_ROOMS = 0;
        public MenuNeutralRoom(int thisRoom,int countOfRooms)
        {
            InitializeComponent();
            ALL_ROOMS = countOfRooms;
            THIS_ROOM = thisRoom;

            if (THIS_ROOM <= 3 && Isaac.isIsaacAlive)
            {
                floorTxtBlck.Text = $"Подвал: {THIS_ROOM}";
            }
            else if (Isaac.isIsaacAlive)
            {
                floorTxtBlck.Text = $"Глубины: {THIS_ROOM}";
            }

            hpTxtBlck.Text = $"Здоровье: {Isaac.Hp.ToString()}";
            damageTxtBlck.Text = $"Урон: {Isaac.Damage.ToString()}";
            defenceTxtBlck.Text = $"Защита: {Isaac.Defence.ToString()}";
        }

        private void GoNextRoom_Click(object sender, RoutedEventArgs e)
        {
            //if (THIS_ROOM )
        }

        private void UseItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
