using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ECommerceAPI.Data;
using ECommerceAPI.Dtos;
using ECommerceAPI.Models;

namespace ECommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CustomersController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _context.Customers.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var c = await _context.Customers.FindAsync(id);
            return c == null ? NotFound() : Ok(c);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateCustomerDto dto)
        {
            var cust = new Customer
            {
                Name  = dto.Name,
                Email = dto.Email
            };
            _context.Customers.Add(cust);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = cust.Id }, cust);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateCustomerDto dto)
        {
            var cust = await _context.Customers.FindAsync(id);
            if (cust == null) return NotFound();
            cust.Name  = dto.Name;
            cust.Email = dto.Email;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cust = await _context.Customers.FindAsync(id);
            if (cust == null) return NotFound();
            _context.Customers.Remove(cust);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}