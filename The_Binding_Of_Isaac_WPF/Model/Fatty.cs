using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Binding_Of_Isaac_WPF.Model
{
    internal class Fatty : Enemy
    {
        private double frozenCrit;

        public Fatty(double health, double damage, double defence, string description, string imgUrl, double frozenCrit) : base(health, damage, defence, description, imgUrl)
        {
            this.frozenCrit = frozenCrit;
        }
        public override double GetFrozenChance() => frozenCrit;
    }
}
