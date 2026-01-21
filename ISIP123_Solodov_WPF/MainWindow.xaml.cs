using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string path = @"C:\Users\234885\Documents\";
        string filePath;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenFileBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            
            if (openFileDialog.ShowDialog() == true) 
            {
                filePath = openFileDialog.FileName;

                mainText.Text = File.ReadAllText(filePath);

                this.Title = filePath;
            }
        }

        private void SaveFileBtn_Click(object sender, RoutedEventArgs e)
        {
            if (filePath == null) 
            {
                SaveKAKFileBtn_Click(sender, e);
            }
            else 
            {
                File.WriteAllText(filePath, mainText.Text);
                MessageBox.Show("Файл успешно сохранён!", "ыы бля", MessageBoxButton.OK, MessageBoxImage.Information);
            }  
        }

        private void SaveKAKFileBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Текстовый документ|*.txt";

            if (saveFileDialog.ShowDialog() == true && !string.IsNullOrWhiteSpace(saveFileDialog.FileName))
            {
                filePath = saveFileDialog.FileName;

                File.WriteAllText(filePath, mainText.Text);

                this.Title = filePath;
            }
        }

        private void CreateFileBtn_Click(object sender, RoutedEventArgs e)
        {
            mainText.Text = "";
            filePath = null;
            this.Title = "TextR";
        }
    }
}
