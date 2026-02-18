using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP123_Solodov_WPF
{
    internal class SessionClass
    {
        private static Afisha _movieAfisha;
        private static List<string> _usedSeats = new List<string>(); 

        public static Afisha MovieAfisha
        {
            get => _movieAfisha;
            set => _movieAfisha = value;
        }

        public static List<string> UsedSeats
        {
            get => _usedSeats;
            set => _usedSeats = value ?? new List<string>(); 
        }

        public static void ClearAfisha()
        {
            _movieAfisha = null;
        }

        public static void ClearSeats() 
        {
            _usedSeats.Clear();
        }
    }
}
