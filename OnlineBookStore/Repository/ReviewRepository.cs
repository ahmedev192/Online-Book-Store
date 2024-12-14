using OnlineBookStore.Database;
using OnlineBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DatabaseContext _context;

        public ReviewRepository( )
        {
            _context = DatabaseConnection.Instance.Context;
        }

        public void Add(Review review)
        {
            if (review == null) throw new ArgumentNullException(nameof(review), "Review cannot be null.");
            _context.Reviews.Add(review);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public List<Review> GetAll()
        {
            return _context.Reviews.ToList();
        }

        public Review GetById(int id)
        {
            return _context.Reviews.FirstOrDefault(r => r.ReviewId == id);
        }
    }
}
