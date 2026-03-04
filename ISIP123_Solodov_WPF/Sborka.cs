using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP123_Solodov_WPF
{
    internal class Sborka
    {
        //1 - Процессоры
        //2 - Видеокарты
        //3 - Оперативная память
        //4 - Материнская плата
        //5 - Корпус
        //6 - Блок питания
        //7 - Кулер от процессора
        //8 - HDD/SSD

        private static int[] _parts = new int[9]; // индексы от 0 до 8, но будем использовать с 1

        public static int[] GetPartsArray() { return _parts; }

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

        public static bool IsAnyPart()
        {
            bool ans = false;

            for (int i = 1; i < 9; i++) 
            {
                if (_parts[i] > 0) 
                {
                    ans = true;
                    break;
                }
            }

            return ans;
        }
    }
}
