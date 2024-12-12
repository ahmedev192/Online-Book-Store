using OnlineBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            _sortingStrategy = sortingStrategy;
        }

        public List<Book> Execute(List<Book> books) => _sortingStrategy.Sort(books);
    }


    public class SearchBooksOperation : IBookOperation
    {
        private readonly string _searchQuery;

        public SearchBooksOperation(string searchQuery)
        {
            _searchQuery = searchQuery;
        }

        public List<Book> Execute(List<Book> books)
        {
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
            _category = category;
        }

        public List<Book> Execute(List<Book> books)
        {
            return books.Where(b => b.Category.Name == _category).ToList();
        }
    }


    public class CompositeBookOperation : IBookOperation
    {
        private readonly List<IBookOperation> _operations = new();

        public void AddOperation(IBookOperation operation)
        {
            _operations.Add(operation);
        }

        public List<Book> Execute(List<Book> books)
        {
            foreach (var operation in _operations)
            {
                books = operation.Execute(books);
            }
            return books;
        }
    }



}
