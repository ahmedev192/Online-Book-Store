using OnlineBookStore.Database;
using OnlineBookStore.Models;
using OnlineBookStore.Repository;
using OnlineBookStore.Utilities;
using System;
using System.Windows;

namespace OnlineBookStore.Services
{
    public class UserManagementService
    {
        private readonly IUserRepository _userRepository;
        private readonly INotificationService _notificationService;


        public UserManagementService( )
        {
            _userRepository = new EFUserRepository();
            _notificationService = new NotificationService();
        }

        public User LogIn(string username, string password)
        {
            var user = _userRepository.GetUserByUsernameAndPassword(username, password);

            if (user == null)
            {
                Console.WriteLine("Invalid username or password.");
                return null;
            }

            UserSession.Instance.SetUserId(user.UserId);

            _notificationService.DisplayAndClearUserNotifications(user.UserId);

            return user;
        }

        public User GetUserById(int userId)
        {
            return _userRepository.GetUserById(userId);
        }

        public bool SignUp(string userType, string username, string password, string address, string phone)
        {
            if (_userRepository.UsernameExists(username))
            {
                Console.WriteLine("Username already exists.");
                return false;
            }

            var user = UserFactory.CreateUser(userType, username, password, address, phone);
            if (user == null) return false;

            _userRepository.AddUser(user);
            _userRepository.SaveChanges();
            return true;
        }

        public bool UpdateAccountDetails(int userId, string username, string password, string address, string phone)
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null) return false;

            user.Username = username;
            user.Password = password;
            user.Address = address;
            user.Phone = phone;

            _userRepository.UpdateUser(user);
            _userRepository.SaveChanges();
            return true;
        }




    }
}
