void mainMenu(int id, List<Product> PRODUCTS, Stack<Product> PRODUCTS_History, List<Product> sellReportLst)
{

    Console.WriteLine("-----------------------------------------\n\t1. Добавить товар\n\t2. Удалить товар\n\t3. Заказать поставку товара\n\t4. Продать товар\n\t5. Поиск товара\n\t6. (DLC) AdminPanel\n\t7. (DLC) Сформировать отчёт о продажах\n\t0. Выход\n-----------------------------------------");
    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":

            PRODUCTS.Add(AddProduct(id));
            id++;
            mainMenu(id, PRODUCTS, PRODUCTS_History, sellReportLst);
            break;

        case "2":
            DeleteProduct(PRODUCTS, PRODUCTS_History);
            mainMenu(id, PRODUCTS, PRODUCTS_History, sellReportLst);
            break;

        case "3":
            DeliveryFromStorage(PRODUCTS, PRODUCTS_History);
            mainMenu(id, PRODUCTS, PRODUCTS_History, sellReportLst);
            break;

        case "4":
            SellProduct(id, PRODUCTS, PRODUCTS_History, sellReportLst);
            break;

        case "5":
            SearchProduct(PRODUCTS);
            mainMenu(id, PRODUCTS, PRODUCTS_History, sellReportLst);
            break;

        case "6":
            AdminPanel(id,PRODUCTS, PRODUCTS_History, sellReportLst);
            break;

        case "7":
            MakeReport(sellReportLst);
            mainMenu(id, PRODUCTS, PRODUCTS_History, sellReportLst);
            break;

        case "0":
            break;
        default: mainMenu(id, PRODUCTS, PRODUCTS_History, sellReportLst); break;
    }
}
void printInfo(Product tovar)
{
    Console.WriteLine($"-----------------------------------------\nID: {tovar.productID}\nНазвание: {tovar.productName}\nЦена: {tovar.productPrice} руб.\nКол-во: {tovar.productCount}\nЕсть на складе? {tovar.isProductInStock}\nКатегория: {tovar.productCategory}\n-----------------------------------------");
}
(double Price, int Count) checkNumericValue(object input)
{
    char[] numsLst = {'1','2', '3', '4', '5', '6', '7', '8', '9', '0', ',' };
    string strInput = (string)input;

    if (strInput != "")
    {
        for (int i = 0; i < strInput.Length; i++)
        {
            if (numsLst.Contains(strInput[i]) == false)
            {
                Console.WriteLine("Error: Введите корректные данные!");
                return checkNumericValue(Console.ReadLine());
            }
        }
        double inputDBL = Convert.ToDouble(input);
        if (inputDBL < 0)
        {
            inputDBL *= -1;
        }
        return (inputDBL, Convert.ToInt32(inputDBL));
    }
    else {
        Console.WriteLine("Error: Введите корректные данные!");
        return checkNumericValue(Console.ReadLine());
    }
}
string checkStringValue(string input)
{
    if (string.IsNullOrEmpty(input) || input[0] == ' ')
    {
        input = "Продукт без названия";
    }
    return input;
}
Product AddProduct(int id)
{

    Console.Write($"Введите название продукта: ");
    string name = checkStringValue(Console.ReadLine());

    Console.Write($"Введите цену продукта (если цена не целая, вводите через запятую): ");
    double price = checkNumericValue(Console.ReadLine()).Price;

    Console.Write($"Введите количество продукта: ");
    int count = checkNumericValue(Console.ReadLine()).Count;

    Console.Write("Есть ли товар на складе? (Да / Нет): ");
    string ans = Console.ReadLine();

    bool isInStock;
    int countInStorage = 0;

    if (ans.Length == 2)
    {
        isInStock = true;
        Console.Write("Введите кол-во товара на складе: ");
        countInStorage = checkNumericValue(Console.ReadLine()).Count;
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
    Console.WriteLine("Операция выполнена успешно!");

    return product;
}
void DeleteProduct(List<Product> PRODUCTS, Stack<Product> PRODUCTS_History)
{
    foreach (Product tovar in PRODUCTS)
    {
        printInfo(tovar);
    }

    Console.Write("Введите ID продукта который хотите удалить: ");
    int idx = checkNumericValue(Console.ReadLine()).Count;
    string message = "Error: Продукта с таким ID не существует!";

    foreach (Product p in PRODUCTS)
    {
        if (idx == p.productID)
        {
            var copy = new Product(p.productID, p.productName, p.productPrice, p.productCount, p.isProductInStock, p.productCategory, p.countInStorage, p.countOfSellProduct);
            PRODUCTS_History.Push(copy);

            PRODUCTS.Remove(p);

            message = "Операция выполнена успешно!";
            break;
        }
    }
    Console.WriteLine(message);
}
void DeliveryFromStorage(List<Product> PRODUCTS, Stack<Product> PRODUCTS_History)
{

    foreach (Product tovar in PRODUCTS)
    {
        printInfo(tovar);
    }

    Console.Write("Введите название продукта, который хотите заказать: ");
    string name = checkStringValue(Console.ReadLine()).ToLower();
    string message = "Error: Товара с таким названием нет на складе.";

    foreach (Product tovar in PRODUCTS)
    {
        string lowerName = tovar.productName.ToLower();
        if (lowerName.IndexOf(name) >= 0)
        {
            Console.WriteLine($"-----------------------------------------\n{tovar.productName}: {tovar.countInStorage} шт.\n-----------------------------------------");
            Console.Write("Сколько вы хотите заказать со склада: ");
            int count = checkNumericValue(Console.ReadLine()).Count;

            if (count <= tovar.countInStorage)
            {
                var copy = new Product(tovar.productID, tovar.productName, tovar.productPrice, tovar.productCount, tovar.isProductInStock, tovar.productCategory, tovar.countInStorage, tovar.countOfSellProduct);
                PRODUCTS_History.Push(copy);

                tovar.countInStorage -= count;
                tovar.productCount += count;
                message = "Операция выполнена успешно!";
                break;
            }
            else { message = "Error: Товар либо закончился, либо его нет в таком количестве."; break; }
        }
    }
    Console.WriteLine(message);
}
void SellProduct(int id,List<Product> PRODUCTS, Stack<Product> PRODUCTS_History, List<Product> sellReportLst)
{
    foreach (Product tovar in PRODUCTS)
    {
        printInfo(tovar);
    }

    Console.Write("Введите название товара: ");
    string name = checkStringValue(Console.ReadLine()).ToLower();
    string message = "Error: Такого товара нет в магазине!";
    foreach (Product t in PRODUCTS)
    {
        string lowerName = t.productName.ToLower();
        if (lowerName.IndexOf(name) >= 0)
        {
            Console.WriteLine($"В данный момент в магазине {t.productName} в кол-ве {t.productCount} шт.");
            Console.Write("Введите кол-во товара, которое хотите продать: ");
            int count = checkNumericValue(Console.ReadLine()).Count;

            if (t.productCount >= count) {

                var copy = new Product(t.productID,t.productName,t.productPrice,t.productCount,t.isProductInStock,t.productCategory, t.countInStorage, t.countOfSellProduct);
                PRODUCTS_History.Push(copy);

                t.productCount -= count;
                t.countOfSellProduct += count;
                sellReportLst.Add(t);

                message = "Операция выполнена успешно!";
            }
            else { message = "Error: В наличии нет столько товара!"; }
        }
    }
    Console.WriteLine(message);
    mainMenu(id, PRODUCTS, PRODUCTS_History, sellReportLst);
}
void SearchProduct(List<Product> PRODUCTS)
{

    Console.Write("Введите ID, название или категорию продукта: ");
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
            Console.WriteLine("Продукта с таким ID не существует.");
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
            Console.WriteLine("Продукта c таким названием/категорией не существует.");
        }
    }
}
void AdminPanel(int id, List<Product> PRODUCTS, Stack<Product> PRODUCTS_History, List<Product> sellReportLst) {

    Console.WriteLine("---------------------------ЭЛЕМЕНТЫ В СТЕКЕ---------------------------");
    foreach (Product p in PRODUCTS_History) {
        printInfo(p);
    }
    string ans = null;
    bool tempMark = false;
    Console.WriteLine("Отменить последнюю продажу? (YES/NO)");
    ans = Console.ReadLine();

    while (tempMark == false) {

        if (ans == "YES")
        {
            Product elem = PRODUCTS_History.Pop();
            bool mark = false;

            foreach (Product check in PRODUCTS) {
                if (check.productID == elem.productID) {

                    PRODUCTS.Remove(check);
                    PRODUCTS.Insert(elem.productID - 1, elem);

                    mark = true;
                    break;
                }
            }
            if (mark == false) {
                PRODUCTS.Insert(elem.productID - 1, elem);
            }

            if (sellReportLst.Count != 0)
            {
                sellReportLst.RemoveAt(sellReportLst.Count - 1);
            }
            Console.WriteLine("Операция выполнена!");
            tempMark = true;
        }
        else if (ans == "NO")
        {
            Console.WriteLine("Операция прервана!");
            tempMark = true;
        }
        else {
            Console.WriteLine("Введите: YES или NO");
            ans = Console.ReadLine();
        }  
    }
    mainMenu(id, PRODUCTS, PRODUCTS_History, sellReportLst);
}
void MakeReport(List<Product> sellReportLst) {

    double sum = 0;
    foreach (Product sellingProduct in sellReportLst) {
        Console.WriteLine($"-----------------------------------------\nID: {sellingProduct.productID}\nНазвание: {sellingProduct.productName}\nЦена: {sellingProduct.productPrice} руб.\nКол-во проданных единиц: {sellingProduct.countOfSellProduct}\n-----------------------------------------");
        sum += sellingProduct.countOfSellProduct * sellingProduct.productPrice;
    }
    Console.WriteLine($"Общая сумма продажи: {sum} руб.");
}

int id = 4;
List<Product> PRODUCTS = new List<Product>();
Stack<Product> PRODUCTS_History = new Stack<Product>();
List<Product> sellReportLst = new List<Product>();

Product p1 = new Product(1, "Помидор", 60, 100, true, "Полезная пища", 1000);
PRODUCTS.Add(p1);
Product p2 = new Product(2, "Чипсы", 120, 60, false, "Вредная пища");
PRODUCTS.Add(p2);
Product p3 = new Product(3, "Кактус", 500, 10, false, "Другое");
PRODUCTS.Add(p3);

mainMenu(id, PRODUCTS, PRODUCTS_History, sellReportLst);

class Product
{
    public int productID;
    public string productName;
    public double productPrice;
    public int productCount;
    public bool isProductInStock;
    public string productCategory;
    public int countInStorage;
    public int countOfSellProduct;

    public Product(int productID = 4, string productName = "Название отсутсвует", double productPrice = 0, int productCount = 0, bool isProductInStock = false, string productCategory = "нет", int countInStorage = 0, int countOfSellProduct = 0)
    {
        this.productID = productID;
        this.productName = productName;
        this.productPrice = productPrice;
        this.productCount = productCount;
        this.isProductInStock = isProductInStock;
        this.productCategory = productCategory;
        this.countInStorage = countInStorage;
        this.countOfSellProduct = countOfSellProduct;
    }
}
