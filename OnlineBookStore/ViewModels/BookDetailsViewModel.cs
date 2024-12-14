using OnlineBookStore.Models;
using OnlineBookStore.Services;
using OnlineBookStore.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace OnlineBookStore.ViewModels
{
    public class BookDetailsViewModel
    {
        private readonly BookService _bookService;
        private readonly OrderManagementService _orderManagementService;
        private readonly ReviewService _reviewService;

        public BookDetailsViewModel()
        {
            _bookService = new BookService();
            _reviewService = new ReviewService();
            _orderManagementService = new OrderManagementService();
        }

        public Book Book { get; private set; }
        public List<Review> Reviews { get; private set; }
        public bool CanLeaveReview { get; private set; }

        public void LoadBookDetails(int bookId)
        {
            Book = _bookService.GetBookById(bookId);

            // Load reviews including customer names
            Reviews = _reviewService.GetBookReviews(bookId);

            // Check if the user has purchased this book
            CanLeaveReview = _orderManagementService.HasPurchasedBook(UserSession.Instance.UserId, bookId);
        }

        public void SubmitReview(int bookId, string reviewText, int rating)
        {
            if (!string.IsNullOrEmpty(reviewText) && rating >= 1 && rating <= 5)
            {
                // Create the new review object
                var review = new Review
                {
                    BookId = bookId,
                    ReviewText = reviewText,
                    Rating = rating,
                    CustomerId = UserSession.Instance.UserId,  // Current user ID
                    ReviewDate = System.DateTime.Now
                };

                // Add the review to the database
                _reviewService.AddReview(review);

                // Reload the reviews after submitting the new one
                LoadBookDetails(bookId);
            }
        }
    }
}
