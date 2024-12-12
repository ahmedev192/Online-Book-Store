using Microsoft.EntityFrameworkCore;
using OnlineBookStore.Database;
using OnlineBookStore.Models;
using OnlineBookStore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.Services
{
    public class OrderService
    {
        private readonly DatabaseContext _context;

        public OrderService()
        {
            _context = DatabaseConnection.Instance.Context;
        }

        public Order PlaceOrder(int customerId, List<Book> books)
        {
            // Create the order object
            var order = new Order
            {
                CustomerId = customerId,
                OrderDate = DateTime.Now,
                Status = "Pending",
                OrderBooks = books.Select(book => new OrderBook
                {
                    BookId = book.BookId
                }).ToList() // Populate the OrderBooks collection
            };


            // Add the order to the database
            _context.Orders.Add(order);
            _context.SaveChanges();

            return order;
        }



        public List<Order> GetOrderHistory()
        {
            int customerId = UserSession.Instance.UserId;
            return _context.Orders.Where(o => o.CustomerId == customerId).ToList();
        }


        // Check if the customer has purchased the given book
        public bool HasPurchasedBook(int customerId, int bookId)
        {
            return _context.Orders
                .Where(o => o.CustomerId == customerId)
                .Any(o => o.OrderBooks.Any(ob => ob.BookId == bookId));
        }


        public void TrackOrderStatus(int orderId)
        {
            var order = _context.Orders.SingleOrDefault(o => o.OrderId == orderId);
            if (order == null)
            {
                Console.WriteLine("Order not found.");
                return;
            }

            Console.WriteLine($"Order Status: {order.Status}");
        }

        public bool CancelOrder(int orderId)
        {
            var order = _context.Orders.SingleOrDefault(o => o.OrderId == orderId && o.Status == "Pending");
            if (order == null)
            {
                Console.WriteLine("Order cannot be canceled.");
                return false;
            }

            order.Status = "Canceled";
            _context.SaveChanges();
            Console.WriteLine($"Order {orderId} canceled.");
            return true;
        }

        public bool NotifyCustomer(int orderId, string message)
        {
            var order = _context.Orders.SingleOrDefault(o => o.OrderId == orderId);
            if (order != null)
            {
                var customer = _context.Customers.SingleOrDefault(c => c.UserId == order.CustomerId);
                if (customer != null)
                {
                    var notificationSystem = new NotificationSystem();
                    notificationSystem.AddObserver(new CustomerObserver(customer.Username));
                    notificationSystem.NotifyObservers(message);
                    return true;
                }
            }

            return false;
        }

        public bool ConfirmOrder(int orderId)
        {
            var order = _context.Orders.SingleOrDefault(o => o.OrderId == orderId && o.Status == "Pending");
            if (order == null)
            {
                Console.WriteLine("Order not found or cannot be confirmed.");
                return false;
            }

            order.Status = "Confirmed";
            _context.SaveChanges();

            // Notify the customer
            var customer = _context.Customers.SingleOrDefault(c => c.UserId == order.CustomerId);
            if (customer != null)
            {
                var notificationSystem = new NotificationSystem();
                var customerObserver = new CustomerObserver(customer.Username);

                notificationSystem.AddObserver(customerObserver);
                notificationSystem.NotifyObservers($"Your order #{orderId} has been confirmed!");

                // Store notification for future display
                SaveNotificationToDatabase(customer.UserId, $"Your order #{orderId} has been confirmed.");
            }

            return true;
        }

        private void SaveNotificationToDatabase(int userId, string message)
        {
            var notification = new Notification
            {
                UserId = userId,
                Message = message,
                DateCreated = DateTime.Now
            };

            _context.Notifications.Add(notification);
            _context.SaveChanges();
        }

        public decimal GetOrderTotalPrice(int orderId)
        {
            return _context.OrderBooks
                .Where(ob => ob.OrderId == orderId)
                .Sum(ob => ob.Book.Price);
        }


    }




}
