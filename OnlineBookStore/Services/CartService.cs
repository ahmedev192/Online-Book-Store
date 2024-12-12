using OnlineBookStore.Database;
using OnlineBookStore.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineBookStore.Services
{
    public class CartService
    {
        private readonly DatabaseContext _context;

        public CartService()
        {
            _context = DatabaseConnection.Instance.Context;
        }

        public Cart GetCartByCustomerId(int customerId)
        {
            return _context.Carts.SingleOrDefault(c => c.CustomerId == customerId);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
