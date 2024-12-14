using OnlineBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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

    public class BookSorter
    {
        private ISortingStrategy _strategy;

        public BookSorter(ISortingStrategy strategy = null)
        {
            _strategy = strategy ?? new SortByPrice(); // Default strategy
        }

        public void SetStrategy(ISortingStrategy strategy)
        {
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy), "Strategy cannot be null.");
        }

        public List<Book> SortBooks(List<Book> books)
        {
            if (books == null) throw new ArgumentNullException(nameof(books), "Books list cannot be null.");
            return _strategy.Sort(books);
        }
    }
}
