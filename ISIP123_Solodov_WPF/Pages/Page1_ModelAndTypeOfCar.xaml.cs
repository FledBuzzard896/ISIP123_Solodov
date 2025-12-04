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
    /// Логика взаимодействия для Page1_ModelAndTypeOfCar.xaml
    /// </summary>
    public partial class Page1_ModelAndTypeOfCar : Page
    {
        string selectedModel = "";
        string selectedEngine = "";

        public Page1_ModelAndTypeOfCar()
        {
            InitializeComponent();
        }

        private void ModelRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Model1_type.Visibility = Visibility.Collapsed;
            Model2_type.Visibility = Visibility.Collapsed;
            Model3_type.Visibility = Visibility.Collapsed;
            Model4_type.Visibility = Visibility.Collapsed;

            if (sender is RadioButton radioButton) 
            {
                switch (radioButton.Name) 
                {
                    case "Model1":
                        Model1_type.Visibility = Visibility.Visible;  break;
                    case "Model2":
                        Model2_type.Visibility = Visibility.Visible;  break;
                    case "Model3":
                        Model3_type.Visibility = Visibility.Visible;  break;
                    case "Model4":
                        Model4_type.Visibility = Visibility.Visible;  break;
                }
                selectedModel = radioButton.Content.ToString();
            }
        }

        private void EngineRadioButton_Checked(object sender, RoutedEventArgs e) 
        {
            if (sender is RadioButton radioButton)
            {
                selectedEngine = radioButton.Content.ToString();
            }
        }

        private void NextBttn_Click(object sender, RoutedEventArgs e)
        {
            if (selectedModel == "" || selectedEngine == "") 
            {
                MessageBox.Show("Выберите модель и тип двигателя автомобиля!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Page2_ColorAndAdditions page2 = new Page2_ColorAndAdditions(selectedModel, selectedEngine);
            NavigationService.Navigate(page2);
        }
        
    }
}
