// See https://aka.ms/new-console-template for more information

List<ConsoleColor> lst = new List<ConsoleColor>();

lst.Add(ConsoleColor.Red);
lst.Add(ConsoleColor.Green);
lst.Add(ConsoleColor.Blue);
lst.Add(ConsoleColor.Yellow);
lst.Add(ConsoleColor.Magenta);
lst.Add(ConsoleColor.Cyan);

while (true) {
    System.Threading.Thread.Sleep(50);

    Random random = new Random();
    int rNum = random.Next(0, lst.Count);

    Console.ForegroundColor = lst[rNum];

    Console.WriteLine("Привет, Мир!");
    Console.SetCursorPosition(0, 0);
}

