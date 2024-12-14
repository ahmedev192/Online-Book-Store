using Microsoft.EntityFrameworkCore;
using OnlineBookStore.Database;
using OnlineBookStore.Models;
using OnlineBookStore.Repository;
using OnlineBookStore.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService( )
        {
            _bookRepository = new BookRepository();
        }

        public List<Book> BrowseBooks()
        {
            return _bookRepository.GetAllBooks();
        }

        public Book GetBookById(int bookId)
        {
            return _bookRepository.GetBookById(bookId);
        }

        public List<string> GetBookReviews(int bookId)
        {
            return _bookRepository.GetBookReviews(bookId);
        }

        public bool AddBook(Book book)
        {
            if (book == null)
            {
                Console.WriteLine("Book cannot be null.");
                return false;
            }

            try
            {
                // Save cover image if path is provided
                if (!string.IsNullOrWhiteSpace(book.CoverImage))
                {
                    book.CoverImage = _bookRepository.SaveCoverImage(book.CoverImage);
                    if (string.IsNullOrEmpty(book.CoverImage))
                    {
                        Console.WriteLine("Failed to save cover image.");
                        return false;
                    }
                }

                // Save the book using the repository
                _bookRepository.AddBook(book);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding book: {ex.Message}");
                return false;
            }
        }






        public bool EditBook(int bookId, string title, string author, decimal price, int stock, string edition, string? newCoverImagePath)
        {
            try
            {
                // Fetch the book from the repository
                var book = _bookRepository.GetBookById(bookId);
                if (book == null)
                {
                    Console.WriteLine($"Book with ID {bookId} not found.");
                    return false;
                }

                // Update book properties
                book.Title = title;
                book.Author = author;
                book.Price = price;
                book.Stock = stock;
                book.Edition = edition;

                // Save the new cover image if provided
                if (!string.IsNullOrWhiteSpace(newCoverImagePath))
                {
                    book.CoverImage = _bookRepository.SaveCoverImage(newCoverImagePath);
                    if (string.IsNullOrEmpty(book.CoverImage))
                    {
                        Console.WriteLine("Failed to save new cover image.");
                        return false;
                    }
                }

                // Update the book in the repository
                _bookRepository.UpdateBook(book);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error editing book: {ex.Message}");
                return false;
            }
        }



        public bool DeleteBook(int bookId)
        {
            var book = _bookRepository.GetBookById(bookId);
            if (book == null)
                throw new InvalidOperationException($"Book with ID {bookId} not found.");

            _bookRepository.DeleteBook(book);
            return true; 
        }


        public string GetBookCoverImage(int bookId)
        {
            var imagePath = _bookRepository.GetBookCoverImagePath(bookId);
            if (string.IsNullOrEmpty(imagePath))
                throw new FileNotFoundException("Image not found.");
            return Convert.ToBase64String(FileHelper.LoadImage(imagePath));
        }

        public List<Book> SearchFilterAndSortBooks(string? searchQuery, string? category, ISortingStrategy? sortingStrategy)
        {
            var allBooks = _bookRepository.GetAllBooks();

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
