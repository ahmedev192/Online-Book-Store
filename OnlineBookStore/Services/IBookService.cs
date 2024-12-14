using OnlineBookStore.Models;
using OnlineBookStore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.Services
{
    public interface IBookService
    {
        List<Book> BrowseBooks();
        Book GetBookById(int bookId);
        List<string> GetBookReviews(int bookId);
        bool AddBook(Book book);
        bool EditBook(int bookId, string title, string author, decimal price, int stock, string edition, string? newCoverImagePath);
        bool DeleteBook(int bookId);
        List<Book> SearchFilterAndSortBooks(string? searchQuery, string? category, ISortingStrategy? sortingStrategy);
    }
}
