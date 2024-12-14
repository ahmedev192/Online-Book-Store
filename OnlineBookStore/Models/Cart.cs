﻿using System;
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
        public virtual List<Book> Books { get; set; } = new List<Book>();

        [NotMapped]
        public decimal TotalAmount => Books.Sum(b => b.Price);

    }

}
