using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP123_Solodov_WPF
{
    internal static class UserClass
    {
        private static string _login;
        private static string _password;
        private static string _fio;
        private static DateTime _birthday;

        public static string Login
        {
            get => _login;
            set => _login = value;
        }

        public static string Password
        {
            get => _password;
            set => _password = value;
        }

        public static string FIO
        {
            get => _fio;
            set => _fio = value;
        }

        public static DateTime Birthday
        {
            get => _birthday;
            set => _birthday = value;
        }

        public static bool IsLogged { get; set; }

        public static bool IsNextPageIsProfile { get; set; }
    }
}
