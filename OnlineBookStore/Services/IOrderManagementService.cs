using OnlineBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.Services
{
    public interface IOrderManagementService
    {
        Order PlaceOrder(int customerId, List<Book> books);
        bool CancelOrder(int orderId);
        bool ConfirmOrder(int orderId);
        List<Order> GetOrderHistory(int customerId);
        decimal GetOrderTotalPrice(int orderId);
        bool HasPurchasedBook(int customerId, int bookId);
        List<Order> GetAllOrders();
    }
}
