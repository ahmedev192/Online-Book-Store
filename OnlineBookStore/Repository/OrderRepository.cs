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
    // Order Repository Implementation
    public class OrderRepository : IOrderRepository
    {
        private readonly DatabaseContext _context;

        public OrderRepository()
        {
            _context = DatabaseConnection.Instance.Context;
        }

        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public Order GetOrderById(int orderId)
        {
            return _context.Orders.Include(o => o.OrderBooks).SingleOrDefault(o => o.OrderId == orderId);
        }

        public List<Order> GetOrdersByCustomerId(int customerId)
        {
            return _context.Orders.Where(o => o.CustomerId == customerId).ToList();
        }

        public bool HasCustomerPurchasedBook(int customerId, int bookId)
        {
            return _context.Orders
                .Where(o => o.CustomerId == customerId)
                .Any(o => o.OrderBooks.Any(ob => ob.BookId == bookId));
        }

        public bool UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            return _context.SaveChanges() > 0;
        }

        public decimal GetOrderTotalPrice(int orderId)
        {
            return _context.OrderBooks
                .Where(ob => ob.OrderId == orderId)
                .Sum(ob => ob.Book.Price);
        }
        public List<Order> GetAllOrders()
        {
            return _context.Orders.Include(o => o.OrderBooks).ToList();
        }

    }

}
