using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace los_api.Models
{
    public class Products
    {
        public int id { get; set; }
        public string name { get; set; }
        public string imageUrl { get; set; }
        public decimal price { get; set; }
    }
}
