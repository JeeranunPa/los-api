using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace los_api.Models
{
    public class Stock 
    {
        public int StockId { get; set; }
        public int productId { get; set; }
        public decimal amount { get; set; }

    }

    public class StockDetail 
    {
        public int StockId { get; set; }
        public int productId { get; set; }
        public string name { get; set; }
        public string imageUrl { get; set; }
        public decimal price { get; set; }
        public decimal amount { get; set; }

    }
}
