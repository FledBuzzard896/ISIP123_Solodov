using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Binding_Of_Isaac_WPF.Model
{
    internal class GurdyJr : Gurgling
    {
        public const string VS_SCREEN = "/Images/VS/isaacVSgurdyJr.png";
        public GurdyJr(double health, double damage, double defence, string description, string imgUrl, bool ignoreArmor) : base(health * 2.5, damage * 1.3, defence * 1.4, description, imgUrl, ignoreArmor) { }

    }
}
