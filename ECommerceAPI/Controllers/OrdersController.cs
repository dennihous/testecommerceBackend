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
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;
        public OrdersController(AppDbContext context) => _context = context;

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get() => Ok(await _context.Orders
            .Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
            .Include(o => o.Customer)
            .ToListAsync());

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var o = await _context.Orders
                .Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(o => o.Id == id);
            return o == null ? NotFound() : Ok(o);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CreateOrderDto dto)
        {
            var order = new Order
            {
                CustomerId = dto.CustomerId,
                OrderDate  = DateTime.UtcNow,
                OrderItems = dto.OrderItems.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity  = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = order.Id }, order);
        }
    }
}