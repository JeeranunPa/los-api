using los_api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace los_api
{

    public class StockDBContext : DbContext
    {
        public StockDBContext(DbContextOptions<StockDBContext> options) : base(options) { }

        public DbSet<Stock> Stock { get; set; }
    }
}
