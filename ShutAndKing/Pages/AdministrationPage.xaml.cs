using ShutAndKing.Classes;
using ShutAndKing.Dialogs;
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
using Microsoft.EntityFrameworkCore;

namespace ShutAndKing.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdministrationPage.xaml
    /// </summary>
    public partial class AdministrationPage : Page
    {
        public AdministrationPage()
        {
            Loaded += Page_Loaded;
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MenuListBox.SelectedIndex = 0;
            SetActivePanel("Complaints");

            LoadComplaints();
            LoadDefrostRequests();
            LoadAuthorRequests();
            LoadFrozenData();
            LoadUsers();
        }
        private void SetActivePanel(string panelTag)
        {
            Panel_Complaints.Visibility = panelTag == "Complaints" ? Visibility.Visible : Visibility.Collapsed;
            Panel_DefrostRequests.Visibility = panelTag == "Defrost" ? Visibility.Visible : Visibility.Collapsed;
            Panel_AuthorRequests.Visibility = panelTag == "AuthorRole" ? Visibility.Visible : Visibility.Collapsed;
            Panel_Frozen.Visibility = panelTag == "Frozen" ? Visibility.Visible : Visibility.Collapsed;
            Panel_Users.Visibility = panelTag == "Users" ? Visibility.Visible : Visibility.Collapsed;
        }

        private void MenuListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsLoaded) return;

            if (MenuListBox.SelectedItem is ListBoxItem item)
            {
                string tag = item.Tag.ToString();
                SetActivePanel(tag);

                // Опционально: обновить данные только для выбранной панели
                switch (tag)
                {
                    case "Complaints": LoadComplaints(); break;
                    case "Defrost": LoadDefrostRequests(); break;
                    case "AuthorRole": LoadAuthorRequests(); break;
                    case "Frozen": LoadFrozenData(); break;
                    case "Users": LoadUsers(); break;
                }
            }
        }

        #region Загрузка данных
        private void LoadComplaints()
        {
            var list = Core.ContextHOME.Complaints
                .Include(c => c.Users)
                .Include(c => c.Books)
                .Include(c => c.UserReviews)
                .Include(c => c.Users)  
                .ToList()
                .Select(c => new
                {
                    c.ID,
                    UserName = c.Users.Name ?? "Unknown",
                    Target = c.BookID != null ? $"Книга: {c.Books.Title}" :
                             c.ReviewID != null ? $"Отзыв #{c.ReviewID} от {c.UserReviews.Users.Name}" :
                             c.AuthorID != null ? $"Автор: {c.Users.Name}" :
                             "Неизвестно",
                    c.Reason
                })
                .ToList();
            Panel_Complaints.ItemsSource = list;
        }

        private void LoadDefrostRequests()
        {
            var list = Core.ContextHOME.DefrostingApplication
                .Include(d => d.Users)   // возможно, для AccountID
                .Include(d => d.Users1)  // для UserID
                .Include(d => d.Books)
                .ToList()
                .Select(d => new
                {
                    d.ID,
                    UserName = d.Users1.Name ?? d.Users.Name ?? "Unknown",
                    TargetType = d.BookID != null ? "Книга" : "Пользователь",
                    TargetID = d.BookID ?? d.AccountID ?? 0,
                    d.Reason
                }).ToList();
            Panel_DefrostRequests.ItemsSource = list;
        }

        private void LoadAuthorRequests()
        {
            var list = Core.ContextHOME.AuthorApplication
                .Include(a => a.Users)
                .Select(a => new
                {
                    a.ID,
                    UserName = a.Users.Name,
                    a.Reason
                }).ToList();
            Panel_AuthorRequests.ItemsSource = list;
        }

        private void LoadFrozenData()
        {
            var frozenBooks = Core.ContextHOME.Books.Where(b => b.Status == "Заморожена").ToList();
            FrozenBooksGrid.ItemsSource = frozenBooks;

            var frozenUsers = Core.ContextHOME.Users.Where(u => u.Status == "Заморожен").ToList();
            FrozenUsersGrid.ItemsSource = frozenUsers;
        }

        private void LoadUsers()
        {
            var users = Core.ContextHOME.Users.ToList();
            Users_LB.ItemsSource = users;
        }
        #endregion

        #region Обработчики для жалоб
        private void AcceptComplaint_Click(object sender, RoutedEventArgs e)
        {
            if (Panel_Complaints.SelectedItem is null)
            {
                MessageBox.Show("Выберите жалобу");
                return;
            }
            int complaintId = (int)Panel_Complaints.SelectedItem.GetType().GetProperty("ID").GetValue(Panel_Complaints.SelectedItem);

            var complaint = Core.ContextHOME.Complaints.Find(complaintId);
            if (complaint != null)
            {
                if (complaint.BookID != null)
                {
                    var book = Core.ContextHOME.Books.Find(complaint.BookID);
                    if (book != null) book.Status = "Заморожена";
                }
                else if (complaint.ReviewID != null)
                {
                    // Потребуется поле IsFrozen в UserReviews
                    // var review = Core.ContextHOME.UserReviews.Find(complaint.ReviewID);
                    // if (review != null) review.IsFrozen = true;
                }
                Core.ContextHOME.Complaints.Remove(complaint);
                Core.ContextHOME.SaveChanges();
            }
            LoadComplaints();
        }

        private void RejectComplaint_Click(object sender, RoutedEventArgs e)
        {
            if (Panel_Complaints.SelectedItem is null) return;
            int complaintId = (int)Panel_Complaints.SelectedItem.GetType().GetProperty("ID").GetValue(Panel_Complaints.SelectedItem);

            var complaint = Core.ContextHOME.Complaints.Find(complaintId);
            if (complaint != null) Core.ContextHOME.Complaints.Remove(complaint);
            Core.ContextHOME.SaveChanges();
            LoadComplaints();
        }
        #endregion

        #region Заявки на разморозку
        private void AcceptDefrost_Click(object sender, RoutedEventArgs e)
        {
            if (Panel_DefrostRequests.SelectedItem is null)
            {
                MessageBox.Show("Выберите заявку на разморозку.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int requestId = (int)Panel_DefrostRequests.SelectedItem.GetType().GetProperty("ID").GetValue(Panel_DefrostRequests.SelectedItem);
            var request = Core.ContextHOME.DefrostingApplication.Find(requestId);
            if (request == null) return;

            if (request.BookID != null)
            {
                var book = Core.ContextHOME.Books.Find(request.BookID);
                if (book != null && book.Status == "Заморожена")
                    book.Status = "Активна";
            }
            else if (request.AccountID != null)
            {
                var user = Core.ContextHOME.Users.Find(request.AccountID);
                if (user != null && user.Status == "Заморожена")
                    user.Status = "Активна";
            }
            Core.ContextHOME.DefrostingApplication.Remove(request);
            Core.ContextHOME.SaveChanges();

            LoadDefrostRequests();
            MessageBox.Show("Заявка принята, объект разморожен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void RejectDefrost_Click(object sender, RoutedEventArgs e)
        {
            if (Panel_DefrostRequests.SelectedItem is null)
            {
                MessageBox.Show("Выберите заявку на разморозку.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int requestId = (int)Panel_DefrostRequests.SelectedItem.GetType().GetProperty("ID").GetValue(Panel_DefrostRequests.SelectedItem);
            var request = Core.ContextHOME.DefrostingApplication.Find(requestId);
            if (request != null)
            {
                Core.ContextHOME.DefrostingApplication.Remove(request);
                Core.ContextHOME.SaveChanges();
            }
            LoadDefrostRequests();
            MessageBox.Show("Заявка отклонена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion

        #region Заявки на авторство
        private void AcceptAuthorRole_Click(object sender, RoutedEventArgs e)
        {
            if (Panel_AuthorRequests.SelectedItem is null)
            {
                MessageBox.Show("Выберите заявку на роль автора.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int requestId = (int)Panel_AuthorRequests.SelectedItem.GetType().GetProperty("ID").GetValue(Panel_AuthorRequests.SelectedItem);
            var request = Core.ContextHOME.AuthorApplication.Include(a => a.Users).FirstOrDefault(a => a.ID == requestId);
            if (request == null) return;

            var authorRole = Core.ContextHOME.Roles.FirstOrDefault(r => r.Title == "Автор");
            if (authorRole == null)
            {
                MessageBox.Show("Роль 'Автор' не найдена в базе данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (request.Users != null)
                request.Users.RoleID = authorRole.ID;

            Core.ContextHOME.AuthorApplication.Remove(request);
            Core.ContextHOME.SaveChanges();

            LoadAuthorRequests();
            LoadUsers();
            MessageBox.Show("Пользователю назначена роль автора.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void RejectAuthorRole_Click(object sender, RoutedEventArgs e)
        {
            if (Panel_AuthorRequests.SelectedItem is null)
            {
                MessageBox.Show("Выберите заявку на роль автора.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int requestId = (int)Panel_AuthorRequests.SelectedItem.GetType().GetProperty("ID").GetValue(Panel_AuthorRequests.SelectedItem);
            var request = Core.ContextHOME.AuthorApplication.Find(requestId);
            if (request != null)
            {
                Core.ContextHOME.AuthorApplication.Remove(request);
                Core.ContextHOME.SaveChanges();
            }
            LoadAuthorRequests();
            MessageBox.Show("Заявка отклонена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion

        #region Пользователи – дополнительные действия
        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            dynamic user = button?.Tag;
            if (user == null) return;

            int userId = user.ID;
            ChangePassword_Dialog dialog = new ChangePassword_Dialog(userId);
            dialog.ShowDialog();
            LoadUsers(); // обновить таблицу пользователей
        }

        private void AssignRole_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            dynamic user = button?.Tag;
            if (user == null) return;

            int userId = user.ID;
            ChangeRole_Dialog dialog = new ChangeRole_Dialog(userId);
            if (dialog.ShowDialog() == true)
                LoadUsers(); // обновить после изменения роли
        }
        #endregion
    }
}
