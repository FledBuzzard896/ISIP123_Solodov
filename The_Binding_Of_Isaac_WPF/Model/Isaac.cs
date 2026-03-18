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
        private static double damage;
        private static double defence;
        private static string inventory;
        public static bool isIsaacAlive = true;

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

        public static void SetStats(double inputHP, double inputDMG, double inputDFNC, string inputInventory) 
        { 
            hp = inputHP;
            damage = inputDMG;
            defence = inputDFNC;
            inventory = inputInventory;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"\n=============== Статистика ===============\nHP: {Math.Round(hp, 1)}\nУрон: {Math.Round(damage, 2)}\nБроня: {Math.Round(defence, 2)}\nPull-up: {inventory}\n==========================================\n");
        }

        private int count = 1;
        public void AddPickUp(Item pickUp)
        {
            inventory += ", ";

            if (count % 3 == 0)
            {
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

                string newInventory = "";
                int c = 0;

                for (int i = 0; i < inventory.Length; i++)
                {
                    // Проверяем, начинается ли с текущей позиции "Ням-сердце"
                    if (i <= inventory.Length - "Ням-сердце".Length &&
                        inventory.Substring(i, "Ням-сердце".Length) == "Ням-сердце" && c == 0)
                    {
                        // Пропускаем "Ням-сердце"
                        i += "Ням-сердце".Length - 1;
                        c = 1;
                    }
                    else
                    {
                        newInventory += inventory[i];
                    }
                }

                inventory = newInventory;
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

        public string GetInventory()
        {
            return inventory;
        }
    }
}
