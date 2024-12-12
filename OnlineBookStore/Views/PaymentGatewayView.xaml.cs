using OnlineBookStore.Utilities;
using System.Windows;

namespace OnlineBookStore.Views
{
    public partial class PaymentGatewayView : Window
    {
        private readonly PaymentContext _paymentContext;
        private readonly decimal _totalAmount;

        public bool PaymentSuccessful { get; private set; } // To track payment success

        public PaymentGatewayView(decimal amount)
        {
            InitializeComponent();
            _paymentContext = new PaymentContext();
            _totalAmount = amount;

            // Display the total amount
            TotalAmountText.Text = $"${_totalAmount:F2}";
        }


        private void Proceed_Click(object sender, RoutedEventArgs e)
        {
            // Determine selected payment method
            if (CreditCardRadio.IsChecked == true)
                _paymentContext.SetPaymentMethod(new CreditCardPayment());
            else if (PayPalRadio.IsChecked == true)
                _paymentContext.SetPaymentMethod(new PayPalPayment());
            else if (CODRadio.IsChecked == true)
                _paymentContext.SetPaymentMethod(new CashOnDeliveryPayment());
            else
            {
                MessageBox.Show("Please select a payment method.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Process the payment
            PaymentSuccessful = _paymentContext.ProcessPayment(_totalAmount);

            // Notify the user of the payment result
            MessageBox.Show(PaymentSuccessful ? "Payment successful!" : "Payment failed.", "Payment Status", MessageBoxButton.OK, MessageBoxImage.Information);

            // Close the payment view
            this.Close();
        }
    }
}
