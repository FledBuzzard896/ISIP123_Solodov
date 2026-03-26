using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Binding_Of_Isaac_WPF.Model
{
    internal class BabyPlum : BoomFly
    {
        public const string VS_SCREEN = "/Images/Boss/VS/isaacVSbabyplum.png";
        public BabyPlum(double health, double damage, double defence, string description, string imgUrl, double crit) : base(health * 2, damage * 1.5, defence * 1.2, description, imgUrl, crit * 1.1) { }
    }
}
