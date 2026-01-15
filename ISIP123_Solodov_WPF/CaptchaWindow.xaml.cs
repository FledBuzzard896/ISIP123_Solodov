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

        Random random = new Random();

        public CaptchaWindow()
        {
            InitializeComponent();
            
            GetCaptcha();
        }

        private void GetCaptcha() 
        {
            Random random = new Random();

            inputText.Text = "";
            mainText.Text = "";

            for (int i = 0; i < 10; i++)
            {
                text += captcha[random.Next(0, captcha.Length)];
            }

            mainText.Text = text;
            text = "";
        }

        private void CheckBtn_Click(object sender, RoutedEventArgs e)
        {
            if (inputText.Text == mainText.Text)
            {
                var res = MessageBox.Show("Капча пройдена! Закрыть окно?", "captha", MessageBoxButton.YesNo, MessageBoxImage.None, MessageBoxResult.No);
                if (res == MessageBoxResult.Yes) 
                {
                    Close();
                }
            }
            else 
            {
                GetCaptcha();
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("Закрыть окно?", "captha", MessageBoxButton.YesNo, MessageBoxImage.None, MessageBoxResult.No);
            if (res == MessageBoxResult.Yes)
            {
                Close();
            }
        }
    }
}
