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
        List<basepart> sborochka_items = new List<basepart>();
        public CategoriesPage()
        {
            InitializeComponent();

            Categories_LB.ItemsSource = categories;

            Loaded += MyPageLoaded;
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
                int n0 = Sborka.GetPart(1);
                int n1 = Sborka.GetPart(4);
                int n2 = Sborka.GetPart(7);
                int n3 = Sborka.GetPart(5);
                int n4 = Sborka.GetPart(3);
                int n5 = Sborka.GetPart(6);
                int n6 = Sborka.GetPart(2);

                int socketCPU = Core.ContextKIP.cpu.First(x => x.id == n0).socketid;
                int socketMotherboard = Core.ContextKIP.motherboard.First(x => x.id == n1).socketid;
                int socketCooler = Core.ContextKIP.socketprocessorcooler.First(x => x.processorcoolerid == n2).socketid;

                if (socketCPU != socketMotherboard) { MessageBox.Show("Сокет процессора не совпадает с сокетом материнской платы", "Несостыковка", MessageBoxButton.OK); return; }
                else if (socketCooler != socketMotherboard) { MessageBox.Show("Сокет кулера не совпадает с сокетом материнской платы", "Несостыковка", MessageBoxButton.OK); return; }
                else if (socketCPU != socketCooler) { MessageBox.Show("Сокет кулера не совпадает с сокетом процессора", "Несостыковка", MessageBoxButton.OK); return; }

                // Проверка на формфактор
                int formfactorMotherboard = Core.ContextKIP.motherboard.First(x => x.id == n1).formfactorid;
                int formfactorCase = Core.ContextKIP.boardformfactorcase.First(x => x.caseid == n3).formfactorid;

                if (formfactorCase != formfactorMotherboard) { MessageBox.Show("Форм фактор корпуса не совпадает с формфактором материнской платы", "Несостыковка", MessageBoxButton.OK); return; }

                // Проверка на тип памяти
                int memoryMotherboard = Core.ContextKIP.motherboard.First(x => x.id == n1).memorytypeid;
                int memoryRAM = Core.ContextKIP.ram.First(x => x.id == n4).memorytypeid;

                if (memoryMotherboard != memoryRAM) { MessageBox.Show("Тип памяти не совпадает с поддерживаемой памятью на материнке", "Несостыковка", MessageBoxButton.OK); return; }

                // Проверка на мощность
                int powerSupply = Core.ContextKIP.powersupply.First(x => x.id == n5).power;
                int powerGPU = (int)Core.ContextKIP.gpu.First(x => x.id == n6).recommendpower;

                if (powerGPU > powerSupply) { MessageBox.Show("Потребление мощности видеокартой превышает мощность бп", "Несостыковка", MessageBoxButton.OK); return; }


                SborkaDialog dialog = new SborkaDialog();
                dialog.ShowDialog();
            }
            else
            {
                MessageBox.Show("Для сохранения сборки добавьте в сборку все компоненты", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.None);
            }
        }

        private void ShowHistoryBtn_Click(object sender, RoutedEventArgs e)
        {
            var newPage = new HistoryPage();
            NavigationService.Navigate(newPage);
        }

        private void MyPageLoaded(object sender, RoutedEventArgs e)
        {
            if (Sborka.IsAnyPart()) 
            {
                for (int i = 1; i < 9; i++)
                {
                    var elem = Sborka.GetPart(i);
                    if (elem == 0) { continue; }
                    sborochka_items.Add(Core.ContextKIP.basepart.First(x => x.id == elem));
                }

                SborkaItems_LB.ItemsSource = sborochka_items;
            }
        }
    }
}