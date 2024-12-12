using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();

        [NotMapped]
        public decimal TotalAmount => Books.Sum(b => b.Price);

        public void AddBook(Book book) => Books.Add(book);
        public void RemoveBook(Book book) => Books.Remove(book);
        public void ClearCart() => Books.Clear();
    }

}
