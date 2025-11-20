using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP123_Solodov.Model
{
    internal class SimpleFactory
    {
        public static Enemy CreateEnemy(string type) 
        {
            switch (type) 
            {
                case "Красная Бомбомуха":
                    return new BoomFly(15, 2, 0.1, "Красная Бомбомуха", 10);
                case "Булькающий":
                    return new Gurgling(20, 3, 0.1, "Булькающий", true);
                case "Толстяк":
                    return new Fatty(20, 2.5, 0.2, "Толстяк", 10);
                default: return null;
            }
        }
    }
}
