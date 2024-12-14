using OnlineBookStore.Services;
using OnlineBookStore.Models;
using System.Linq;
using System.Windows;
using OnlineBookStore.Utilities;

namespace OnlineBookStore.Views
{
    public partial class ManageOrdersView : Window
    {
        private readonly OrderManagementService _orderManagementService;

        public ManageOrdersView()
        {
            InitializeComponent();
            _orderManagementService = new OrderManagementService();
            LoadOrders();
        }

        private void LoadOrders()
        {
            var orders = _orderManagementService.GetAllOrders()
                .Select(o => new
                {
                    o.OrderId,
                    o.UserId,
                    o.Status,
                    OrderDate = o.OrderDate.ToString("yyyy-MM-dd HH:mm:ss"),
                })
                .ToList();

            OrdersDataGrid.ItemsSource = orders;
        }

        private void ConfirmOrder_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrder = OrdersDataGrid.SelectedItem as dynamic;
            if (selectedOrder != null)
            {
                var orderId = selectedOrder.OrderId;
                var success = _orderManagementService.ConfirmOrder(orderId); // Ensure ConfirmOrder is implemented in OrderService
                if (success)
                {
                    MessageBox.Show("Order confirmed successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadOrders();
                }
                else
                {
                    MessageBox.Show("Failed to confirm order. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select an order to confirm.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CancelOrder_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrder = OrdersDataGrid.SelectedItem as dynamic;
            if (selectedOrder != null)
            {
                var orderId = selectedOrder.OrderId;
                var success = _orderManagementService.CancelOrder(orderId);
                if (success)
                {
                    MessageBox.Show("Order canceled successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadOrders();
                }
                else
                {
                    MessageBox.Show("Failed to cancel order. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select an order to cancel.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

   

    }
}
