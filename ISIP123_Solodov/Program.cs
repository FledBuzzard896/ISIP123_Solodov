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

List<Item> lstOfpickUps = new List<Item>();

Weapon Pentagram = new Weapon("Пентаграмма", 1, "+ Урон, + 10% шанс сделки");
Weapon Synthoil = new Weapon("Шприц синтола", 1, "+ Урон, + Дальность выстрела");
Weapon DarkMatter = new Weapon("Черная материя", 1, "+ Урон, + Шанс наложить на врага эффект страха");
Weapon Barley = new Weapon("Ячмень", 1, "+ Урон");
Weapon Stapler = new Weapon("Степлер", 1, "+ Урон, Все слёзы стреляют из правого глаза");
Weapon SacredHeart = new Weapon("Священное сердце", 2.3, "+ Урон, Выстрелы самонаводятся");
Weapon Guillotine = new Weapon("Гильётина", 1, "+ Урон, Голова персонажа летает отдельно от него");
Weapon Ipecac = new Weapon("Рвотный корень", 40, "+ Мега урон, взрывные слёзы");

lstOfpickUps.Add(Pentagram);
lstOfpickUps.Add(Synthoil);
lstOfpickUps.Add(DarkMatter);
lstOfpickUps.Add(Barley);
lstOfpickUps.Add(Stapler);
lstOfpickUps.Add(SacredHeart);
lstOfpickUps.Add(Guillotine);
lstOfpickUps.Add(Ipecac);

Armor MomsUnderwear = new Armor("Мамино бельё", 0.1, "Снижает получаемый урон на 10%");
Armor Pjs = new Armor("Пижама", 0.2, "Снижает получаемый урон на 20%");
Armor Habit = new Armor("Одеяние", 0.1, "Снижает получаемый урон на 10%");

lstOfpickUps.Add(MomsUnderwear);
lstOfpickUps.Add(Pjs);
lstOfpickUps.Add(Habit);

Item YumHeart = new Item("Ням сердце", "Даёт возможность его съесть когда у персонажа остается меньше 25% здоровья");
Item LuckyLeg = new Item("Счастливая нога", "+ 1 удача");
Item TheBelt = new Item("Ремень", "Вы быстрее бегаете");
Item PiggyBank = new Item("Свинюшка", "+ 3 монеты");
Item Bumbo = new Item("Бамбо", "фамильяр, который собирает монетки и растет за счет них, больше он ничего не делает");

lstOfpickUps.Add(YumHeart);
lstOfpickUps.Add(LuckyLeg);
lstOfpickUps.Add(TheBelt);
lstOfpickUps.Add(PiggyBank);
lstOfpickUps.Add(Bumbo);

if (choice == "1") {

    Console.WriteLine("=========== Выберите персонажа ===========\n1. Айзек \t(HP\t|||\tDMG ||)\n2. Каин \t(HP\t||\tDMG |||)\n3. Магдалина \t(HP\t||||\tDMG ||)");
    Console.Write(">>> ");
    string hero = Console.ReadLine();

    double hp = 6 * 10;
    double damage = 3.5;
    double defence = 0.1;
    string inventory = "";

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

    Isaac Character = new Isaac(hp,damage,defence,inventory);

    // Начало игры
    for (int lvl = 1; lvl < 6; lvl++) 
    {
        if (lvl <= 3)
        {
            Console.WriteLine($"Подвал: {lvl}");
        }
        else 
        {
            Console.WriteLine($"Глубины: {lvl - 3}");
        }

        int countOfRooms = random.Next(4, 7); // кол-во комнат от 4 до 6

        while (countOfRooms != 0)
        {
            Console.WriteLine("----------- Выберите действие ------------\n1. Посмотреть статистику\n2. Зайти в следующую комнату");
            Console.Write(">>> ");
            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Character.PrintInfo();
                    break;
            }
        }
    }
    
}

void generateRoom(int countOfRooms) {

    if (countOfRooms > 1) {
        
        int mobOrChest = random.Next(1, 101);

        if (mobOrChest >= 75) {

            Console.WriteLine($"На твоём пути встала комната сокровищ, в ней находится {1}");
            Console.WriteLine("1. Взять предмет\n2. Пропустить предмет");
        }
    }
}

class Isaac {

    private double hp;
    private double damage;
    private double defence;

    private string inventory;

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
}
class Item {

    private string name;
    private string description;

    public Item(string name,string description)
    {

        this.description = description;
        this.name = name;
    }
}
class Weapon : Item{

    private double weaponDamage;

    public Weapon(string name, double weaponDamage, string description) : base(name,description)
    {    
        this.weaponDamage = weaponDamage;
    }
}
class Armor : Item{

    private double armorDefence;

    public Armor(string name, double armorDefence, string description) : base(name, description)
    {
        this.armorDefence = armorDefence;
    }
}

class Enemy {

    private double health;
    private double damage;
    private double defence;

    public Enemy(double health, double damage, double defence)
    {
        this.health = health;
        this.damage = damage;
        this.defence = defence;
    }
}
class BoomFly : Enemy
{

    private string description;
    private double critChance;

    public BoomFly(double health, double damage, double defence, string description, double crit) : base(health, damage, defence)
    {
        this.description = description;
        this.critChance = crit;
    }

}
class Gurgling : Enemy
{

    private string description;
    bool ignoreArmor;
    public Gurgling(double health, double damage, double defence, string description, bool ignoreArmor) : base(health, damage, defence)
    {
        this.description = description;
        this.ignoreArmor = ignoreArmor;
    }

}
class Fatty : Enemy
{

    private string description;
    private double frozenCrit;

    public Fatty(double health, double damage, double defence, string description, double frozenCrit) : base(health, damage, defence)
    {
        this.description = description;
        this.frozenCrit = frozenCrit;
    }
}
class BabyPlum : BoomFly {

    public BabyPlum(double health, double damage, double defence, string description, double crit) : base(health * 2, damage * 1.5, defence * 1.2, description, crit * 1.1) { }
}
class GurdyJr : Gurgling
{
    public GurdyJr(double health, double damage, double defence, string description, bool ignoreArmor) : base(health * 2.5, damage * 1.3, defence * 1.4, description, ignoreArmor) { }
}
class MegaFatty : Fatty {

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