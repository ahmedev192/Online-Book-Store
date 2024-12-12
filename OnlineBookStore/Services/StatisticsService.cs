using OnlineBookStore.Database;
using System.Collections.Generic;
using System.Linq;

namespace OnlineBookStore.Services
{
    public class StatisticsService
    {
        private readonly DatabaseContext _context;

        public StatisticsService()
        {
            _context = DatabaseConnection.Instance.Context;
        }

        public List<string> GetTopSellingBooks()
        {
            return _context.OrderBooks
                .GroupBy(ob => ob.Book.Title)
                .OrderByDescending(g => g.Count())
                .Take(5)
                .Select(g => $"{g.Key} ({g.Count()} sales)")
                .ToList();
        }
        public List<string> GetPopularCategories()
        {
            return _context.OrderBooks
                .GroupBy(ob => ob.Book.Category.Name)
                .OrderByDescending(g => g.Count())
                .Take(5)
                .Select(g => $"{g.Key} ({g.Count()} sales)")
                .ToList();
        }

    }
}
