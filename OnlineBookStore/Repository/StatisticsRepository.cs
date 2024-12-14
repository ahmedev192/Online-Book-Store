using OnlineBookStore.Database;
using OnlineBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.Repository
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly DatabaseContext _context;

        public StatisticsRepository( )
        {
            _context = DatabaseConnection.Instance.Context;
        }

        public IEnumerable<IGrouping<string, OrderBook>> GetOrderBooksGroupedByBookTitle()
        {
            return _context.OrderBooks
                .GroupBy(ob => ob.Book.Title)
                .ToList();
        }

        public IEnumerable<IGrouping<string, OrderBook>> GetOrderBooksGroupedByCategoryName()
        {
            return _context.OrderBooks
                .GroupBy(ob => ob.Book.Category.Name)
                .ToList();
        }
    }

}
