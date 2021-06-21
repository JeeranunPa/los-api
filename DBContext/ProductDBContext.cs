using los_api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace los_api
{

    public class ProductDBContext : DbContext
    {
        public ProductDBContext(DbContextOptions<ProductDBContext> options) : base(options) { }

        public DbSet<Products> Products { get; set; }

        public DbSet<Stock> Stock { get; set; }
    }
}
