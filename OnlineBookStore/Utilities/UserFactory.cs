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
        // Method to create a user based on userType (Customer or Admin)
        public static User CreateUser(string userType, string username, string password, string address, string phone)
        {
            switch (userType.ToLower())
            {
                case "customer":
                    return new Customer
                    {
                        Username = username,
                        Password = password,
                        Address = address,
                        Phone = phone
                    };
                case "admin":
                    return new Admin
                    {
                        Username = username,
                        Password = password,
                        Address = address,
                        Phone = phone
                    };
                default:
                    return null; // Return null if the userType is not valid
            }
        }

        // Method to fetch a user from the database (for login) without passing userType
        public static User GetUser(string username, string password, DatabaseContext context)
        {
            // First, try to find the user in the Customers table
            var customer = context.Customers.SingleOrDefault(u => u.Username == username && u.Password == password);
            if (customer != null)
            {
                return customer;
            }

            // If not found in Customers, check in the Admins table
            var admin = context.Admins.SingleOrDefault(u => u.Username == username && u.Password == password);
            if (admin != null)
            {
                return admin;
            }

            // If not found in both tables, return null
            return null;
        }

        // Method to update user details (both Customer and Admin)
        // Update account details using Factory to ensure the user type is consistent
        // Method to update user details (both Customer and Admin)
        public static bool UpdateAccountDetails(int userId, string username, string password, string address, string phone, string userType, DatabaseContext context)
        {
            User user = null;

            // Check if the user is a Customer or Admin and fetch the user accordingly
            if (userType.ToLower() == "customer")
            {
                user = context.Customers.SingleOrDefault(u => u.UserId == userId);
            }
            else if (userType.ToLower() == "admin")
            {
                user = context.Admins.SingleOrDefault(u => u.UserId == userId);
            }

            // If the user is not found, return false
            if (user == null)
            {
                Console.WriteLine($"{userType} not found.");
                return false;
            }

            // Update the user's details
            user.Username = username;
            user.Password = password;
            user.Address = address;
            user.Phone = phone;

            // Save changes to the database
            context.SaveChanges();
            Console.WriteLine($"{userType} account details updated.");
            return true;
        }
    }
}

