using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Binding_Of_Isaac_WPF.Model.Model
{
    internal class SimpleFactory
    {
        public static Enemy CreateEnemy(int type) 
        {
            switch (type) 
            {
                case 0:
                    return new BoomFly(15, 2, 0.1, "Красная Бомбомуха", "Images/Enemy/neutral/redboomfly_neutral.png", 10);
                case 1:
                    return new Gurgling(20, 3, 0.1, "Булькающий", "Images/Enemy/neutral/gurgling_neutral.png", true);
                case 2:
                    return new Fatty(20, 2.5, 0.2, "Толстяк", "Images/Enemy/neutral/fatty_neutral.png", 10);
                default: return null;
            }
        }
    }
}
