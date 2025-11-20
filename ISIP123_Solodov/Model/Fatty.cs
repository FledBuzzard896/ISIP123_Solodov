using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP123_Solodov.Model
{
    internal class Fatty : Enemy
    {
        private double frozenCrit;

        public Fatty(double health, double damage, double defence, string description, double frozenCrit) : base(health, damage, defence, description)
        {
            this.frozenCrit = frozenCrit;
        }

        public double GetFrozenCrit() { return frozenCrit; }
    }
}
