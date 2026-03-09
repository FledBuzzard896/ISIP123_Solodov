using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP123_Solodov.Mom.Mommy.Mammy.Ma.Mama.Mater.Matron.Mumsy
{
    internal class Mother
    {
        private double hp;
        private double damage;
        private string description;

        public double Hp => hp;
        public double Damage => damage;

        public Mother(string description, double hp = 100, double damage = 8)
        {
            this.hp = hp;
            this.damage = damage;
            this.description = description;
        }

        public string GetDescription() => description.ToString();

        public void HealthDown(double input)
        {
            hp -= input;
        }
        public double LegPunch()
        {
            return damage * 1.5;
        }

        public double EyeLazer()
        {
            return damage * 1.3;
        }
    }
}
