using System;
using System.CodeDom.Compiler;
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
            int USER_ID = 0;

            List<likeNaGeev> storage = Core.Context.likeNaGeev.ToList();
            List<Client> users = Core.Context.Client.ToList();
            List<ShoppingCart> shoppingCart = Core.Context.ShoppingCart.ToList();

            Console.WriteLine("=============== MENU ===============\n1. Ассортимент\n2. Зарегистрироваться/Войти\n3. Корзина\n====================================");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CheckAssortiment(storage, USER_ID);
                    break;
                case "2":
                    RegOrLog(users);
                    break;
                case "3":
                    if (USER_ID != 0)
                    {

                    }
                    else 
                    {
                        Console.WriteLine("Сначала зарегестрируйтесь или войдите в аккаунт");
                    }
                    break;
            }
        }

        static public void CheckAssortiment(List<likeNaGeev> storage, int USER_ID) 
        {
            List<int> tempID = new List<int>(); // для хранения доступных ID товаров
            List<int> tempCount = new List<int>();

            foreach (var item in storage) 
            {
                Console.WriteLine("====================================");
                Console.WriteLine($"ID: {item.ID}\nНазвание: {item.ClothName}\nЦена: {item.ClothPrice}\nКол-во: {item.ClothCount}\nТип одежды: {item.ClothType}\nРазмер: {item.ClothSize}\nПол: {item.ClothSex}");
                Console.WriteLine("====================================");

                tempID.Add(item.ID);
                tempCount.Add((int)item.ClothCount);
            }

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

                        Core.Context.ShoppingCart.Add(newElem);

                        likeNaGeev editCloth = Core.Context.likeNaGeev.First(x => x.ID == choice);
                        editCloth.ClothCount -= count;

                        Core.Context.SaveChanges();

                        Console.WriteLine("Товар добавлен в корзину!");
                    }
                    else
                    {
                        Console.WriteLine("Ошибка!");
                    }
                }
                else
                {
                    Console.WriteLine($"Товар с ID = {choice} не найден!");
                }
            }
            else 
            {
                Console.WriteLine("Невохможно выбрать товар, вы не вошли в аккаунт");
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
                            Console.WriteLine("Вы успешно вошли в аккаунт!");
                            return USER_ID;
                        }
                        Console.WriteLine("Неправильный пароль, попробуйте еще раз. Если забыли пароль --> введите 0");
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

            Console.Write("Введите ФИО: ");
            string FIO = Console.ReadLine();
            Console.Write("Введите сумму денег, которую хотите потратить у нас в приложении: ");
            double balance = Convert.ToDouble(Console.ReadLine());

            Client newClient = new Client 
            {
                FIO = FIO,
                Balance = balance,
                LastTransaction = DateTime.Now,
                Login = login,
                Password = password,
            };

            Core.Context.Client.Add(newClient);

            Core.Context.SaveChanges();

            USER_ID = Core.Context.Client.First(x => x.Login == login).ID;
            return USER_ID;
        }
        static public void CheckShoppingCart(int USER_ID) 
        {
            
        }
    }
}
