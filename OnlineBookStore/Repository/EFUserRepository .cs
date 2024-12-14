using OnlineBookStore.Database;
using OnlineBookStore.Models;
using System;
using System.Linq;

namespace OnlineBookStore.Repository
{
    public class EFUserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        public EFUserRepository()
        {
            _context = DatabaseConnection.Instance.Context;
        }

        // Retrieve a user by ID and cast based on their role
        public User GetUserById(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == userId);
        }

        // Retrieve a user by username and password, cast to appropriate role
        public User GetUserByUsernameAndPassword(string username, string password)
        {
            return _context.Users.SingleOrDefault(u => u.Username == username && u.Password == password);
        }

        // Check if a username exists
        public bool UsernameExists(string username)
        {
            return _context.Users.Any(u => u.Username == username);
        }

        // Add a new user (admin or customer)
        public void AddUser(User user)
        {
            _context.Users.Add(user);
        }

        // Update an existing user
        public void UpdateUser(User user)
        {
            _context.Users.Update(user); // EF Core tracks changes
        }

        // Save changes to the database
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        // Additional method to check user role
        public string GetUserRole(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            return user is not null ? (user is Admin ? "Admin" : "Customer") : null;
        }
    }
}
