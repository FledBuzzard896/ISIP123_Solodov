List<Teacher> teachers = new List<Teacher>();

List<Student> ISIP = new List<Student>();
List<Student> OIBAS = new List<Student>();

List<Student> FirstCourse = new List<Student>();
List<Student> SecondCourse = new List<Student>();
List<Student> ThirdCourse = new List<Student>();
List<Student> FourthCourse = new List<Student>();
List<Student> FifthCourse = new List<Student>();

int id = 1;
string choice;
do
{
    Console.WriteLine($"1. Добавить студента\n2. Удалить студента\n3. Перевести студента\n4. Добавить преподавателя\n5. Вывод всех преподавателей\n6. Выввод всех студегнтов\n0. Выход");
    choice = Console.ReadLine();

    switch (choice) {

        case "1":
            AddStudent(id);
            id++;
            break;
        case "2":
            DeleteStudent();
            break;
        case "3":
            SwithStudent();
            break;
        case "4":
            AddTeacher();
            break;
        case "5":
            PrintTeachers();
            break;
        case "6":
            PrintStudents();
            break;
        case "0": break;
    }
} while (choice != "0"); 


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

    Console.Write("В какую группу добавить студента (123,223,323): ");
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
    Console.WriteLine("Студент добавлден!");
}
void DeleteStudent() {

    Console.WriteLine("Введите ID студента");
    int id = Convert.ToInt32(Console.ReadLine());
    string message = "Студент не найден!";

    foreach (var item in ISIP)
    {
        if (item.GetID() == id)
        {
            ISIP.Remove(item);

            message = "Студент удален!";        
        }
    }

    foreach (var item in OIBAS)
    {
        if (item.GetID() == id)
        {
            OIBAS.Remove(item);

            message = "Студент удален!";
        }
    }

    Console.WriteLine(message);
}
void SwithStudent() {
    
    Console.WriteLine("введите ID студента: ");
    int id = Convert.ToInt32( Console.ReadLine());
    string message = "Студент не найден";
    bool temp = true;

    foreach (var item in ISIP)
    {
        if (item.GetID() == id)
        {
            ISIP.Remove(item);
            OIBAS.Add(item);

            temp = false;
            message = "Студент переведён!";
        }
    }

    if (temp) {
        foreach (var item in OIBAS)
        {
            if (item.GetID() == id)
            {
                OIBAS.Remove(item);
                ISIP.Add(item);

                message = "Студент переведён!";
            }
        }
    }
    Console.WriteLine(message);
}
void AddTeacher() {

    Console.WriteLine("введите имя");
    string name = Console.ReadLine();

    Console.WriteLine("введите возраст");
    int age = Convert.ToInt32( Console.ReadLine());

    Console.WriteLine("what is u genda?");
    string genda = Console.ReadLine();

    Console.WriteLine("В какой городе вы жрвете?");
    string city = Console.ReadLine();

    Console.WriteLine("Введите опыт работы");
    int exp = Convert.ToInt32( Console.ReadLine());

    Console.WriteLine("Введите дисциплину");
    string sub = Console.ReadLine();

    Console.WriteLine("Введите зарплату");
    double salary = Convert.ToDouble( Console.ReadLine());

    Teacher teacher = new Teacher(name, age, genda, city, exp, sub, salary);

    teachers.Add(teacher);
    Console.WriteLine("Преподаватель добавлен!");
}
void PrintTeachers() {

    foreach (var teacher in teachers)
    {
        Console.WriteLine("=======================================================");
        teacher.PrintInfo();
    }
    Console.WriteLine("=======================================================");
}
void PrintStudents() {

    Console.WriteLine($"======================= 1 курс =======================");
    foreach (var item in FirstCourse) { item.PrintInfo(); }
    Console.WriteLine($"======================= 2 курс =======================");
    foreach (var item in SecondCourse) { item.PrintInfo(); }
    Console.WriteLine($"======================= 3 курс =======================");
    foreach (var item in ThirdCourse) { item.PrintInfo(); }
    Console.WriteLine($"======================= 4 курс =======================");
    foreach (var item in FourthCourse) { item.PrintInfo(); }
    Console.WriteLine($"======================= 5 курс =======================");
    foreach (var item in FifthCourse) { item.PrintInfo(); }
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

    public int GetID() { return id; }
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