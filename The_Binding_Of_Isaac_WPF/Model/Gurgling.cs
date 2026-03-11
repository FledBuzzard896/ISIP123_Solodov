using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Binding_Of_Isaac_WPF.Model
{
    internal class Gurgling : Enemy
    {
        private bool ignoreArmor;

        public Gurgling(double health, double damage, double defence, string description, string imgUrl, bool ignoreArmor) : base(health, damage, defence, description, imgUrl)
        {
            this.ignoreArmor = ignoreArmor;
        }
        public override bool GetIgnoreArmor() => ignoreArmor;
    }
}
