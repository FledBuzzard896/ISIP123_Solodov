List<Teacher> teachers = new List<Teacher>();

List<Student> ISIP = new List<Student>();
List<Student> OIBAS = new List<Student>();

List<Student> FirstCourse = new List<Student>();
List<Student> SecondCourse = new List<Student>();
List<Student> ThirdCourse = new List<Student>();
List<Student> FourthCourse = new List<Student>();
List<Student> FifthCourse = new List<Student>();

do
{
    Console.WriteLine($"1. Добавить студента\n2. Удалить студента\n3. Перевести студента\n4. Добавить преподавателя");
} while (true);

void AddStudent(int id) {

    Console.Write("Введите ФИО студента: ");
    string name = Console.ReadLine();

    Console.Write("Введите возраст: ");
    int age = Convert.ToInt32(Console.ReadLine());

    Console.Write("What is u genda (makanic?): ");
    string gender = Console.ReadLine();

    Console.Write("В каком городе вы живёте: ");
    string city = Console.ReadLine();

    Console.Write("На какой курс поступает студент: ");
    int course = Convert.ToInt32(Console.ReadLine());

    Console.Write("В какую группу добавить студента: ");
    int group = Convert.ToInt32(Console.ReadLine());

    Console.Write("Введите направление (ISIP_WEB, ISIP_PROG, OIBAS_WEB, OIBAS_PROG)");
    string speciality = Console.ReadLine();

    Console.WriteLine("Назначить студенту особую стипендию? (Да/Нет)");
    string choice = Console.ReadLine();

    Student newStudent = new Student(id, name, age, gender, city, group, speciality, course);
    id++;

    switch (course) 
    {
        case 1: FirstCourse.Add(newStudent); break;
        case 2: SecondCourse.Add(newStudent); break;
        case 3: ThirdCourse.Add(newStudent); break;
        case 4: FourthCourse.Add(newStudent); break;
        case 5: FifthCourse.Add(newStudent); break;
    }

    switch (speciality) 
    {
        case "ISIP_WEB": ISIP.Add(newStudent); break;
        case "ISIP_PROG": ISIP.Add(newStudent); break;
        case "OIBAS_WEB": OIBAS.Add(newStudent); break;
        case "OIBAS_PROG": OIBAS.Add(newStudent); break;
    }
}

class Person {

    private string name;
    private int age;
    private string gender;
    private string city;

    public Person(string name, int age, string gender, string city) {

        this.name = name;
        this.age = age;
        this.gender = gender;
        this.city = city;
    }

    public virtual void PrintInfo() {

        Console.WriteLine($"Имя: {name}\nВозраст: {age}\nГендер: {gender}\nГород проживания: {city}");
    }
}

class Student : Person {

    private int id;

    private int course;
    private int group;
    private string speciality;

    private double grant; 

    public Student(int id, string name, int age, string gender, string city, int group, string speciality, int course = 1, double grant = 799.99) : base (name,age,gender,city) {
        
        this.id = id;   
        this.course = course;
        this.group = group;
        this.speciality = speciality;
        this.grant = grant;

    }

    public override void PrintInfo() {

        Console.WriteLine($"ID: {id}");
        base.PrintInfo();
        Console.WriteLine($"Курс: {course}\nГруппа: {group}\nНаправление: {speciality}\nСтипендия: {grant}");
    }
}
class Teacher : Person {

    private int experience;
    private string subject;

    private double salary;

    public Teacher(string name, int age, string gender, string city, int experience, string subject, double salary) : base(name, age, gender, city) {
        
        this.experience = experience;
        this.subject = subject;
        this.salary = salary;

    }

    public override void PrintInfo() {

        base.PrintInfo();
        Console.WriteLine($"Опыт работы: {experience}\nПреподаваемая дисциплина: {subject}\nЗарплата: {salary}");
    }
}