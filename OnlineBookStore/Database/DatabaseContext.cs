using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using OnlineBookStore.Models;

namespace OnlineBookStore.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<OrderBook> OrderBooks { get; set; }

        // Constructor to accept DbContextOptions
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        // Configures the connection string
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=online_book_store;Trusted_Connection=True;");
            }
        }

        // Configures relationships and model constraints
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Customer)
                .WithOne()
                .HasForeignKey<Cart>(c => c.CustomerId);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Reviews)
                .HasForeignKey(r => r.CustomerId);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Book)
                .WithMany(b => b.Reviews)
                .HasForeignKey(r => r.BookId);

            // Configure primary key
            modelBuilder.Entity<OrderBook>()
                .HasKey(ob => ob.OrderBookId);

            // Configure relationships
            modelBuilder.Entity<OrderBook>()
                .HasOne(ob => ob.Order)
                .WithMany(o => o.OrderBooks)
                .HasForeignKey(ob => ob.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderBook>()
                .HasOne(ob => ob.Book)
                .WithMany(b => b.OrderBooks)
                .HasForeignKey(ob => ob.BookId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }








    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=online_book_store;Trusted_Connection=True;");

            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}
