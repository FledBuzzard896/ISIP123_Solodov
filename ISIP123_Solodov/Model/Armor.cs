using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP123_Solodov.Model
{
    internal class Armor : Item
    {
        private double armorDefence;

        public Armor(string name, double armorDefence, string description) : base(name, description)
        {
            this.armorDefence = armorDefence;
        }
        public override void PrintInfo()
        {
            base.PrintInfo();
        }
        public double GetDefence()
        {
            return armorDefence;
        }
    }
}
