using OnlineBookStore.Models;
using OnlineBookStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.Utilities
{

    public interface ICommand
    {
        void Execute();
    }

    public class AddToCartCommand : ICommand
    {
        private readonly CartService _cartService;
        private readonly int _customerId;
        private readonly Book _book;

        public AddToCartCommand(CartService cartService, int customerId, Book book)
        {
            _cartService = cartService;
            _customerId = customerId;
            _book = book;
        }

        public void Execute()
        {
            var cart = _cartService.GetCartByCustomerId(_customerId) ?? new Cart
            {
                CustomerId = _customerId,
                Books = new List<Book>()
            };

            cart.Books.Add(_book);

            if (cart.CartId == 0) // New cart
            {
                _cartService.GetCartByCustomerId(_customerId)?.Books.Add(_book);
            }

            _cartService.SaveChanges();
            Console.WriteLine($"Book '{_book.Title}' added to cart.");
        }
    }
    public class RemoveFromCartCommand : ICommand
    {
        private readonly CartService _cartService;
        private readonly int _customerId;
        private readonly Book _book;

        public RemoveFromCartCommand(CartService cartService, int customerId, Book book)
        {
            _cartService = cartService;
            _customerId = customerId;
            _book = book;
        }

        public void Execute()
        {
            var cart = _cartService.GetCartByCustomerId(_customerId);

            if (cart == null || !cart.Books.Remove(_book))
            {
                Console.WriteLine($"Failed to remove book '{_book.Title}' from cart.");
                return;
            }

            _cartService.SaveChanges();
            Console.WriteLine($"Book '{_book.Title}' removed from cart.");
        }
    }




    public class ClearCartCommand : ICommand
    {
        private readonly CartService _cartService;
        private readonly int _customerId;

        public ClearCartCommand(CartService cartService, int customerId)
        {
            _cartService = cartService;
            _customerId = customerId;
        }

        public void Execute()
        {
            var cart = _cartService.GetCartByCustomerId(_customerId);

            if (cart == null)
            {
                Console.WriteLine("Cart is already empty.");
                return;
            }

            cart.Books.Clear();
            _cartService.SaveChanges();
            Console.WriteLine("Cart cleared.");
        }
    }

}
