using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; } 

        [ForeignKey("Book")]
        public int BookId { get; set; }
        public Book Book { get; set; }
        public string ReviewText { get; set; }
        public int Rating { get; set; } 
        public DateTime ReviewDate { get; set; }
    }



}
