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
            var cart = _cartService.GetCartByCustomerId(_customerId);


                cart.Books.Add(_book);
                _cartService.SaveCart(cart);
                Console.WriteLine($"Book '{_book.Title}' added to cart.");

        }
    }




    public class RemoveFromCartCommand : ICommand
    {
        private readonly CartService _cartService;
        private readonly int _customerId;
        private readonly int _bookId;

        public RemoveFromCartCommand(CartService cartService, int customerId, int bookId)
        {
            _cartService = cartService;
            _customerId = customerId;
            _bookId = bookId;
        }

        public void Execute()
        {
            var cart = _cartService.GetCartByCustomerId(_customerId);

            var book = cart.Books.FirstOrDefault(b => b.BookId == _bookId);
            if (book != null)
            {
                cart.Books.Remove(book);
                _cartService.SaveCart(cart);
                Console.WriteLine($"Book '{book.Title}' removed from cart.");
            }
            else
            {
                Console.WriteLine($"Book with ID '{_bookId}' not found in cart.");
            }
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

            if (cart.Books.Any())
            {
                cart.Books.Clear();
                _cartService.SaveCart(cart);
                Console.WriteLine("Cart cleared.");
            }
            else
            {
                Console.WriteLine("Cart is already empty.");
            }
        }
    }


}
