using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;



// Список пользователей
var users = new List<User>
{
    new User("Иванов Иван Иванович",  "ivanovii",   new DateTime(1990, 5, 18), new List<string> { "Чтение", "Путешествия", "Программирование" }),
    new User("Петров Петр Петрович",  "petrovpp",   new DateTime(1985, 10, 2), new List<string> { "Футбол", "Кулинария" }),
    new User("Сидорова Анна Михайловна","sidorovana", new DateTime(2000, 3, 15), new List<string> { "Рисование", "Программирование" }),
    new User("Смирнов Алексей Николаевич","smirnovan",new DateTime(1995, 7, 30), new List<string> { "Хоккей", "Фотография" }),
    new User("Кузнецова Ольга Валерьевна","kuznetsovao",new DateTime(1992, 12, 12), new List<string> { "Йога", "Путешествия" })
};

// 1
var olderThen30 = users.Where(user => 2025 - user.DateOfBirth.Year > 30).ToList();

// 2
var sortedLogins = users.OrderBy(user => user.Login).ToList();

// 3
var programmists = users.Where(user => user.Hobbies.Contains("Программирование"));

// 4
Dictionary<int, int> yearCount = new Dictionary<int, int>();
foreach (var user in users) {
    if (yearCount.ContainsKey(user.DateOfBirth.Year))
    {
        yearCount[user.DateOfBirth.Year]++;
    }
    else {
        yearCount.Add(user.DateOfBirth.Year, 1);
    }
}

// 5
Dictionary<string, string> logName = new Dictionary<string, string>();
foreach (var user in users)
{
    logName.Add(user.Login, user.FullName);
}

// 6
var minUser = users.Min(user => user.DateOfBirth.Year);
var maxUser = users.Max(user => user.DateOfBirth.Year);

// 7
// уникальные хобби

// 8
var hobbyTravelCount = users.Where(user => user.Hobbies.Contains("Путешествия")).Count();

// 9
var namesLst = users.Where(user => user.FullName.StartsWith('С')).ToList();

// 10
var HobbyUsers = users.OrderBy(user => user.Hobbies).ToList();
List<User> maxHobbyUsers = new List<User>();

for (int i = 0; i < 2; i++) {
    maxHobbyUsers.Add(users[i]);
}
// Класс для задания
public class User
{
    public string FullName { get; set; }
    public string Login { get; set; }
    public DateTime DateOfBirth { get; set; }
    public List<string> Hobbies { get; set; }

    public User(string fullName, string login, DateTime dateOfBirth, List<string> hobbies)
    {
        FullName = fullName;
        Login = login;
        DateOfBirth = dateOfBirth;
        Hobbies = hobbies;
    }
}

