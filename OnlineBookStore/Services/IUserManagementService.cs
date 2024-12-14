using OnlineBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.Services
{
    public interface IUserManagementService
    {
        User LogIn(string username, string password);

        User GetUserById(int userId);

        bool SignUp(string userType, string username, string password, string address, string phone);

        bool UpdateAccountDetails(int userId, string username, string password, string address, string phone);

        void LogOut();
    }
}
