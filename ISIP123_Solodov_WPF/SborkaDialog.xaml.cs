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

namespace ISIP123_Solodov_WPF
{
    /// <summary>
    /// Логика взаимодействия для SborkaDialog.xaml
    /// </summary>
    public partial class SborkaDialog : Window
    {
        public SborkaDialog()
        {
            InitializeComponent();
        }

        private void savebtn_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text != "" && title.Text != "")
            {
                var newSborka = new assembly
                {
                    name = title.Text,
                    author = name.Text,
                };

                Core.ContextKIP.assembly.Add(newSborka);
                Core.ContextKIP.SaveChanges();

                for (int i = 1; i < Sborka.GetPartsArray().Length; i++) 
                {
                    var newPart = new partassembly 
                    {
                        partid = Sborka.GetPartsArray()[i],
                        assemblyid = newSborka.id,
                    };

                    Core.ContextKIP.partassembly.Add(newPart);
                    Core.ContextKIP.SaveChanges();
                }

                MessageBox.Show("Ваша крутая сборка сохранена!!!","Пнятно", MessageBoxButton.OK, MessageBoxImage.Information);
                Sborka.Clear();
                this.Close();
            }
            else { MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Stop); } 
        }
    }
}
