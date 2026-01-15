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
using System.Windows.Shapes;

namespace ISIP123_Solodov_WPF
{
    /// <summary>
    /// Логика взаимодействия для CaptchaWindow.xaml
    /// </summary>
    public partial class CaptchaWindow : Window
    {
        string captcha = "qazwsxedcrvftgbyhnujmikolp1029384756";
        string text = "";
        public CaptchaWindow()
        {
            InitializeComponent();

            Random random = new Random();

            for (int i = 0; i < 10; i++) 
            {
                text += captcha[random.Next(0, captcha.Length + 1)];
            }

            mainText.Text = text;
        }
    }
}
