using Nail_nail.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nail_nail.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminConsole.xaml
    /// </summary>
    public partial class AdminConsole : Page
    {
        private string pathToCommand = $"C:/Users/{IUser.AppUser.Login}>>> ";

        private Users mainUser = null;

        private bool CHANGE_FLAG_choice = false;
        private bool CHANGE_FLAG_input = false;
        private int edited_field = 0;

        public AdminConsole()
        {
            InitializeComponent();
            path.Text = $"C:/Users/{IUser.AppUser.Login}>>>";
        }

        private void InputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (CHANGE_FLAG_input) 
            {
                if (e.Key == Key.Enter) 
                {
                    ChangeUser_FinalProcess();
                    inputCommands.Clear();
                }
                return;
            }
            if (CHANGE_FLAG_choice) 
            {
                if (e.Key == Key.Enter) 
                {
                    string choice = inputCommands.Text;
                    AddConsole(pathToCommand, choice);
                    ChangeUser_Flags(choice);
                    inputCommands.Clear();
                }
                return;
            }

            if (e.Key == Key.Enter)
            {
                string command = inputCommands.Text.Trim();
                AddConsole(pathToCommand, command);

                if (command == "adm") PrintConsoleCommands(); 
                else if (command == "adm users") PrintUsers(); 
                else if (command.StartsWith("adm dlt_user"))
                {
                    string[] parts = command.Split(' ');
                    if (parts.Length == 3 && int.TryParse(parts[2], out int userId))
                    {
                        DeleteUser(Convert.ToInt32(parts[2]));
                    }
                    else ConsoleTB.Inlines.Add(new Run("Error: adm dlt_user [целое число]: Введите целое число\n\n") { Foreground = Brushes.DarkRed });
                }
                else if (command.StartsWith("adm chng_user"))
                {
                    string[] parts = command.Split(' ');
                    if (parts.Length == 3 && int.TryParse(parts[2], out int userId))
                    {
                        CHANGE_FLAG_choice = true;
                        ChangeUser_Choice(Convert.ToInt32(parts[2]));
                    }
                    else ConsoleTB.Inlines.Add(new Run("Error: adm chng_user [целое число]: Введите целое число\n\n") { Foreground = Brushes.DarkRed });
                }
                else if (command == "adm crt_user")
                {
                    createUser.Visibility = Visibility.Visible;
                    mainFrame.Visibility = Visibility.Collapsed;
                    ConsoleScroll.Visibility = Visibility.Collapsed;
                }
                else if (command == "clear") ConsoleTB.Inlines.Clear(); 
                else if (command == "exit") NavigationService.GoBack();
                else ConsoleTB.Inlines.Add(new Run($"Error: {command}: Комманда не найдена\n\n") { Foreground = Brushes.DarkRed }); 

                inputCommands.Clear();
                e.Handled = true;
            }
        }
        private void newUserTB_KeyDown(object sender, KeyEventArgs e) 
        {
            if (e.Key != Key.Enter) { return; }
            else 
            {
                int roleID = -1;

                if (loginTB.Text == "" || passwordTB.Text == "" || nameTB.Text == "" || phoneTB.Text == "" || roleTB.Text == "") { return; }
                if (Core.ContextHOME.Users.FirstOrDefault(x => x.Login == loginTB.Text) != null) { ErrorTB.Text = $"Пользователь с логином {loginTB.Text} уже существует"; return; }
                try { roleID = Convert.ToInt32(roleTB.Text); } catch { ErrorTB.Text = "В поле \"Роль\" нужно вписать ID роли (целочисленное значение)"; return; }
                if (roleID < 0 || roleID > 4) { ErrorTB.Text = $"ID роли пользователя может быть только в диапазоне от 1 до 4"; return; }

                Users newUser = new Users() 
                {
                    Login = loginTB.Text,
                    Password = passwordTB.Text,
                    FullName = nameTB.Text,
                    PhoneNumber = phoneTB.Text,
                    Role = roleID,
                    CreatedAt = DateTime.Now,
                    Cover = null,
                };
                Core.ContextHOME.Users.Add(newUser);
                Core.ContextHOME.SaveChanges();

                createUser.Visibility = Visibility.Collapsed;
                mainFrame.Visibility = Visibility.Visible;
                ConsoleScroll.Visibility = Visibility.Visible;

                ConsoleTB.Inlines.Add(new Run("Пользователь был успешно добавлен!\n\n") { Foreground = Brushes.Yellow });
            }
        }

        private void PrintConsoleCommands() 
        {
            ConsoleTB.Inlines.Add(new Run("Базовые команды:\n"));
            ConsoleTB.Inlines.Add(new Run("\tadm\t") { Foreground = Brushes.Purple});
            ConsoleTB.Inlines.Add(new Run("Вывод всех команд\n"));
            ConsoleTB.Inlines.Add(new Run("\tclear\t") { Foreground = Brushes.Purple });
            ConsoleTB.Inlines.Add(new Run("Очистить консоль\n"));
            ConsoleTB.Inlines.Add(new Run("\texit\t") { Foreground = Brushes.Purple });
            ConsoleTB.Inlines.Add(new Run("Закрыть консоль\n"));
            ConsoleTB.Inlines.Add(new Run("\nКоманды для взаимодейтсвия с пользователями:\n"));
            ConsoleTB.Inlines.Add(new Run("\tadm users\t\t\t") { Foreground = Brushes.Purple });
            ConsoleTB.Inlines.Add(new Run("Вывод пользователей и небольшой информации о них\n"));
            ConsoleTB.Inlines.Add(new Run("\tadm dlt_user [id]\t\t") { Foreground = Brushes.Purple });
            ConsoleTB.Inlines.Add(new Run("Удаление пользователя по аргументу (id)\n"));
            ConsoleTB.Inlines.Add(new Run("\tadm chng_user [id]\t\t") { Foreground = Brushes.Purple });
            ConsoleTB.Inlines.Add(new Run("Изменение пользователя по аргументу (id)\n"));
            ConsoleTB.Inlines.Add(new Run("\tadm crt_user\t\t\t") { Foreground = Brushes.Purple });
            ConsoleTB.Inlines.Add(new Run("Создание пользователя\n\n"));
        }

        private void PrintUser(Users user) 
        {
            AutoEqualPrint($"FullName:\t {user.FullName}", "Пользователь", 1);
            ConsoleTB.Inlines.Add(new Run($"Login:\t\t {user.Login}\n") { Foreground = Brushes.Yellow });
            ConsoleTB.Inlines.Add(new Run($"FullName:\t {user.FullName}\n") { Foreground = Brushes.Yellow });
            ConsoleTB.Inlines.Add(new Run($"PhoneNum:\t {user.PhoneNumber}\n") { Foreground = Brushes.Yellow });
            ConsoleTB.Inlines.Add(new Run($"Role:\t\t {user.Roles.RoleName}\n") { Foreground = Brushes.Yellow });
            ConsoleTB.Inlines.Add(new Run($"CreatedAt:\t {user.CreatedAt}\n") { Foreground = Brushes.Yellow });
        }
        private void PrintUsers() 
        {
            var users = Core.ContextHOME.Users.Where(x => x.ID != IUser.AppUser.UserID).ToList();
            ConsoleTB.Inlines.Add(new Run("Список всех пользователей:\n"));
            foreach (var user in users) 
            {
                ConsoleTB.Inlines.Add(new Run($"ID: {user.ID}\t\tLogin: {user.Login}\t\tРоль: {user.Role}\n"));
            }

            var roles = Core.ContextHOME.Roles.ToList();
            ConsoleTB.Inlines.Add(new Run("\nСписок всех ролей:\n"));
            foreach (var role in roles) 
            {
                ConsoleTB.Inlines.Add(new Run($"RoleID: {role.ID}\tНазвание роли: {role.RoleName}\n"));
            }
            ConsoleTB.Inlines.Add(new Run("\n"));
        }

        private void DeleteUser(int id) 
        {
            var deleteUser = Core.ContextHOME.Users.FirstOrDefault(x => x.ID == id);
            if (deleteUser != null) 
            {
                Core.ContextHOME.Users.Remove(deleteUser);
                Core.ContextHOME.SaveChanges();
                ConsoleTB.Inlines.Add(new Run("Пользователь был успешно удалён!\n\n") { Foreground = Brushes.Yellow });
                return;
            }
            ConsoleTB.Inlines.Add(new Run($"Error: adm dlt_user {id}: Пользователь с таким ID не найден\n\n") { Foreground = Brushes.DarkRed });
        }

        private void ChangeUser_Choice(int id) 
        {
            mainUser = Core.ContextHOME.Users.FirstOrDefault(x => x.ID == id);
            if (mainUser != null)
            {
                PrintUser(mainUser);
                ConsoleTB.Inlines.Add(new Run("Введите параметр, который хотите изменить...\n"));
                ConsoleTB.Inlines.Add(new Run(" - Login (1)\n"));
                ConsoleTB.Inlines.Add(new Run(" - FullName (2)\n"));
                ConsoleTB.Inlines.Add(new Run(" - PhoneNum (3)\n"));
                ConsoleTB.Inlines.Add(new Run(" - Role (4)\n\n"));
                return;
            }
            ConsoleTB.Inlines.Add(new Run($"Error: adm chng_user {id}: Пользователь с таким ID не найден\n\n") { Foreground = Brushes.DarkRed });
        }
        private void ChangeUser_Flags(string choice) 
        {
            switch (choice)
            {
                case "1":
                    ConsoleTB.Inlines.Add(new Run("Введите новый логин...\n\n") { Foreground = Brushes.Yellow });
                    CHANGE_FLAG_choice = false;
                    CHANGE_FLAG_input = true;
                    edited_field = 1;
                    break;
                case "2":
                    ConsoleTB.Inlines.Add(new Run("Введите новое имя...\n\n") { Foreground = Brushes.Yellow });
                    CHANGE_FLAG_choice = false;
                    CHANGE_FLAG_input = true;
                    edited_field = 2;
                    break;
                case "3":
                    ConsoleTB.Inlines.Add(new Run("Ввыдите новый номер телефона...\n\n") { Foreground = Brushes.Yellow });
                    CHANGE_FLAG_choice = false;
                    CHANGE_FLAG_input = true;
                    edited_field = 3;
                    break;
                case "4":
                    ConsoleTB.Inlines.Add(new Run("Задайте новую роль (id)...\n\n") { Foreground = Brushes.Yellow });
                    CHANGE_FLAG_choice = false;
                    CHANGE_FLAG_input = true;
                    edited_field = 4;
                    break;
                default:
                    ConsoleTB.Inlines.Add(new Run($"Error: Ввод нераспознан\n\n") { Foreground = Brushes.DarkRed });
                    break;
            }
        }
        private void ChangeUser_FinalProcess() 
        {
            switch (edited_field)
            {
                case 1:
                    string newLogin = inputCommands.Text;
                    AddConsole(pathToCommand, newLogin);

                    mainUser.Login = newLogin;
                    Core.ContextHOME.SaveChanges();

                    ConsoleTB.Inlines.Add(new Run($"Логин успешно изменён!\n\n") { Foreground = Brushes.Yellow });
                    CHANGE_FLAG_input = false;
                    break;
                case 2:
                    string newName = inputCommands.Text;
                    AddConsole(pathToCommand, newName);

                    mainUser.FullName = newName;
                    Core.ContextHOME.SaveChanges();

                    ConsoleTB.Inlines.Add(new Run($"Имя пользователя успешно изменёно!\n\n") { Foreground = Brushes.Yellow });
                    CHANGE_FLAG_input = false;
                    break;
                case 3:
                    string newPhonenum = inputCommands.Text;
                    AddConsole(pathToCommand, newPhonenum);

                    mainUser.PhoneNumber = newPhonenum;
                    Core.ContextHOME.SaveChanges();

                    ConsoleTB.Inlines.Add(new Run($"Номер телефона успешно изменён!\n\n") { Foreground = Brushes.Yellow });
                    CHANGE_FLAG_input = false;
                    break;
                case 4:
                    try
                    {
                        int newRoleID = Convert.ToInt32(inputCommands.Text);
                        AddConsole(pathToCommand, newRoleID.ToString());

                        if (newRoleID < 0 || newRoleID > 4) { ConsoleTB.Inlines.Add(new Run($"ID роли должен быть в диапазоне от 1 до 4\n\n") { Foreground = Brushes.DarkRed }); break; }
                        mainUser.Role = newRoleID;
                        Core.ContextHOME.SaveChanges();

                        ConsoleTB.Inlines.Add(new Run($"Роль пользователя успешно изменёна!\n\n") { Foreground = Brushes.Yellow });
                        CHANGE_FLAG_input = false;
                    }
                    catch
                    {
                        ConsoleTB.Inlines.Add(new Run($"Error {inputCommands.Text}: Невозможно преобразовать в целочисленный тип данных\n\n") { Foreground = Brushes.DarkRed });
                    }
                    break;

                default:
                    ConsoleTB.Inlines.Add(new Run($"Error: Ввод нераспознан\n\n") { Foreground = Brushes.DarkRed });
                    break;
            }
        }

        private void AutoEqualPrint(string longestLine, string title, int countOfTab) 
        {
            int lenOfLine = longestLine.Length + (4 * countOfTab);
            int lenOfTitle = title.Length + 2;

            int difference = lenOfLine - lenOfTitle;
            int center = difference / 2;

            string filler = "";
            for (int i = 0; i != center; i++) 
            {
                filler += "=";
            }

            // Подбор под чет/нечет
            string finalTitle = "";
            if (difference % 2 != 0) { finalTitle = filler + " " + title + " " + filler + "="; }
            else { finalTitle = filler + " " + title + " " + filler; }

            ConsoleTB.Inlines.Add(new Run(finalTitle + "\n"));
        }
        private void AddConsole(string path, string command)
        {
            ConsoleTB.Inlines.Add(new Run(path) { Foreground = Brushes.DarkGreen });
            ConsoleTB.Inlines.Add(new Run(command + "\n") { Foreground = Brushes.Purple });
            ConsoleScroll.ScrollToBottom();
        }
    }
}
