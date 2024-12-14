using Microsoft.EntityFrameworkCore;
using OnlineBookStore.Database;
using OnlineBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly DatabaseContext _context;

        public CartRepository( )
        {
            _context = DatabaseConnection.Instance.Context;
        }

        public Cart GetCartByCustomerId(int customerId)
        {
            return _context.Carts.Include(c => c.Books).SingleOrDefault(c => c.CustomerId == customerId);
        }

        public void Save(Cart cart)
        {
            if (cart.CartId == 0)
            {
                _context.Carts.Add(cart);
            }
            else
            {
                _context.Update(cart);
            }

            _context.SaveChanges();
        }
    }
}
