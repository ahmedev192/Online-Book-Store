using OnlineBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineBookStore.Utilities
{
    public interface IBookOperation
    {
        List<Book> Execute(List<Book> books);
    }

    public class SortBooksOperation : IBookOperation
    {
        private readonly ISortingStrategy _sortingStrategy;

        public SortBooksOperation(ISortingStrategy sortingStrategy)
        {
            _sortingStrategy = sortingStrategy ?? throw new ArgumentNullException(nameof(sortingStrategy), "Sorting strategy cannot be null.");
        }

        public List<Book> Execute(List<Book> books)
        {
            if (books == null) throw new ArgumentNullException(nameof(books), "Books list cannot be null.");
            return _sortingStrategy.Sort(books);
        }
    }

    public class SearchBooksOperation : IBookOperation
    {
        private readonly string _searchQuery;

        public SearchBooksOperation(string searchQuery)
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
                throw new ArgumentException("Search query cannot be null or empty.", nameof(searchQuery));
            _searchQuery = searchQuery;
        }

        public List<Book> Execute(List<Book> books)
        {
            if (books == null) throw new ArgumentNullException(nameof(books), "Books list cannot be null.");
            return books.Where(b => b.Title.Contains(_searchQuery, StringComparison.OrdinalIgnoreCase) ||
                                    b.Author.Contains(_searchQuery, StringComparison.OrdinalIgnoreCase))
                        .ToList();
        }
    }

    public class FilterBooksOperation : IBookOperation
    {
        private readonly string _category;

        public FilterBooksOperation(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
                throw new ArgumentException("Category cannot be null or empty.", nameof(category));
            _category = category;
        }

        public List<Book> Execute(List<Book> books)
        {
            if (books == null) throw new ArgumentNullException(nameof(books), "Books list cannot be null.");
            return books.Where(b => b.Category?.Name == _category).ToList();
        }
    }

    public class CompositeBookOperation : IBookOperation
    {
        private readonly List<IBookOperation> _operations = new();

        public CompositeBookOperation AddOperation(IBookOperation operation)
        {
            if (operation == null)
                throw new ArgumentNullException(nameof(operation), "Operation cannot be null.");
            _operations.Add(operation);
            return this;
        }

        public List<Book> Execute(List<Book> books)
        {
            if (books == null) throw new ArgumentNullException(nameof(books), "Books list cannot be null.");
            foreach (var operation in _operations)
            {
                books = operation.Execute(books);
            }
            return books;
        }
    }
}
