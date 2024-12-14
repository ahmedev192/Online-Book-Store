using OnlineBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.Repository
{
    public interface IOrderRepository
    {
        void AddOrder(Order order);
        Order GetOrderById(int orderId);
        List<Order> GetOrdersByCustomerId(int customerId);
        bool HasCustomerPurchasedBook(int customerId, int bookId);
        bool UpdateOrder(Order order);
        decimal GetOrderTotalPrice(int orderId);
        List<Order> GetAllOrders();

    }

}
