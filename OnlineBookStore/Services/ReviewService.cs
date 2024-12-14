using OnlineBookStore.Database;
using OnlineBookStore.Models;
using OnlineBookStore.Repository;
using OnlineBookStore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineBookStore.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService( )
        {
            _reviewRepository = new ReviewRepository();
        }

        public void AddReview(Review review)
        {
            if (review == null) throw new ArgumentNullException(nameof(review), "Review cannot be null.");
            _reviewRepository.Add(review);
            _reviewRepository.SaveChanges();
        }

        public List<Review> GetBookReviews(int bookId)
        {
            return _reviewRepository.GetAll()
                .Where(r => r.BookId == bookId)
                .ToList();
        }
    }
}
