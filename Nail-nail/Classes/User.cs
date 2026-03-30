using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nail_nail.Classes
{
    internal static class User
    {
        public static string FullName { get; set; }
        public static string PhoneNumber { get; set; }

        public static string Login { get; set; }
        public static string Password { get; set; }

        public static bool isAuthorizated { get; set; } = false; 
    }
}
