using ShutAndKing.DB_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShutAndKing.Classes
{
    internal class Core
    {
        public static KIP_ReadWriteAndTakeYourTimeEntities ContextKIP = new KIP_ReadWriteAndTakeYourTimeEntities();
    }
}
