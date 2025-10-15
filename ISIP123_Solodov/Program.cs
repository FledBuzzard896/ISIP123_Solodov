// Враги:
// 1. Бомбуха 
// 2. Булька 
// 3. Толстяк

// Боссы:
// 1. Сливка (расса Бомбуха)
// 2. Гёрди Младшая (расса Булька)
// 3. Мега Толстяк (расса Толстяк)
// 4. Гёрди Старшая (расса Булька)

// Меню
Console.WriteLine("   +++============+*#+++=============+#% =+===============   ================+  +================   \r\n   +++++======+++++#*=====++++++++++++%%*=====++++++=======#*======+++++=======%+=====++++++======  \r\n    %%%#+====+%%%%%%+=====*%%%%%%%%%%%%%+=====+%%%%#=====+#%#=====+#%%%#+=====*%*=====+%%%%*=====+  \r\n       +=====*%%%   ======+++=======    ==================#%#========++++=====*%*=====+  %%%######  \r\n      =======#%%     ================*# ==================*%#=================*%#+====== %%%%%%%%   \r\n      ======+%%%       %%%%%%%#+=====#%#+=====+*   +======*%%======*%%%%+=====+%#+=====+    +=====+ \r\n ===========*###         +****+======#%%=======+   +======+%%+=====+  %%*=====+%%+====== ###+====== \r\n+===============+#%+==============+*#%%%========   ++======%%+======  %%#=====+%%#*+=============++ \r\n+**##############%%################%%%%%########   ########%%######*  %%%######%%%%############+++  \r\n  ##%%%%%%%%%%%%%%%  %%%%%%%%%%%%%%%%%  %%%%%%%    %%%%%%%%%%##%%%    %%%%%%%%  %%%%%%%%%%%%%%#   \n");
Console.WriteLine("1. Начать игру\n0. Выйти");
Console.Write(">>> ");
string choice = Console.ReadLine();


Random random = new Random();

List<Item> lstOfPickUps = new List<Item>();
List<Enemy> lstOfEnemies = new List<Enemy>();
List<Enemy> lstOfBosses = new List<Enemy>();

Weapon Pentagram = new Weapon("Пентаграмма", 1, "+ Урон, + 10% шанс сделки");
Weapon Synthoil = new Weapon("Шприц синтола", 1, "+ Урон, + Дальность выстрела");
Weapon DarkMatter = new Weapon("Черная материя", 1, "+ Урон, + Шанс наложить на врага эффект страха");
Weapon Barley = new Weapon("Ячмень", 1, "+ Урон");
Weapon Stapler = new Weapon("Степлер", 1, "+ Урон, Все слёзы стреляют из правого глаза");
Weapon SacredHeart = new Weapon("Священное сердце", 2.3, "+ Урон, Выстрелы самонаводятся");
Weapon Guillotine = new Weapon("Гильотина", 1, "+ Урон, Голова персонажа летает отдельно от него");
Weapon Ipecac = new Weapon("Рвотный корень", 40, "+ Мега урон, взрывные слёзы");

lstOfPickUps.Add(Pentagram);
lstOfPickUps.Add(Synthoil);
lstOfPickUps.Add(DarkMatter);
lstOfPickUps.Add(Barley);
lstOfPickUps.Add(Stapler);
lstOfPickUps.Add(SacredHeart);
lstOfPickUps.Add(Guillotine);
lstOfPickUps.Add(Ipecac);

Armor MomsUnderwear = new Armor("Мамино бельё", 0.1, "Снижает получаемый урон на 10%");
Armor Pjs = new Armor("Пижама", 0.2, "Снижает получаемый урон на 20%");
Armor Habit = new Armor("Одеяние", 0.1, "Снижает получаемый урон на 10%");

lstOfPickUps.Add(MomsUnderwear);
lstOfPickUps.Add(Pjs);
lstOfPickUps.Add(Habit);

Item YumHeart = new Item("Ням сердце", "Даёт возможность его съесть когда у персонажа остается меньше 25% здоровья");
Item LuckyLeg = new Item("Счастливая нога", "+ 1 удача");
Item TheBelt = new Item("Ремень", "Вы быстрее бегаете");
Item PiggyBank = new Item("Свинюшка", "+ 3 монеты");
Item Bumbo = new Item("Бамбо", "Фамильяр, который собирает монетки и растет за счет них, больше он ничего не делает");

lstOfPickUps.Add(YumHeart);
lstOfPickUps.Add(LuckyLeg);
lstOfPickUps.Add(TheBelt);
lstOfPickUps.Add(PiggyBank);
lstOfPickUps.Add(Bumbo);

BoomFly enemy1 = new BoomFly(15, 2, 0.1, "Красная Бомбомуха", 10);
Gurgling enemy2 = new Gurgling(20, 3, 0.1, "Булькающий", true);
Fatty enemy3 = new Fatty(20, 2.5, 0.2, "Толстяк", 10);

lstOfEnemies.Add(enemy1);
lstOfEnemies.Add(enemy2);
lstOfEnemies.Add(enemy3);

BabyPlum boss1 = new BabyPlum(15, 2, 0.1, "Сливка", 10);
GurdyJr boss2 = new GurdyJr(20,3,0.1,"Гёрди Младшая", true);
MegaFatty boss3 = new MegaFatty(20, 2.5, 0.2, "Мега Толстяк", 10);
Gurdy boss4 = new Gurdy(20, 3, 0.1, "Гёрди", true, 10);

lstOfBosses.Add(boss1);
lstOfBosses.Add(boss2);
lstOfBosses.Add(boss3);
lstOfBosses.Add(boss4);

if (choice == "1") {

    Console.WriteLine("=========== Выберите персонажа ===========\n1. Айзек \t(HP\t|||\tDMG ||)\n2. Каин \t(HP\t||\tDMG |||)\n3. Магдалина \t(HP\t||||\tDMG ||)");
    Console.Write(">>> ");
    string hero = Console.ReadLine();

    double hp = 6 * 10;
    double damage = 3.5;
    double defence = 0.1;
    string inventory = "Девственные слёзы";

    List<Item> lstInventory = new List<Item>();

    while (hero != "used")
    {
        switch (hero)
        {

            case "1":

                Console.WriteLine("Выбранный персонаж: Айзек\n");
                hero = "used";
                break;

            case "2":

                hp = 4 * 10;
                damage = 3.5 * 1.2;
                defence = 0.25;
                inventory = "Счастливая нога";

                lstInventory.Add(LuckyLeg);
                
                Console.WriteLine("Выбранный персонаж: Каин\n");
                hero = "used";
                break;

            case "3":

                hp = 8 * 10;
                damage = 3.5;
                defence = 0.05;
                inventory = "Ням-сердце";

                lstInventory.Add(YumHeart);

                Console.WriteLine("Выбранный персонаж: Магдалина\n");
                hero = "used";
                break;

            default:
                hero = Convert.ToString(random.Next(1, 4));
                break;
        }
    }

    double MAX_HP = hp;
    Isaac Character = new Isaac(hp,damage,defence,inventory);
    Console.Clear();
    // Начало игры
    for (int lvl = 1; lvl < 6; lvl++) 
    {
        // вывод информации о номере этажа
        if (lvl <= 3)
        {
            Console.WriteLine($"Подвал: {lvl}");
        }
        else 
        {
            Console.WriteLine($"Глубины: {lvl - 3}");
        }

        int countOfRooms = random.Next(4, 7); // кол-во комнат от 4 до 6
        bool isIsaacAlive = true;

        while (countOfRooms != 0 && isIsaacAlive == true)
        {
            Console.WriteLine("----------- Выберите действие ------------\n1. Посмотреть статистику\n2. Зайти в следующую комнату\n3. Использовать предмет");
            Console.Write(">>> ");
            choice = Console.ReadLine();
            
            switch (choice)
            {
                case "1":
                    Character.PrintInfo();
                    break;

                case "2":
                    isIsaacAlive = generateRoom(countOfRooms, Character);
                    countOfRooms--;
                    break;

                case "3":
                    if (Character.GetInventory().Contains("Ням сердце"))
                    {
                        Character.HealthUp(MAX_HP);
                    }
                    else 
                    {
                        Console.WriteLine("У вас нет предметов, которые можно использовать!\n");
                    }
                    break;
            }
        }
    }
    
}

bool generateRoom(int countOfRooms, Isaac isaac) {

    bool isIsaacAlive = true;

    if (countOfRooms > 1)
    {

        int mobOrChest = random.Next(1, 101);

        if (mobOrChest > 50)
        {

            int randomItem = random.Next(0, lstOfPickUps.Count());
            string barier = "==========================================";

            Console.WriteLine($"\nНа твоём пути встала комната сокровищ");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"В ней находится \"{lstOfPickUps[randomItem].name}\"\n");
            Console.ResetColor();

            Console.WriteLine("Описание:");
            // вывод информации о предмете
            if (lstOfPickUps[randomItem].description.Length > barier.Length)
            {

                barier = "";
                for (int i = 0; i < lstOfPickUps[randomItem].description.Length; i++)
                {

                    barier += "=";
                }
            }

            Console.WriteLine(barier);
            lstOfPickUps[randomItem].PrintInfo();
            Console.WriteLine(barier);

            // подобрать или оставить предмет
            Console.WriteLine("\n1. Взять предмет\n2. Пропустить предмет\n");
            Console.Write(">>> ");
            choice = Console.ReadLine();

            switch (choice)
            {

                case "1":
                    isaac.AddPickUp(lstOfPickUps[randomItem]);

                    if (lstOfPickUps[randomItem] is Weapon)
                    {
                        isaac.AddDamage((Weapon)lstOfPickUps[randomItem]);
                    }
                    else if (lstOfPickUps[randomItem] is Armor)
                    {
                        isaac.AddDefence((Armor)lstOfPickUps[randomItem]);
                    }
                    break;

                default: break;
            }
        }
        else
        {
            int randomEnemy = random.Next(0, lstOfEnemies.Count());

            double enemyHP = lstOfEnemies[randomEnemy].Health;
            double enemyDAMAGE = lstOfEnemies[randomEnemy].Damage;
            double enemyDEFENCE = lstOfEnemies[randomEnemy].Defence;

            double enemyCRIT_CHANCE = 0;
            double enemyFROZEN_CHANCE = 0;
            bool enemyIGNORE_ARMOR = false;

            if (lstOfEnemies[randomEnemy] is BoomFly)
            {
                enemyCRIT_CHANCE = enemy1.GetCritChance();
            }
            else if (lstOfEnemies[randomEnemy] is Gurgling)
            {
                enemyIGNORE_ARMOR = true;
            }
            else
            {
                enemyFROZEN_CHANCE = enemy3.GetFrozenCrit();
            }

            Console.WriteLine($"Вы зашли в комнату, в комнате на вас напал {lstOfEnemies[randomEnemy].description}\n");

            isIsaacAlive = Fight(enemyHP, enemyDAMAGE, enemyDEFENCE, enemyCRIT_CHANCE, enemyFROZEN_CHANCE, enemyIGNORE_ARMOR, isaac);
        }
    }
    else
    {
        int randomBoss = random.Next(1, lstOfBosses.Count);

        double bossHP = lstOfBosses[randomBoss].Health;
        double bossDEFENCE = lstOfBosses[randomBoss].Defence;
        double bossDAMAGE = lstOfBosses[randomBoss].Damage;

        double bossCRIT_CHANCE = 0;
        double bossFROZEN_CHANCE = 0;
        bool bossIGNORE_ARMOR = false;

        if (lstOfBosses[randomBoss] is BabyPlum)
        {
            bossCRIT_CHANCE = boss1.GetCritChance();
            lstOfBosses.Remove(boss1);
        }
        else if (lstOfBosses[randomBoss] is GurdyJr)
        {
            bossIGNORE_ARMOR = true;
            lstOfBosses.Remove(boss2);
        }
        else if (lstOfBosses[randomBoss] is MegaFatty)
        {
            bossFROZEN_CHANCE = boss3.GetFrozenCrit();
            lstOfBosses.Remove(boss3);
        }
        else
        {
            bossIGNORE_ARMOR = true;
            bossFROZEN_CHANCE = boss3.GetFrozenCrit();
            lstOfBosses.Remove(boss4);
        }

        Console.WriteLine($"Вы зашли в комнату босса, в комнате оказался {lstOfBosses[randomBoss].description}\n");

        isIsaacAlive = Fight(bossHP, bossDAMAGE, bossDEFENCE, bossCRIT_CHANCE, bossFROZEN_CHANCE, bossIGNORE_ARMOR, isaac);
    }

    return isIsaacAlive;
}

bool Fight(double enemyHP, double enemyDAMAGE, double enemyDEFENCE, double enemyCRIT_CHANCE, double enemyFROZEN_CHANCE, bool enemyIGNORE_ARMOR, Isaac isaac) {

    bool isIsaacAlive = true;

    string message = "";
    double damage = 0;
    bool userMoveSkip = false;

    while (isaac.Hp > 0 && enemyHP > 0)
    {
        Console.WriteLine($"\t\t  Враг:\n=========================================\nHP: {enemyHP}\nDamage: {enemyDAMAGE}\nCritDamage: {enemyDAMAGE * 1.5}\n");
        Console.WriteLine($"\t\t  Вы:\n=========================================\nHP: {isaac.Hp}\nDamage: {isaac.Damage}\n");

        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(message);
        Console.ResetColor();

        if (userMoveSkip)
        {
            isaac.HealthDown(damage);
        }

        Console.WriteLine("1. Атаковать\n2. Уклониться");
        Console.Write(">>> ");
        string userMove = Console.ReadLine();
        Console.WriteLine();

        switch (userMove)
        {

            case "1":
                enemyHP -= isaac.Damage - (isaac.Damage * enemyDEFENCE);
                isaac.HealthDown(damage);
                break;

            case "2":
                int chance = random.Next(1, 101);

                if (chance <= 40)
                {
                    Console.WriteLine("Вы успешно уклонились");
                }
                else
                {
                    Console.WriteLine("Во время уклонения вас задело, но вы вовремя успели защититься, получаемый урон снижен на 70 - 100%");

                    chance = random.Next(1, 101);

                    if (chance <= 70)
                    {
                        isaac.HealthDown(damage - (damage * (70 / 100)));
                    }
                    else
                    {
                        isaac.HealthDown(damage - (damage * (chance / 100)));
                    }
                }


                break;
        }

        if (enemyCRIT_CHANCE != 0)
        {
            int chance = random.Next(1, 101);

            if (chance <= enemyCRIT_CHANCE)
            {
                damage = (enemyDAMAGE * 1.5) - (enemyDAMAGE * 1.5 * isaac.Defence);
                message = "Враг наносит критический удар!\n";
            }
            else
            {
                damage = enemyDAMAGE - (enemyDAMAGE * isaac.Defence);
                message = "Враг наносит удар\n";
            }
        }
        else if (enemyIGNORE_ARMOR)
        {
            damage = enemyDAMAGE;
            message = "Враг игнорирует твою броню и наносит удар\n";
        }
        else
        {
            int chance = random.Next(1, 101);

            if (chance <= enemyFROZEN_CHANCE)
            {
                userMoveSkip = true;
                message = "Враг использовал заморозку, вы пропускаете ход\n";
            }
            else
            {
                userMoveSkip = false;
                message = "Враг наносит удар\n";
            }
            damage = enemyDAMAGE - (enemyDAMAGE * isaac.Defence);
        }
    }

    if (isaac.Hp > 0)
    {
        Console.WriteLine("============= Враг повержен! =============\n");
    }
    else
    {
        Console.WriteLine("Дорогой дневник, сегодня я умер. Все свои вещи я завещаю моему коту Гаппи. Прощай, жестокий мир. Люблю, целую, Айзек!");
        isIsaacAlive = false;
    }

    return isIsaacAlive;
}
class Isaac {

    private double hp;
    private double damage;
    private double defence;
    private string inventory;

    public double Hp => hp;
    public double Damage => damage;
    public double Defence => defence;

    public Isaac(double hp, double damage, double defence, string inventory)
    {
        this.hp = hp;
        this.damage = damage;
        this.defence = defence;
        this.inventory = inventory;
    }

    public void PrintInfo()
    {
        Console.WriteLine($"=============== Статистика ===============\nHP: {hp}\nУрон: {damage}\nБроня: {defence}\nPull-up: {inventory}\n==========================================\n");
    }

    private int count = 1;
    public void AddPickUp(Item pickUp) 
    {
        inventory += ", ";

        if (count % 3 == 0) {
            inventory += "\n";
        }

        inventory += pickUp.name;
        count++;
    }
    public void AddDamage(Weapon pickUp) 
    {
        damage += pickUp.GetDamage();
    }
    public void AddDefence(Armor pickUp) 
    {
        defence += pickUp.GetDefence();
    }
    public void HealthUp(double maxHP) 
    {
        if (hp <= maxHP * 0.25)
        {
            hp = maxHP;
        }
        else
        {
            Console.WriteLine("Вы еще не можете использовать этот предмет!");
        }
    }
    public void HealthDown(double damage) 
    {    
        hp -= damage;
    }
    public string GetInventory() {
        return inventory;
    }
}
class Item {

    public string name;
    public string description;

    public Item(string name,string description)
    {

        this.description = description;
        this.name = name;
    }

    public virtual void PrintInfo() 
    {
        Console.WriteLine(description);
    }
}
class Weapon : Item{

    private double weaponDamage;

    public Weapon(string name, double weaponDamage, string description) : base(name,description)
    {    
        this.weaponDamage = weaponDamage;
    }
    public override void PrintInfo()
    {
        base.PrintInfo();
    }
    public double GetDamage() 
    {
        return weaponDamage;
    }
}
class Armor : Item{

    private double armorDefence;

    public Armor(string name, double armorDefence, string description) : base(name, description)
    {
        this.armorDefence = armorDefence;
    }
    public override void PrintInfo()
    {
        base.PrintInfo();
    }
    public double GetDefence() 
    {
        return armorDefence;
    }
}

class Enemy 
{
    private double health;
    private double damage;
    private double defence;
    public string description;

    public Enemy(double health, double damage, double defence, string description)
    {
        this.health = health;
        this.damage = damage;
        this.defence = defence;
        this.description = description;
    }

    public double Health => health;
    public double Damage => damage;
    public double Defence => defence;
}
class BoomFly : Enemy
{
    private double critChance;
    public BoomFly(double health, double damage, double defence, string description, double crit) : base(health, damage, defence, description)
    {
        this.critChance = crit;
    }

    public double GetCritChance() { return critChance; }
}
class Gurgling : Enemy
{
    private bool ignoreArmor;

    public Gurgling(double health, double damage, double defence, string description, bool ignoreArmor) : base(health, damage, defence, description)
    {
        this.ignoreArmor = ignoreArmor;
    }
}
class Fatty : Enemy
{
    private double frozenCrit;

    public Fatty(double health, double damage, double defence, string description, double frozenCrit) : base(health, damage, defence, description)
    {
        this.frozenCrit = frozenCrit;
    }

    public double GetFrozenCrit() { return frozenCrit; }
}

class BabyPlum : BoomFly 
{
    public BabyPlum(double health, double damage, double defence, string description, double crit) : base(health * 2, damage * 1.5, defence * 1.2, description, crit * 1.1) { }
}
class GurdyJr : Gurgling
{
    public GurdyJr(double health, double damage, double defence, string description, bool ignoreArmor) : base(health * 2.5, damage * 1.3, defence * 1.4, description, ignoreArmor) { }
}
class MegaFatty : Fatty 
{
    public MegaFatty(double health, double damage, double defence, string description, double frozenCrit) : base(health * 1.8, damage * 1.6, defence * 1.1, description, frozenCrit + 0.1) { }
}
class Gurdy : Gurgling
{
    private double frozenCrit;
    public Gurdy(double health, double damage, double defence, string description, bool ignoreArmor, double frozenCrit) : base(health * 1.3, damage * 1.8, defence * 0.6, description, ignoreArmor) 
    {
        this.frozenCrit = frozenCrit + 0.15;
    }
}

class Mother 
{
    private double hp;
    private double damage;
    private string description;

    public double HP => hp;
    public double DAMAGE => damage;
    
    public Mother(string description, double hp = 150, double damage = 8)
    {
        this.hp = hp;
        this.damage = damage;
        this.description = description;
    }

    public string GetDescription() { return description; }
}