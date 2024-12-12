using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.Utilities
{
    public interface IPaymentMethod
    {
        bool ProcessPayment(decimal amount);
    }

    public class CreditCardPayment : IPaymentMethod
    {
        public bool ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Processing credit card payment of {amount:C}");
            return true; // Simulate success
        }
    }

    public class PayPalPayment : IPaymentMethod
    {
        public bool ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Processing PayPal payment of {amount:C}");
            return true; // Simulate success
        }
    }

    public class CashOnDeliveryPayment : IPaymentMethod
    {
        public bool ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Processing cash on delivery payment of {amount:C}");
            return true; // Simulate success
        }
    }
    public class PaymentContext
    {
        private IPaymentMethod _paymentMethod;

        public void SetPaymentMethod(IPaymentMethod paymentMethod)
        {
            _paymentMethod = paymentMethod;
        }

        public bool ProcessPayment(decimal amount)
        {
            if (_paymentMethod == null)
            {
                throw new InvalidOperationException("Payment method is not set.");
            }

            return _paymentMethod.ProcessPayment(amount);
        }
    }


}
