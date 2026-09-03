using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Program
    {
        static void task1() 
        {
            int age = 0;
            int choice = 0;
            int day = 0;

            double total = 0;

            Console.WriteLine("Введите свой возраст: ");
            while (age == 0)
            {
                try
                {
                    age = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Введите целое число!");
                }
            }


            Console.WriteLine("\nВыберите тип билета:\n1. обычный\n2. студенченский\n3. VIP");
            while (choice == 0)
            {
                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                    if (choice < 1 || choice > 3)
                    {
                        choice = 0;
                        throw new Exception();
                    }
                }
                catch
                {
                    Console.WriteLine("Введите целое число! (в диапазоне от 1 до 3)");
                }
            }

            Console.WriteLine("\nВведите день недели:");
            while (day == 0)
            {
                try
                {
                    day = Convert.ToInt32(Console.ReadLine());
                    if (day < 1 || day > 7)
                    {
                        day = 0;
                        throw new Exception();
                    }
                }
                catch
                {
                    Console.WriteLine("Введите целое число! (в диапазоне от 1 до 7)");
                }
            }


            switch (choice)
            {
                case 1:
                    total += 500;
                    Console.WriteLine("\nТип билета: обычный");
                    break;

                case 2:
                    total += 350;
                    Console.WriteLine("\nТип билета: студенченский");
                    break;

                case 3:
                    total += 1000;
                    Console.WriteLine("\nТип билета: VIP");
                    break;
            }


            if (age <= 18)
            {
                total *= 0.8;
                Console.WriteLine("Возрастная скидка: есть");
            }
            else if (age >= 60)
            {
                total *= 0.7;
                Console.WriteLine("Возрастная скидка: есть");
            }
            else
            {
                Console.WriteLine("Возрастная скидка: нет");
            }


            total *= (day == 6 || day == 7) ? 1.1 : 1;
            if (day == 6 || day == 7)
            {
                Console.WriteLine("Выходной день: да");
            }
            else
            {
                Console.WriteLine("Выходной день: нет");
            }

            Console.WriteLine($"Итоговая стоимость: {Math.Round(total, 2)} р.");
        }
        static void task2()
        {
            int[] massive = {1, 5, 8, 19 ,23 };
            int sum = 0;

            Console.WriteLine("Вывод:");
            foreach (var elem in massive) { Console.Write($"{elem} "); sum += elem; }
            Console.WriteLine($"\nСумма: {sum}");

            Console.WriteLine("\nВведите целое число");
            massive[2] = Convert.ToInt32(Console.ReadLine());
            foreach (var elem in massive) { Console.Write($"{elem} "); }

            Console.WriteLine($"\nМаксимальный элемент: {massive.Max()}\nМинимальный элемент: {massive.Min()}");

            Console.WriteLine("\nПоменяем местиами первый и последний элементы:");
            (massive[0], massive[4]) = (massive[4], massive[0]);
            foreach (var elem in massive) { Console.Write($"{elem} "); }
            Console.WriteLine("\n");


            Random r = new Random();
            int[,] Massive = new int[3, 4];

            Console.WriteLine("Двумерный массив:");
            for (int i = 0; i < 3; i++) 
            {
                for (int j = 0; j < 4; j++) 
                {
                    Massive[i, j] = r.Next();
                    Console.Write($"{Massive[i, j]} ");
                }
                Console.WriteLine();
            }


            Console.WriteLine("\nОтсортированный одномерный массив: ");
            Array.Sort(massive);
            foreach (var elem in massive) { Console.Write($"{elem} "); }
            Console.WriteLine("\n");


            Console.WriteLine("Сумма каждой строки двумернорго массива:");
            for (int i = 0; i < 3; i++)
            {
                sum = 0;
                for (int j = 0; j < 4; j++)
                {
                    sum += Massive[i, j];
                }
                Console.WriteLine(sum);
            }

            Console.WriteLine("\nТранспонированный массив:");
            int[,] transposed = new int[4, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    transposed[j, i] = Massive[i, j];
                }
            }

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write($"{transposed[i, j]} ");
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            //task1();
            task2();
        }
    }
}
