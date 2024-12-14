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

    public partial class SignUpView : Window
    {
        private readonly UserManagementService _userService;

        public SignUpView()
        {
            InitializeComponent();
            _userService = new UserManagementService();
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            string username = Username.Text;
            string password = Password.Password;
            string address = Address.Text;
            string phone = Phone.Text;
            string userType = (UserType.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (_userService.SignUp(userType, username, password, address, phone))
            {
                MessageBox.Show("User signed up successfully.");
                MainWindow mainView = new MainWindow();
                mainView.Show();
                this.Close(); 

            }
            else
            {
                ErrorMessage.Visibility = Visibility.Visible;
                ErrorMessage.Text = "Username already exists.";
            }
        }
    }
}
