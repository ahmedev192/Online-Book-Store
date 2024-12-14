using OnlineBookStore.Services;
using OnlineBookStore.Utilities;
using System.Windows;

namespace OnlineBookStore.Views
{
    public partial class OrderConfirmationView : Window
    {
        private readonly CheckoutFacade _checkoutFacade;
        private readonly int _customerId;

        public OrderConfirmationView(int customerId)
        {
            InitializeComponent();
            _customerId = customerId;

            // Initialize the facade with the required services
            _checkoutFacade = new CheckoutFacade();

            LoadOrderDetails();
        }

        private void LoadOrderDetails()
        {
            var cartService = new CartService(); 
            var cart = cartService.GetCartByCustomerId(_customerId);

            if (cart != null)
            {
                OrderBooksGrid.ItemsSource = cart.Books;
                TotalPriceText.Text = cart.Books.Sum(b => b.Price).ToString("C");
                ShippingAddressTextBox.Text = cart.Customer.Address;
                ShippingPhoneTextBox.Text = cart.Customer.Phone;
            }
        }

        private void PlaceOrder_Click(object sender, RoutedEventArgs e)
        {
            var shippingAddress = ShippingAddressTextBox.Text;
            var shippingPhone = ShippingPhoneTextBox.Text;

            // Validate shipping details
            if (string.IsNullOrEmpty(shippingAddress) || string.IsNullOrEmpty(shippingPhone))
            {
                MessageBox.Show("Shipping details are required.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Calculate total amount
            var cartService = new CartService();
            var cart = cartService.GetCartByCustomerId(_customerId);
            if (cart == null || !cart.Books.Any())
            {
                MessageBox.Show("Your cart is empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            decimal totalAmount = cart.Books.Sum(book => book.Price);

            // Redirect to PaymentGatewayView
            var paymentView = new PaymentGatewayView(totalAmount);
            paymentView.ShowDialog(); // Open payment screen modally

            // Handle payment result
            if (paymentView.PaymentSuccessful)
            {
                _checkoutFacade.Checkout(_customerId);
                // Clear cart and redirect back to CartView
                var clearCartCommand = new ClearCartCommand(new CartService(), _customerId);
                clearCartCommand.Execute();
                MessageBox.Show("Payment successful! Your order has been placed.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                this.Close(); // Close order confirmation
            }
            else
            {
                MessageBox.Show("Payment failed. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
