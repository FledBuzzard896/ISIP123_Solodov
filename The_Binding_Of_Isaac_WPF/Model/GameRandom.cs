using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Binding_Of_Isaac_WPF.Model.Model;

namespace The_Binding_Of_Isaac_WPF.Model
{
    internal class GameRandom
    {
        private Random _random = new Random();
        private bool Chance(double input) 
        {
            return _random.NextDouble() <= input;
        }

        public string RandomHero() 
        {
            return Convert.ToString(_random.Next(1,4));
        }
        public int GenerateRooms() 
        {
            return _random.Next(4, 7);
        }
        public int RandomNightmare(List<string> list) 
        {
            return _random.Next(0,list.Count);
        }
        public bool MobOrChest() 
        {
            return Chance(0.5);
        }
        public bool EvasionOrDamage() 
        {
            return Chance(0.4);
        }
        public double MaxDamageOrMinDamage() 
        {
            double dmg = _random.NextDouble();
            if (dmg > 0.7) { return dmg; }
            else { return 0.7; }
        }
        public bool IsSpecialSkill(double specialChance) 
        {
            return Chance(specialChance);
        }
        public Item RandomItem(List<Item> list) 
        {
            var PickUp = Data.lstOfPickUps[_random.Next(0, list.Count)];
            Data.lstOfPickUps.Remove(PickUp);
            return PickUp;
        }
        public Enemy RandomEnemy() 
        {
            var enemy = SimpleFactory.CreateEnemy(_random.Next(0, 3));
            return enemy;
        }
        public Enemy RandomBoss(List<Enemy> list) 
        {
            var boss = Data.lstOfBosses[_random.Next(0, list.Count)];
            Data.lstOfBosses.Remove(boss);
            return boss;
        }

        public double MotherAttack() 
        {
            return _random.NextDouble();
        }
    }
}
