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
        public MenuNeutralRoom()
        {
            InitializeComponent();
            var mainWindow = (MainWindow)Application.Current.MainWindow;

            if (Isaac.floorsLeft > 1 && Isaac.isIsaacAlive)
            {
                floorTxtBlck.Text = $"Подвал: {Math.Abs(Isaac.floorsLeft - 5)}";
                // Обращение к Images в MainWimdow.xaml
                mainWindow.BasementBackImage.Visibility = Visibility.Visible;
            }
            else if (Isaac.isIsaacAlive && Isaac.floorsLeft == 0) 
            {
                floorTxtBlck.Text = $"Комната матери.";
            }
            else if (Isaac.isIsaacAlive)
            {
                mainWindow.BasementBackImage.Visibility = Visibility.Collapsed;
                mainWindow.DepthBackImage.Visibility = Visibility.Visible;  
                floorTxtBlck.Text = $"Глубины: {Isaac.floorsLeft}";
            }

            hpTxtBlck.Text = $"Здоровье: {Math.Round(Isaac.Hp, 2)}";
            damageTxtBlck.Text = $"Урон: {Math.Round(Isaac.Damage, 2)}";
            defenceTxtBlck.Text = $"Защита: {Math.Round(Isaac.Defence, 2)}";

            ToolBar.ItemsSource = Isaac.inventory;
        }

        private void GoNextRoom_Click(object sender, RoutedEventArgs e)
        {
            if (Model.Floor.IS_FINAL_BOSS)
            {
                // Навигация на мать
            }
            else 
            {
                NavigationService.Navigate(new Floor());
            }
        }

        private void UseItem_Click(object sender, MouseButtonEventArgs e)
        {
            var listBox = sender as ListBox;
            if (listBox == null) return;

            // Получаем выбранный элемент
            var clickedItem = listBox.SelectedItem as Item;

            if (clickedItem != null && clickedItem.name == "Ням-сердце" && Isaac.Hp <= Isaac.Max_hp * 0.25) 
            {
                Isaac.HealthUp();
                ToolBar.ItemsSource = null;
                ToolBar.ItemsSource = Isaac.inventory;
            }
            e.Handled = true;
        }
    }
}
