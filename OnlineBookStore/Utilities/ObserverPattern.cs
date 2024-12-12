using System;
using System.Collections.Generic;

namespace OnlineBookStore.Utilities
{
    public interface IObserver
    {
        void Update(string message);
    }

    public class CustomerObserver : IObserver
    {
        public string CustomerName { get; set; }
        public List<string> Notifications { get; } = new();

        public CustomerObserver(string customerName)
        {
            CustomerName = customerName;
        }

        public void Update(string message)
        {
            Notifications.Add(message); // Store the notification

        }

        public List<string> GetNotifications()
        {
            return Notifications;
        }

        public void ClearNotifications()
        {
            Notifications.Clear();
        }
    }

    public class NotificationSystem
    {
        private readonly List<IObserver> _observers = new();

        public void AddObserver(IObserver observer) => _observers.Add(observer);
        public void RemoveObserver(IObserver observer) => _observers.Remove(observer);

        public void NotifyObservers(string message)
        {
            foreach (var observer in _observers)
            {
                observer.Update(message);
            }
        }
    }
}