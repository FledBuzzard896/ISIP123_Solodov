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
using The_Binding_Of_Isaac_WPF.Model;
using static System.Net.Mime.MediaTypeNames;

namespace The_Binding_Of_Isaac_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для Floor.xaml
    /// </summary>
    public partial class Floor : Page
    {
        GameRandom random = new GameRandom();
        BitmapImage bitmap = new BitmapImage();
        bool mobOrChest = false;
        bool mobIsBoss = false;
        Enemy randomEnemy = null;
        Enemy randomBoss = null;
        Item randomItem = null;
        double enemyDamage = 0;
        public Floor()
        {
            InitializeComponent();

            Loaded += Page_Loaded;
        }

        private async void PozitiveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (mobOrChest)
            {
                Isaac.inventory.Add(randomItem);

                if (randomItem is Weapon) { Isaac.AddDamage((Weapon)randomItem); }
                else if (randomItem is Armor) { Isaac.AddDefence((Armor)randomItem); }
                
                NavigationService.Navigate(new MenuNeutralRoom());
            }
            else if (mobOrChest == false && mobIsBoss == false)
            {
                bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(randomEnemy.imgUrl, UriKind.Relative);
                bitmap.EndInit();
                Enemy.Source = bitmap;

                Isaac.HealthDown(enemyDamage);
                randomEnemy.health -= Isaac.Damage - (Isaac.Damage * randomEnemy.Defence);
                
                EnemyHealthBar.Text = $"Здоровье: {randomEnemy.health}";
                hpBar.Text = $"{Isaac.Hp}";
                Info.Text = "";

                await Task.Delay(2000);

                if (randomEnemy.health <= 0) 
                {
                    MessageBox.Show("Моб повержен!");
                    NavigationService.Navigate(new MenuNeutralRoom());
                }
                else 
                {
                    bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(Data.FindAttackImageForEnemy(randomEnemy), UriKind.Relative);
                    bitmap.EndInit();
                    Enemy.Source = bitmap;
                    OneCicle();
                }
            }
            else if (mobOrChest == false && mobIsBoss == true) 
            {
                randomBoss.health -= Isaac.Damage - (Isaac.Damage * randomBoss.Defence);
                EnemyHealthBar.Text = $"Здоровье: {randomBoss.health}";

                if (randomBoss.health <= 0)
                {
                    MessageBox.Show("Босс повержен!\nПереход на новый этаж...");
                    NavigationService.Navigate(new GenerateFloor());
                }
                else
                {
                    OneCicle();
                }
            }
        }

        private void NegativeBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void OneCicle() 
        {
            bool userMoveSkip = false;

            if (mobOrChest == false && mobIsBoss == false) // для обычного моба
            {
                if (randomEnemy is BoomFly)
                {
                    // Будет ли крит урон?
                    if (random.IsSpecialSkill(randomEnemy.GetCritChance()))
                    {
                        enemyDamage = (randomEnemy.Damage * 1.5) - (randomEnemy.Damage * 1.5 * Isaac.Defence);
                        Info.Text = "Враг наносит критический удар!";
                    }
                    else
                    {
                        enemyDamage = randomEnemy.Damage - (randomEnemy.Damage * Isaac.Defence);
                        Info.Text = "Враг наносит удар";
                    }
                }
                else if (randomEnemy is Fatty)
                {
                    // Будет ли заморозка?
                    if (random.IsSpecialSkill(randomEnemy.GetFrozenChance()))
                    {
                        userMoveSkip = true;
                        Info.Text = "Враг использовал заморозку, вы пропускаете ход!";
                    }
                    else
                    {
                        userMoveSkip = false;
                        Info.Text = "Враг наносит удар";
                    }
                    enemyDamage = randomEnemy.Damage - (randomEnemy.Damage * Isaac.Defence);
                }
                else
                {
                    // Игнор брони + добавить заморозку для босса герди
                    enemyDamage = randomEnemy.Damage;
                    Info.Text = "Враг наносит удар (игнорируя вашу броню)";
                }

                if (userMoveSkip)
                {
                    Isaac.HealthDown(enemyDamage);
                    hpBar.Text = $"{Math.Round(Isaac.Hp,2)}";
                    hpBar.Foreground = Brushes.Red;
                    await Task.Delay(1000);
                    hpBar.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ece0e4"));
                    await Task.Delay(2000);
                }
            }
            else if (mobOrChest == false && mobIsBoss == true) // для босса
            {

            }

            if (Isaac.Hp <= 0) 
            {
                //Навигация на концовку
            } 
        }

        private void UseItem_Click(object sender, MouseButtonEventArgs e)
        {
            var listBox = sender as ListBox;
            if (listBox == null) return;

            // Получаем выбранный элемент
            var clickedItem = listBox.SelectedItem as Item;

            if (clickedItem != null && clickedItem.name == "Ням-сердце")
            {
                Isaac.HealthUp();
                ToolBar.ItemsSource = null;
                ToolBar.ItemsSource = Isaac.inventory;
            }
            e.Handled = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) 
        {
            ToolBar.ItemsSource = Isaac.inventory;

            hpBar.Text = Isaac.Hp.ToString();
            dmgBar.Text = Isaac.Damage.ToString();
            dfncBar.Text = Isaac.Defence.ToString();

            if (Model.Floor.THIS_ROOM < Model.Floor.ALL_ROOMS)
            {
                mobIsBoss = false;
                if (mobOrChest = random.MobOrChest()) // true - сундук; false - моб
                {
                    randomItem = random.RandomItem(Data.lstOfPickUps);

                    Chest.Visibility = Visibility.Visible;
                    bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(randomItem.imgUrl, UriKind.Relative);
                    bitmap.EndInit();
                    Item.Source = bitmap;

                    ItemDescriptionBar.Text = $"Описание:\n{randomItem.PrintInfo()}";

                    PozitiveBtn.Content = "Взять";
                    NegativeBtn.Content = "Пропустить";
                }
                else
                {
                    randomEnemy = random.RandomEnemy();

                    bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(randomEnemy.imgUrl, UriKind.Relative);
                    bitmap.EndInit();
                    Enemy.Source = bitmap;

                    EnemyHealthBar.Text = $"Здоровье: {randomEnemy.health}";
                    EnemyDamageBar.Text = $"Урон: {randomEnemy.Damage}";

                    PozitiveBtn.Content = "Атаковать";
                    NegativeBtn.Content = "Уклониться";
                    Info.Visibility = Visibility.Visible;
                }

                //System.Threading.Thread.Sleep(3000);
                //OneCicle();
            }
            else
            {
                mobOrChest = false;
                mobIsBoss = true;
                randomBoss = random.RandomBoss(Data.lstOfBosses);

                bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(randomBoss.imgUrl, UriKind.Relative);
                bitmap.EndInit();
                Enemy.Source = bitmap;

                EnemyHealthBar.Text = $"Здоровье: {randomBoss.health}";
                EnemyDamageBar.Text = $"Урон: {randomBoss.Damage}";

                PozitiveBtn.Content = "Атаковать";
                NegativeBtn.Content = "Уклониться";
                Info.Visibility = Visibility.Visible;

                //System.Threading.Thread.Sleep(3000);
                //OneCicle();
            }
        }
    }
}
