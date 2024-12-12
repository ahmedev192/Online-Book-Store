using OnlineBookStore.Database;
using OnlineBookStore.Models;
using OnlineBookStore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineBookStore.Services
{
    public class ReviewService
    {
        private readonly DatabaseContext _context;

        public ReviewService()
        {
            _context = DatabaseConnection.Instance.Context;
        }

        public void AddReview(Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
        }



        public List<Review> GetBookReviews(int bookId)
        {
            return _context.Reviews
                       .Where(r => r.BookId == bookId)
                       .Select(r => new Review
                       {
                           ReviewText = r.ReviewText,
                           Rating = r.Rating,
                           ReviewDate = r.ReviewDate,
                           Customer = r.Customer // Assuming 'Customer' has a 'Name' property
                       })
                       .ToList();
        }

        public List<Book> GetPurchasedBooks(int customerId)
        {
            // Retrieve books that the customer has purchased via their orders
            var purchasedBooks = _context.Orders
                .Where(o => o.CustomerId == customerId)
                .SelectMany(o => o.OrderBooks.Select(ob => ob.Book))
                .Distinct()
                .ToList();

            return purchasedBooks;
        }

    }
}
