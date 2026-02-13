using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP123_Solodov_WPF
{
    internal static class UserClass
    {
        private static string Login;
        private static string Password;
        private static string FIO;
        private static DateTime Birthday;

        public static void SetLogin(string input) 
        {
            Login = input;
        }
        public static void SetPassword(string input)
        {
            Password = input;
        }
        public static void SetFIO(string input)
        {
            FIO = input;
        }
        public static void SetBirthday(DateTime input)
        {
            Birthday = input;
        }
    }
}
