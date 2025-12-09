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
    /// Логика взаимодействия для Page5_EndForm.xaml
    /// </summary>
    public partial class Page5_EndForm : Page
    {
        public Page5_EndForm()
        {
            InitializeComponent();
        }

        bool part1, part2, part3;

        private void FullName_TextChanged(object sender, TextChangedEventArgs e)
        {
            part1 = false;
            string patern = "1234567890!@#№$;%^:&?*()_-+=.,/|\\\"'";

            bool containsAny = patern.Any(x => FullName.Text.Contains(x));

            if (containsAny == false)
            {
                part1 = true;
                
            }
        }

        private void Mail_TextChanged(object sender, TextChangedEventArgs e)
        {
            part2 = false;
            string patern1 = "@";
            string patern2 = ".";

            bool containsAt = patern1.Any(x => Mail.Text.Contains(x));
            bool containsDot = patern2.Any(x => Mail.Text.Contains(x));

            if (containsAt && containsDot && Mail.Text.Length >= 6)
            {
                part2 = true;
            }
        }

        private void PhoneNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            part3 = false;
            if (PhoneNum.Text.Contains(" ") == false && PhoneNum.Text.Length >= 7)
            {
                part3 = true;
            }
        }

        private void ToBack_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack) 
            {
                NavigationService.GoBack();
            }
        }

        private void ToNext_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void bttn_Click(object sender, RoutedEventArgs e)
        {
            if (part1 == false) { MessageBox.Show("В имени не может быть цифр и спец символов!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); return; }
            else if (part2 == false) { MessageBox.Show("Почта должна быть хотя бы 9 символов в длину и содержать специальные знаки (@ и .)", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            else if (part3 == false) { MessageBox.Show("Телефон введен неверно!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }

            if (part1 && part2 && part3)
            {
                MessageBox.Show("Форма отправлена!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Заполните всю форму!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TextBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text[0]);
        }
    }
}
