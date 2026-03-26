using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Binding_Of_Isaac_WPF.Model
{
    internal class Gurdy : Gurgling
    {
        private double frozenCrit;
        public const string VS_SCREEN = "/Images/Boss/VS/isaacVSgurdy.png";
        public Gurdy(double health, double damage, double defence, string description, bool ignoreArmor, string imgUrl, double frozenCrit) : base(health * 1.3, damage * 1.8, defence * 0.6, description, imgUrl, ignoreArmor)
        {
            this.frozenCrit = frozenCrit + 0.15;
        }
    }
}
