using OnlineBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.Utilities
{
    public interface ISortingStrategy
    {
        List<Book> Sort(List<Book> books);
    }

    public class SortByPrice : ISortingStrategy
    {
        public List<Book> Sort(List<Book> books) => books.OrderBy(b => b.Price).ToList();
    }

    public class SortByPopularity : ISortingStrategy
    {
        public List<Book> Sort(List<Book> books) => books.OrderBy(b => b.Popularity).ToList();
    }

    public class SortByTitle : ISortingStrategy
    {
        public List<Book> Sort(List<Book> books) => books.OrderBy(b => b.Title).ToList();
    }

    // Context to use the sorting strategies
    public class BookSorter
    {
        private ISortingStrategy _strategy;

        public void SetStrategy(ISortingStrategy strategy)
        {
            _strategy = strategy;
        }

        public List<Book> SortBooks(List<Book> books)
        {
            if (_strategy == null) throw new InvalidOperationException("Sorting strategy is not set.");
            return _strategy.Sort(books);
        }
    }
}
