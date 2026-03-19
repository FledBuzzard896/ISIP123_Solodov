using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Binding_Of_Isaac_WPF.Model
{
    internal class Weapon : Item
    {
        private double weaponDamage;

        public Weapon(string name, double weaponDamage, string description, string imgUrl) : base(name, description, imgUrl)
        {
            this.weaponDamage = weaponDamage;
        }
        public override string PrintInfo()
        {
            return base.PrintInfo();
        }
        public double GetDamage()
        {
            return weaponDamage;
        }
    }
}
