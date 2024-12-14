using OnlineBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.Services
{
    public interface IReviewService
    {
        void AddReview(Review review);
        List<Review> GetBookReviews(int bookId);
    }


}
