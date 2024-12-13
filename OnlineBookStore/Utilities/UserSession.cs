
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.Utilities
{
    public class UserSession
    {
        private static UserSession _instance;

        // Property to hold the user ID
        public int UserId { get; private set; }

        // Private constructor to prevent instantiation
        private UserSession() { }

        // Method to get the singleton instance
        public static UserSession Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserSession();
                }
                return _instance;
            }
        }

        // Method to set the user ID after login
        public void SetUserId(int userId)
        {
            UserId = userId;
        }

        // Method to clear the session (log out)
        public void ClearSession()
        {
            UserId = 0;  // Reset the user ID on logout
        }
    }

}
