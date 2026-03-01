using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP123_Solodov_WPF
{
    internal class Sborka
    {
        //public static int _cpu;
        //public static int _gpu;
        //public static int _ram;
        //public static int _motherboard;
        //public static int _case;
        //public static int _powersupply;
        //public static int _processorcooler;
        //public static int _storagedevice;

        //public static void Clear() 
        //{
        //    _cpu = 0;
        //    _gpu = 0;
        //    _ram = 0;
        //    _motherboard = 0;
        //    _case = 0;
        //    _powersupply = 0;
        //    _storagedevice = 0;
        //    _processorcooler = 0;
        //}
        private static int[] _parts = new int[9]; // индексы от 0 до 8, но будем использовать с 1

        public static int GetPart(int partTypeId)
        {
            return _parts[partTypeId];
        }

        public static void SetPart(int partTypeId, int id)
        {
            _parts[partTypeId] = id;
        }

        public static void Clear()
        {
            for (int i = 0; i < _parts.Length; i++)
                _parts[i] = 0;
        }
    }
}
