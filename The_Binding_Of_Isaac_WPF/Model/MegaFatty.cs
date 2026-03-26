using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Binding_Of_Isaac_WPF.Model
{
    internal class MegaFatty : Fatty
    {
        public const string VS_SCREEN = "/Images/Boss/VS/isaacVSmegafatty.png";
        public MegaFatty(double health, double damage, double defence, string description, string imgUrl, double frozenCrit) : base(health * 1.8, damage * 1.6, defence * 1.1, description, imgUrl, frozenCrit + 0.1) { }
    }
}
