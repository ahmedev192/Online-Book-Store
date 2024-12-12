using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.Models
{
    public class Admin : User
    {
        public List<Book> ManagedBooks { get; set; } = new List<Book>();
    }


}
