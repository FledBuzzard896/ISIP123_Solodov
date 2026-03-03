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
    /// Логика взаимодействия для CategoriesPage.xaml
    /// </summary>
    public partial class CategoriesPage : Page
    {
        List<string> categories = new List<string> { "Процессоры", "Видеокарты", "Оперативная память", "Материнская плата", "Корпуса", "Блок питания", "Кулер для ЦП", "Жесткий диск/SSD" };
        public CategoriesPage()
        {
            InitializeComponent();

            Categories_LB.ItemsSource = categories;
        }

        void Category_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string title = btn.Content.ToString();

            OneProductCategoryPage page = new OneProductCategoryPage(title);
            NavigationService.Navigate(page);
        }

        private void SaveSborkaBtn_Click(object sender, RoutedEventArgs e)
        {
            int c = 0;
            foreach (var elem in Sborka.GetPartsArray()) { if (elem > 0) { c++; } }

            if (c == 8)
            {
                // Проверка на сокеты
                int socketCPU = Core.ContextHOME.cpu.First(x => x.id == Sborka.GetPart(1)).socketid;
                int socketMotherboard = Core.ContextHOME.motherboard.First(x => x.id == Sborka.GetPart(4)).socketid;
                int socketCooler = Core.ContextHOME.socketprocessorcooler.First(x => x.processorcoolerid == Sborka.GetPart(7)).socketid;

                if (socketCPU != socketMotherboard) { MessageBox.Show("Сокет процессора не совпадает с сокетом материнской платы", "Несостыковка", MessageBoxButton.OK); return; }
                else if (socketCooler != socketMotherboard) { MessageBox.Show("Сокет кулера не совпадает с сокетом материнской платы", "Несостыковка", MessageBoxButton.OK); return; }
                else if (socketCPU != socketCooler) { MessageBox.Show("Сокет кулера не совпадает с сокетом процессора", "Несостыковка", MessageBoxButton.OK); return; }

                // Проверка на формфактор
                int formfactorMotherboard = Core.ContextHOME.motherboard.First(x => x.id == Sborka.GetPart(4)).formfactorid;
                int formfactorCase = Core.ContextHOME.boardformfactorcase.First(x => x.caseid == Sborka.GetPart(5)).formfactorid;

                if (formfactorCase != formfactorMotherboard) { MessageBox.Show("Форм фактор корпуса не совпадает с формфактором материнской платы", "Несостыковка", MessageBoxButton.OK); return; }

                // Проверка на тип памяти
                int memoryMotherboard = Core.ContextHOME.motherboard.First(x => x.id == Sborka.GetPart(4)).memorytypeid;
                int memoryRAM = Core.ContextHOME.ram.First(x => x.id == Sborka.GetPart(3)).memorytypeid;

                if (memoryMotherboard != memoryRAM) { MessageBox.Show("Тип памяти не совпадает с поддерживаемой памятью на материнке", "Несостыковка", MessageBoxButton.OK); return; }

                // Проверка на мощность
                int powerSupply = Core.ContextHOME.powersupply.First(x => x.id == Sborka.GetPart(6)).power;
                int powerGPU = (int)Core.ContextHOME.gpu.First(x => x.id == Sborka.GetPart(2)).recommendpower;

                if (powerGPU > powerSupply) { MessageBox.Show("Потребление мощности видеокартой превышает мощность бп", "Несостыковка", MessageBoxButton.OK); return; }

                // Продолжение кода с сохранением сборки и переходом на главную страницу
            }
            else
            {
                MessageBox.Show("Для сохранения сборки добавьте в сборку все компоненты", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.None);
            }
        }

        private void ShowHistoryBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
