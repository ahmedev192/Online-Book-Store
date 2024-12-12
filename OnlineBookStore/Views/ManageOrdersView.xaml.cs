using OnlineBookStore.Services;
using OnlineBookStore.Models;
using System.Linq;
using System.Windows;

namespace OnlineBookStore.Views
{
    public partial class ManageOrdersView : Window
    {
        private readonly OrderService _orderService;

        public ManageOrdersView()
        {
            InitializeComponent();
            _orderService = new OrderService();
            LoadOrders();
        }

        private void LoadOrders()
        {
            var orders = _orderService.GetOrderHistory()
                .Select(o => new
                {
                    o.OrderId,
                    o.CustomerId,
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
                var success = _orderService.ConfirmOrder(orderId); // Ensure ConfirmOrder is implemented in OrderService
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
                var success = _orderService.CancelOrder(orderId);
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
