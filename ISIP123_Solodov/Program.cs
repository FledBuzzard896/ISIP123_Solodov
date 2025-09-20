void mainMenu(int id, List<Product> PRODUCTS)
{

    Console.WriteLine("1.Добавить товар\n2.Удалить тоывар\n3.Заказать поставку товара\n4.Продать товар\n5.Поиск товара\n0.Выход");
    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            AddProduct(id, PRODUCTS);
            break;
        case "2":
            DeleteProduct(id, PRODUCTS);
            break;
        case "3":
            DeliveryFromStorage(id, PRODUCTS);
            break;
        case "4":
            SellProduct(id, PRODUCTS);
            break;
        case "5":
            SearchProduct(id, PRODUCTS);
            break;
        case "0":
            break;
        default: mainMenu(id, PRODUCTS); break;
    }
}
void printInfo(Product tovar)
{
    Console.WriteLine($"---------------------------------\nID: {tovar.productID}\nНазвание: {tovar.productName}\nЦена: {tovar.productPrice}\nКол-во: {tovar.productCount}\nЕсть на складе? {tovar.isProductInStock}\nКатегория: {tovar.productCategory}\n---------------------------------");
}
(double Price, int Count) checkNumericValue(double input)
{
    if (input < 0)
    {
        input *= -1;
    }
    return (input, Convert.ToInt32(input));
}
string checkStringValue(string input)
{
    if (string.IsNullOrEmpty(input) || input[0] == ' ')
    {
        input = "Продукт без названия";
    }
    return input;
}
void AddProduct(int id, List<Product> PRODUCTS)
{

    Console.WriteLine($"Введите название продукта");
    string name = checkStringValue(Console.ReadLine());

    Console.WriteLine($"Введите цену продукта");
    double price = checkNumericValue(Convert.ToDouble(Console.ReadLine())).Price;

    Console.WriteLine($"Введите количество продукта");
    int count = checkNumericValue(Convert.ToInt32(Console.ReadLine())).Count;

    Console.WriteLine("Есть ли товар на складе? (Да / Нет)");
    string ans = Console.ReadLine();

    bool isInStock;
    int countInStorage = 0;

    if (ans.Length == 2)
    {
        isInStock = true;
        Console.WriteLine("Сколько товара на складе?");
        countInStorage = checkNumericValue(Convert.ToInt32(Console.ReadLine())).Count;
    }
    else
    {
        isInStock = false;
    }

    Console.WriteLine($"Введите категорию продукта из перечисленных:\n1.Полезная пища\n2.Вредная пища\n3.Другое");
    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            choice = "Полезная пища";
            break;
        case "2":
            choice = "Вредная пища";
            break;
        case "3":
            choice = "Другое";
            break;
        default: choice = "Другое"; break;
    }
    Product product = new Product(id, name, price, count, isInStock, choice, countInStorage);
    id++;
    PRODUCTS.Add(product);
    Console.WriteLine("Операция выполнена успешно!\n");
    mainMenu(id, PRODUCTS);
}
void DeleteProduct(int id, List<Product> PRODUCTS)
{

    foreach (Product tovar in PRODUCTS)
    {
        printInfo(tovar);
    }

    Console.WriteLine("Введите ID продукта который хотите удалить");
    int idx = Convert.ToInt32(Console.ReadLine());
    string message = "Error! Продукта с таким ID не существует!";

    foreach (Product p in PRODUCTS)
    {
        if (idx == p.productID)
        {
            PRODUCTS.Remove(p);
            message = "Операция выполнена успешно!\n";
            break;
        }
    }
    Console.WriteLine(message);
    mainMenu(id, PRODUCTS);
}
void DeliveryFromStorage(int id, List<Product> PRODUCTS)
{

    foreach (Product tovar in PRODUCTS)
    {
        printInfo(tovar);
    }

    Console.WriteLine("Какой продукт вы хотите заказать?");
    string name = checkStringValue(Console.ReadLine());
    string message = "Error! Товара с таким названием нет на складе.\n";

    foreach (Product tovar in PRODUCTS)
    {
        if (tovar.productName.IndexOf(name) >= 0)
        {
            Console.WriteLine($"-----------------------------\n{tovar.productName}: {tovar.countInStorage} шт.\n-----------------------------");
            Console.Write("Сколько вы хотите заказать со склада ");
            int count = checkNumericValue(Convert.ToInt32(Console.ReadLine())).Count;

            if (count <= tovar.countInStorage)
            {
                tovar.countInStorage -= count;
                tovar.productCount += count;
                message = "Операция выполнена успешно!\n";
                break;
            }
            else { message = "Error! Товар либо закончился, либо его нет в таком количестве.\n"; break; }
        }
    }
    Console.WriteLine(message);
    mainMenu(id, PRODUCTS);
}
void SellProduct(int id, List<Product> PRODUCTS)
{

    foreach (Product tovar in PRODUCTS)
    {
        printInfo(tovar);
    }

    Console.WriteLine("Какой товар вы продали?");
    string name = checkStringValue(Console.ReadLine());

    foreach (Product t in PRODUCTS)
    {

        if (t.productName.IndexOf(name) >= 0)
        {
            Console.WriteLine($"В данный момент в магазине {t.productName} в кол-ве {t.productCount} шт.");
            Console.Write("Какое кол-во товара вы хотите продать?");
            int count = Convert.ToInt32(Console.ReadLine());

            if (t.productCount >= count) { t.productCount -= count; Console.WriteLine("Операция выполнена успешно!\n"); }
            else { Console.WriteLine("Error! В наличии нет столько товара"); }
        }
        else { Console.WriteLine("Error! Такого товара нет в магазине"); }
    }
    mainMenu(id, PRODUCTS);
}
void SearchProduct(int id, List<Product> PRODUCTS)
{

    Console.WriteLine("Введите ID, название или категорию продукта");
    string p = Console.ReadLine();

    try
    {
        bool mark = false;
        int INTp = Convert.ToInt32(p);

        foreach (Product t in PRODUCTS)
        {
            if (t.productID == INTp)
            {
                printInfo(t);
                mark = true;

            }
        }
        if (mark == false)
        {
            Console.WriteLine("Продукта с таким ID не существует.\n");
        }
    }
    catch
    {
        bool mark = false;
        foreach (Product t in PRODUCTS)
        {
            if (t.productName.StartsWith(p) || (t.productCategory.StartsWith(p)))
            {
                printInfo(t);
                mark = true;
            }
        }
        if (mark == false)
        {
            Console.WriteLine("Продукта c таким названием/категорией не существует.\n");
        }
    }
    finally { mainMenu(id, PRODUCTS); }
}

int id = 4;
List<Product> PRODUCTS = new List<Product>();
Product p1 = new Product(1, "Помидор", 60, 100, true, "Полезная пища", 1000);
PRODUCTS.Add(p1);
Product p2 = new Product(2, "Чипсы", 120, 60, false, "Вредная пища");
PRODUCTS.Add(p2);
Product p3 = new Product(3, "Кактус", 500, 10, false, "Другое");
PRODUCTS.Add(p3);
mainMenu(id, PRODUCTS);
class Product
{
    public int productID;
    public string productName;
    public double productPrice;
    public int productCount;
    public bool isProductInStock;
    public string productCategory;
    public int countInStorage;

    public Product(int productID = 4, string productName = "Название отсутсвует", double productPrice = 0, int productCount = 0, bool isProductInStock = false, string productCategory = "нет", int countInStorage = 0)
    {
        this.productID = productID;
        this.productName = productName;
        this.productPrice = productPrice;
        this.productCount = productCount;
        this.isProductInStock = isProductInStock;
        this.productCategory = productCategory;
        this.countInStorage = countInStorage;
    }
}
