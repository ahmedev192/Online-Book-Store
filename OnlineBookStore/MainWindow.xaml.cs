using OnlineBookStore.Models;
using OnlineBookStore.Services;
using OnlineBookStore.Views;
using System.Windows;

namespace OnlineBookStore
{
    public partial class MainWindow : Window
    {
        private readonly AuthenticationService _authService;

        public MainWindow()
        {
            InitializeComponent();
            _authService = new AuthenticationService();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text;
            var password = PasswordBox.Password;

            var user = _authService.LogIn(username, password);

            if (user != null)
            {
                if (user is Customer)
                {
                    var customerDashboard = new CustomerDashboardView();
                    customerDashboard.Show();
                }
                else if (user is Admin)
                {
                    var adminDashboard = new AdminDashboardView();
                    adminDashboard.Show();
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            var signUpView = new SignUpView();
            signUpView.Show();
            this.Close();
        }
    }
}
