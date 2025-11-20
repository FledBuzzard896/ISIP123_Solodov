using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP123_Solodov.Model
{
    internal class BoomFly : Enemy
    {
        private double critChance;
        public BoomFly(double health, double damage, double defence, string description, double crit) : base(health, damage, defence, description)
        {
            this.critChance = crit;
        }

        public double GetCritChance() { return critChance; }
    }
}
