using OnlineBookStore.Services;
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

namespace OnlineBookStore.Views
{
    /// <summary>
    /// Interaction logic for SignUpView.xaml
    /// </summary>
    public partial class SignUpView : Window
    {
        private readonly AuthenticationService _authenticationService;

        public SignUpView()
        {
            InitializeComponent();
            _authenticationService = new AuthenticationService();
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            string username = Username.Text;
            string password = Password.Password;
            string address = Address.Text;
            string phone = Phone.Text;
            string userType = (UserType.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (_authenticationService.SignUp(userType, username, password, address, phone))
            {
                MessageBox.Show("User signed up successfully.");
                // Open the mainView after sign-up success
                MainWindow mainView = new MainWindow();
                mainView.Show();
                this.Close(); // Close the sign-up window after success

            }
            else
            {
                ErrorMessage.Visibility = Visibility.Visible;
                ErrorMessage.Text = "Username already exists.";
            }
        }
    }
}
