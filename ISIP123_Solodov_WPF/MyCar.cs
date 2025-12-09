using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ISIP123_Solodov_WPF
{
    static internal class MyCar
    {
        public static double price;
        public static string model;
        public static string engine;
        public static string color;
        public static string adds;
        public static string adds_comment;

        // Переменные для прверки параметров
        private static string _check_model = model;
        private static string _check_engine = engine;
        private static string _check_adds = adds;
        private static string _check_adds_comment = adds_comment;

        // Проверка на изменение состояния после перехода меж страницами
        public static string CheckModel() { return _check_model; }
        public static string CheckEngine() { return _check_engine; }
        public static string CheckAdds() { return _check_adds; }
        public static string CheckAddsComment() { return _check_adds_comment; }

        // Базовое значение цвета, получение и изменение
        private static SolidColorBrush color_foreground = Brushes.Gray;
        public static SolidColorBrush GetColorForeground() { return color_foreground; }
        public static void SetColorFackground(SolidColorBrush input) { color_foreground = input; }

        public static string PrintInfo() 
        {
            return $"Модель: {model}\nТип двигателя: {engine}\nЦвет: {color}\nДополнительные опции:{adds}\nВаш комментарий: {adds_comment}";
        }
    }
}
