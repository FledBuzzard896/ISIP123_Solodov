using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP123_Solodov_WPF
{
    internal class Core
    {
        //public static FilmSearchEntities ContextHome = new FilmSearchEntities(); // подключение к домашней БД
        public static FilmSearchEntities1 ContextKIP = new FilmSearchEntities1(); // подключение к колледжной БД
    }
}
