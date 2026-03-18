using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int RandomNightmare() 
        {
            return _random.Next(0,3);
        }
        public bool MobOrChest() 
        {
            return Chance(0.5);
        }
        public bool EvasionOrDamage() 
        {
            return Chance(0.4);
        }
        public bool MaxDamageOrMinDamage() 
        {
            return Chance(0.7);
        }
        public bool IsSpecialSkill( double specialChance) 
        {
            return Chance(specialChance);
        }

        public int RandomItem(List<Item> list) 
        {
            return _random.Next(0, list.Count);
        }
        public int RandomEnemy() 
        {
            return _random.Next(0, 3);
        }
        public int RandomBoss(List<Enemy> list) 
        {
            return _random.Next(0, list.Count);
        }

        public double MotherAttack() 
        {
            return _random.NextDouble();
        }
    }
}
