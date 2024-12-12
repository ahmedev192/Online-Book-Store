using OnlineBookStore.Services;
using OnlineBookStore.ViewModels;
using System.Linq;
using System.Windows;

namespace OnlineBookStore.Views
{
    public partial class OrderStatusTrackingView : Window
    {
        private readonly OrderService _orderService;

        public OrderStatusTrackingView()
        {
            InitializeComponent();
            _orderService = new OrderService();
            LoadOrders();
        }

        private void LoadOrders()
        {
            try
            {
                var orders = _orderService.GetOrderHistory();
                OrderListView.ItemsSource = orders.Select(o => new OrderViewModel
                {
                    OrderId = o.OrderId,
                    OrderDate = o.OrderDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    Status = o.Status,
                    TotalPrice = _orderService.GetOrderTotalPrice(o.OrderId)
                }).ToList();
            }
            catch
            {
                MessageBox.Show("Error loading orders. Please try again later.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void CancelOrderButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrder = OrderListView.SelectedItem as OrderViewModel;

            if (selectedOrder == null)
            {
                MessageBox.Show("Please select an order to cancel.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (selectedOrder.Status != "Pending")
            {
                MessageBox.Show("Only pending orders can be canceled.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to cancel order {selectedOrder.OrderId}?", "Confirm Cancellation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                if (_orderService.CancelOrder(selectedOrder.OrderId))
                {
                    MessageBox.Show($"Order {selectedOrder.OrderId} has been canceled.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadOrders(); // Refresh the order list
                }
                else
                {
                    MessageBox.Show("Unable to cancel the order. Please try again later.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }


}
