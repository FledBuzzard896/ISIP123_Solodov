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
        string startMessage = "================================================================================================\n" +
            "Приветсвуем вас в AdminConsole для управление пользователями. Ниже представлены всевозможные команды команды:\n" +
            "\nБазовые команды:\n" +
            "\tadm\tВывод всех команд\n" +
            "\tclear\tОчистить консоль\n" +
            "\texit\tЗакрыть консоль\n" +
            "\nКоманды для взаимодейтсвия с пользователями:\n" +
            "\tadm users\tВывод пользователей и небольшой информации о них\n" +
            "\tadm delete_user [id]\tУдаление пользователя по аргументу (id)\n" +
            "\tadm change_user []";
        public AdminConsole()
        {
            InitializeComponent();
        }
    }
}

//private void AddLog(string addition)
//{
//    LogTB.Text += addition + "\n";
//    LogScroll.ScrollToBottom();
//}
