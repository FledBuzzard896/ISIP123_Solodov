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
using The_Binding_Of_Isaac_WPF.Model;

namespace The_Binding_Of_Isaac_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для Ending.xaml
    /// </summary>
    public partial class Ending : Page
    {
        public Ending()
        {
            InitializeComponent();

            if (Isaac.Hp > 0) 
            {
                EndText.Text = "Айзек загнан в угол своей матерью, которая говорит:\n" +
                    "\"Я сделаю то, что Ты мне сказал, Господь. Я люблю тебя больше всего на свете\"\n\n" +
                    "В тот момент, когда мать готова убить Айзека, Бог вмешивается. \n" +
                    "Словно сама по себе, Библия, которая стояла на полке выше, вдруг падает прямо на голову Матери Айзека.\n" +
                    "Она не пережила такого...\n\n" +
                    "Айзек посмотрел на тело Матери, и понял, что ему ещё многое нужно будет обдумать, но одно он мог сказать сразу, в этом доме ему больше нечего делать.\n" +
                    "Айзек взял фото своей мамы, собрал кулёк еды и отправился из дома прямо в лес, искать, где-то потерявшегося, по словам Матери, кота Гаппи...";
            }
            else
            {
                EndText.Text = "Дорогой дневник, сегодня я умер. Я был убит этим существом → Мать. \nВсе свои вещи я завещаю моему коту Гаппи. Прощай, жестокий мир. Люблю, целую, Айзек!";
            }
        }

        private void theEnd_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
