using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP123_Solodov_WPF
{
    partial class  basepart
    {
        public string PrintInfo
        {
            get
            {
                switch (parttypeid)
                {
                    case 1:
                        return $"Сокет {cpu.socket.name} ";
                }

                return "";
            }
        }


    }
}
