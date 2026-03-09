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
using ISIP123_Solodov.Model;
using ISIP123_Solodov.Mom.Mommy.Mammy.Ma.Mama.Mater.Matron.Mumsy;

Console.WriteLine("   +++============+*#+++=============+#% =+===============   ================+  +================   \r\n   +++++======+++++#*=====++++++++++++%%*=====++++++=======#*======+++++=======%+=====++++++======  \r\n    %%%#+====+%%%%%%+=====*%%%%%%%%%%%%%+=====+%%%%#=====+#%#=====+#%%%#+=====*%*=====+%%%%*=====+  \r\n       +=====*%%%   ======+++=======    ==================#%#========++++=====*%*=====+  %%%######  \r\n      =======#%%     ================*# ==================*%#=================*%#+====== %%%%%%%%   \r\n      ======+%%%       %%%%%%%#+=====#%#+=====+*   +======*%%======*%%%%+=====+%#+=====+    +=====+ \r\n ===========*###         +****+======#%%=======+   +======+%%+=====+  %%*=====+%%+====== ###+====== \r\n+===============+#%+==============+*#%%%========   ++======%%+======  %%#=====+%%#*+=============++ \r\n+**##############%%################%%%%%########   ########%%######*  %%%######%%%%############+++  \r\n  ##%%%%%%%%%%%%%%%  %%%%%%%%%%%%%%%%%  %%%%%%%    %%%%%%%%%%##%%%    %%%%%%%%  %%%%%%%%%%%%%%#   \n");
Console.WriteLine("1. Начать игру\n0. Выйти");
Console.Write(">>> ");
string choice = Console.ReadLine();


GameRandom random = new GameRandom();

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

Item YumHeart = new Item("Ням-сердце", "Даёт возможность его съесть когда у персонажа остается меньше 25% здоровья");
Item LuckyLeg = new Item("Счастливая нога", "+ 1 удача");
Item TheBelt = new Item("Ремень", "Вы быстрее бегаете");
Item PiggyBank = new Item("Свинюшка", "+ 3 монеты");
Item Bumbo = new Item("Бамбо", "Фамильяр, который собирает монетки и растет за счет них, больше он ничего не делает");

lstOfPickUps.Add(YumHeart);
lstOfPickUps.Add(LuckyLeg);
lstOfPickUps.Add(TheBelt);
lstOfPickUps.Add(PiggyBank);
lstOfPickUps.Add(Bumbo);

//BoomFly enemy1 = new BoomFly(15, 2, 0.1, "Красная Бомбомуха", 10);
//Gurgling enemy2 = new Gurgling(20, 3, 0.1, "Булькающий", true);
//Fatty enemy3 = new Fatty(20, 2.5, 0.2, "Толстяк", 10);

//lstOfEnemies.Add(enemy1);
//lstOfEnemies.Add(enemy2);
//lstOfEnemies.Add(enemy3);

BabyPlum boss1 = new BabyPlum(15, 2, 0.1, "Сливка", 10);
GurdyJr boss2 = new GurdyJr(20, 3, 0.1, "Гёрди Младшая", true);
MegaFatty boss3 = new MegaFatty(20, 2.5, 0.2, "Мега Толстяк", 10);
Gurdy boss4 = new Gurdy(20, 3, 0.1, "Гёрди", true, 10);

lstOfBosses.Add(boss1);
lstOfBosses.Add(boss2);
lstOfBosses.Add(boss3);
lstOfBosses.Add(boss4);

if (choice == "1")
{
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
                hero = random.RandomHero();
                break;
        }
    }

    double MAX_HP = hp;
    Isaac Character = new Isaac(hp, damage, defence, inventory);

    // Начало игры. Предисловие
    Console.WriteLine("Айзек и его мать жили одни в маленьком домике на холме. " +
        "\nАйзек был сам по себе, рисовал картинки и играл со своими игрушками, пока его мама смотрела христианские передачи по телевизору. " +
        "\nЖизнь была проста, и они оба были счастливы. Так было до тех пор, пока однажды мать Айзека не услышала голос свыше:\n");

    Console.ForegroundColor = ConsoleColor.DarkRed;
    Console.WriteLine("\"Твой сын погряз в грехе! Его нужно спасти!\"\n");
    Console.ResetColor();

    Console.ForegroundColor = ConsoleColor.DarkRed;
    Console.Write("\"Я сделаю все возможное, чтобы спасти его, Господи\" ");
    Console.ResetColor();

    Console.WriteLine("- ответила мать Айзека, вбегая в комнату Айзека, убирая все зло из его жизни.\n");
    Console.WriteLine("Снова голос позвал ее:");

    Console.ForegroundColor = ConsoleColor.DarkRed;
    Console.WriteLine("\"Душа Айзека все еще испорчена! Его нужно отрезать от всего злого в этом мире и исповедать его грехи\".\n");
    Console.Write("\"Я последую твоим указаниям, Господи. Я верю в тебя\"");
    Console.ResetColor();

    Console.WriteLine(", - ответила мать Айзека, запирая Айзека в его комнате, подальше от зол мира.\n");
    Console.WriteLine("В последний раз мать Айзека услышала голос Бога, зовущий ее: ");

    Console.ForegroundColor = ConsoleColor.DarkRed;
    Console.WriteLine("\"Ты сделала, как я просил, но я все еще сомневаюсь в твоей преданности мне. Чтобы доказать свою веру, я попрошу тебя еще об одном.\"\n");
    Console.Write("\"Да, Господи. Что угодно\"");
    Console.ResetColor();

    Console.WriteLine(", - взмолилась мать Айзека.\n");

    Console.ForegroundColor = ConsoleColor.DarkRed;
    Console.WriteLine("\"Чтобы доказать свою любовь и преданность, я требую жертву. Твой сын Айзек будет этой жертвой.\n" +
        "Зайди в его комнату и лиши его жизни в качестве подношения мне, чтобы доказать, что ты любишь меня превыше всего!\"\n");
    Console.Write("\"Да, Господи!\"");
    Console.ResetColor();

    Console.WriteLine(", - ответила она, схватив кухонный нож. Айзек, наблюдая через щель в двери, задрожал от страха.\n" +
        "Метаясь по комнате в поисках укрытия, он заметил люк в подвал, спрятанный под ковром. \n" +
        "Не раздумывая, он распахнул люк как раз в тот момент, когда его мать ворвалась в дверь, и бросился в неизвестную глубину внизу.\n");

    Console.WriteLine("Нажмите \"Enter\"");
    string input = Console.ReadLine();

    bool isIsaacAlive = true;

    for (int lvl = 1; lvl < 5; lvl++)
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        // вывод информации о номере этажа
        if (lvl <= 3 && isIsaacAlive)
        {
            Console.WriteLine($"Подвал: {lvl}");
            Console.ResetColor();
        }
        else if (isIsaacAlive)
        {
            Console.WriteLine($"Глубины: {lvl - 3}");
            Console.ResetColor();
        }

        int countOfRooms = random.GenerateRooms(); // кол-во комнат от 4 до 6

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
                    isIsaacAlive = generateRoom(countOfRooms, Character, MAX_HP);
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
    if (isIsaacAlive)
    {
        Console.Write("Вы дошли до самого дна подвала, где сейчас решится ваша судьба, сможете ли вы простить все грехи и принять свою судьбу, или дадите отпор!");
        Console.Write("Босс: ");

        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("Мать\n");
        Console.ResetColor();

        Mother finalBoss = new Mother("Мама, желает тебе всего лучшего, готова тебя растоптать ради искупления грехов!");

        var MENUcoordinates = Console.GetCursorPosition();

        isIsaacAlive = FinalBossFight(finalBoss, Character, MENUcoordinates);

        if (isIsaacAlive)
        {
            Console.WriteLine("\t\tКонцовка.");
            Console.WriteLine("=========================================");
            Console.Write("Айзек загнан в угол своей матерью, которая говорит:\n");

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\"Я сделаю то, что Ты мне сказал, Господь. Я люблю тебя больше всего на свете\"\n");
            Console.ResetColor();

            Console.WriteLine("В тот момент, когда мать готова убить Айзека, Бог вмешивается. \n" +
                "Словно сама по себе, Библия, которая стояла на полке выше, вдруг падает прямо на голову Матери Айзека.");
            Console.WriteLine("Она не пережила такого...\n");
            Console.WriteLine("Айзек посмотрел на тело Матери, и понял, что ему ещё многое нужно будет обдумать, но одно он мог сказать сразу, в этом доме ему больше нечего делать.");
            Console.WriteLine("Айзек взял фото своей мамы, собрал кулёк еды и отправился из дома прямо в лес, искать, где-то потерявшегося, по словам Матери, кота Гаппи...");
        }
        else
        {
            Console.WriteLine("\t\tКонцовка.");
            Console.WriteLine("=========================================");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Дорогой дневник, сегодня я умер. Я был убит этим существом → Мать. \nВсе свои вещи я завещаю моему коту Гаппи. Прощай, жестокий мир. Люблю, целую, Айзек!");
            Console.ResetColor();
        }
    }
}

bool generateRoom(int countOfRooms, Isaac isaac, double MAX_HP)
{
    bool isIsaacAlive = true;

    if (countOfRooms > 1)
    {
        if (random.MobOrChest())
        {

            int randomItem = random.RandomItem(lstOfPickUps);
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
            Console.WriteLine("\n1. Взять предмет\n2. Пропустить предмет");
            Console.Write(">>> ");
            choice = Console.ReadLine();
            Console.WriteLine();

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

                    lstOfPickUps.Remove(lstOfPickUps[randomItem]);
                    break;

                default: break;
            }
        }
        else
        {
            int randomEnemy = random.RandomEnemy();

            var vrag = SimpleFactory.CreateEnemy(randomEnemy);
            
            Console.Write("Вы зашли в комнату, в комнате на вас напал ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(vrag.description + "\n");
            Console.ResetColor();

            var MENUcoordinates = Console.GetCursorPosition();

            isIsaacAlive = Fight(vrag.Health, vrag.Damage, vrag.Defence, vrag.GetCritChance(), vrag.GetFrozenChance(), vrag.GetIgnoreArmor(), isaac, MAX_HP, MENUcoordinates);
        }
    }
    else
    {
        int randomBoss = random.RandomBoss(lstOfBosses);

        Console.Write($"Вы зашли в комнату босса, в комнате оказался ");
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(lstOfBosses[randomBoss].description + "\n");
        Console.ResetColor();

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
            bossIGNORE_ARMOR = boss2.GetIgnoreArmor();
            lstOfBosses.Remove(boss2);
        }
        else if (lstOfBosses[randomBoss] is MegaFatty)
        {
            bossFROZEN_CHANCE = boss3.GetFrozenChance();
            lstOfBosses.Remove(boss3);
        }
        else
        {
            bossIGNORE_ARMOR = true;
            bossFROZEN_CHANCE = boss3.GetFrozenChance();
            lstOfBosses.Remove(boss4);
        }

        var MENUcoordinates = Console.GetCursorPosition();

        isIsaacAlive = Fight(bossHP, bossDAMAGE, bossDEFENCE, bossCRIT_CHANCE, bossFROZEN_CHANCE, bossIGNORE_ARMOR, isaac, MAX_HP, MENUcoordinates);
    }

    return isIsaacAlive;
}
bool Fight(double enemyHP, double enemyDAMAGE, double enemyDEFENCE, double enemyCRIT_CHANCE, double enemyFROZEN_CHANCE, bool enemyIGNORE_ARMOR, Isaac isaac, double MAX_HP, (int left, int top) MENUcoordinates)
{
    bool isIsaacAlive = true;

    string message = " ";
    double damage = 0;
    bool userMoveSkip = false;

    var INPUTcoordinates = Console.GetCursorPosition();

    while (isaac.Hp > 0 && enemyHP > 0)
    {
        if (isaac.Hp <= MAX_HP * 0.25)
        {
            isaac.HealthUp(MAX_HP);
        }

        Console.WriteLine($"\t\t  Враг:\n=========================================\nHP: {Math.Round(enemyHP, 1)}\nDamage: {Math.Round(enemyDAMAGE, 2)}\nCritDamage: {Math.Round(enemyDAMAGE * 1.5, 2)}\n");
        Console.WriteLine($"\t\t   Вы:\n=========================================\nHP: {Math.Round(isaac.Hp, 1)}\nDamage: {Math.Round(isaac.Damage, 2)}\n");

        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(message);
        Console.ResetColor();

        if (userMoveSkip)
        {
            isaac.HealthDown(damage);
        }

        Console.WriteLine("1. Атаковать\n2. Уклониться");
        Console.Write(">>>  ");

        INPUTcoordinates = Console.GetCursorPosition();
        Console.SetCursorPosition(INPUTcoordinates.Left - 1, INPUTcoordinates.Top);

        string userMove = Console.ReadLine();

        switch (userMove)
        {
            case "1":
                enemyHP -= isaac.Damage - (isaac.Damage * enemyDEFENCE);
                isaac.HealthDown(damage);
                break;

            case "2":
                var tempCoords = Console.GetCursorPosition();

                if (random.EvasionOrDamage())
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Вы успешно уклонились");
                    Console.ResetColor();
                    Console.SetCursorPosition(0, tempCoords.Top);
                    System.Threading.Thread.Sleep(2000);
                    Console.WriteLine("                                                                                                     ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Во время уклонения вас задело, но вы вовремя успели защититься, получаемый урон снижен на 70 - 100%");
                    Console.ResetColor();
                    Console.SetCursorPosition(0, tempCoords.Top);
                    System.Threading.Thread.Sleep(5000);
                    Console.WriteLine("                                                                                                     ");

                    if (random.MaxDamageOrMinDamage())
                    {
                        isaac.HealthDown(damage - (damage * (70 / 100)));
                    }
                    else
                    {
                        isaac.HealthDown(damage - (damage * (85 / 100)));
                    }
                }
                break;
        }

        if (enemyCRIT_CHANCE != 0)
        {
            if (random.IsSpecialSkill(enemyCRIT_CHANCE))
            {
                damage = (enemyDAMAGE * 1.5) - (enemyDAMAGE * 1.5 * isaac.Defence);
                message = "Враг наносит критический удар!                                   ";
            }
            else
            {
                damage = enemyDAMAGE - (enemyDAMAGE * isaac.Defence);
                message = "Враг наносит удар                                                ";
            }
        }
        else if (enemyIGNORE_ARMOR)
        {
            damage = enemyDAMAGE;
            message = "Враг игнорирует твою броню и наносит удар                            ";
        }
        else
        {
            if (random.IsSpecialSkill(enemyFROZEN_CHANCE))
            {
                userMoveSkip = true;
                message = "Враг использовал заморозку, вы пропускаете ход                   ";
            }
            else
            {
                userMoveSkip = false;
                message = "Враг наносит удар                                                ";
            }
            damage = enemyDAMAGE - (enemyDAMAGE * isaac.Defence);
        }

        Console.SetCursorPosition(MENUcoordinates.left, MENUcoordinates.top);
    }
    Console.SetCursorPosition(0, INPUTcoordinates.Top + 1);

    if (isaac.Hp > 0)
    {
        Console.WriteLine("============= Враг повержен! =============\n");
    }
    else
    {
        Console.WriteLine("\t\tКонцовка.");
        Console.WriteLine("=========================================");

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("Дорогой дневник, сегодня я умер. Все свои вещи я завещаю моему коту Гаппи. \nПрощай, жестокий мир. Люблю, целую, Айзек!");
        Console.ResetColor();

        isIsaacAlive = false;
    }

    return isIsaacAlive;
}
bool FinalBossFight(Mother boss, Isaac isaac, (int left, int top) MENUcoordinates)
{
    bool isIsaacAlive = true;
    string message = "";
    double damage = 0;

    string str = boss.GetDescription();

    var INPUTcoordinates = Console.GetCursorPosition();

    while (isaac.Hp > 0 && boss.Hp > 0)
    {
        Console.WriteLine($"\t\t  Мама:\n=========================================\nHP: {Math.Round(boss.Hp, 1)}\nDamage: {Math.Round(boss.Damage, 2)}\nLegDamage: {Math.Round(boss.LegPunch(), 2)}\nEyeLazerDamage: {Math.Round(boss.EyeLazer(), 2)}\n{str}");
        Console.WriteLine($"\t\t   Вы:\n=========================================\nHP: {Math.Round(isaac.Hp, 1)}\nDamage: {Math.Round(isaac.Damage, 2)}\n");

        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(message);
        Console.ResetColor();

        Console.WriteLine("1. Атаковать\n2. Уклониться");
        Console.Write(">>>  ");

        INPUTcoordinates = Console.GetCursorPosition();
        Console.SetCursorPosition(INPUTcoordinates.Left - 1, INPUTcoordinates.Top);

        string userMove = Console.ReadLine();

        switch (userMove)
        {
            case "1":
                boss.HealthDown(isaac.Damage);
                isaac.HealthDown(damage);
                break;

            case "2":
                var tempCoords = Console.GetCursorPosition();

                if (random.EvasionOrDamage())
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Вы успешно уклонились");
                    Console.ResetColor();
                    Console.SetCursorPosition(0, tempCoords.Top);
                    System.Threading.Thread.Sleep(2000);
                    Console.WriteLine("                                                                                                     ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Во время уклонения вас задело, но вы вовремя успели защититься, получаемый урон снижен на 70 - 100%");
                    Console.ResetColor();
                    Console.SetCursorPosition(0, tempCoords.Top);
                    System.Threading.Thread.Sleep(5000);
                    Console.WriteLine("                                                                                                     ");

                    if (random.MaxDamageOrMinDamage())
                    {
                        isaac.HealthDown(damage - (damage * (70 / 100)));
                    }
                    else
                    {
                        isaac.HealthDown(damage - (damage * (85 / 100)));
                    }
                }
                break;
        }

        double attack = random.MotherAttack();

        if (attack <= 0.33)
        {
            message = "Мать наносит обычный удар!       ";
            damage = boss.Damage;
        }
        else if (0.33 < attack && attack <= 0.66)
        {
            message = "Мать наносит удар ногой!!!       ";
            damage = boss.LegPunch();
        }
        else
        {
            message = "Мать использует свой глаз!!!     ";
            damage = boss.EyeLazer();
        }
        Console.SetCursorPosition(MENUcoordinates.left, MENUcoordinates.top);
    }

    Console.SetCursorPosition(0, INPUTcoordinates.Top + 1);

    if (isaac.Hp <= 0)
    {
        isIsaacAlive = false;
    }

    return isIsaacAlive;
}
