void mainMenu(List<string> tasks, int num)
{
    Console.WriteLine($"Выберите задачу для проверки:\n1. Работа с задачами\n2. Система отмены\n3. Анализ текста");
    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            taskManagment(tasks, num);
            break;
        case "2":
            sysCancel();
            break;
        case "3":
            textAnaliz();
            break;
        default: break;
    }
}
void taskManagment(List<string> tasks, int numOfTask)
{

    List<string> tasksLst = tasks;
    Console.WriteLine($"Выберите желаемсое действие:\n1.Добавить\n2.Удалить\n3.Вывод\n4.Сортировка\n5.Поиск по слову\n0.Выход");
    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":

            Console.Write("Введите задачу: ");
            string task = Console.ReadLine();
            string strNumOfTask = Convert.ToString(numOfTask);

            strNumOfTask += " ";
            strNumOfTask += task;
            tasksLst.Add(strNumOfTask);
            numOfTask++;
            Console.WriteLine("Задача успешно добавлена!\n");

            taskManagment(tasksLst, numOfTask);
            break;

        case "2":

            Console.Write("Введите номер задачи, которую хотите удалить: ");
            string deleteTask = Console.ReadLine();

            for (int i = 0; i < tasksLst.Count; i++)
            {

                if (tasksLst[i].StartsWith(deleteTask))
                {
                    tasksLst.Remove(tasksLst[i]);

                    for (int j = i; j < tasksLst.Count; j++)
                    {
                        string tempStr = Convert.ToString(j + 1) + " " + tasksLst[j].Substring(2);
                        tasksLst.RemoveAt(j);
                        tasksLst.Insert(j, tempStr);
                    }
                    break;
                }
            }
            Console.WriteLine("Задача успешно удалена!\n");
            numOfTask--;

            taskManagment(tasksLst, numOfTask);
            break;

        case "3":

            Console.WriteLine("-------------------------------");
            foreach (string str in tasksLst)
            {
                Console.WriteLine(str);
            }
            Console.WriteLine("-------------------------------");

            taskManagment(tasksLst, numOfTask);
            break;

        case "4":

            Console.WriteLine("1. Отсортировать по номеру задачи / 2. Отсортировать по алфавиту");
            int vibor = Convert.ToInt32(Console.ReadLine());

            if (vibor == 1)
            {
                tasksLst.Sort();
                taskManagment(tasksLst, numOfTask);
            }
            else if (vibor == 2)
            {
                List<string> sortedLst = [];

                foreach (string s in tasksLst)
                {
                    string temp = s.Substring(2) + s.Substring(0, 2);
                    sortedLst.Add(temp);
                }

                sortedLst.Sort();

                for (int i = 0; i < sortedLst.Count; i++)
                {
                    string temp = sortedLst[i].Substring(sortedLst[i].Length - 2) + sortedLst[i].Substring(0, sortedLst[i].Length - 2);
                    sortedLst[i] = temp;
                }
                Console.WriteLine("Список отсортирован!\n");
                taskManagment(sortedLst, numOfTask);
            }
            break;

        case "5":

            Console.Write("Введите задачу которую хотите найти (либо слово): ");
            string searching = Console.ReadLine();

            for (int i = 0; i < tasksLst.Count; i++)
            {

                if (tasksLst[i].IndexOf(searching) != -1)
                {
                    Console.WriteLine(tasksLst[i]);
                }
            }
            taskManagment(tasksLst, numOfTask);
            break;

        default: break;
    }
}
void sysCancel()
{

    Stack<string> mainStck = new Stack<string>();
    Stack<Stack<string>> history = new Stack<Stack<string>>();

    string choice = "";
    string product;

    while (choice != "0")
    {

        if (mainStck.Count > 0)
        {
            Console.WriteLine("---------------------------");
            foreach (string str in mainStck)
            {
                Console.WriteLine(str);
            }
            Console.WriteLine("---------------------------");
        }

        Console.WriteLine($"Выберите действие:\n1. Добавить продукт в список\n2. Удалить продукт из списка\n3. Изменить продукт в списке\n4. Вернуться в прошлое состояние списка\n0. Выход");
        choice = Console.ReadLine();

        switch (choice)
        {

            case "1":
                Console.Write("Какой продукт вы хотите добавить? ");
                product = Console.ReadLine();
                history.Push(mainStck);
                mainStck.Push(product);
                break;

            case "2":
                Console.Write("Какой продукт вы хотите удалить? ");
                product = Console.ReadLine();
                Stack<string> tempCopyStck = new Stack<string>();

                history.Push(mainStck);
                foreach (string str in mainStck.Reverse<string>())
                {
                    if (str.IndexOf(product) < 0)
                    {
                        tempCopyStck.Push(str);
                    }
                }
                mainStck = tempCopyStck;
                break;

            case "3":
                Console.Write("Какой продукт вы хотите изменить? ");
                product = Console.ReadLine();
                tempCopyStck = new Stack<string>();

                history.Push(mainStck);
                foreach (string str in mainStck.Reverse<string>())
                {
                    if (str.IndexOf(product) < 0)
                    {
                        tempCopyStck.Push(str);
                    }
                    else if (str.IndexOf(product) >= 0)
                    {
                        Console.Write("Введите изменённый продукт: ");
                        product = Console.ReadLine();
                        tempCopyStck.Push(product);
                    }
                }
                mainStck = tempCopyStck;
                break;

            case "4":
                mainStck = history.Pop();
                break;

            case "0": break;
        }
    }

}
void textAnaliz()
{
    Dictionary<string, int> wordsCountPairs = new Dictionary<string, int>();
    string text;
    char[] symbols = { '.', ',', '!', '?', ';', ':', ' ' };

    Console.WriteLine("Введите строчку текста");
    text = Console.ReadLine();
    string[] words = text.Split(symbols);

    for (int i = 0; i < words.Length; i++)
    {

        int count = 0;
        for (int j = 0; j < words.Length; j++)
        {

            if (words[i] == words[j]) { count++; }
        }

        if (wordsCountPairs.ContainsKey(words[i]) == false)
        {
            if (words[i].Length > 0)
            {
                wordsCountPairs.Add(words[i], count);
            }

        }
    }

    Console.WriteLine("-----------------------");
    foreach (var pair in wordsCountPairs)
    {
        Console.WriteLine($"Слово: {pair.Key} встречалось {pair.Value} раз(а)");
    }
    Console.WriteLine("-----------------------");


}

List<string> temp = [];
mainMenu(temp, 1);