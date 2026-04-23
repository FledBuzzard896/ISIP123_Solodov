using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShutAndKing.Classes
{
    internal static class User
    {
        public static int ID {  get; set; }    
        public static string Login { get; set; }
        public static string Password { get; set; }
        public static string Name { get; set; }
        public static string Email { get; set; }
        public static DateTime RegisteredOn { get; set; }
        public static int RoleID { get; set; }
        public static string Status { get; set; }
    }
}
