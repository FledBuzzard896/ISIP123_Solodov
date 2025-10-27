using System.Diagnostics;

//for (int i = 0; i < 10; i++)
//{
//    //string filePath = "путь к файлу";
//    //Process.Start("notepad",filePath);

//    Process.Start("notepad");
//}

//while (true) 
//{
//    Process.Start("notepad");
//}

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

class Program
{
    // Импорт функций из user32.dll для работы с окнами
    [DllImport("user32.dll")]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    static extern IntPtr FindWindow(string lpClassName, string lpWindowName); // Находит окно по имени класса и/или заголовку.

    [DllImport("user32.dll")]
    static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect); // Получает прямоугольник, описывающий позицию и размеры окна.

    // Константы для SetWindowPos
    private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
    private const uint SWP_NOSIZE = 0x0001;
    private const uint SWP_NOZORDER = 0x0004;

    // Структура для хранения координат окна
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left, Top, Right, Bottom;
    }

    static void Main()
    {
       
        // Получаем размеры экрана
        int screenWidth = GetSystemMetrics(0);
        int screenHeight = GetSystemMetrics(1);

        int x = 0;
        int y = 0;

        for (int i = 0; i < 20; i++) 
        {
            // Запуск блокнота
            Process notepad = Process.Start("notepad.exe");

            // Даем окну время инициализироваться
            Thread.Sleep(50);

            // Поиск окна по классу (класс окна Блокнота)
            IntPtr notepadHandle = FindWindow("Notepad", null);

            // Получаем размеры окна
            GetWindowRect(notepadHandle, out RECT windowRect);
            int windowWidth = windowRect.Right - windowRect.Left;
            int windowHeight = windowRect.Bottom - windowRect.Top;

            // Вычисляем координаты правого верхнего угла
            x = screenWidth - windowWidth - i * 10;
            y += 20;

            // Устанавливаем позицию окна
            SetWindowPos(
                notepadHandle,
                IntPtr.Zero,
                x,
                y,
                0,
                0,
                SWP_NOSIZE | SWP_NOZORDER
            );
        }

        Console.Beep();
        Console.WriteLine("Блокнот перемещен в правый верхний угол.");
        Console.ReadLine();
    }

    [DllImport("user32.dll")]
    static extern int GetSystemMetrics(int nIndex);
}
