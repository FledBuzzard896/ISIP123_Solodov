using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ISIP123_Solodov_Databases
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int USER_ID = 0;
            Console.ForegroundColor = ConsoleColor.Gray;

            List<likeNaGeev> storage = Core.ContextKIP.likeNaGeev.ToList();
            List<Client> users = Core.ContextKIP.Client.ToList();
            List<ShoppingCart> shoppingCart = Core.ContextKIP.ShoppingCart.ToList();

            string choice = "";

            while (choice != "0") 
            {
                Console.WriteLine("=============== MENU ===============\n1. Ассортимент\n2. Зарегистрироваться/Войти\n3. Корзина\n4. Пополнить баланс\n0. Выход\n====================================");
                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CheckAssortiment(storage, USER_ID, shoppingCart);
                        storage = Core.ContextKIP.likeNaGeev.ToList();
                        shoppingCart = Core.ContextKIP.ShoppingCart.ToList();
                        break;

                    case "2":
                        USER_ID = RegOrLog(users);

                        //Console.WriteLine($"АЙЛИ {USER_ID}");
                        break;

                    case "3":
                        if (USER_ID != 0)
                        {
                            CheckShoppingCart(USER_ID, shoppingCart, storage, users);
                            storage = Core.ContextKIP.likeNaGeev.ToList();
                            shoppingCart = Core.ContextKIP.ShoppingCart.ToList();
                            users = Core.ContextKIP.Client.ToList();
                        }
                        else 
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Сначала войдите в аккаунт");
                            Console.ResetColor();
                        }
                        break;

                    case "4":
                        AddBalance(USER_ID, users);
                        users = Core.ContextKIP.Client.ToList();
                        break;

                    case "0":
                        break;
                }
            }
            
        }

        static public void CheckAssortiment(List<likeNaGeev> storage, int USER_ID, List<ShoppingCart> shoppingCart) 
        {
            List<int> tempID = new List<int>(); // для хранения доступных ID товаров
            List<int> tempCount = new List<int>();

            foreach (var item in storage) 
            {
                Console.WriteLine("====================================");
                Console.WriteLine($"ID: {item.ID}\nНазвание: {item.ClothName}\nЦена: {item.ClothPrice} руб.\nКол-во: {item.ClothCount}\nТип одежды: {item.ClothType}\nРазмер: {item.ClothSize}\nПол: {item.ClothSex}");

                tempID.Add(item.ID);
                tempCount.Add((int)item.ClothCount);
            }
            Console.WriteLine("====================================");

            Console.Write("\nВведите ID товара, который хотите купить: ");
            int choice = Convert.ToInt32(Console.ReadLine());

            if (USER_ID != 0)
            {
                if (tempID.Contains(choice))
                {
                    int maxCount = tempCount[tempID.FindIndex(x => x == choice)];
                    Console.WriteLine("Сколько товара добавить в корзину? ");
                    Console.Write(">>> ");
                    int count = Convert.ToInt32(Console.ReadLine());

                    if (count <= maxCount && count > 0)
                    {
                        ShoppingCart newElem = new ShoppingCart
                        {
                            id_Сlient = USER_ID,
                            id_Cloth = choice,
                            Quantity = count,
                        };

                        bool isFalse = true;
                        foreach (var elem in shoppingCart) 
                        {
                            // изменение количества товара в корзине
                            if (elem.id_Сlient == USER_ID) 
                            {
                                
                                var change = Core.ContextKIP.ShoppingCart.Where(x => x.id_Сlient == USER_ID).ToList(); 
                                ShoppingCart s = change.First(x => x.id_Cloth == newElem.id_Cloth);
                                s.Quantity += count;

                                Core.ContextKIP.SaveChanges();

                                isFalse = false;
                                break;
                            }
                        }

                        // добавление товара в корзину если изначально его в ней не было
                        if (isFalse)
                        {
                            Core.ContextKIP.ShoppingCart.Add(newElem);
                        }

                        likeNaGeev editCloth = Core.ContextKIP.likeNaGeev.First(x => x.ID == choice); // снижение кол-ва товара из асортимента
                        editCloth.ClothCount -= count;

                        Core.ContextKIP.SaveChanges();

                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("Товар добавлен в корзину!");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Ошибка!");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"Товар с ID = {choice} не найден!");
                    Console.ResetColor();
                }
            }
            else 
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Невозможно выбрать товар, вы не вошли в аккаунт");
                Console.ResetColor();
            }
        }
        static public int RegOrLog(List<Client> users)
        {
            int USER_ID;
            Console.Write("Введите login: ");
            string login = Console.ReadLine();
            Console.Write("Введите пароль: ");
            string password = Console.ReadLine();

            foreach (var user in users)
            {
                if (login == user.Login)
                {
                    while (true)
                    {
                        if (password == user.Password)
                        {
                            USER_ID = user.ID;

                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine("Вы успешно вошли в аккаунт!");
                            Console.ResetColor();

                            return USER_ID;
                        }

                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Неправильный пароль, попробуйте еще раз. Если забыли пароль --> введите 0");
                        Console.ResetColor();

                        Console.Write("Введите пароль: ");
                        password = Console.ReadLine();

                        if (password == "0")
                        {
                            return 0;
                        }
                    }
                }
            }
            Console.WriteLine("Вы еще не зарегистрированы, давайте сделаем это!");

            Console.Write("Введите login: ");
            login = Console.ReadLine();
            Console.Write("Введите пароль: ");
            password = Console.ReadLine();

            string copy_password = "";

            while (copy_password != password) 
            {
                Console.Write("Повторите пароль: ");
                copy_password = Console.ReadLine();

                if (password != copy_password) 
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"Пароль введён не правильно!");
                    Console.ResetColor();
                }
            }

            Console.Write("Введите ФИО: ");
            string FIO = Console.ReadLine();
            Console.Write("Введите сумму денег, которую хотите потратить у нас в приложении: ");
            double balance = Convert.ToDouble(Console.ReadLine());

            Console.ForegroundColor= ConsoleColor.DarkGreen;
            Console.WriteLine("Вы успешно зарегистрировались!");
            Console.ResetColor();

            Client newClient = new Client 
            {
                FIO = FIO,
                Balance = balance,
                LastTransaction = DateTime.Now,
                Login = login,
                Password = password,
            };

            Core.ContextKIP.Client.Add(newClient);

            Core.ContextKIP.SaveChanges();

            USER_ID = Core.ContextKIP.Client.First(x => x.Login == login).ID;

            return USER_ID;
        }
        static public void CheckShoppingCart(int USER_ID, List<ShoppingCart> cart, List<likeNaGeev> storage, List<Client> users) 
        {
            double totalSum = 0;
            bool printSomething = false;

            foreach (var line in cart) 
            {
                if (line.id_Сlient == USER_ID) //  && line.Quantity != null 
                {
                    printSomething = true;

                    
                    Console.WriteLine("===== Корзиночка с покупочками =====");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"ID корзины: {line.ID}");
                    Console.ResetColor();
                    Console.WriteLine("------------------------------------");
                    break;
                } 
            }


            if (printSomething)
            {
                foreach (var item in cart)
                {
                    if (item.id_Сlient == USER_ID)
                    {
                        likeNaGeev Product = Core.ContextKIP.likeNaGeev.First(x => x.ID == item.id_Cloth);

                        Console.WriteLine($"ID товара: {item.id_Cloth}\nНазвание: {Product.ClothName}\nРазмер: {Product.ClothSize}\nКолво: {item.Quantity}\nЦена: {item.Quantity * Product.ClothPrice}\n");
                        totalSum += (double)item.Quantity * Product.ClothPrice;
                    }
                }

                Console.WriteLine($"Общая сумма ВБ корзинки: {totalSum} руб.\n");

                string choice = "";

                while (choice != "1" && choice != "0" && choice != "2")
                {
                    Console.WriteLine("Выберите действие:");
                    Console.WriteLine($"1. Оплатить\n2. Удалить товар из корзины\n0. Выход");
                    choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            Console.WriteLine("Выберите ПВЗ:");
                            Console.WriteLine("1. Онлайн доставка\n2. Приморский Край, Владивосток, улица Успенского, 69");

                            string pvz = Console.ReadLine();

                            switch (pvz)
                            {
                                case "1":
                                    Console.Write("Введите свой адресс: ");
                                    string address = Console.ReadLine();
                                    break;
                                case "2":
                                    break;
                            }

                            Client client = Core.ContextKIP.Client.First(x => x.ID == USER_ID);
                            if (client.Balance >= totalSum)
                            {
                                client.Balance -= totalSum;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("Ошибка транзакции!");
                                Console.ResetColor();
                            }


                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine("Спасиьбьо за заказ! Вам поступит уведомление когда он приедет :)");
                            Console.ResetColor();

                            foreach (var item in cart)
                            {
                                Core.ContextKIP.ShoppingCart.Remove(item);
                            }
                            Core.ContextKIP.SaveChanges();

                            break;

                        case "2":
                            Console.Write("Введите ID товара, который хотите удалить из корзины: ");
                            int deletedID = Convert.ToInt32(Console.ReadLine());

                            foreach (var item in cart)
                            {
                                if (item.id_Cloth == deletedID)
                                {
                                    Console.WriteLine("\nВыберите: \n1. Удалить полностью. \n2. Сократить количество товара в корзине.");
                                    choice = Console.ReadLine();

                                    switch (choice)
                                    {
                                        case "1":
                                            Core.ContextKIP.ShoppingCart.Remove(item);
                                            Core.ContextKIP.SaveChanges();
                                            break;

                                        case "2":
                                            Console.Write("Введите сколько позиций товара вы хотите удалить: ");
                                            int c = Convert.ToInt32(Console.ReadLine());

                                            // ИЗМЕНИ УДАЛЕНИЕ!!!!
                                            //var change = Core.ContextKIP.ShoppingCart.Where(x => x.id_Сlient == USER_ID).ToList();
                                            //ShoppingCart s = change.First(x => x.id_Cloth == newElem.id_Cloth);
                                            //s.Quantity += count;
                                            ShoppingCart changingItem = Core.ContextKIP.ShoppingCart.First(x => x.id_Cloth == deletedID);
                                            changingItem.Quantity -= c;

                                            if (changingItem.Quantity == 0)
                                            {
                                                Core.ContextKIP.ShoppingCart.Remove(item);
                                            }

                                            // возврат позиций в ассортимент
                                            likeNaGeev changing = Core.ContextKIP.likeNaGeev.First(x => x.ID == item.id_Cloth);
                                            changing.ClothCount += c;

                                            Core.ContextKIP.SaveChanges();
                                            break;
                                    }
                                    Console.WriteLine("Корзина изменена!");
                                    break;
                                }
                            }
                            break;

                        case "0":
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Корзина пуста!");
            }
        }
        static public void AddBalance(int USER_ID, List<Client> users) 
        {
            if (USER_ID != 0)
            {
                Client ChangeClient = Core.ContextKIP.Client.First(x => x.ID == USER_ID);

                Console.WriteLine($"Текущий баланс: {ChangeClient.Balance} руб.");

                Console.Write("Введите сумму пополнения: ");
                double money = Convert.ToDouble(Console.ReadLine());

                ChangeClient.Balance += money;

                Console.WriteLine("Операция выполнена!");

                Core.ContextKIP.SaveChanges();
            }
            else 
            {
                Console.WriteLine("Войдите в аккаунт");
            }
        }
    }
}
