using OnlineBookStore.Database;
using OnlineBookStore.Models;
using OnlineBookStore.Utilities;
using System;
using System.Windows;

namespace OnlineBookStore.Services
{
    public class AuthenticationService
    {
        private readonly DatabaseContext _context;

        public AuthenticationService()
        {
            _context = DatabaseConnection.Instance.Context;
        }


        // Get user details by ID
        public User GetUserDetails(int userId)
        {
            // Search for the user in both Customer and Admin tables
            var user = _context.Customers.FirstOrDefault(c => c.UserId == userId)
                       ?? (User)_context.Admins.FirstOrDefault(a => a.UserId == userId);

            if (user == null)
            {
                Console.WriteLine("User not found.");
            }

            return user;
        }

        // SignUp method, now uses the Factory for dynamic user creation
        public bool SignUp(string userType, string username, string password, string address, string phone)
        {
            if (_context.Customers.Any(u => u.Username == username) || _context.Admins.Any(u => u.Username == username))
            {
                Console.WriteLine("Username already exists.");
                return false;
            }

            // Use the factory to create the appropriate user
            var user = UserFactory.CreateUser(userType, username, password, address, phone);

            if (user == null)
            {
                Console.WriteLine("Invalid user type.");
                return false;
            }

            if (user is Customer)
            {
                _context.Customers.Add(user as Customer);
            }
            else if (user is Admin)
            {
                _context.Admins.Add(user as Admin);
            }

            _context.SaveChanges();
            Console.WriteLine($"{userType} signed up successfully.");
            return true;
        }

        // LogIn method refactored to use the Factory for both Customer and Admin
        public User LogIn(string username, string password)
        {
            // Use the Factory to search for both Customer and Admin without specifying userType
            var user = UserFactory.GetUser(username, password, _context);

            if (user == null)
            {
                Console.WriteLine("Invalid username or password.");
            }
            else
            {
                UserSession.Instance.SetUserId(user.UserId);


                // Check for notifications specific to the logged-in user
                var notifications = _context.Notifications
                    .Where(n => n.UserId == user.UserId)
                    .OrderByDescending(n => n.DateCreated)
                    .ToList();

                if (notifications.Any())
                {
                    // Display notifications
                    foreach (var notification in notifications)
                    {
                        MessageBox.Show(notification.Message, "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    // Clear notifications after displaying them
                    _context.Notifications.RemoveRange(notifications);
                    _context.SaveChanges();
                }
            }

            return user;
        }

        // Update account details using the Factory to ensure the user type is consistent
        public bool UpdateAccountDetails(int userId, string username, string password, string address, string phone, string userType)
        {
            return UserFactory.UpdateAccountDetails( userId, username, password, address, phone, userType, _context);
        }
    }
}
