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
        Enemy randomMob = null;
        Enemy randomBoss = null;
        Mother mom = Model.Data.Mom;
        Item randomItem = null;
        double enemyDamage = 0;

        public Floor()
        {
            InitializeComponent();

            Loaded += Page_Loaded;
        }

        private async void PozitiveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Model.Floor.IS_FINAL_BOSS)
            {
                // Нёрф урона
                if (Isaac.Damage > 30)
                {
                    mom.HealthDown(Isaac.Damage / 2);
                }
                else 
                {
                    mom.HealthDown(Isaac.Damage);
                }

                // Проверка + анимка
                if (mom.Hp < 0) { EnemyHealthBar.Text = $"Здоровье: 0"; }
                else
                {
                    EnemyHealthBar.Text = $"Здоровье: {Math.Round(mom.Hp, 2)}";
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

                if (mom.Hp <= 0)
                {
                    // Навигация на концовку.
                }
                else
                {
                    await Task.Delay(500);
                    // ===================== Смена на атаку =========================
                    //Enemy.Source = ChangeImageForEnemies(randomMob, false); ;
                    OneCicle(randomMob);
                }

                await Task.Delay(2000);
                MomsOneCicle();
            }
            if (mobOrChest) // сундук
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
            else if (mobOrChest == false && mobIsBoss == false) // моб
            {
                // Смена состояния моба
                Enemy.Source = ChangeImageForEnemies(randomMob, true); ;
                // Проверка на отрицательное значение хп
                randomMob.health -= Isaac.Damage - (Isaac.Damage * randomMob.Defence);
                if (randomMob.health < 0) { EnemyHealthBar.Text = $"Здоровье: 0"; }
                else { 
                    EnemyHealthBar.Text = $"Здоровье: {Math.Round(randomMob.health, 2)}";
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

                if (randomMob.health <= 0) 
                {
                    MessageBox.Show("Моб повержен!");
                    Model.Floor.THIS_ROOM += 1;
                    NavigationService.Navigate(new MenuNeutralRoom());
                }
                else 
                {
                    await Task.Delay(500);
                    Enemy.Source = ChangeImageForEnemies(randomMob, false); ;
                    OneCicle(randomMob);
                }
            }
            else if (mobOrChest == false && mobIsBoss == true) // босс
            {
                // Смена состояния босса
                Enemy.Source = ChangeImageForEnemies(randomBoss, true);
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
                    Enemy.Source = ChangeImageForEnemies(randomBoss, false);
                    OneCicle(randomBoss);
                }
            }
        }
        private async void NegativeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (mobOrChest) // сундук
            {
                Model.Floor.THIS_ROOM += 1;
                NavigationService.Navigate(new MenuNeutralRoom());
            }
            else if (mobOrChest == false && mobIsBoss == false) // моб
            {
                if (random.EvasionOrDamage())
                {
                    Info.Text = "Вы успешно уклонились!";

                    // Смена состояния моба
                    Enemy.Source = ChangeImageForEnemies(randomMob, true);
                    await Task.Delay(500);

                    Enemy.Source = ChangeImageForEnemies(randomMob, false);
                    OneCicle(randomMob);
                }
                else
                {
                    Info.Text = "Во время уклонения вас задело, получаемый урон снижен на 70 - 100%";
                    Isaac.HealthDown(enemyDamage - (enemyDamage * random.MaxDamageOrMinDamage()));
                    hpBar.Text = $"{Math.Round(Isaac.Hp, 2)}";
                    hpBar.Foreground = Brushes.Red;
                    await Task.Delay(250);
                    hpBar.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ece0e4"));

                    // Смена состояния моба
                    Enemy.Source = ChangeImageForEnemies(randomMob, true);
                    await Task.Delay(1500);

                    Enemy.Source = ChangeImageForEnemies(randomMob, false);
                    OneCicle(randomMob);
                }
            }
            else if (mobOrChest == false && mobIsBoss == true) // босс
            {
                if (random.EvasionOrDamage())
                {
                    Info.Text = "Вы успешно уклонились!";

                    // Смена состояния босса
                    Enemy.Source = ChangeImageForEnemies(randomBoss, true);
                    await Task.Delay(500);

                    Enemy.Source = ChangeImageForEnemies(randomBoss, false);
                    OneCicle(randomBoss);
                }
                else
                {
                    Info.Text = "Во время уклонения вас задело, получаемый урон снижен на 70 - 100%";

                    Isaac.HealthDown(enemyDamage - (enemyDamage * random.MaxDamageOrMinDamage()));
                    hpBar.Text = $"{Math.Round(Isaac.Hp, 2)}";
                    hpBar.Foreground = Brushes.Red;
                    await Task.Delay(250);
                    hpBar.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ece0e4"));

                    // Смена состояния босса
                    Enemy.Source = ChangeImageForEnemies(randomBoss, true);
                    await Task.Delay(1500);

                    Enemy.Source = ChangeImageForEnemies(randomBoss, false);
                    OneCicle(randomBoss);
                }
            } 
        }

        private async void OneCicle(Enemy enemy) 
        {
            userMoveSkip = false;

            if (enemy is BoomFly || enemy is BoomFly)
            {
                // Будет ли крит урон?
                if (random.IsSpecialSkill(enemy.GetCritChance()))
                {
                    enemyDamage = (enemy.Damage * 1.5) - (enemy.Damage * 1.5 * Isaac.Defence);
                    Info.Text = "Враг наносит критический удар!";
                }
                else
                {
                    enemyDamage = enemy.Damage - (enemy.Damage * Isaac.Defence);
                    Info.Text = "Враг наносит удар";
                }
            }
            else if (enemy is Fatty || enemy is MegaFatty)
            {
                // Будет ли заморозка?
                if (random.IsSpecialSkill(enemy.GetFrozenChance()))
                {
                    userMoveSkip = true;
                    Info.Text = "Враг использовал заморозку, вы пропускаете ход!";
                }
                else
                {
                    Info.Text = "Враг наносит удар";
                }
                enemyDamage = enemy.Damage - (enemy.Damage * Isaac.Defence);
            }
            else if (enemy is Gurgling || enemy is GurdyJr)
            {
                // Игнор брони
                enemyDamage = enemy.Damage;
                Info.Text = "Враг наносит удар (игнорируя вашу броню)";
            }
            else 
            {
                // Игнор брони + возможная заморозка
                if (random.IsSpecialSkill(enemy.GetFrozenChance()))
                {
                    userMoveSkip = true;
                    Info.Text = "Враг использовал заморозку и игнорируя броню наносит удар!";
                }
                enemyDamage = enemy.Damage;
                Info.Text = "Враг игнорирует броню и наносит удар";
                    
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
                Enemy.Source = ChangeImageForEnemies(enemy, true);

                // Возврат в класическое состояние
                await Task.Delay(500);
                Enemy.Source = ChangeImageForEnemies(enemy, false);
                Info.Text = "Враг наносит удар";
            }

            if (Isaac.Hp <= 0) 
            {
                //Навигация на концовку
            } 
        }
        private async void MomsOneCicle() 
        {
            double chance = random.MotherAttack();

            if (chance <= 0.33)
            {
                Info.Text = "Мать наносит удар рукой";
                MomsHand.Visibility = Visibility.Visible;

                enemyDamage = mom.Damage;
                EnemyDamageBar.Text = $"Урон: {enemyDamage}";
            }
            else if (chance <= 66)
            {
                Info.Text = "Мать наносит удар ногой";
                MomsLeg.Visibility = Visibility.Visible;

                enemyDamage = mom.LegPunch();
                EnemyDamageBar.Text = $"Урон: {enemyDamage}";
            }
            else 
            {
                Info.Text = "Мать готовится стрельнуть лазером";
                MomsEye.Visibility = Visibility.Visible;

                enemyDamage += mom.EyeLazer();
                EnemyDamageBar.Text = $"Урон: {enemyDamage}";
            }
        }
        private async void ShowVSImage(Enemy boss) 
        {
            if (boss is BabyPlum)
            {
                VS_Image.Visibility = Visibility.Visible;

                bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(BabyPlum.VS_SCREEN, UriKind.Relative);
                bitmap.EndInit();
                VS_Image.Source = bitmap;

                await Task.Delay(2000);
                VS_Image.Visibility = Visibility.Collapsed;
            }
            else if (boss is MegaFatty)
            {
                VS_Image.Visibility = Visibility.Visible;

                bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(MegaFatty.VS_SCREEN, UriKind.Relative);
                bitmap.EndInit();
                VS_Image.Source = bitmap;

                VS_Image.Source = bitmap;
                await Task.Delay(2000);
                VS_Image.Visibility = Visibility.Collapsed;
            }
            else if (boss is GurdyJr)
            {
                VS_Image.Visibility = Visibility.Visible;

                bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(GurdyJr.VS_SCREEN, UriKind.Relative);
                bitmap.EndInit();
                VS_Image.Source = bitmap;

                VS_Image.Source = bitmap;
                await Task.Delay(2000);
                VS_Image.Visibility = Visibility.Collapsed;
            }
            else 
            {
                VS_Image.Visibility = Visibility.Visible;

                bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(Gurdy.VS_SCREEN, UriKind.Relative);
                bitmap.EndInit();
                VS_Image.Source = bitmap;

                VS_Image.Source = bitmap;
                await Task.Delay(2000);
                VS_Image.Visibility = Visibility.Collapsed;
            }
        }
        private BitmapImage ChangeImageForEnemies(Enemy enemy, bool isNeutralImage) 
        {
            bitmap = new BitmapImage();
            bitmap.BeginInit();

            if (isNeutralImage)
            {
                bitmap.UriSource = new Uri(enemy.imgUrl, UriKind.Relative);
            }
            else 
            {
                bitmap.UriSource = new Uri(Data.FindAttackImageForEnemy(enemy), UriKind.Relative);
            }

            bitmap.EndInit();
            return bitmap;
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
            
            if (Model.Floor.IS_FINAL_BOSS)
            {
                Door.Visibility = Visibility.Visible;
                IsaacOnTheField.Visibility = Visibility.Visible;

                EnemyHealthBar.Text = $"Здоровье: {Data.Mom.Hp}";
                EnemyDamageBar.Text = $"Урон: {Data.Mom.Damage}";
                PozitiveBtn.Content = "Атаковать";
                NegativeBtn.Content = "Уклониться";
                Info.Text = Data.Mom.GetDescription();

                Info.Visibility = Visibility.Visible;
            }
            else
            {
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
                        randomMob = random.RandomEnemy();
                        Enemy.Source = ChangeImageForEnemies(randomMob, true); ;

                        EnemyHealthBar.Text = $"Здоровье: {randomMob.health}";
                        EnemyDamageBar.Text = $"Урон: {randomMob.Damage}";
                        PozitiveBtn.Content = "Атаковать";
                        NegativeBtn.Content = "Уклониться";
                        Info.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    mobOrChest = false;
                    mobIsBoss = true;

                    randomBoss = random.RandomBoss(Data.lstOfBosses);
                    // Загрузка VS экрана
                    ShowVSImage(randomBoss);
                    Enemy.Source = ChangeImageForEnemies(randomBoss, true); ;

                    EnemyHealthBar.Text = $"Здоровье: {randomBoss.health}";
                    EnemyDamageBar.Text = $"Урон: {randomBoss.Damage}";
                    PozitiveBtn.Content = "Атаковать";
                    NegativeBtn.Content = "Уклониться";
                    Info.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
