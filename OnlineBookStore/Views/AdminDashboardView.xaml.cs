using System.Windows;

namespace OnlineBookStore.Views
{
    public partial class AdminDashboardView : Window
    {
        public AdminDashboardView()
        {
            InitializeComponent();
        }

        private void ViewStatistics_Click(object sender, RoutedEventArgs e)
        {
            var statisticsView = new StatisticsMonitoringView();
            statisticsView.Show();
        }

        private void ManageBooks_Click(object sender, RoutedEventArgs e)
        {
            var addBookView = new ManageBooksView();
            addBookView.Show();
        }

        private void ViewOrders_Click(object sender, RoutedEventArgs e)
        {
            var manageOrdersView = new ManageOrdersView();
            manageOrdersView.Show();
        }

        private void UpdateOrderStatus_Click(object sender, RoutedEventArgs e)
        {
            var manageOrdersView = new ManageOrdersView();
            manageOrdersView.Show();
        }


        private void ManageCategories_Click(object sender, RoutedEventArgs e)
        {
            // Open the Category Management View
            var categoryManagementView = new CategoryManagementView();
            categoryManagementView.ShowDialog();
        }

        private void NotifyCustomers_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Notify customers functionality will be added soon.", "Notification");
        }
    }
}
