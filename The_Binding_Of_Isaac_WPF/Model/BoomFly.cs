using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Binding_Of_Isaac_WPF.Model
{
    internal class BoomFly : Enemy
    {
        private double critChance;

        public BoomFly(double health, double damage, double defence, string description, double crit) : base(health, damage, defence, description)
        {
            this.critChance = crit;
        }
        public override double GetCritChance() => critChance;
    }
}
