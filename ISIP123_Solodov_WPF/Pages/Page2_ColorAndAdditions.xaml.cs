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
    /// Логика взаимодействия для Page2_ColorAndAdditions.xaml
    /// </summary>
    public partial class Page2_ColorAndAdditions : Page
    {
        private string _model;
        private string _engine;
        public Page2_ColorAndAdditions(string model, string engine) 
        {
            InitializeComponent();
            _model = model;
            _engine = engine;
        }
    }
}
