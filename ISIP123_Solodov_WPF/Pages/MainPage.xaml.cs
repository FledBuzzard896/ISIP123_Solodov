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

namespace ISIP123_Solodov_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            List<Pizza> pizzas = new List<Pizza>
            {
                new Pizza("Пепперони", "Типо колбаски такие, кружочки", 500),
                new Pizza("Гагавайская", "Фу, ананасы", 1000),
                new Pizza("С курицей и анансами", "ыыы, опять ананасы", 800),
                new Pizza("Мясная", "Посмотрите на этот чизбургер, с беконом, из паба... О господи.. ох.. там еще и картошка фри!", 700),
                new Pizza("4 сына", "Пицца 4 сына биля, я пока к тэбе ехал, мой матиз др др др, бым бым бым, сломался ыа!!!", 1500),
            };
            PizzaListBox.ItemsSource = pizzas;
        }

        public class Pizza 
        
            string name;
            string description;
            double price;

            public Pizza(string n, string d, double p) 
            {
                this.name = n;
                this.description = d;
                this.price = p;
            }
        }
    }
}
