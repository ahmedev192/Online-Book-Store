using OnlineBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.Repository
{
    public interface IBookRepository
    {
        List<Book> GetAllBooks();
        Book GetBookById(int bookId);
        List<string> GetBookReviews(int bookId);
        void AddBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(Book book);
        string GetBookCoverImagePath(int bookId);
        string SaveCoverImage(string coverImagePath);


    }
}
