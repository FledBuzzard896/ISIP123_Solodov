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
    /// Логика взаимодействия для GoodsGrid_I_like_It.xaml
    /// </summary>
    public partial class GoodsGrid_I_like_It : Page
    {
        public GoodsGrid_I_like_It()
        {
            InitializeComponent();
        }

        private void MyImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Ваша логика при клике
            MessageBox.Show("Картинка была кликнута!");
        }
    }
}
