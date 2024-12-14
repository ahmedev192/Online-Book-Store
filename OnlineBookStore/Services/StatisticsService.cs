using OnlineBookStore.Database;
using OnlineBookStore.Repository;
using System.Collections.Generic;
using System.Linq;

namespace OnlineBookStore.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public StatisticsService( )
        {
            _statisticsRepository = new StatisticsRepository();
        }

        public List<string> GetTopSellingBooks(int topN = 5)
        {
            var groupedBooks = _statisticsRepository.GetOrderBooksGroupedByBookTitle();

            return groupedBooks
                .OrderByDescending(g => g.Count())
                .Take(topN)
                .Select(g => $"{g.Key} ({g.Count()} sales)")
                .ToList();
        }

        public List<string> GetPopularCategories(int topN = 5)
        {
            var groupedCategories = _statisticsRepository.GetOrderBooksGroupedByCategoryName();

            return groupedCategories
                .OrderByDescending(g => g.Count())
                .Take(topN)
                .Select(g => $"{g.Key} ({g.Count()} sales)")
                .ToList();
        }
    }
}
