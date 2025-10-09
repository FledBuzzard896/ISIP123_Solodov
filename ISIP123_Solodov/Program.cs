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
string choice = Console.ReadLine();

Random random = new Random();

if (choice == "1") {

    Console.WriteLine("Выберите персонажа:\n1. Айзек \t(HP\t|||\tDMG ||)\n2. Каин \t(HP\t||\tDMG |||)\n3. Магдалина \t(HP\t||||\tDMG ||)");
    string hero = Console.ReadLine();

    while (hero != "used")
    {
        switch (hero)
        {

            case "1":
                Isaac ClassicIsaac = new Isaac(6 * 10, 3.5, 0.1, "");
                Console.WriteLine("Выбранный персонаж: Айзек");
                hero = "used";
                break;

            case "2":
                Isaac KainIsaac = new Isaac(4 * 10, 3.5 * 1.2, 0.3, "Счастливая нога");
                Console.WriteLine("Выбранный персонаж: Каин");
                hero = "used";
                break;

            case "3":
                Isaac MagdaleneIsaac = new Isaac(8 * 10, 3.5, 0.05, "Ням-сердце");
                Console.WriteLine("Выбранный персонаж: Магдалина");
                hero = "used";
                break;

            default:
                hero = Convert.ToString(random.Next(1, 4));
                break;
        }
    }

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

        while (countOfRooms != 1)
        {
            Console.WriteLine("========== Выберите действие ==========\n1. Посмотреть статистику\n2. Зайти в следующую комнату");
            choice = Console.ReadLine();


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
        Console.WriteLine($"----------Статистика----------\nHP: {hp}\nУрон: {damage}\nБроня: {defence}\nPull-up: {inventory}");
    }
}
class Weapon {

    private double weaponDamage;
    private string description;

    public Weapon(double weaponDamage, string description) {
        
        this.weaponDamage = weaponDamage;
        this.description = description;
    }
}
class Armor {

    private double armorDefence;
    private string description;

    public Armor(double armorDefence, string description) {

        this.armorDefence = armorDefence;
        this.description = description;
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