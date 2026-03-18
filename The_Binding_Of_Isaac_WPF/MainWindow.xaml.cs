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

namespace The_Binding_Of_Isaac_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (MainFrame.Content is Pages.GenerateFloor || MainFrame.Content is Pages.Floor || MainFrame.Content is Pages.MenuNeutralRoom)
                {
                    return;
                }

                // Пытаемся получить текущую страницу из фрейма
                if (MainFrame.Content is Page currentPage)
                {
                    // Получаем NavigationService страницы
                    var navService = NavigationService.GetNavigationService(currentPage);

                    if (navService != null && navService.CanGoBack)
                    {
                        navService.GoBack();
                        e.Handled = true; // Помечаем событие обработанным
                    }
                    else
                    {
                        // Если нет истории назад, можно, например, закрыть окно
                        this.Close();
                    }
                }
            }
        }
    }
}
