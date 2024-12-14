using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.Services
{
    public interface INotificationService
    {
        void NotifyCustomer(int customerId, string message);
        void DisplayAndClearUserNotifications(int userId);


    }
}
