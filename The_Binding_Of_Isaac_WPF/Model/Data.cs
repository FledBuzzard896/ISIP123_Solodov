using The_Binding_Of_Isaac_WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Binding_Of_Isaac_WPF.Model
{
    internal class Data
    {
        
        public static List<Enemy> lstOfEnemies = new List<Enemy>();
        public static  List<Enemy> lstOfBosses = new List<Enemy>();

        public static Armor MomsUnderwear = new Armor("Мамино бельё", 0.1, "Снижает получаемый урон на 10%");
        public static Armor Pjs = new Armor("Пижама", 0.2, "Снижает получаемый урон на 20%");
        public static Armor Habit = new Armor("Одеяние", 0.1, "Снижает получаемый урон на 10%");

        public static Item YumHeart = new Item("Ням-сердце", "Даёт возможность его съесть когда у персонажа остается меньше 25% здоровья");
        public static Item LuckyLeg = new Item("Счастливая нога", "+ 1 удача");
        public static Item TheBelt = new Item("Ремень", "Вы быстрее бегаете");
        public static Item PiggyBank = new Item("Свинюшка", "+ 3 монеты");
        public static Item Bumbo = new Item("Бамбо", "Фамильяр, который собирает монетки и растет за счет них, больше он ничего не делает");

        public static List<Item> lstOfPickUps = new List<Item>
        {
            new Weapon("Пентаграмма", 1, "+ Урон, + 10% шанс сделки", "Images/Item/pentagram.png"),
            new Weapon("Шприц синтола", 1, "+ Урон, + Дальность выстрела", "Images/Item/synthoil.png"),
            new Weapon("Черная материя", 1, "+ Урон, + Шанс наложить на врага эффект страха", "Images/Item/darkMatter.png"),
            new Weapon("Ячмень", 1, "+ Урон", "Images/Item/stye.png"),
            new Weapon("Степлер", 1, "+ Урон, Все слёзы стреляют из правого глаза", "Images/Item/stapler.png"),
            new Weapon("Священное сердце", 2.3, "+ Урон, Выстрелы самонаводятся", "Images/Item/sacredHeart.png"),
            new Weapon("Гильотина", 1, "+ Урон, Голова персонажа летает отдельно от него", "Images/Item/guillotine.png"),
            new Weapon("Рвотный корень", 40, "+ Мега урон, взрывные слёзы", "Images/Item/ipecac.png"),

            new Armor("Мамино бельё", 0.1, "Снижает получаемый урон на 10%", "Images/Item/momsUnderwear.png"),
            new Armor("Пижама", 0.2, "Снижает получаемый урон на 20%", "Images/Item/pjs.png"),
            new Armor("Одеяние", 0.1, "Снижает получаемый урон на 10%", "Images/Item/habit.png"),

            new Item("Ням-сердце", "Даёт возможность его съесть когда у персонажа остается меньше 25% здоровья", "Images/Item/yumHeart.png"),

            Pentagram, Synthoil, DarkMatter, Barley, Stapler, SacredHeart, Guillotine, Ipecac,
            MomsUnderwear, Pjs, Habit,
            YumHeart, LuckyLeg, TheBelt, PiggyBank, Bumbo
        };
    }
}
