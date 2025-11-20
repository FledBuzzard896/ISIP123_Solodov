using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP123_Solodov.Model
{
    internal class Weapon : Item
    {
        private double weaponDamage;

        public Weapon(string name, double weaponDamage, string description) : base(name, description)
        {
            this.weaponDamage = weaponDamage;
        }
        public override void PrintInfo()
        {
            base.PrintInfo();
        }
        public double GetDamage()
        {
            return weaponDamage;
        }
    }
}
