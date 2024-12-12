using OnlineBookStore.Database;
using OnlineBookStore.Models;
using OnlineBookStore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.Services
{
    public class BookService
    {
        private readonly DatabaseContext _context;

        public BookService()
        {
            _context = DatabaseConnection.Instance.Context;
        }

        public List<Book> BrowseBooks()
        {
            return _context.Books.ToList();
        }


        public Book GetBookById(int bookId)
        {
            return _context.Books.FirstOrDefault(b => b.BookId == bookId);
        }
        public List<string> GetBookReviews(int bookId)
        {
            var reviews = _context.Reviews
                .Where(r => r.BookId == bookId)
                .Select(r => r.ReviewText)
                .ToList();

            return reviews;
        }

        public bool AddBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
            Console.WriteLine($"Book '{book.Title}' added.");
            return true;
        }

        public bool EditBook(int bookId, string title, string author, decimal price, int stock, string edition, string coverImage)
        {
            var book = _context.Books.SingleOrDefault(b => b.BookId == bookId);
            if (book == null)
            {
                Console.WriteLine("Book not found.");
                return false;
            }

            book.Title = title;
            book.Author = author;
            book.Price = price;
            book.Stock = stock;
            book.Edition = edition;
            book.CoverImage = coverImage;

            _context.SaveChanges();
            Console.WriteLine($"Book '{title}' updated.");
            return true;
        }

        public bool DeleteBook(int bookId)
        {
            var book = _context.Books.SingleOrDefault(b => b.BookId == bookId);
            if (book == null)
            {
                Console.WriteLine("Book not found.");
                return false;
            }

            _context.Books.Remove(book);
            _context.SaveChanges();
            Console.WriteLine($"Book '{book.Title}' deleted.");
            return true;
        }



        public List<Book> SearchFilterAndSortBooks(string? searchQuery, string? category, ISortingStrategy? sortingStrategy)
        {
            var allBooks = _context.Books.ToList();
            var compositeOperation = new CompositeBookOperation();

            if (!string.IsNullOrEmpty(searchQuery))
                compositeOperation.AddOperation(new SearchBooksOperation(searchQuery));

            if (!string.IsNullOrEmpty(category))
                compositeOperation.AddOperation(new FilterBooksOperation(category));

            if (sortingStrategy != null)
                compositeOperation.AddOperation(new SortBooksOperation(sortingStrategy));

            return compositeOperation.Execute(allBooks);
        }


    }
}
