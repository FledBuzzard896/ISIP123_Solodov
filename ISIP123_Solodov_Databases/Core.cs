using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP123_Solodov_Databases
{
    internal class Core
    {
        public static Solodov_NagievOrderEntities Context = new Solodov_NagievOrderEntities();
        public static Solodov_NagievOrderEntities1 ContextKIP = new Solodov_NagievOrderEntities1(); // для КИПа
    }
}
