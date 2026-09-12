using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop
{
    internal class Core
    {
        public static db_for_importEntities Context => new db_for_importEntities();

        public static Users_ AuthUser = null;
    }
}
