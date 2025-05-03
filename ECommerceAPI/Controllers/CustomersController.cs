using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ECommerceAPI.Data;
using ECommerceAPI.Dtos;
using ECommerceAPI.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Text;

namespace ECommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CustomersController(AppDbContext context) => _context = context;

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetMe()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email is null) return Unauthorized();

            var cust = await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);

            if (cust is null)
            {
                cust = new Customer
                {
                    Name  = email.Split('@')[0],
                    Email = email
                };
                _context.Customers.Add(cust);
                await _context.SaveChangesAsync();
            }

            return Ok(cust);
        }

        [HttpPut("me")]
        [Authorize]
        public async Task<IActionResult> UpdateMe(UpdateProfileDto dto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email is null) return Unauthorized();

            var cust = await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
            if (cust is null) return NotFound();

            cust.Name = dto.Name;
            cust.Email = dto.Email;

            await _context.SaveChangesAsync();
            return Ok(cust);
        }


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