using OnlineBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.Repository
{
    public interface INotificationRepository
    {
        List<Notification> GetNotificationsByUserId(int userId);
        void ClearNotifications(IEnumerable<Notification> notifications);
        void AddNotification(Notification notification);


    }

}
