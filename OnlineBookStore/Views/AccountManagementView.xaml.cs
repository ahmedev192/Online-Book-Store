using OnlineBookStore.Services;
using OnlineBookStore.Utilities;
using System.Windows;

namespace OnlineBookStore.Views
{
    /// <summary>
    /// Interaction logic for AccountManagementView.xaml
    /// </summary>
    public partial class AccountManagementView : Window
    {
        private readonly UserManagementService _userService;
        private readonly int _userId;

        public AccountManagementView()
        {
            InitializeComponent();
            _userService = new UserManagementService();
            _userId = UserSession.Instance.UserId;

            LoadUserData();
        }

        // Load current user data into the fields
        private void LoadUserData()
        {
            var user = _userService.GetUserById(_userId);
            if (user != null)
            {
                Username.Text = user.Username;
                Password.Password = user.Password; // Use PasswordBox for password
                Address.Text = user.Address;
                Phone.Text = user.Phone;
            }
            else
            {
                MessageBox.Show("Unable to load user details.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string username = Username.Text;
            string password = Password.Password;
            string address = Address.Text;
            string phone = Phone.Text;

            if (_userService.UpdateAccountDetails(_userId, username, password, address, phone))
            {
                Message.Text = "Account details updated successfully.";
                Message.Visibility = Visibility.Visible;
            }
            else
            {
                Message.Text = "Failed to update account details.";
                Message.Visibility = Visibility.Visible;
            }
        }
    }
}
