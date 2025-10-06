//2.Задание «Умная лампа»Создайте консольное приложение C# для управления умной лампой.

//У лампы должны быть следующие параметры:
//1. Уникальный идентификатор(генерируется автоматически при создании)
//2. Состояние(включена / выключена; только для чтения извне)
//3. Яркость (целое число 0..100; в сеттере ограничить диапазон)
//4. Цветовая температура (целое число 2700..6500; в сеттере ограничить диапазон)
//5. Режим (только для чтения; вычисляется из текущей цветовой температуры, напр.: ≤3000 — «Ночь», 3001–5000 — «Чтение», >5000 — «День»)

//Мы можем работать с лампой через команды:
//1. Включить лампу
//2. Выключить лампу
//3. Установить яркость
//4. Установить цветовую температуру
//5. Применить быструю сцену («Ночь», «Чтение», «День») — настроить яркость и температуру предустановленными значениями
//6. Показать информацию о лампе (идентификатор, состояние, яркость, температура, режим)

SmartLamp YandexLamp = new SmartLamp();

Console.WriteLine("Базовые настройки лампы: ");
YandexLamp.PrintInfo();

Console.WriteLine("---------------MENU---------------\n1. Включить лампу\n2. Выключить лампу\n3. Установить яркость\n4. Установить цветовую температуру\n5. Показать информацию о лампе\n0. Выход\n----------------------------------");
string choice = Console.ReadLine();

while (choice != "0") {

    switch (choice) {

        case "1":
            YandexLamp.TurnOn();
            break;

        case "2":
            YandexLamp.TurnOff();
            break;

        case "3":
            Console.Write("Установите яркость (0-100): ");
            int value = Convert.ToInt32(Console.ReadLine());

            YandexLamp.SetBright(value);
            break;

        case "4":
            Console.Write("Установите цветовую температуру (2700-6500): ");
            value = Convert.ToInt32(Console.ReadLine());

            YandexLamp.SetColorTemperatureAndStatus(value);
            break;

        case "5":
            YandexLamp.PrintInfo();
            break;

        case "0":
            break;
    }
    Console.WriteLine("---------------MENU---------------\n1. Включить лампу\n2. Выключить лампу\n3. Установить яркость\n4. Установить цветовую температуру\n5. Показать информацию о лампе\n0. Выход\n----------------------------------");
    choice = Console.ReadLine();
}

class SmartLamp {
    public int id { get; }
    public bool isTurnedOn { get; private set; }
    public int bright { get; private set; }
    public int colorTemperature { get; private set; }
    public string status { get; private set; }

    public SmartLamp() {

        Random random = new Random();
        this.id = random.Next(1000, 9999);

        this.isTurnedOn = false;

        this.bright = 50;

        this.colorTemperature = 4500;

        this.status = "Чтение";
    }

    public void TurnOn() { this.isTurnedOn = true; Console.WriteLine("Лампа включена"); }
    public void TurnOff() { this.isTurnedOn = false; Console.WriteLine("Лампа выключена"); }
    public void SetBright(int input) {

        if (input > 0 && input <= 100)
        {
            this.bright = input;
            Console.WriteLine($"Установленная яркость: {bright}");
        }
        else 
        {
            Console.WriteLine($"Установленная яркость: {input} не входит в диапазон допустимых значений (0-100)");
        }
    }
    public void SetColorTemperatureAndStatus(int input) {

        if (input >= 2700 && input <= 6500)
        {
            this.colorTemperature = input;

            if (colorTemperature <= 3000)
            {
                this.status = "Ночь";
            }
            else if (colorTemperature >= 3001 && colorTemperature <= 5000)
            {
                this.status = "Чтение";
            }
            else
            {
                this.status = "День";
            }

            Console.WriteLine($"Установленная цветовая температура: {colorTemperature}");
        }
        else
        {
            Console.WriteLine($"Установленная цветовая температура: {colorTemperature} не входит в диапазон допустимых значений (2700 - 6500)");
        }
    }
    public void PrintInfo() {

        Console.WriteLine($"==================================\nID лампы: {id}\nВключена ли лампа: {isTurnedOn}\nЯркость: {bright}\nЦветовая температура: {colorTemperature}\nРежим: {status}\n==================================");
    }
}
