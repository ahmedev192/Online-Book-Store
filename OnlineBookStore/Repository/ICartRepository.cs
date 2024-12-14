using OnlineBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.Repository
{
    public interface ICartRepository
    {
        Cart GetCartByCustomerId(int customerId);
        void Save(Cart cart);
    }

}
