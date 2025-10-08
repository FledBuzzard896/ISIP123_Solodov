//Необходимо создать класс "Транспорт".
//От него должен наследоваться класс "Машина", "Скутер", "Электро-самокат".

//Пользователь должен иметь возможность:
//1. Видеть мощность двигателя
//2. Видеть количество колес
//3. Видеть максимальную скорость

//Если (a > 10) {
//    Консоль.ВывестиЛинию(правда);    
//}

Car test = new Car(123414, 230, 4, 210);
test.Info();
Console.WriteLine("===========================================");
Scooter test2 = new Scooter(2 ,20, 2, 40);
test2.Info();
Console.WriteLine("===========================================");
Samokat test3 = new Samokat(true, 5, 2, 25);
test3.Info();

class Transport {

    private int Power;
    private int CountOfWheels;
    private double MaxSpeed;

    public Transport(int power, int wheels, double speed) {

        Power = power;
        CountOfWheels = wheels;
        MaxSpeed = speed;
    }

    public virtual void Info() {

        Console.WriteLine($"Мощность двигателя: {Power} л.с.\nКол-во колёс: {CountOfWheels}\nМаксимальная развиваемая скорость: {MaxSpeed} км/ч");
    }
}

class Car : Transport {

    private int ID;

    public Car(int iD, int power, int wheels, double speed) : base(power, wheels, speed)
    {
        ID = iD;
    }

    public override void Info() {

        Console.WriteLine($"ID автомобиля: {ID}");
        base.Info();
    }
}
class Scooter : Transport {

    private int countOfFridge;
    public Scooter(int fridge, int power, int wheels, double speed) : base(power, wheels, speed) { countOfFridge = fridge; }

    public override void Info() {

        Console.WriteLine($"Кол-во холодильников в скутере: {countOfFridge}");
        base.Info();
    }
}
class Samokat : Transport {

    private bool IsRideable;

    public Samokat(bool isRideable, int power, int wheels, double speed) : base(power,wheels,speed)
    {
        IsRideable = isRideable;
    }

    public override void Info() {

        Console.WriteLine($"Пригоден ли самокат для езды: {IsRideable}");
        base.Info();
    }
}