using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Binding_Of_Isaac_WPF.Model.Model
{
    internal class Gurgling : Enemy
    {
        private bool ignoreArmor;

        public Gurgling(double health, double damage, double defence, string description, bool ignoreArmor) : base(health, damage, defence, description)
        {
            this.ignoreArmor = ignoreArmor;
        }
        public override bool GetIgnoreArmor() => ignoreArmor;
    }
}
