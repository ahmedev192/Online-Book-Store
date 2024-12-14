using OnlineBookStore.Database;
using OnlineBookStore.Models;
using OnlineBookStore.Repository;
using System.Collections.Generic;
using System.Linq;

namespace OnlineBookStore.Services
{
    public class CartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService( )
        {
            _cartRepository = new CartRepository();
        }

        public Cart GetCartByCustomerId(int customerId)
        {
            return _cartRepository.GetCartByCustomerId(customerId) ?? new Cart
            {
                UserId = customerId,
                Books = new List<Book>()
            };
        }

        public void SaveCart(Cart cart)
        {
            _cartRepository.Save(cart);
        }
    }

}
