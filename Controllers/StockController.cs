using los_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace los_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockController : ControllerBase
    {
        private ProductDBContext _context;
        public StockController(ProductDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Stock> GetStockItemList()
        {
            var stocks = _context.Stock.ToList();
            return stocks.ToArray();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StockDetail>> GetStockItem(int id)
        {
           //var StrockItem = await _context.Stock.Where(x => x.productId == id).FirstAsync();
           var ProductItem = await _context.Products.Where(x => x.id == id).FirstAsync();

         
             
            var StrockDetailItem = await _context.Stock.Join(
            _context.Products, itm => itm.productId, bcd => bcd.id, (itm, bcd) => new { Stock = itm, Products = bcd })
                               .Where(i => i.Stock.productId == id).Select(i => new StockDetail() { 
                                    StockId = i.Stock.StockId,
                                    productId = i.Stock.productId,
                                    name = i.Products.name,
                                    imageUrl = i.Products.imageUrl,
                                    price = i.Products.price,
                                    amount = i.Stock.amount
                               }).FirstAsync();

            if (StrockDetailItem == null)
            {
                return NotFound();
            }

            return StrockDetailItem;
        }

        [HttpPost]
        public async Task<ActionResult<Stock>> Add(Stock stocks)
        {
            _context.Stock.Add(stocks);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStockItemList),  stocks);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, Stock Stocks)
        {
            if (id != Stocks.StockId)
            {
                return BadRequest();
            }

            _context.Entry(Stocks).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StocksItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var stocks = await _context.Stock.FindAsync(id);
            if (stocks == null)
            {
                return NotFound();
            }

            _context.Stock.Remove(stocks);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StocksItemExists(long id) => _context.Stock.Any(e => e.StockId == id);
    }
}
