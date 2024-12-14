using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.Services
{
    public interface IStatisticsService
    {
        List<string> GetTopSellingBooks(int topN = 5);
        List<string> GetPopularCategories(int topN = 5);
    }

}
