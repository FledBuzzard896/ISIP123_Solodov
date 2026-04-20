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
        string pathToCommand = $"C:/Users/{IUser.AppUser.Login}>>> ";
        public AdminConsole()
        {
            InitializeComponent();
            path.Text = $"C:/Users/{IUser.AppUser.Login}>>>";
        }

        private void InputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string command = inputCommands.Text.Trim();
                AddConsole(pathToCommand, command);

                if (command == "adm")
                {
                    PrintConsoleCommands();
                }
                else if (command == "adm users")
                {
                    PrintUsers();
                }
                else if (command.StartsWith("adm dlt_user"))
                {
                    string[] parts = command.Split(' ');
                    if (parts.Length == 3 && int.TryParse(parts[2], out int userId))
                    {
                        // Команда удаления юзеров
                    }
                    else
                    {
                        ConsoleTB.Inlines.Add(new Run("Error: adm dlt_user [целое число]: Введите целое число\n") { Foreground = Brushes.DarkRed });
                    }
                }
                else if (command.StartsWith("adm chng_user"))
                {
                    string[] parts = command.Split(' ');
                    if (parts.Length == 3 && int.TryParse(parts[2], out int userId))
                    {
                        // Команда изменения юзеров
                    }
                    else
                    {
                        ConsoleTB.Inlines.Add(new Run("Error: adm chng_user [целое число]: Введите целое число\n") { Foreground = Brushes.DarkRed });
                    }
                }
                else if (command.StartsWith("adm chng_user_role"))
                {
                    string[] parts = command.Split(' ');
                    if (parts.Length == 3 && int.TryParse(parts[2], out int userId))
                    {
                        // Команда изменения ролей юзеров
                    }
                    else
                    {
                        ConsoleTB.Inlines.Add(new Run("Error: adm chng_user_role [целое число]: Введите целое число\n") { Foreground = Brushes.DarkRed });
                    }
                }
                else if (command == "adm crt_user")
                {
                    // Создание пользователя
                }
                else if (command == "clear")
                {
                    ConsoleTB.Inlines.Clear();    
                }
                else if (command == "exit")
                {
                    NavigationService.GoBack();
                }
                else 
                {
                    ConsoleTB.Inlines.Add(new Run($"Error: {command}: Комманда не найдена\n") { Foreground = Brushes.DarkRed });
                }

                
                inputCommands.Clear();
                e.Handled = true;
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
            ConsoleTB.Inlines.Add(new Run("\tadm chng_user_role [id]\t") { Foreground = Brushes.Purple });
            ConsoleTB.Inlines.Add(new Run("Изменение роли пользователя по аргументу (id)\n"));
            ConsoleTB.Inlines.Add(new Run("\tadm crt_user\t\t\t") { Foreground = Brushes.Purple });
            ConsoleTB.Inlines.Add(new Run("Создание пользователя\n\n"));
        }
        private void PrintUsers() 
        {
            var users = Core.ContextKIP.Users.Where(x => x.ID != IUser.AppUser.UserID).ToList();
            ConsoleTB.Inlines.Add(new Run("Список всех пользователей:\n"));
            foreach (var user in users) 
            {
                ConsoleTB.Inlines.Add(new Run($"ID: {user.ID}\t\tLogin: {user.Login}\t\tРоль: {user.Role}\n"));
            }

            var roles = Core.ContextKIP.Roles.ToList();
            ConsoleTB.Inlines.Add(new Run("\nСписок всех ролей:\n"));
            foreach (var role in roles) 
            {
                ConsoleTB.Inlines.Add(new Run($"RoleID: {role.ID}\tНазвание роли: {role.RoleName}\n"));
            }

        }

        private void AddConsole(string path, string command)
        {
            ConsoleTB.Inlines.Add(new Run(path) { Foreground = Brushes.DarkGreen });
            ConsoleTB.Inlines.Add(new Run(command + "\n") { Foreground = Brushes.Purple });
            ConsoleScroll.ScrollToBottom();
        }
    }
}
