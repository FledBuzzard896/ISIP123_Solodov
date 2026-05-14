using ShutAndKing.Classes;
using ShutAndKing.DB_Models;
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

namespace ShutAndKing.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для ChangeRole_Dialog.xaml
    /// </summary>
    public partial class ChangeRole_Dialog : Window
    {
        private int thisUserID;
        private Users thisUser;

        public ChangeRole_Dialog(int inputUserID)
        {
            InitializeComponent();
            this.thisUserID = inputUserID;
            Loaded += PageLoaded;
        }


        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }


        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedRole = RoleCombo.SelectedItem as Roles;
            if (selectedRole != null)
            {
                thisUser.RoleID = selectedRole.ID;
                Core.ContextKIP_Local.SaveChanges();
                DialogResult = true;
            }
            else 
            {
                MessageBox.Show("Вы не выбрали новую роль пользователя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }


        private void PageLoaded(object sender, RoutedEventArgs e) 
        {
            thisUser = Core.ContextKIP_Local.Users.FirstOrDefault(x => x.ID == thisUserID);
            if (thisUser == null)
            {
                MessageBox.Show("Пользователь не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Stop);
                this.DialogResult = false;
            }

            top.Text = $"Текущая роль {thisUser.Login}: {thisUser.Roles.Title}";

            var listOfRoles = Core.ContextKIP_Local.Roles.Where(x => x.ID != thisUser.RoleID).ToList();
            RoleCombo.ItemsSource = listOfRoles;
        }
    }
}
