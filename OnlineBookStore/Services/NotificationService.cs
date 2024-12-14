using OnlineBookStore.Models;
using OnlineBookStore.Repository;
using OnlineBookStore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OnlineBookStore.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly NotificationSystem _notificationSystem;

        public NotificationService( )
        {
            _notificationRepository = new NotificationRepository();
            _notificationSystem = new NotificationSystem();
        }




        public void NotifyCustomer(int customerId, string message)
        {
            // Add observer for the customer
            var customerObserver = new CustomerObserver($"Customer-{customerId}");
            _notificationSystem.AddObserver(customerObserver);

            // Notify all observers
            _notificationSystem.NotifyObservers(message);

            // Save notification to the database
            var notification = new Notification
            {
                UserId = customerId,
                Message = message,
                DateCreated = DateTime.Now
            };
            _notificationRepository.AddNotification(notification);
        }

        public void DisplayAndClearUserNotifications(int userId)
        {
            // Fetch notifications
            var notifications = _notificationRepository.GetNotificationsByUserId(userId);

            if (notifications.Any())
            {
                // Display notifications
                foreach (var notification in notifications)
                {
                    MessageBox.Show(notification.Message, "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                // Clear notifications
                _notificationRepository.ClearNotifications(notifications);
            }
        }






    }

}
