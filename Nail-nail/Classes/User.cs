using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nail_nail.Classes
{

    public static class IUser
    {
        public static User AppUser { get; set; } = new User();
    }

    public class User
    {
        public  string FullName { get; set; }
        public  string PhoneNumber { get; set; }

        public  string Login { get; set; }
        public  string Password { get; set; }

        public  bool isAuthorizated { get; set; } = false; 
    }
}
