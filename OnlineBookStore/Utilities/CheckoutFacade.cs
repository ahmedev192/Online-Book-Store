using OnlineBookStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.Utilities
{
    public class CheckoutFacade
    {
        private readonly CartService _cartService;
        private readonly OrderService _orderService;

        public CheckoutFacade(CartService cartService, OrderService orderService)
        {
            _cartService = cartService;
            _orderService = orderService;
        }

        public void Checkout(int customerId)
        {
            var cart = _cartService.GetCartByCustomerId(customerId);
            if (cart == null || !cart.Books.Any())
            {
                Console.WriteLine("Cart is empty. Cannot proceed to checkout.");
                return;
            }

            var order = _orderService.PlaceOrder(customerId, cart.Books);
            var clearCartCommand = new ClearCartCommand(_cartService, customerId);
            clearCartCommand.Execute(); // Execute the command to clear the cart
            Console.WriteLine($"Order placed successfully with ID: {order.OrderId}");
        }
    }

}
