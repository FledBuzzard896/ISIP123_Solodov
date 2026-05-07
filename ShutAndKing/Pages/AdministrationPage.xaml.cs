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
            InitializeComponent();
            Loaded += Page_Loaded;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadComplaints();
            LoadDefrostRequests();
            LoadAuthorRequests();
            LoadFrozenData();
            LoadUsers();
        }

        private void MenuListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Panel_Complaints.Visibility = Visibility.Collapsed;
            Panel_DefrostRequests.Visibility = Visibility.Collapsed;
            Panel_AuthorRequests.Visibility = Visibility.Collapsed;
            Panel_Frozen.Visibility = Visibility.Collapsed;
            Panel_Users.Visibility = Visibility.Collapsed;

            if (MenuListBox.SelectedItem is ListBoxItem item)
            {
                switch (item.Tag.ToString())
                {
                    case "Complaints":
                        Panel_Complaints.Visibility = Visibility.Visible;
                        LoadComplaints();
                        break;
                    case "Defrost":
                        Panel_DefrostRequests.Visibility = Visibility.Visible;
                        LoadDefrostRequests();
                        break;
                    case "AuthorRole":
                        Panel_AuthorRequests.Visibility = Visibility.Visible;
                        LoadAuthorRequests();
                        break;
                    case "Frozen":
                        Panel_Frozen.Visibility = Visibility.Visible;
                        LoadFrozenData();
                        break;
                    case "Users":
                        Panel_Users.Visibility = Visibility.Visible;
                        LoadUsers();
                        break;
                }
            }
        }

        #region Загрузка данных
        private void LoadComplaints()
        {
            var list = Core.ContextKIP.Complaints
                .Include(c => c.Users)       // подгружаем пользователя
                .Include(c => c.Books)       // подгружаем книгу
                .Include(c => c.UserReviews) // подгружаем отзыв
                .Select(c => new             // ← обязательно new { }
                {
                    c.ID,
                    UserName = c.Users.Name,           // имя пользователя из связанной таблицы
                    BookTitle = c.Books.Title,         // название книги (если жалоба на книгу)
                    c.Reason,
                    // Если нужно картинку – возьмите из Books или Users
                    CoverPath = c.Books.PathToCover
                })
                .ToList();
            ComplaintsGrid.ItemsSource = list;
        }

        private void LoadDefrostRequests()
        {
            var list = Core.ContextKIP.DefrostingApplication
                .Include(d => d.Users)
                .Include(d => d.Books)
                .ToList()
                .Select(d => new
                {
                    d.ID,
                    UserName = d.Users?.Name ?? "Unknown",
                    TargetType = d.BookID != null ? "Книга" : "Пользователь",
                    TargetID = d.BookID ?? d.AccountID ?? 0,
                    d.Reason
                }).ToList();
            DefrostRequestsGrid.ItemsSource = list;
        }

        private void LoadAuthorRequests()
        {
            var list = Core.ContextKIP.AuthorApplication
                .Include(a => a.Users)
                .Select(a => new
                {
                    a.ID,
                    UserName = a.Users.Name,
                    a.Reason
                }).ToList();
            AuthorRequestsGrid.ItemsSource = list;
        }

        private void LoadFrozenData()
        {
            var frozenBooks = Core.ContextKIP.Books.Where(b => b.Status == "Frozen").ToList();
            FrozenBooksGrid.ItemsSource = frozenBooks;

            var frozenUsers = Core.ContextKIP.Users.Where(u => u.Status == "Frozen").ToList();
            FrozenUsersGrid.ItemsSource = frozenUsers;

            // Если добавили поле IsFrozen в UserReviews – раскомментировать
            // var frozenReviews = Core.ContextHOME.UserReviews.Where(r => r.IsFrozen == true).ToList();
            // FrozenReviewsGrid.ItemsSource = frozenReviews;
        }

        private void LoadUsers()
        {
            var users = Core.ContextKIP.Users
                .Include(u => u.Roles)
                .Select(u => new
                {
                    u.ID,
                    u.Login,
                    u.Name,
                    u.Email,
                    RoleName = u.Roles.Title,
                    u.Status
                }).ToList();
            UsersGrid.ItemsSource = users;
        }
        #endregion

        #region Обработчики для жалоб
        private void AcceptComplaint_Click(object sender, RoutedEventArgs e)
        {
            if (ComplaintsGrid.SelectedItem is null)
            {
                MessageBox.Show("Выберите жалобу");
                return;
            }
            int complaintId = (int)ComplaintsGrid.SelectedItem.GetType().GetProperty("ID").GetValue(ComplaintsGrid.SelectedItem);

            var complaint = Core.ContextKIP.Complaints.Find(complaintId);
            if (complaint != null)
            {
                if (complaint.BookID != null)
                {
                    var book = Core.ContextKIP.Books.Find(complaint.BookID);
                    if (book != null) book.Status = "Frozen";
                }
                else if (complaint.ReviewID != null)
                {
                    // Потребуется поле IsFrozen в UserReviews
                    // var review = Core.ContextHOME.UserReviews.Find(complaint.ReviewID);
                    // if (review != null) review.IsFrozen = true;
                }
                Core.ContextKIP.Complaints.Remove(complaint);
                Core.ContextKIP.SaveChanges();
            }
            LoadComplaints();
        }

        private void RejectComplaint_Click(object sender, RoutedEventArgs e)
        {
            if (ComplaintsGrid.SelectedItem is null) return;
            int complaintId = (int)ComplaintsGrid.SelectedItem.GetType().GetProperty("ID").GetValue(ComplaintsGrid.SelectedItem);

            var complaint = Core.ContextKIP.Complaints.Find(complaintId);
            if (complaint != null) Core.ContextKIP.Complaints.Remove(complaint);
            Core.ContextKIP.SaveChanges();
            LoadComplaints();
        }
        #endregion

        #region Заявки на разморозку
        private void AcceptDefrost_Click(object sender, RoutedEventArgs e)
        {
            if (DefrostRequestsGrid.SelectedItem is null)
            {
                MessageBox.Show("Выберите заявку на разморозку.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int requestId = (int)DefrostRequestsGrid.SelectedItem.GetType().GetProperty("ID").GetValue(DefrostRequestsGrid.SelectedItem);
            var request = Core.ContextKIP.DefrostingApplication.Find(requestId);
            if (request == null) return;

            if (request.BookID != null)
            {
                var book = Core.ContextKIP.Books.Find(request.BookID);
                if (book != null && book.Status == "Frozen")
                    book.Status = "Active";
            }
            else if (request.AccountID != null)
            {
                var user = Core.ContextKIP.Users.Find(request.AccountID);
                if (user != null && user.Status == "Frozen")
                    user.Status = "Active";
            }
            Core.ContextKIP.DefrostingApplication.Remove(request);
            Core.ContextKIP.SaveChanges();

            LoadDefrostRequests();
            MessageBox.Show("Заявка принята, объект разморожен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void RejectDefrost_Click(object sender, RoutedEventArgs e)
        {
            if (DefrostRequestsGrid.SelectedItem is null)
            {
                MessageBox.Show("Выберите заявку на разморозку.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int requestId = (int)DefrostRequestsGrid.SelectedItem.GetType().GetProperty("ID").GetValue(DefrostRequestsGrid.SelectedItem);
            var request = Core.ContextKIP.DefrostingApplication.Find(requestId);
            if (request != null)
            {
                Core.ContextKIP.DefrostingApplication.Remove(request);
                Core.ContextKIP.SaveChanges();
            }
            LoadDefrostRequests();
            MessageBox.Show("Заявка отклонена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion

        #region Заявки на авторство
        private void AcceptAuthorRole_Click(object sender, RoutedEventArgs e)
        {
            if (AuthorRequestsGrid.SelectedItem is null)
            {
                MessageBox.Show("Выберите заявку на роль автора.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int requestId = (int)AuthorRequestsGrid.SelectedItem.GetType().GetProperty("ID").GetValue(AuthorRequestsGrid.SelectedItem);
            var request = Core.ContextKIP.AuthorApplication.Include(a => a.Users).FirstOrDefault(a => a.ID == requestId);
            if (request == null) return;

            var authorRole = Core.ContextKIP.Roles.FirstOrDefault(r => r.Title == "Author");
            if (authorRole == null)
            {
                MessageBox.Show("Роль 'Author' не найдена в базе данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (request.Users != null)
                request.Users.RoleID = authorRole.ID;

            Core.ContextKIP.AuthorApplication.Remove(request);
            Core.ContextKIP.SaveChanges();

            LoadAuthorRequests();
            LoadUsers();
            MessageBox.Show("Пользователю назначена роль автора.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void RejectAuthorRole_Click(object sender, RoutedEventArgs e)
        {
            if (AuthorRequestsGrid.SelectedItem is null)
            {
                MessageBox.Show("Выберите заявку на роль автора.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int requestId = (int)AuthorRequestsGrid.SelectedItem.GetType().GetProperty("ID").GetValue(AuthorRequestsGrid.SelectedItem);
            var request = Core.ContextKIP.AuthorApplication.Find(requestId);
            if (request != null)
            {
                Core.ContextKIP.AuthorApplication.Remove(request);
                Core.ContextKIP.SaveChanges();
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

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            // TODO: реализовать диалог добавления пользователя
            MessageBox.Show("Открыть диалог добавления пользователя", "TODO", MessageBoxButton.OK);
        }
        #endregion
    }
}
