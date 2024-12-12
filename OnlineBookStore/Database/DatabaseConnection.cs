using Microsoft.EntityFrameworkCore;
using System;

namespace OnlineBookStore.Database
{
    public class DatabaseConnection
    {
        private static DatabaseConnection _instance;
        private readonly DatabaseContext _context;

        // Private constructor to enforce Singleton
        private DatabaseConnection()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=online_book_store;Trusted_Connection=True;")
                .Options;

            _context = new DatabaseContext(options);

            // Ensure database is created and up-to-date
            _context.Database.Migrate(); // Apply any pending migrations to the database
        }

        // Public static instance getter
        public static DatabaseConnection Instance => _instance ??= new DatabaseConnection();

        // Getter for the DatabaseContext
        public DatabaseContext Context => _context;
    }
}
