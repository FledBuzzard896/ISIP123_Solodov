using System;
using System.Collections;
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
    /// Логика взаимодействия для OneProductCategoryPage.xaml
    /// </summary>
    public partial class OneProductCategoryPage : Page
    {
        public string title { get; set; }
        public OneProductCategoryPage(string inputTitle)
        {
            InitializeComponent();

            title = inputTitle;

            DataContext = this;

            Loaded += MyPageLoaded;
        }

        private void MyPageLoaded(object sender, RoutedEventArgs e) 
        {
            switch (title)
            {
                case "Процессоры":
                    var cpusID = Core.ContextHOME.cpu.ToList().Select(c => c.id).ToList();
                    List<basepart> Products = Core.ContextHOME.basepart.Where(x => cpusID.Contains(x.id)).ToList();
                    Products_LB.ItemsSource = Products;
                    break;

                case "Видеокарты":
                    var gpusID = Core.ContextHOME.gpu.ToList().Select(g => g.id).ToList();
                    Products = Core.ContextHOME.basepart.Where(x => gpusID.Contains(x.id)).ToList();
                    Products_LB.ItemsSource = Products;
                    break;

                case "Оперативная память":
                    var ramsID = Core.ContextHOME.ram.ToList().Select(r => r.id).ToList();
                    Products = Core.ContextHOME.basepart.Where(x => ramsID.Contains(x.id)).ToList();
                    Products_LB.ItemsSource = Products;
                    break;

                case "Материнская плата":
                    var motherboardsID = Core.ContextHOME.motherboard.ToList().Select(m => m.id).ToList();
                    Products = Core.ContextHOME.basepart.Where(x => motherboardsID.Contains(x.id)).ToList();
                    Products_LB.ItemsSource = Products;
                    break;

                case "Корпуса":
                    var casesID = Core.ContextHOME.@case.ToList().Select(c => c.id).ToList();
                    Products = Core.ContextHOME.basepart.Where(x => casesID.Contains(x.id)).ToList();
                    Products_LB.ItemsSource = Products;
                    break;

                case "Блок питания":
                    var powersID = Core.ContextHOME.powersupply.ToList().Select(p => p.id).ToList();
                    Products = Core.ContextHOME.basepart.Where(x => powersID.Contains(x.id)).ToList();
                    Products_LB.ItemsSource = Products;
                    break;

                case "Кулер для ЦП":
                    var cpucoolersID = Core.ContextHOME.processorcooler.ToList().Select(cc => cc.id).ToList();
                    Products = Core.ContextHOME.basepart.Where(x => cpucoolersID.Contains(x.id)).ToList();
                    Products_LB.ItemsSource = Products;
                    break;

                case "Жесткий диск/SSD":
                    var storagedevicesID = Core.ContextHOME.storagedevice.ToList().Select(sd => sd.id).ToList();
                    Products = Core.ContextHOME.basepart.Where(x => storagedevicesID.Contains(x.id)).ToList();
                    Products_LB.ItemsSource = Products;
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void AddBasePart(object sender, RoutedEventArgs e) 
        {
            Button button = sender as Button;

            var product = button.DataContext as basepart;

            // Не изменять стиль но проверять на элемент, если он же самый, то предлагать удалить
            switch (Convert.ToInt32(product.parttypeid)) 
            {
                case 1: CheckAvailabityAndAddAndDelete(product, 1);  break;
                case 2: CheckAvailabityAndAddAndDelete(product, 2);  break;
                case 3: CheckAvailabityAndAddAndDelete(product, 3);  break;
                case 4: CheckAvailabityAndAddAndDelete(product, 4);  break;
                case 5: CheckAvailabityAndAddAndDelete(product, 5);  break;
                case 6: CheckAvailabityAndAddAndDelete(product, 6);  break;
                case 7: CheckAvailabityAndAddAndDelete(product, 7);  break;
                case 8: CheckAvailabityAndAddAndDelete(product, 8);  break;
                }
        }
        private void CheckSborka_Click(object sender, RoutedEventArgs e)
        {
            int id = Sborka.GetPart(1);
            var cpu = Core.ContextHOME.basepart.FirstOrDefault(x => x.id == id);
            string _cpuName = cpu?.name ?? "Отсутствует";

            id = Sborka.GetPart(2);
            var gpu = Core.ContextHOME.basepart.FirstOrDefault(x => x.id == id);
            string _gpuName = gpu?.name ?? "Отсутствует";

            id = Sborka.GetPart(3);
            var ram = Core.ContextHOME.basepart.FirstOrDefault(x => x.id == id);
            string _ramName = ram?.name ?? "Отсутствует";

            id = Sborka.GetPart(4);
            var motherboard = Core.ContextHOME.basepart.FirstOrDefault(x => x.id == id);
            string _motherboardName = motherboard?.name ?? "Отсутствует";

            id = Sborka.GetPart(5);
            var caase = Core.ContextHOME.basepart.FirstOrDefault(x => x.id == id);
            string _caseName = caase?.name ?? "Отсутствует";

            id = Sborka.GetPart(6);
            var powersuply = Core.ContextHOME.basepart.FirstOrDefault(x => x.id == id);
            string _powersuplyName = powersuply?.name ?? "Отсутствует";

            id = Sborka.GetPart(7);
            var processorcooler = Core.ContextHOME.basepart.FirstOrDefault(x => x.id == id);
            string _processorcoolerName = processorcooler?.name ?? "Отсутствует";

            id = Sborka.GetPart(8);
            var storagedevice = Core.ContextHOME.basepart.FirstOrDefault(x => x.id == id);
            string _storagedeviceName = storagedevice?.name ?? "Отсутствует";

            string sborkaDescription = $"CPU: \t\t\t{_cpuName}\nGPU: \t\t\t{_gpuName}\nRAM: \t\t\t{_ramName}\nМатеринская плата: \t{_motherboardName}\nКорпус: \t\t\t{_caseName}\nБлок питания: \t\t{_powersuplyName}\nКулер для процессора: \t{_processorcoolerName}\nЖесткий диск/SSD: \t{_storagedeviceName}";

            MessageBox.Show(sborkaDescription, "Ваша шедевро-сборка", MessageBoxButton.OK, MessageBoxImage.None);
        }

        private void CheckAvailabityAndAddAndDelete(basepart inputProduct, int inputPartID) 
        {
            // Проверка на то есть компонент в сборке
            if (Sborka.GetPart(inputPartID) != 0)
            {
                // Проверка на то, хотим ли мы добавить тот же самый компонент (т.е. => предлагаем удалить)
                if (Sborka.GetPart(inputPartID) == inputProduct.id)
                {
                    string isDelete = $"В вашей сборке уже есть данный компонент, хотите удалить его?";
                    var deleteRes = MessageBox.Show(isDelete, "Предупреждение!", MessageBoxButton.YesNo, MessageBoxImage.None);

                    switch (deleteRes)
                    {
                        // Если да => удаляем
                        case MessageBoxResult.Yes:
                            Sborka.SetPart(inputPartID, 0);
                            MessageBox.Show("Компонент удалён из сборки", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
                            break;

                        // Если нет => пропускаем
                        case MessageBoxResult.No:
                            return;
                    }
                }
                // Проверка на то какой компонент добавлен (т. е. => предлагаем заменить)
                else 
                {
                    int productID = Sborka.GetPart(inputPartID);                                                        // ID товара в сборке
                    string productName = Core.ContextHOME.basepart.First(x => x.id == productID).name;                  // Название товара в сборке
                    string productType = Core.ContextHOME.parttype.First(x => x.id == inputProduct.parttypeid).name;    // Тип товара

                    string description = $"В вашей сборке уже добавлен \n{productType}: {productName}\nХотите заменить компонент?";
                    var res = MessageBox.Show(description, "Предупреждение!", MessageBoxButton.YesNo, MessageBoxImage.None);

                    if (res == MessageBoxResult.Yes)
                    {
                        Sborka.SetPart(inputPartID, inputProduct.id);
                        MessageBox.Show("Вы заменили компонент в сборке!", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }  
            }
            // Компонента нет => просто добавляем
            else
            {
                Sborka.SetPart(inputPartID, inputProduct.id);
                MessageBox.Show("Компонент успешно добавлен в сборку!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
