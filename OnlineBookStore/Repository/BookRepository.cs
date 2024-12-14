using Microsoft.EntityFrameworkCore;
using OnlineBookStore.Database;
using OnlineBookStore.Models;
using OnlineBookStore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly DatabaseContext _context;

        public BookRepository( )
        {
            _context = DatabaseConnection.Instance.Context;
        }

        public List<Book> GetAllBooks()
        {
            return _context.Books.Include(b => b.Category).ToList();
        }

        public Book GetBookById(int bookId)
        {
            return _context.Books.Include(b => b.Category).FirstOrDefault(b => b.BookId == bookId);
        }

        public List<string> GetBookReviews(int bookId)
        {
            return _context.Reviews
                           .Where(r => r.BookId == bookId)
                           .Select(r => r.ReviewText)
                           .ToList();
        }

        public void AddBook(Book book)
        {
            _context.Books.Add(book);

            _context.SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
        }

        public void DeleteBook(Book book)
        {
            _context.Books.Remove(book);
            _context.SaveChanges();
        }

        public string GetBookCoverImagePath(int bookId)
        {
            var book = _context.Books.FirstOrDefault(b => b.BookId == bookId);
            return book?.CoverImage;
        }

        public string SaveCoverImage(string coverImagePath)
        {
            try
            {
                var imageBytes = FileHelper.LoadImage(coverImagePath);
                if (imageBytes == null || imageBytes.Length == 0)
                {
                    Console.WriteLine("Failed to load the cover image.");
                    return null;
                }

                var savedImagePath = FileHelper.SaveImage(imageBytes);
                return savedImagePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving cover image: {ex.Message}");
                return null;
            }
        }
    }
}
