using Microsoft.EntityFrameworkCore;
using OnlineBookStore.Database;
using OnlineBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.Utilities
{
    public static class UserFactory
    {
        public static User CreateUser(string userType, string username, string password, string address, string phone) =>
            userType.ToLower() switch
            {
                "customer" => new Customer { Username = username, Password = password, Address = address, Phone = phone },
                "admin" => new Admin { Username = username, Password = password, Address = address, Phone = phone },
                _ => null
            };
    }

}

