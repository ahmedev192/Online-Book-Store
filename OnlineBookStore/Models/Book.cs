using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineBookStore.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string Author { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        [StringLength(50)]
        public string Edition { get; set; }

        [StringLength(500)]
        public string CoverImage { get; set; } 

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public double Popularity { get; set; } 

        public List<Review> Reviews { get; set; } = new List<Review>();

        public List<OrderBook> OrderBooks { get; set; } = new List<OrderBook>();

    }
}
