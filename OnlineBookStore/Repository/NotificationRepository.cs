using OnlineBookStore.Database;
using OnlineBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly DatabaseContext _context;

        public NotificationRepository( )
        {
            _context = DatabaseConnection.Instance.Context;
        }

        public List<Notification> GetNotificationsByUserId(int userId)
        {
            return _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.DateCreated)
                .ToList();
        }

        public void ClearNotifications(IEnumerable<Notification> notifications)
        {
            _context.Notifications.RemoveRange(notifications);
            _context.SaveChanges();
        }

        public void AddNotification(Notification notification)
        {
            _context.Notifications.Add(notification);
            _context.SaveChanges();
        }


    }
}
