using OnlineBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.Repository
{
    public interface IUserRepository
    {
        User GetUserById(int userId);
        User GetUserByUsernameAndPassword(string username, string password);
        bool UsernameExists(string username);
        void AddUser(User user);
        void UpdateUser(User user);
        void SaveChanges();
    }
}
