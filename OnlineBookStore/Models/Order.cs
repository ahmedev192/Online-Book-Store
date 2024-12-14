using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; } 

        public List<OrderBook> OrderBooks { get; set; } = new List<OrderBook>();
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }

        [NotMapped]
        public decimal TotalAmount => OrderBooks.Sum(b => b.Book.Price);
    }


}
