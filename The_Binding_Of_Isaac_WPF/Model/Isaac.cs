using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Binding_Of_Isaac_WPF.Model
{
    internal class Isaac
    {
        private static double hp;
        private static double MAX_HP;
        private static double damage;
        private static double defence;

        public static bool isIsaacAlive = true;
        public static int floorsLeft = 4;
        public static List<Item> inventory = new List<Item>();
        public static List<string> listOfNightmares = new List<string>()
        {
            "/Images/Nightmares/nightmare1.png",
            "/Images/Nightmares/nightmare2.png",
            "/Images/Nightmares/nightmare3.png",
            "/Images/Nightmares/nightmare4.png"
        };

        public static double Hp => hp;
        public static double Damage => damage;
        public static double Defence => defence;
        public static double Max_hp => MAX_HP;

        public static void SetStats(double inputHP, double inputMaxHP, double inputDMG, double inputDFNC, List<Item> inputInventory) 
        { 
            hp = inputHP;
            MAX_HP = inputMaxHP;
            damage = inputDMG;
            defence = inputDFNC;
            inventory = inputInventory;
        }

        public static void AddPickUp(Item pickUp)
        {
            inventory.Add(pickUp);
        }

        public static void AddDamage(Weapon pickUp)
        {
            damage += pickUp.GetDamage();
        }

        public static void AddDefence(Armor pickUp)
        {
            defence += pickUp.GetDefence();
        }

        public static void HealthUp()
        {
            hp = MAX_HP;

            var heart = inventory.First(x => x.name == "Ням-сердце");
            inventory.Remove(heart);
        }

        public static void HealthDown(double damage)
        {
            hp -= damage;
        }
    }
}
