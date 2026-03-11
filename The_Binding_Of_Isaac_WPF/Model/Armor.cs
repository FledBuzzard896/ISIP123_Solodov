using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Binding_Of_Isaac_WPF.Model
{
    internal class Armor : Item
    {
        private double armorDefence;

        public Armor(string name, double armorDefence, string description, string imgUrl) : base(name, description, imgUrl)
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
