using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Binding_Of_Isaac_WPF.Model
{
    internal class Enemy
    {
        private double health;
        private double damage;
        private double defence;
        public string description;
        public string imgUrl;

        public Enemy(double health, double damage, double defence, string description, string imgUrl)
        {
            this.health = health;
            this.damage = damage;
            this.defence = defence;
            this.description = description;
            this.imgUrl = imgUrl;
        }

        public double Health => health;
        public double Damage => damage;
        public double Defence => defence;

        public virtual double GetCritChance() => 0;
        public virtual bool GetIgnoreArmor() => false;
        public virtual double GetFrozenChance() => 0;
    }
}
