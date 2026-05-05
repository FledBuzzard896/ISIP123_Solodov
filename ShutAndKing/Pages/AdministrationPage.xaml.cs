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
            using (var context = new ContextHOME())
            {
                var list = context.Complaints
                    .Include(c => c.Users)
                    .Include(c => c.Books)
                    .Include(c => c.UserReviews)
                    .ToList()
                    .Select(c => new
                    {
                        c.ID,
                        UserName = c.Users?.Name ?? "Unknown",
                        Target = c.BookID != null ? $"Книга: {c.Books.Title}" :
                                 c.ReviewID != null ? $"Отзыв #{c.ReviewID}" : "Неизвестно",
                        c.Reason
                    }).ToList();
                ComplaintsGrid.ItemsSource = list;
            }
        }

        private void LoadDefrostRequests()
        {
            using (var context = new ContextHOME())
            {
                var list = context.DefrostingApplication
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
        }

        private void LoadAuthorRequests()
        {
            using (var context = new ContextHOME())
            {
                var list = context.AuthorApplication
                    .Include(a => a.Users)
                    .Select(a => new
                    {
                        a.ID,
                        UserName = a.Users.Name,
                        a.Reason
                    }).ToList();
                AuthorRequestsGrid.ItemsSource = list;
            }
        }

        private void LoadFrozenData()
        {
            using (var context = new ContextHOME())
            {
                var frozenBooks = context.Books.Where(b => b.Status == "Frozen").ToList();
                FrozenBooksGrid.ItemsSource = frozenBooks;

                var frozenUsers = context.Users.Where(u => u.Status == "Frozen").ToList();
                FrozenUsersGrid.ItemsSource = frozenUsers;

                // Если добавили поле IsFrozen в UserReviews – раскомментировать
                // var frozenReviews = context.UserReviews.Where(r => r.IsFrozen == true).ToList();
                // FrozenReviewsGrid.ItemsSource = frozenReviews;
            }
        }

        private void LoadUsers()
        {
            using (var context = new ContextHOME())
            {
                var users = context.Users.Include(u => u.Roles)
                    .Select(u => new
                    {
                        u.ID,
                        u.Login,
                        u.Name,
                        u.Email,
                        RoleName = u.Roles.Name,
                        u.Status
                    }).ToList();
                UsersGrid.ItemsSource = users;
            }
        }
        #endregion

        #region Обработчики для жалоб
        private void AcceptComplaint_Click(object sender, RoutedEventArgs e)
        {
            var selected = ComplaintsGrid.SelectedItem;
            if (selected == null) { MessageBox.Show("Выберите жалобу"); return; }
            int complaintId = (int)selected.GetType().GetProperty("ID").GetValue(selected);
            using (var context = new ContextHOME())
            {
                var complaint = context.Complaints.Find(complaintId);
                if (complaint != null)
                {
                    if (complaint.BookID != null)
                    {
                        var book = context.Books.Find(complaint.BookID);
                        if (book != null) book.Status = "Frozen";
                    }
                    else if (complaint.ReviewID != null)
                    {
                        // Потребуется поле IsFrozen в UserReviews
                        // var review = context.UserReviews.Find(complaint.ReviewID);
                        // if (review != null) review.IsFrozen = true;
                    }
                    context.Complaints.Remove(complaint);
                    context.SaveChanges();
                }
            }
            LoadComplaints();
        }

        private void RejectComplaint_Click(object sender, RoutedEventArgs e)
        {
            var selected = ComplaintsGrid.SelectedItem;
            if (selected == null) return;
            int complaintId = (int)selected.GetType().GetProperty("ID").GetValue(selected);
            using (var context = new ContextHOME())
            {
                var complaint = context.Complaints.Find(complaintId);
                if (complaint != null) context.Complaints.Remove(complaint);
                context.SaveChanges();
            }
            LoadComplaints();
        }
        #endregion

        #region Заявки на разморозку
        private void AcceptDefrost_Click(object sender, RoutedEventArgs e)
        {
            var selected = DefrostRequestsGrid.SelectedItem;
            if (selected == null)
            {
                MessageBox.Show("Выберите заявку на разморозку.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int requestId = (int)selected.GetType().GetProperty("ID").GetValue(selected);
            using (var context = new ContextHOME())
            {
                var request = context.DefrostingApplication.Find(requestId);
                if (request == null) return;

                if (request.BookID != null)
                {
                    var book = context.Books.Find(request.BookID);
                    if (book != null && book.Status == "Frozen")
                        book.Status = "Active";
                }
                else if (request.AccountID != null)
                {
                    var user = context.Users.Find(request.AccountID);
                    if (user != null && user.Status == "Frozen")
                        user.Status = "Active";
                }
                context.DefrostingApplication.Remove(request);
                context.SaveChanges();
            }
            LoadDefrostRequests();
            MessageBox.Show("Заявка принята, объект разморожен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void RejectDefrost_Click(object sender, RoutedEventArgs e)
        {
            var selected = DefrostRequestsGrid.SelectedItem;
            if (selected == null)
            {
                MessageBox.Show("Выберите заявку на разморозку.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int requestId = (int)selected.GetType().GetProperty("ID").GetValue(selected);
            using (var context = new ContextHOME())
            {
                var request = context.DefrostingApplication.Find(requestId);
                if (request != null)
                {
                    context.DefrostingApplication.Remove(request);
                    context.SaveChanges();
                }
            }
            LoadDefrostRequests();
            MessageBox.Show("Заявка отклонена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion

        #region Заявки на авторство
        private void AcceptAuthorRole_Click(object sender, RoutedEventArgs e)
        {
            var selected = AuthorRequestsGrid.SelectedItem;
            if (selected == null)
            {
                MessageBox.Show("Выберите заявку на роль автора.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int requestId = (int)selected.GetType().GetProperty("ID").GetValue(selected);
            using (var context = new ContextHOME())
            {
                var request = context.AuthorApplication.Include(a => a.Users).FirstOrDefault(a => a.ID == requestId);
                if (request == null) return;

                var authorRole = context.Roles.FirstOrDefault(r => r.Name == "Author");
                if (authorRole == null)
                {
                    MessageBox.Show("Роль 'Author' не найдена в базе данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var user = request.Users;
                if (user != null)
                    user.RoleID = authorRole.ID;

                context.AuthorApplication.Remove(request);
                context.SaveChanges();
            }
            LoadAuthorRequests();
            LoadUsers();
            MessageBox.Show("Пользователю назначена роль автора.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void RejectAuthorRole_Click(object sender, RoutedEventArgs e)
        {
            var selected = AuthorRequestsGrid.SelectedItem;
            if (selected == null)
            {
                MessageBox.Show("Выберите заявку на роль автора.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int requestId = (int)selected.GetType().GetProperty("ID").GetValue(selected);
            using (var context = new ContextHOME())
            {
                var request = context.AuthorApplication.Find(requestId);
                if (request != null)
                {
                    context.AuthorApplication.Remove(request);
                    context.SaveChanges();
                }
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
        }

        private void AssignRole_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            dynamic user = button?.Tag;
            if (user == null) return;

            int userId = user.ID;
            ChangeRole_Dialog dialog = new ChangeRole_Dialog(userId);
            dialog.ShowDialog();
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Открыть диалог добавления пользователя", "TODO", MessageBoxButton.OK);
        }
        #endregion
    }
}
