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
        private bool _isProcessing;

        GameRandom random = new GameRandom();
        BitmapImage bitmap = new BitmapImage();
        bool mobOrChest = false;
        bool mobIsBoss = false;
        bool userMoveSkip = false;
        bool handAttack = false;
        bool eyeAttack = false;
        bool legAttack = false;
        Enemy randomMob = null;
        Enemy randomBoss = null;
        Mother mom = Data.Mom;
        Item randomItem = null;
        double enemyDamage = 0;
        double chance = 0;

        public Floor()
        {
            InitializeComponent();

            Loaded += Page_Loaded;
        }

        private async void PozitiveBtn_Click(object sender, RoutedEventArgs e)
        {
            // Обработка двойного нажатия на кнопку во время анимки
            if (_isProcessing) return;

            _isProcessing = true;



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

                // Смена состояния
                if (handAttack)
                {
                    MomsHand.Visibility = Visibility.Collapsed;
                    MomsHandAttack.Visibility = Visibility.Visible;

                    // Урон Айзеку
                    if (enemyDamage != 0) { await ApplyDamage(enemyDamage); }
                    Info.Text = "";

                    MomsHandAttack.Visibility = Visibility.Collapsed;
                    MomsHand.Visibility = Visibility.Visible;
                    await Task.Delay(150);
                    MomsHand.Visibility = Visibility.Collapsed;
                }
                else if (legAttack)
                {
                    MomsLeg.Visibility = Visibility.Collapsed;
                    MomsLegAttack.Visibility = Visibility.Visible;

                    // Урон Айзеку
                    if (enemyDamage != 0) { await ApplyDamage(enemyDamage); }
                    Info.Text = "";

                    MomsLegAttack.Visibility = Visibility.Collapsed;
                    MomsLeg.Visibility = Visibility.Visible;
                    await Task.Delay(150);
                    MomsLeg.Visibility = Visibility.Collapsed;
                }
                else
                {
                    MomsEye.Visibility = Visibility.Collapsed;
                    MomsEyeAttack.Visibility = Visibility.Visible;
                    await Task.Delay(150);
                    Laser.Visibility = Visibility.Visible;

                    // Урон Айзеку
                    if (enemyDamage != 0) { await ApplyDamage(enemyDamage); }
                    Info.Text = "";

                    Laser.Visibility = Visibility.Collapsed;
                    await Task.Delay(150);
                    MomsEyeAttack.Visibility = Visibility.Collapsed;
                    MomsEye.Visibility = Visibility.Visible;
                    await Task.Delay(150);
                    MomsEye.Visibility = Visibility.Collapsed;
                }

                if (Isaac.Hp <= 0) NavigationService.Navigate(new Ending());

                if (mom.Hp <= 0) NavigationService.Navigate(new Ending());
                else
                {
                    await Task.Delay(500);
                    MomsOneCicle();
                }
            }
            else
            {
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
                    else
                    {
                        EnemyHealthBar.Text = $"Здоровье: {Math.Round(randomMob.health, 2)}";
                        EnemyHealthBar.Foreground = Brushes.Red;
                        await Task.Delay(250);
                        EnemyHealthBar.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ece0e4"));
                    }

                    // Урон Айзеку
                    if (enemyDamage != 0) await ApplyDamage(enemyDamage);
                    Info.Text = "";

                    if (Isaac.Hp <= 0) NavigationService.Navigate(new Ending());

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
                    if (enemyDamage != 0) await ApplyDamage(enemyDamage);
                    Info.Text = "";

                    if (Isaac.Hp <= 0) NavigationService.Navigate(new Ending());

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


            _isProcessing = false;
        }
        private async void NegativeBtn_Click(object sender, RoutedEventArgs e)
        {
            // Обработка двойного нажатия на кнопку во время анимки
            if (_isProcessing) return;

            _isProcessing = true;



            if (Model.Floor.IS_FINAL_BOSS)
            {
                if (random.EvasionOrDamage()) // Успешное уклонение
                {
                    Info.Text = "Вы успешно уклонились!";

                    if (handAttack)
                    {
                        MomsHand.Visibility = Visibility.Collapsed;
                        MomsHandAttack.Visibility = Visibility.Visible;
                        await Task.Delay(150);
                        MomsHandAttack.Visibility = Visibility.Collapsed;
                        MomsHand.Visibility = Visibility.Visible;
                        await Task.Delay(150);
                        MomsHand.Visibility = Visibility.Collapsed;

                    }
                    else if (legAttack)
                    {
                        MomsLeg.Visibility = Visibility.Collapsed;
                        MomsLegAttack.Visibility = Visibility.Visible;
                        await Task.Delay(150);
                        MomsLegAttack.Visibility = Visibility.Collapsed;
                        MomsLeg.Visibility = Visibility.Visible;
                        await Task.Delay(150);
                        MomsLeg.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        MomsEye.Visibility = Visibility.Collapsed;
                        MomsEyeAttack.Visibility = Visibility.Visible;
                        await Task.Delay(150);
                        Laser.Visibility = Visibility.Visible;
                        await Task.Delay(150);
                        Laser.Visibility = Visibility.Collapsed;
                        await Task.Delay(150);
                        MomsEyeAttack.Visibility = Visibility.Collapsed;
                        MomsEye.Visibility = Visibility.Visible;
                        await Task.Delay(150);
                        MomsEye.Visibility = Visibility.Collapsed;
                    }
                }
                else // Получение небольшого урона
                {
                    Info.Text = "Во время уклонения вас задело, получаемый урон снижен на 70 - 100%";

                    // Урон Айзеку
                    ApplyDamage(enemyDamage, random.MaxDamageOrMinDamage);

                    //Смена состояния
                    if (handAttack)
                    {
                        MomsHand.Visibility = Visibility.Collapsed;
                        MomsHandAttack.Visibility = Visibility.Visible;
                        await Task.Delay(150);
                        MomsHandAttack.Visibility = Visibility.Collapsed;
                        MomsHand.Visibility = Visibility.Visible;
                        await Task.Delay(150);
                        MomsHand.Visibility = Visibility.Collapsed;

                    }
                    else if (legAttack)
                    {
                        MomsLeg.Visibility = Visibility.Collapsed;
                        MomsLegAttack.Visibility = Visibility.Visible;
                        await Task.Delay(150);
                        MomsLegAttack.Visibility = Visibility.Collapsed;
                        MomsLeg.Visibility = Visibility.Visible;
                        await Task.Delay(150);
                        MomsLeg.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        MomsEye.Visibility = Visibility.Collapsed;
                        MomsEyeAttack.Visibility = Visibility.Visible;
                        await Task.Delay(150);
                        Laser.Visibility = Visibility.Visible;
                        await Task.Delay(150);
                        Laser.Visibility = Visibility.Collapsed;
                        await Task.Delay(150);
                        MomsEyeAttack.Visibility = Visibility.Collapsed;
                        MomsEye.Visibility = Visibility.Visible;
                        await Task.Delay(150);
                        MomsEye.Visibility = Visibility.Collapsed;
                    }

                    if (Isaac.Hp <= 0) NavigationService.Navigate(new Ending());
                }
                await Task.Delay(1500);
                MomsOneCicle();
            }
            else
            {
                if (mobOrChest) // сундук
                {
                    Model.Floor.THIS_ROOM += 1;
                    NavigationService.Navigate(new MenuNeutralRoom());
                }
                else if (mobOrChest == false && mobIsBoss == false) // моб
                {
                    if (random.EvasionOrDamage()) // Успешное уклонение
                    {
                        Info.Text = "Вы успешно уклонились!";

                        // Смена состояния моба
                        Enemy.Source = ChangeImageForEnemies(randomMob, true);
                        await Task.Delay(500);
                        Enemy.Source = ChangeImageForEnemies(randomMob, false);
                    }
                    else // Получение небольшого урона
                    {
                        Info.Text = "Во время уклонения вас задело, получаемый урон снижен на 70 - 100%";

                        // Урон Айзеку
                        ApplyDamage(enemyDamage, random.MaxDamageOrMinDamage);

                        if (Isaac.Hp <= 0) NavigationService.Navigate(new Ending());

                        // Смена состояния моба
                        Enemy.Source = ChangeImageForEnemies(randomMob, true);
                        await Task.Delay(1500);
                        Enemy.Source = ChangeImageForEnemies(randomMob, false);
                    }
                    OneCicle(randomMob);
                }
                else if (mobOrChest == false && mobIsBoss == true) // босс
                {
                    if (random.EvasionOrDamage()) // Успешное уклонение
                    {
                        Info.Text = "Вы успешно уклонились!";

                        // Смена состояния босса
                        Enemy.Source = ChangeImageForEnemies(randomBoss, true);
                        await Task.Delay(500);
                        Enemy.Source = ChangeImageForEnemies(randomBoss, false);
                    }
                    else // Получение небольшого урона
                    {
                        Info.Text = "Во время уклонения вас задело, получаемый урон снижен на 70 - 100%";

                        // Урон Айзеку
                        ApplyDamage(enemyDamage, random.MaxDamageOrMinDamage);

                        if (Isaac.Hp <= 0) NavigationService.Navigate(new Ending());

                        // Смена состояния босса
                        Enemy.Source = ChangeImageForEnemies(randomBoss, true);
                        await Task.Delay(1500);
                        Enemy.Source = ChangeImageForEnemies(randomBoss, false);
                    }
                    OneCicle(randomBoss);
                }
            }


            _isProcessing = false;
        }

        private async void OneCicle(Enemy enemy) 
        {
            userMoveSkip = false;

            if (enemy is BoomFly || enemy is BoomFly)
            {
                // Будет ли крит урон?
                if (random.IsSpecialSkill(enemy.GetCritChance()))
                {
                    enemyDamage = enemy.Damage * 1.5;
                    Info.Text = "Враг наносит критический удар!";
                }
                else
                {
                    enemyDamage = enemy.Damage;
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
                enemyDamage = enemy.Damage;
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
                _isProcessing = true;

                // Урон Айзеку
                await ApplyDamage(enemyDamage);

                // Смена состояния моба
                Enemy.Source = ChangeImageForEnemies(enemy, true);

                if (Isaac.Hp <= 0) NavigationService.Navigate(new Ending());

                // Возврат в класическое состояние
                await Task.Delay(500);
                Enemy.Source = ChangeImageForEnemies(enemy, false);
                Info.Text = "Враг наносит удар";

                _isProcessing = false;
            }
        }
        private void MomsOneCicle() 
        {
            handAttack = false;
            legAttack = false;
            eyeAttack = false;

            chance = random.MotherAttack();
            if (chance <= 0.33)
            {
                Info.Text = "Мать наносит удар рукой";
                MomsHand.Visibility = Visibility.Visible;
                handAttack = true;
                enemyDamage = mom.Damage;
                EnemyDamageBar.Text = $"Урон: {enemyDamage}";
            }
            else if (chance <= 0.66)
            {
                Info.Text = "Мать наносит удар ногой";
                MomsLeg.Visibility = Visibility.Visible;
                legAttack = true;
                enemyDamage = mom.LegPunch();
                EnemyDamageBar.Text = $"Урон: {enemyDamage}";
            }
            else 
            {
                Info.Text = "Мать готовится стрельнуть лазером";
                MomsEye.Visibility = Visibility.Visible;
                eyeAttack = true;
                enemyDamage = mom.EyeLazer();
                EnemyDamageBar.Text = $"Урон: {enemyDamage}";
            }
        }

        private async Task ApplyDamage(double damage)
        {
            Isaac.HealthDown(damage);

            if (Isaac.Hp > 0) hpBar.Text = $"{Math.Round(Isaac.Hp, 2)}";
            else hpBar.Text = "0";

            hpBar.Foreground = Brushes.Red;
            await Task.Delay(250);
            hpBar.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ece0e4"));
        }
        private async void ApplyDamage(double damage, Func<double> maxOrMinDamageFunc) 
        {
            Isaac.HealthDown(damage - (damage * maxOrMinDamageFunc()));

            if (Isaac.Hp > 0) hpBar.Text = $"{Math.Round(Isaac.Hp, 2)}";
            else hpBar.Text = "0";

            hpBar.Foreground = Brushes.Red;
            await Task.Delay(250);
            hpBar.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ece0e4"));
        }

        private async Task ShowVSImage(Enemy boss) 
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
            else if (boss is Gurdy)
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
            else 
            {
                VS_Image.Visibility = Visibility.Visible;

                bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(Mother.VS_SCREEN, UriKind.Relative);
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
        private async void Page_Loaded(object sender, RoutedEventArgs e) 
        {
            ToolBar.ItemsSource = Isaac.inventory;

            hpBar.Text = Math.Round(Isaac.Hp, 2).ToString();
            dmgBar.Text = Math.Round(Isaac.Damage, 2).ToString();
            dfncBar.Text = Math.Round(Isaac.Defence, 2).ToString();
            
            if (Model.Floor.IS_FINAL_BOSS)
            {
                Enemy television = new Enemy(0, 0, 0, "Телевизор", "");
                await ShowVSImage(television);
                Door.Visibility = Visibility.Visible;
                IsaacOnTheField.Visibility = Visibility.Visible;

                EnemyHealthBar.Text = $"Здоровье: {Data.Mom.Hp}";
                EnemyDamageBar.Text = $"Урон: {Data.Mom.Damage}";
                PozitiveBtn.Content = "Атаковать";
                NegativeBtn.Content = "Уклониться";

                Info.Visibility = Visibility.Visible;
                chance = random.MotherAttack();

                if (chance <= 0.33)
                {
                    Info.Text = "Мать наносит удар рукой";
                    MomsHand.Visibility = Visibility.Visible;
                    enemyDamage = mom.Damage;
                    EnemyDamageBar.Text = $"Урон: {enemyDamage}";
                    handAttack = true;
                }
                else if (chance <= 66)
                {
                    Info.Text = "Мать наносит удар ногой";
                    MomsLeg.Visibility = Visibility.Visible;
                    enemyDamage = mom.LegPunch();
                    EnemyDamageBar.Text = $"Урон: {enemyDamage}";
                    legAttack = true;
                }
                else
                {
                    Info.Text = "Мать готовится стрельнуть лазером";
                    MomsEye.Visibility = Visibility.Visible;
                    enemyDamage = mom.EyeLazer();
                    EnemyDamageBar.Text = $"Урон: {enemyDamage}";
                    eyeAttack = true;
                }
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
                    await ShowVSImage(randomBoss);
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
