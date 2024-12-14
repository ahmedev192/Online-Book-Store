using OnlineBookStore.Models;
using OnlineBookStore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.Services
{
    // Order Management Service
    public class OrderManagementService : IOrderManagementService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly INotificationService _notificationService;

        public OrderManagementService()
        {
            _orderRepository = new OrderRepository();
            _notificationService = new NotificationService();
        }

        public Order PlaceOrder(int customerId, List<Book> books)
        {
            var order = new Order
            {
                CustomerId = customerId,
                OrderDate = DateTime.Now,
                Status = "Pending",
                OrderBooks = books.Select(book => new OrderBook { BookId = book.BookId }).ToList()
            };

            _orderRepository.AddOrder(order);
            return order;
        }
        public bool CancelOrder(int orderId)
        {
            var order = _orderRepository.GetOrderById(orderId);
            if (order == null || order.Status != "Pending")
            {
                return false;
            }

            order.Status = "Canceled";
            var updated = _orderRepository.UpdateOrder(order);

            if (updated)
            {
                _notificationService.NotifyCustomer(order.CustomerId, $"Order #{orderId} has been canceled.");
            }

            return updated;
        }

        public bool ConfirmOrder(int orderId)
        {
            var order = _orderRepository.GetOrderById(orderId);
            if (order == null || order.Status != "Pending")
            {
                return false;
            }

            order.Status = "Confirmed";
            var updated = _orderRepository.UpdateOrder(order);

            if (updated)
            {
                _notificationService.NotifyCustomer(order.CustomerId, $"Your order #{orderId} has been confirmed!");
            }

            return updated;
        }

        public List<Order> GetOrderHistory(int customerId)
        {
            return _orderRepository.GetOrdersByCustomerId(customerId);
        }

        public decimal GetOrderTotalPrice(int orderId)
        {
            return _orderRepository.GetOrderTotalPrice(orderId);
        }


        public bool HasPurchasedBook(int customerId, int bookId)
        {
            return _orderRepository.HasCustomerPurchasedBook(customerId, bookId);
        }
        public List<Order> GetAllOrders()
        {
            return _orderRepository.GetAllOrders();
        }

    }
}
