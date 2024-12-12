using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public string OrderDate { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }
    }

}
