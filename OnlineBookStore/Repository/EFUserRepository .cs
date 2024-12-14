using OnlineBookStore.Database;
using OnlineBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.Repository
{
    public class EFUserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        public EFUserRepository( )
        {
            _context = DatabaseConnection.Instance.Context;
        }

        public User GetUserById(int userId) =>
            (User)_context.Customers.FirstOrDefault(c => c.UserId == userId) ??
            _context.Admins.FirstOrDefault(a => a.UserId == userId);

        public User GetUserByUsernameAndPassword(string username, string password) =>
            (User)_context.Customers.SingleOrDefault(u => u.Username == username && u.Password == password) ??
            _context.Admins.SingleOrDefault(u => u.Username == username && u.Password == password);


        public bool UsernameExists(string username) =>
            _context.Customers.Any(u => u.Username == username) ||
            _context.Admins.Any(u => u.Username == username);

        public void AddUser(User user)
        {
            if (user is Customer customer)
                _context.Customers.Add(customer);
            else if (user is Admin admin)
                _context.Admins.Add(admin);
        }

        public void UpdateUser(User user)
        {
            _context.Update(user); // EF Core tracks changes
        }

        public void SaveChanges() => _context.SaveChanges();
    }

}
