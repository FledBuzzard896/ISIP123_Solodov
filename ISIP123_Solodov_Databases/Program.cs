using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP123_Solodov_Databases
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            Balance balance = Core.Context.Balance.First();
            List<Storage> storage = Core.Context.Storage.ToList();
            List<Financial_operations> operations = Core.Context.Financial_operations.ToList();

            List<string> allDetails = new List<string>();
            allDetails.Add("Поршень");
            allDetails.Add("Коробка передач");
            allDetails.Add("Тормозные колодки");
            allDetails.Add("Тормозной диск");
            allDetails.Add("Амортизатор");
            allDetails.Add("Глушитель");
            allDetails.Add("Аккумулятор");
            allDetails.Add("Свеча зажигания");
            allDetails.Add("Капот");
            allDetails.Add("Дворники");
            allDetails.Add("Карданный вал");

            string input = "";
            bool next = true;
            string order = "";

            while (input != "0")
            {
                if (next)
                {
                    int randomNum = random.Next(allDetails.Count);

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    order = allDetails[randomNum];
                    Console.WriteLine($"\nПриехал новый бедолага. Поломка: {order}\n");
                    Console.ResetColor();
                }

                next = false;
                Console.WriteLine($"=============== MENU ===============\n1. Посмотреть баланс\n2. Посмотреть детали на складе\n3. Докупить детали\n4. Принять заказ\n5. Отклонить заказ");
                Console.Write(">>> ");
                input = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                switch (input)
                {
                    case "1":
                        Console.WriteLine("\t\t Баланс:");
                        Console.ResetColor();
                        PrintBalance(balance);
                        break;
                    case "2":
                        Console.WriteLine("\t Детали на складе:");
                        Console.ResetColor();
                        PrintStorage(storage);
                        break;
                    case "3":
                        Console.ResetColor();
                        BuyDetails(storage, balance, operations);
                        break;
                    case "4":
                        AcceptOrder(order, storage);
                        Console.ResetColor();
                        next = true;
                        break;
                    case "5":
                        DeclineOrder();
                        Console.ResetColor();
                        next = true;
                        break;
                    case "0":
                        break;
                }
            }
        }

        public static void PrintBalance(Balance balance)
        {
            Console.WriteLine($"=========================================\nБаланс: {balance.TotalBalance} руб.\nПоследняя транзакция: {balance.LastUpdated}\n=========================================");
            Console.WriteLine();
        }
        public static void PrintStorage(List<Storage> storage)
        {
            foreach (var item in storage)
            {
                Console.WriteLine($"====================================\nID: {item.ID}\nНазвание: {item.DetailName}\nКоличество: {item.Quantity}\nЦена детали: {item.DetailPrice}\n====================================");
                Console.WriteLine();
            }
        }
        public static void BuyDetails(List<Storage> storage, Balance balance, List<Financial_operations> operations)
        {
            PrintStorage(storage);

            Console.Write("Введите ID детали, которую хотите купить: ");
            int id = Convert.ToInt32(Console.ReadLine());
            string message = "Деталь не найдена!";

            foreach (var item in storage)
            {
                if (item.ID == id)
                {
                    Console.Write("Сколько деталей вы хотите докупить (кол-во): ");
                    int count = Convert.ToInt32(Console.ReadLine());

                    double cost = Convert.ToDouble(item.DetailPrice * count);
                    Console.WriteLine($"Это будет стоить: {cost}");
                    Console.WriteLine("Оформить заказ? (Да/Нет)");
                    string answer = Console.ReadLine();

                    switch (answer)
                    {
                        case "Да":
                            Storage editStorageDetail = Core.Context.Storage.First(x => x.ID == id);
                            editStorageDetail.Quantity += count;

                            Balance editBalance = Core.Context.Balance.First();
                            editBalance.TotalBalance -= cost;
                            editBalance.LastUpdated = DateTime.Now;

                            Financial_operations operation = new Financial_operations
                            {
                                OperationType = true,
                                Description = $"Закупка \"{item.DetailName}\" в размере {count} шт.",
                                Amount = count,
                                OperationDate = DateTime.Now,
                                DetailID = id
                            };
                            Core.Context.Financial_operations.Add(operation);

                            Core.Context.SaveChanges();

                            message = "Заказ деталей оформлен!";

                            break;

                        case "Нет":

                            message = "Заказ отменён!";

                            break;
                    }
                }
            }
            Console.WriteLine(message);
        }
        public static void AcceptOrder(string detail, List<Storage> storage)
        {
            bool temp = false;
            foreach (var item in storage)
            {
                if (item.DetailName == detail)
                {
                    temp = true;
                }
            }

            if (temp)
            {
                double sum = 5000;

                Storage changedDetail = Core.Context.Storage.First(x => x.DetailName == detail);
                changedDetail.Quantity--;

                sum += Convert.ToDouble(changedDetail.DetailPrice);

                Financial_operations operation = new Financial_operations
                {
                    OperationType = false,
                    Description = $"Замена \"{changedDetail.DetailName}\"",
                    Amount = 1,
                    OperationDate = DateTime.Now,
                    DetailID = changedDetail.ID
                };
                Core.Context.Financial_operations.Add(operation);

                Balance editBalance = Core.Context.Balance.First();
                editBalance.TotalBalance += sum;
                editBalance.LastUpdated = DateTime.Now;

                Core.Context.SaveChanges();
                Console.WriteLine($"Замена произведена, полученная прибыль: {sum} руб.");
            }
            else
            {
                DeclineOrder();
            }
        }
        public static void DeclineOrder()
        {
            Balance editBalance = Core.Context.Balance.First();
            editBalance.TotalBalance -= 2500;
            editBalance.LastUpdated = DateTime.Now;
            Core.Context.SaveChanges();

            Console.WriteLine("Мы не можем произвести замену вашей детали по причине: нет подходящих запчастей.\nПриносим свои извенения за доставленные неудобства! В связи с этим, выплачиваем вам 2500.00 руб.");

        }
    }
}
