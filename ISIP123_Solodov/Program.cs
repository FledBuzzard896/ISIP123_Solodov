using System.Diagnostics;
using System.Xml.Linq;

Dictionary<int, Book> lst = new Dictionary<int, Book>();
List<Book> shoppingCart = new List<Book>();

Book b1 = new Book("Мертвые души", "Н.В. Гоголь", "Роман, Сатира", 1842, 1499.99);
Book b2 = new Book("Мертвые души 2", "Н.В. Гоголь", "Роман, Сатира", 1852, 15999.99);
Book b3 = new Book("Машины сказки", "Маша Медведева", "Российский Фольклор", 2012, 100100);
Book b4 = new Book("Смешарики", "А.А. Краснова", "Хоррор", 2048, 414.13);
Book b5 = new Book("Евгений Онегин", "А.С. Пушкин", "Роман", 1234, 806);
lst.Add(1, b1);
lst.Add(2, b2);
lst.Add(3, b3);
lst.Add(4, b4);
lst.Add(5, b5);

int idx = 6;
Console.WriteLine("---------------MENU---------------\n1. Вывести все книги\n2. Добавить книгу\n3. Удалить книгу\n4. Найти книгу\n5. Отсортировать книги\n6. Вывести книгу(дорогая/дешевая)\n7. Группировка книг\n8. Вставить блок книг\n9. Добавить книгу в корзину");
Console.Write(">>> ");
string choice = Console.ReadLine();

while (choice != "0") {
    switch (choice) {

        case "1":
            PrintBooks();
            break;

        case "2":
            idx = AddBook(idx);
            break;

        case "3":
            Console.Write("Введите ID книги которую хотите удалить: ");
            DeleteBook(checkNumValue(Console.ReadLine()).Year);
            break;

        case "4":
            SearchBook();
            break;

        case "5":
            SortingBook();
            break;

        case "6":
            PrintMinMaxPriceBook();
            break;

        case "7":
            GroupByBooks();
            break;
        case "8":
            break;
        case "9":
            break;
    }
    Console.WriteLine("\n---------------MENU---------------\n1. Вывести все книги\n2. Добавить книгу\n3. Удалить книгу\n4. Найти книгу\n5. Отсортировать книги\n6. Вывести книгу(дорогая/дешевая)\n7. Группировка книг\n8. Вставить блок книг\n9. Добавить книгу в корзину");
    Console.Write(">>> ");
    choice = Console.ReadLine();
}

string checkStrValue(string input) {

    if (input == null || input == " ") {
        Console.WriteLine("Введите корректные данные");
        input = Console.ReadLine();

        checkStrValue(input);
    }
    return input;
}
(int Year, double Price) checkNumValue(object input) {

    try
    {
        double num = Convert.ToDouble(input);

        if (num < 0)
        {
            num *= -1;
        }

        return (Convert.ToInt32(num), num);
    }
    catch
    {
        Console.WriteLine("Введите корректные данные!");
        return checkNumValue(Console.ReadLine());
    }

}
void PrintBooks() {
    foreach (var item in lst) {
        Console.WriteLine($"----------------------------------\nID: {item.Key}\nНазвание книги: {item.Value.name}\nАвтор: {item.Value.author}\nЖанр книги: {item.Value.genre}\nГод издания: {item.Value.year}\nЦена книги: {item.Value.price} руб.\n----------------------------------");
    }
}
int AddBook(int id) {

    Console.Write("Введите название книги: ");
    string name = checkStrValue(Console.ReadLine());

    Console.Write("Введите автора (И.Ф. Отчество или псевдоним): ");
    string author = checkStrValue(Console.ReadLine());

    Console.Write("Введите жанр(ы) книги: ");
    string genre = checkStrValue(Console.ReadLine());

    Console.Write("Введите год издания: ");
    int year = checkNumValue(Console.ReadLine()).Year;

    Console.Write("Введите цену книги: ");
    double price = checkNumValue(Console.ReadLine()).Price;

    Book newBook = new Book(name, author, genre, year, price);
    lst.Add(id, newBook);

    Console.WriteLine("Операция выполнена успешно!");
    id++;
    return id;
}
void DeleteBook(int delID) {

    if (lst.ContainsKey(delID))
    {
        lst.Remove(delID);
        Console.WriteLine("Операция выполнена успешно!");
    }
    else {
        Console.WriteLine("Книги с таким ID не существует!");
    }
}
void SearchBook() {

    Console.WriteLine("По какому критерию вы хотите найти книги:\n1. По названию\n2. По автору\n3. По жанру\n4. По году выпуска\n5. По цене\n6. По ID");
    Console.Write(">>> ");
    string critery = checkStrValue(Console.ReadLine());

    switch (critery) {

        case "1":
            Console.Write("Введите название книги: ");
            string name = checkStrValue(Console.ReadLine()).ToLower();
            string message = "Книги с таким названием не найдено!";

            foreach (var item in lst) 
            {
                string lowerName = item.Value.name.ToLower();
                if (lowerName.StartsWith(name)) 
                {
                    Console.WriteLine($"--------------------------------\nID: {item.Key}\nНазвание книги: {item.Value.name}\nАвтор: {item.Value.author}\nЖанр книги: {item.Value.genre}\nГод издания: {item.Value.year}\nЦена книги: {item.Value.price} руб.\n--------------------------------");
                    message = "Операция выполнена успешно!";
                }
            }
            Console.WriteLine(message);
            break;
        case "2":
            Console.Write("Введите автора: ");
            string author = checkStrValue(Console.ReadLine());
            message = "Автор не найден!";

            foreach (var item in lst)
            {
                string lowerAuthor = item.Value.author.ToLower();
                if (lowerAuthor.StartsWith(author))
                {
                    Console.WriteLine($"--------------------------------\nID: {item.Key}\nНазвание книги: {item.Value.name}\nАвтор: {item.Value.author}\nЖанр книги: {item.Value.genre}\nГод издания: {item.Value.year}\nЦена книги: {item.Value.price} руб.\n--------------------------------");
                    message = "Операция выполнена успешно!";
                }
            }
            Console.WriteLine(message);
            break;
        case "3":
            Console.Write("Введите жанр: ");
            string genre = checkStrValue(Console.ReadLine());
            message = "Книги с таким жанром не найдено!";

            foreach (var item in lst)
            {
                string lowerGenre = item.Value.author.ToLower();
                if (lowerGenre.IndexOf(genre) >= 0)
                {
                    Console.WriteLine($"--------------------------------\nID: {item.Key}\nНазвание книги: {item.Value.name}\nАвтор: {item.Value.author}\nЖанр книги: {item.Value.genre}\nГод издания: {item.Value.year}\nЦена книги: {item.Value.price} руб.\n--------------------------------");
                    message = "Операция выполнена успешно!";
                }
            }
            Console.WriteLine(message);
            break;
        case "4":

            Console.Write("Введите год издания книги: ");
            int year = checkNumValue(Console.ReadLine()).Year;
            message = "Книги выпущенной в этом году не найдено!";

            foreach (var item in lst)
            {
                if (item.Value.year == year)
                {
                    Console.WriteLine($"--------------------------------\nID: {item.Key}\nНазвание книги: {item.Value.name}\nАвтор: {item.Value.author}\nЖанр книги: {item.Value.genre}\nГод издания: {item.Value.year}\nЦена книги: {item.Value.price} руб.\n--------------------------------");
                    message = "Операция выполнена успешно!";
                }
            }
            Console.WriteLine(message);
            break;
        case "5":

            Console.Write("Введите цену книги: ");
            double price = checkNumValue(Console.ReadLine()).Price;
            message = "Книги c такой ценой не найдено!";

            foreach (var item in lst)
            {
                if (item.Value.price == price)
                {
                    Console.WriteLine($"--------------------------------\nID: {item.Key}\nНазвание книги: {item.Value.name}\nАвтор: {item.Value.author}\nЖанр книги: {item.Value.genre}\nГод издания: {item.Value.year}\nЦена книги: {item.Value.price} руб.\n--------------------------------");
                    message = "Операция выполнена успешно!";
                }
            }
            Console.WriteLine(message);
            break;
        case "6":
            Console.Write("Введите ID книги: ");
            int id = checkNumValue(Console.ReadLine()).Year;
            message = "Книги с таким ID не найдено!";

            foreach (var item in lst)
            {
                if (item.Key == id)
                {
                    Console.WriteLine($"--------------------------------\nID: {item.Key}\nНазвание книги: {item.Value.name}\nАвтор: {item.Value.author}\nЖанр книги: {item.Value.genre}\nГод издания: {item.Value.year}\nЦена книги: {item.Value.price} руб.\n--------------------------------");
                    message = "Операция выполнена успешно!";
                }
            }
            Console.WriteLine(message);
            break;
        default: { Console.WriteLine("Ошибка!"); break; }
    }
}
void SortingBook() {
    Dictionary<string, Book> copyLst = new Dictionary<string, Book>();

    Console.WriteLine("По какому критернию вы хотите отсортировать список книг? \n1. По названию\n2. По году");
    Console.Write(">>> ");
    string choice = checkStrValue(Console.ReadLine());

    switch (choice) {
        case "1":

            foreach (var item in lst) {
                copyLst.Add(item.Value.name, item.Value);
            }

            var sortedLst = copyLst.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);

            Console.WriteLine("Сортировка по названию:\n----------------------------------");
            foreach (var item in sortedLst) {
                Console.WriteLine($"{item.Key}:\t{item.Value.price} руб.");
            }
            Console.WriteLine("----------------------------------");


            break;
        case "2":

            foreach (var item in lst) {
                copyLst.Add(Convert.ToString(item.Value.year), item.Value);
            }

            sortedLst = copyLst.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);

            Console.WriteLine("Сортировка по году:\n----------------------------------");
            foreach (var item in sortedLst) {
                Console.WriteLine($"{item.Key} год: {item.Value.name}");
            }
            Console.WriteLine("----------------------------------");

            break;
    }
    
}
void PrintMinMaxPriceBook() {

    var maxPriceBook = lst.OrderByDescending(x => x.Value.price).ToDictionary(x => x.Key, x => x.Value).First();
    var minPriceBook = lst.OrderBy(x => x.Value.price).ToDictionary(x => x.Key, x => x.Value).First();

    Console.WriteLine($"--------Самая дорогая книга-------\nID: {maxPriceBook.Key}\nНазвание книги: {maxPriceBook.Value.name}\nАвтор: {maxPriceBook.Value.author}\nЖанр книги: {maxPriceBook.Value.genre}\nГод издания: {maxPriceBook.Value.year}\nЦена книги: {maxPriceBook.Value.price} руб.\n----------------------------------");
    Console.WriteLine($"--------Самая дешёвая книга-------\nID: {minPriceBook.Key}\nНазвание книги: {minPriceBook.Value.name}\nАвтор: {minPriceBook.Value.author}\nЖанр книги: {minPriceBook.Value.genre}\nГод издания: {minPriceBook.Value.year}\nЦена книги: {minPriceBook.Value.price} руб.\n----------------------------------");

}
void GroupByBooks() {

    var AuthorCountBooks = lst.GroupBy(x => x.Value.author).Select(x => new { author = x.Key, bookCount = x.Count() });

    Console.WriteLine("--------Автор и его кол-во книг--------");
    foreach (var temp in AuthorCountBooks) {
        Console.WriteLine($"{temp.author}:\t{temp.bookCount}");
    }
    Console.WriteLine("---------------------------------------");
}

void buyBook() {

    Console.Write("Введите ID книги, которую хотите купить: ");
    int id = Convert.ToInt32(Console.ReadLine());
    string message = "Книга не найдена!";

    foreach (var item in lst) {

        if (item.Key == id) {
            shoppingCart.Add(item.Value);
            message = "Операция выполнена!";
        }
    }

    Console.WriteLine(message);
}
class Book {

    public string name;
    public string author;
    public string genre;
    public int year;
    public double price;

    public Book(string name, string author, string genre, int year, double price)
    {
        this.name = name;
        this.author = author;
        this.genre = genre;
        this.year = year;
        this.price = price;
    }
}