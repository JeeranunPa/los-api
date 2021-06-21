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
    public class ProductController : ControllerBase
    {
        private ProductDBContext _context;

        public ProductController(ProductDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Products> GetProductsItemList()
        {
            var products = _context.Products.ToList();
            return products.ToArray();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProductsItem(int id)
        {
            var todoItem = await _context.Products.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        [HttpPost]
        public async Task<ActionResult<Products>> Add(Products products)
        {
            _context.Products.Add(products);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductsItemList), products);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, Products products)
        {
            if (id != products.id)
            {
                return BadRequest();
            }

            _context.Entry(products).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsItemExists(id))
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
            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }

            _context.Products.Remove(products);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductsItemExists(long id) => _context.Products.Any(e => e.id == id);
    }
}
