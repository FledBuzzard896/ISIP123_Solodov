using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Binding_Of_Isaac_WPF.Model;

namespace The_Binding_Of_Isaac_WPF.Model
{
    internal class Data
    {
        public static List<Item> lstOfPickUps = new List<Item>
        {
            new Weapon("Пентаграмма", 1, "+ Урон, + 10% шанс сделки", "/Images/Item/pentagram.png"),
            new Weapon("Шприц синтола", 1, "+ Урон, + Дальность выстрела", "/Images/Item/synthoil.png"),
            new Weapon("Черная материя", 1, "+ Урон, + Шанс наложить на врага эффект страха", "/Images/Item/darkMatter.png"),
            new Weapon("Ячмень", 1, "+ Урон", "/Images/Item/stye.png"),
            new Weapon("Степлер", 1, "+ Урон, Все слёзы стреляют из правого глаза", "/Images/Item/stapler.png"),
            new Weapon("Священное сердце", 2.3, "+ Урон, Выстрелы самонаводятся", "/Images/Item/sacredHeart.png"),
            new Weapon("Гильотина", 1, "+ Урон, Голова персонажа летает отдельно от него", "/Images/Item/guillotine.png"),
            new Weapon("Рвотный корень", 40, "+ Мега урон, взрывные слёзы", "/Images/Item/ipecac.png"),

            new Armor("Мамино бельё", 0.1, "Снижает получаемый урон на 10%", "/Images/Item/momsUnderwear.png"),
            new Armor("Пижама", 0.2, "Снижает получаемый урон на 20%", "/Images/Item/pjs.png"),
            new Armor("Одеяние", 0.1, "Снижает получаемый урон на 10%", "/Images/Item/habit.png"),

            new Item("Ням-сердце", "Даёт возможность его съесть когда у персонажа остается меньше 25% здоровья", "/Images/Item/yumHeart.png"),
            new Item("Счастливая нога", "+ 1 удача", "/Images/Item/luckyFoot.png"),
            new Item("Ремень", "Вы быстрее бегаете", "/Images/Item/theBelt.png"),
            new Item("Свинюшка", "+ 3 монеты", "/Images/Item/piggyBank.png"),
            new Item("Бамбо", "Фамильяр, который собирает монетки и растет за счет них, больше он ничего не делает", "/Images/Item/bumbo.png")
        };

        public static List<Enemy> lstOfBosses = new List<Enemy>()
        {
            new BabyPlum(15, 2, 0.1, "Сливка", "/Images/Boss/neutral/babyplum_neutral.png", 10),
            new GurdyJr(20, 3, 0.1, "Гёрди Младшая", "/Images/Boss/neutral/gurdyjr_neutral.png", true),
            new MegaFatty(20, 2.5, 0.2, "Мега Толстяк", "/Images/neutral/megafatty_neutral.png", 10),
            new Gurdy(20, 3, 0.1, "Гёрди", true, "/Images/neutral/gurdy_neutral.png", 10)
        };

        public static string FindAttackImageForEnemy(Enemy inputEnemy)
        {
            if (inputEnemy is BabyPlum) { return "/Images/Boss/attack/babyplum_attack.png"; }
            else if (inputEnemy is GurdyJr) { return "/Images/Boss/attack/gurdyjr_attack.png"; }
            else if (inputEnemy is Gurdy) { return "/Images/Boss/attack/gurdy_attack.png"; }
            else if (inputEnemy is MegaFatty) { return "/Images/Boss/attack/megafatty_attack.png"; }
            else if (inputEnemy is BoomFly) { return "/Images/Enemy/attack/redboomfly_attack.png"; }
            else if (inputEnemy is Gurgling) { return "/Images/Enemy/attack/gurgling.png"; }
            else { return "/Images/Enemy/attack/fatty_attack.png"; }
        }
    }
}
