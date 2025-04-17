using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ECommerceAPI.Data;
using ECommerceAPI.Dtos;
using ECommerceAPI.Models;

namespace ECommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductsController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _context.Products.Include(p => p.Reviews).ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var p = await _context.Products.Include(p => p.Reviews)
                                          .FirstOrDefaultAsync(p => p.Id == id);
            return p == null ? NotFound() : Ok(p);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CreateProductDto dto)
        {
            var prod = new Product
            {
                Name        = dto.Name,
                Description = dto.Description,
                Price       = dto.Price,
                Stock       = dto.Stock
            };
            _context.Products.Add(prod);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = prod.Id }, prod);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, UpdateProductDto dto)
        {
            if (id != dto.Id) return BadRequest();
            var prod = await _context.Products.FindAsync(id);
            if (prod == null) return NotFound();

            prod.Name        = dto.Name;
            prod.Description = dto.Description;
            prod.Price       = dto.Price;
            prod.Stock       = dto.Stock;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var prod = await _context.Products.FindAsync(id);
            if (prod == null) return NotFound();
            _context.Products.Remove(prod);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}