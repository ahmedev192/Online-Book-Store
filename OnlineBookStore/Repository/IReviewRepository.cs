using OnlineBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.Repository
{
    public interface IReviewRepository
    {
        void Add(Review review);
        void SaveChanges();
        List<Review> GetAll();
        Review GetById(int id);
    }

}
