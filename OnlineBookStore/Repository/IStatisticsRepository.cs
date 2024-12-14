using OnlineBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.Repository
{    public interface IStatisticsRepository
    {
        IEnumerable<IGrouping<string, OrderBook>> GetOrderBooksGroupedByBookTitle();
        IEnumerable<IGrouping<string, OrderBook>> GetOrderBooksGroupedByCategoryName();
    }
}
