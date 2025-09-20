void mainMenu(string[] goods, double[] price, string typeOfvalute, double[] immutabilityRubPrice)
{
    int choice;

    Console.WriteLine("--------------------------------\n1.Вывод данных\n2.Статистика\n3.Сортировка по цене\n4.Конвертация\n5.Поиск по названию\n0.Выход\n--------------------------------");
    choice = Convert.ToInt32(Console.ReadLine());
    switch (choice)
    {
        case 1:
            printData(goods, price, typeOfvalute);
            mainMenu(goods, price, typeOfvalute, immutabilityRubPrice);
            break;
        case 2:
            printStats(price);
            mainMenu(goods, price, typeOfvalute, immutabilityRubPrice);
            break;
        case 3:
            var change = sortByCash(goods, price);
            goods = change.goods;
            price = change.price;
            printData(goods, price, typeOfvalute);
            mainMenu(goods, price, typeOfvalute, immutabilityRubPrice);
            break;
        case 4:
            var result = convertingCash(price, immutabilityRubPrice);
            typeOfvalute = result.typeOfvalute;
            price = result.price;
            printData(goods, price, typeOfvalute);
            mainMenu(goods, price, typeOfvalute, immutabilityRubPrice);
            break;
        case 5:
            searching(goods, price, typeOfvalute);
            mainMenu(goods, price, typeOfvalute, immutabilityRubPrice);
            break;
        default:
            break;
    }
}

void printData(string[] goods, double[] price, string typeOfvalute)
{
    for (int i = 0; i < goods.Length; i++)
    {
        Console.WriteLine($"Товар: {goods[i]} по цене: {price[i]} {typeOfvalute}");
    }
}

void printStats(double[] price)
{
    double sum = 0, average, minNum, maxNum;

    for (int i = 0; i < price.Length; i++)
    {
        sum += price[i];
    }
    average = sum / price.Length;
    minNum = price.Min();
    maxNum = price.Max();

    Console.WriteLine($"Потраченная сумма за все операции: {sum} \nСредняя цена за товар: {average}\nМинимальная стоимость товара: {minNum}\nМаксимальная стоимость товара: {maxNum}");
}

(string[] goods, double[] price) sortByCash(string[] goods, double[] price)
{
    for (int i = 1; i < price.Length; i++)
    {
        for (int j = i; j < price.Length; j++)
        {
            if (price[j] < price[i])
            {
                double copyMin = price[j];
                price[j] = price[i];
                price[i] = copyMin;

                string copyProduct = goods[j];
                goods[j] = goods[i];
                goods[i] = copyProduct;
            }
        }
    }
    return (goods, price);
}

(string typeOfvalute, double[] price) convertingCash(double[] price, double[] immutabilityRubPrice)
{
    Console.WriteLine($"Выберете, в какую валюту хотите конвертировать стоимость продуктов:\n1. Доллар США (1 доллар = 83.75 руб.)\n2. Евро (1 евро = 97.77 руб.)\n3. Юани (1 юань = 11.76 руб.)\n4. Рубли");
    string choice = Console.ReadLine();
    string typeOfvalute;
    switch (choice)
    {
        case "1":
            for (int i = 0; i < price.Length; i++)
            {
                price[i] = immutabilityRubPrice[i] * 0.012;
            }
            typeOfvalute = "USD";
            break;
        case "2":
            for (int i = 0; i < price.Length; i++)
            {
                price[i] = immutabilityRubPrice[i] * 0.010;
            }
            typeOfvalute = "EUR";
            break;
        case "3":
            for (int i = 0; i < price.Length; i++)
            {
                price[i] = immutabilityRubPrice[i] * 0.085;
            }
            typeOfvalute = "CNY";
            break;
        default:
            for (int i = 0; i < price.Length; i++)
            {
                price[i] = immutabilityRubPrice[i];
            }
            typeOfvalute = "RUB";
            break;
    }
    return (typeOfvalute, price);
}

void searching(string[] goods, double[] price, string typeOfvalute)
{
    Console.Write("Напишите то, что хотите найти: ");
    string search = Console.ReadLine();
    int c = 0;
    for (int i = 0; i < goods.Length; i++)
    {
        if (goods[i].StartsWith(search))
        {
            Console.WriteLine($"{goods[i]} {price[i]} {typeOfvalute}");
            c++;
        }
    }
    if (c == 0)
    {
        Console.WriteLine("Ничего не найдено!");
    }
}
Console.WriteLine("Введите количетсво операций (от 2 до 40)");
int countOfOperations = Convert.ToInt32(Console.ReadLine());
double[] price = new double[countOfOperations];
string[] goods = new string[countOfOperations];
Console.WriteLine("Вводите свои покупки и их стоимость в рублях (Название;Кол-во денег)");
for (int i = 0; i < countOfOperations; i++)
{
    string[] massive = Console.ReadLine().Split(';');
    goods[i] = massive[0]; ;
    price[i] = Convert.ToDouble(massive[1]);
}

double[] immutabilityRubPrice = new double[price.Length];
Array.Copy(price, immutabilityRubPrice, price.Length);
string typeOfvalute = "RUB";
mainMenu(goods, price, typeOfvalute, immutabilityRubPrice);