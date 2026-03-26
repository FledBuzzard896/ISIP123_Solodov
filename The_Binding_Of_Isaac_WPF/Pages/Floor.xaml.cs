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
        bool userMoveSkip = false;
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

                if (randomItem is Weapon) 
                {
                    Isaac.AddDamage((Weapon)randomItem);
                    dmgBar.Text = Isaac.Damage.ToString();
                }
                else if (randomItem is Armor) 
                { 
                    Isaac.AddDefence((Armor)randomItem);
                    dfncBar.Text = Isaac.Defence.ToString();
                }

                Model.Floor.THIS_ROOM += 1;
                NavigationService.Navigate(new MenuNeutralRoom());
            }
            else if (mobOrChest == false && mobIsBoss == false)
            {
                // Смена состояния моба
                bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(randomEnemy.imgUrl, UriKind.Relative);
                bitmap.EndInit();
                Enemy.Source = bitmap;
                // Проверка на отрицательное значение хп
                randomEnemy.health -= Isaac.Damage - (Isaac.Damage * randomEnemy.Defence);
                if (randomEnemy.health < 0) { EnemyHealthBar.Text = $"Здоровье: 0"; }
                else { 
                    EnemyHealthBar.Text = $"Здоровье: {Math.Round(randomEnemy.health, 2)}";
                    EnemyHealthBar.Foreground = Brushes.Red;
                    await Task.Delay(250);
                    EnemyHealthBar.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ece0e4"));
                }

                // Урон Айзеку
                if (enemyDamage != 0)
                {
                    Isaac.HealthDown(enemyDamage);
                    hpBar.Text = $"{Math.Round(Isaac.Hp, 2)}";
                    hpBar.Foreground = Brushes.Red;
                    await Task.Delay(250);
                    hpBar.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ece0e4"));
                }
                Info.Text = "";

                if (randomEnemy.health <= 0) 
                {
                    MessageBox.Show("Моб повержен!");
                    Model.Floor.THIS_ROOM += 1;
                    NavigationService.Navigate(new MenuNeutralRoom());
                }
                else 
                {
                    await Task.Delay(500);
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
                // Смена состояния босса
                bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(randomBoss.imgUrl, UriKind.Relative);
                bitmap.EndInit();
                Enemy.Source = bitmap;
                // Проверка на отрицательное значение хп
                randomBoss.health -= Isaac.Damage - (Isaac.Damage * randomBoss.Defence);
                if (randomBoss.health < 0) { EnemyHealthBar.Text = $"Здоровье: 0"; }
                else
                {
                    EnemyHealthBar.Text = $"Здоровье: {Math.Round(randomBoss.health, 2)}";
                    EnemyHealthBar.Foreground = Brushes.Red;
                    await Task.Delay(250);
                    EnemyHealthBar.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ece0e4"));
                }

                // Урон Айзеку
                if (enemyDamage != 0)
                {
                    Isaac.HealthDown(enemyDamage);
                    hpBar.Text = $"{Math.Round(Isaac.Hp, 2)}";
                    hpBar.Foreground = Brushes.Red;
                    await Task.Delay(250);
                    hpBar.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ece0e4"));
                }
                Info.Text = "";

                if (randomBoss.health <= 0)
                {
                    MessageBox.Show("Босс повержен!\nПереход на новый этаж...");
                    NavigationService.Navigate(new GenerateFloor());
                }
                else
                {
                    await Task.Delay(500);
                    bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(Data.FindAttackImageForEnemy(randomBoss), UriKind.Relative);
                    bitmap.EndInit();
                    Enemy.Source = bitmap;
                    OneCicle();
                }
            }
        }

        private async void NegativeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (mobOrChest)
            {
                Model.Floor.THIS_ROOM += 1;
                NavigationService.Navigate(new MenuNeutralRoom());
            }
            else if (mobOrChest == false && mobIsBoss == false) 
            {
                if (random.EvasionOrDamage())
                {
                    Info.Text = "Вы успешно уклонились!";

                    // Смена состояния моба
                    bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(randomEnemy.imgUrl, UriKind.Relative);
                    bitmap.EndInit();
                    Enemy.Source = bitmap;
                    await Task.Delay(500);

                    bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(Data.FindAttackImageForEnemy(randomEnemy), UriKind.Relative);
                    bitmap.EndInit();
                    Enemy.Source = bitmap;
                    OneCicle();
                }
                else 
                {
                    Info.Text = "Во время уклонения вас задело, получаемый урон снижен на 70 - 100%";
                    Isaac.HealthDown(enemyDamage - (enemyDamage * (70/100)));
                    hpBar.Text = $"{Math.Round(Isaac.Hp, 2)}";
                    hpBar.Foreground = Brushes.Red;
                    await Task.Delay(250);
                    hpBar.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ece0e4"));

                    // Смена состояния моба
                    bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(randomEnemy.imgUrl, UriKind.Relative);
                    bitmap.EndInit();
                    Enemy.Source = bitmap;
                    await Task.Delay(1500);

                    bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(Data.FindAttackImageForEnemy(randomEnemy), UriKind.Relative);
                    bitmap.EndInit();
                    Enemy.Source = bitmap;
                    OneCicle();
                }
            }
        }

        private async void OneCicle() 
        {
            userMoveSkip = false;

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
                        Info.Text = "Враг наносит удар";
                    }
                    enemyDamage = randomEnemy.Damage - (randomEnemy.Damage * Isaac.Defence);
                }
                else
                {
                    // Игнор брони
                    enemyDamage = randomEnemy.Damage;
                    Info.Text = "Враг наносит удар (игнорируя вашу броню)";
                }

                if (userMoveSkip)
                {
                    // Урон Айзеку
                    Isaac.HealthDown(enemyDamage);
                    hpBar.Text = $"{Math.Round(Isaac.Hp, 2)}";
                    hpBar.Foreground = Brushes.Red;
                    await Task.Delay(350);
                    hpBar.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ece0e4"));

                    // Смена состояния моба
                    bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(randomEnemy.imgUrl, UriKind.Relative);
                    bitmap.EndInit();
                    Enemy.Source = bitmap;

                    // Возврат в класическое состояние
                    await Task.Delay(500);
                    bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(Data.FindAttackImageForEnemy(randomEnemy), UriKind.Relative);
                    bitmap.EndInit();
                    Enemy.Source = bitmap;
                    Info.Text = "Враг наносит удар";
                }
            }
            else if (mobOrChest == false && mobIsBoss == true) // для босса
            {
                if (randomBoss is BabyPlum)
                {
                    if (random.IsSpecialSkill(randomBoss.GetCritChance())) 
                    {
                        enemyDamage = (randomBoss.Damage * 1.5) - (randomBoss.Damage * 1.5 * Isaac.Defence);
                        Info.Text = "Враг наносит критический удар!";
                    }
                    else
                    {
                        enemyDamage = randomBoss.Damage - (randomBoss.Damage * Isaac.Defence);
                        Info.Text = "Враг наносит удар";
                    }
                }
                else if (randomBoss is GurdyJr)
                {
                    enemyDamage = randomBoss.Damage;
                    Info.Text = "Враг наносит удар (игнорируя вашу броню)";

                }
                else if (randomBoss is MegaFatty)
                {
                    if (random.IsSpecialSkill(randomBoss.GetFrozenChance()))
                    {
                        userMoveSkip = true;
                        Info.Text = "Враг использовал заморозку, вы пропускаете ход!";
                    }
                    else
                    {
                        Info.Text = "Враг наносит удар";
                    }
                    enemyDamage = randomBoss.Damage - (randomBoss.Damage * Isaac.Defence);
                }
                else 
                {
                    if (random.IsSpecialSkill(randomBoss.GetFrozenChance())) 
                    {
                        userMoveSkip = true;
                    }
                    enemyDamage = randomBoss.Damage;
                }
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

                hpBar.Text = $"{Math.Round(Isaac.Hp, 2)}";
            }
            e.Handled = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) 
        {
            ToolBar.ItemsSource = Isaac.inventory;

            hpBar.Text = Math.Round(Isaac.Hp, 2).ToString();
            dmgBar.Text = Math.Round(Isaac.Damage, 2).ToString();
            dfncBar.Text = Math.Round(Isaac.Defence, 2).ToString();

            if (Model.Floor.THIS_ROOM < Model.Floor.ALL_ROOMS)
            {
                mobIsBoss = false;
                if (mobOrChest = random.MobOrChest() && Data.lstOfPickUps.Count > 0) // true - сундук; false - моб
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
            }
            else
            {
                // Загрузка VS экрана
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
            }
        }
    }
}
