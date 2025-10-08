
do
{
    Console.WriteLine($"1. Добавить студента\n2. Удалить студента\n3. Перевести студента");
} while (true);

List<Teacher> teachers = new List<Teacher>();   
List<List<Student>> specialists = new List<List<Student>>();

List<Student> ISIP_WEB = new List<Student>();
List<Student> ISIP_PROG = new List<Student>();
List<Student> OIBAS_WEB = new List<Student>();
List<Student> OIBAS_PROG = new List<Student>();

List<Student> FirstCourse = new List<Student>();
List<Student> SecondCourse = new List<Student>();
List<Student> ThirdCourse = new List<Student>();
List<Student> FourthCourse = new List<Student>();
List<Student> FifthCourse = new List<Student>();

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

    private int course;
    private int group;
    private string speciality;

    private double grant; 

    public Student(string name, int age, string gender, string city, int course, int group, string speciality, double grant) : base (name,age,gender,city) {
        
        this.course = course;
        this.group = group;
        this.speciality = speciality;
        this.grant = grant;

    }

    public override void PrintInfo() {

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