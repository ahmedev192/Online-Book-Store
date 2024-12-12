using OnlineBookStore.Utilities;
using OnlineBookStore.Views;
using System.Windows;

namespace OnlineBookStore.Views
{
    public partial class CustomerDashboardView : Window
    {
        public CustomerDashboardView()
        {
            InitializeComponent();
        }

        private void BrowseBooksButton_Click(object sender, RoutedEventArgs e)
        {
            new BooksView().Show();
        }

        private void ViewCartButton_Click(object sender, RoutedEventArgs e)
        {
            new CartView().Show();
        }

        private void TrackOrderButton_Click(object sender, RoutedEventArgs e)
        {
            new OrderStatusTrackingView().Show();
        }

        private void ManageAccountButton_Click(object sender, RoutedEventArgs e)
        {
            new AccountManagementView().Show();
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
    }
}
