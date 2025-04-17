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
    public class ReviewsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ReviewsController(AppDbContext context) => _context = context;

        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetByProduct(int productId) => Ok(
            await _context.Reviews
                .Where(r => r.ProductId == productId)
                .Include(r => r.Customer)
                .ToListAsync());

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CreateReviewDto dto)
        {
            var review = new Review
            {
                CustomerId = dto.CustomerId,
                ProductId  = dto.ProductId,
                Content    = dto.Content,
                Rating     = dto.Rating
            };
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByProduct), new { productId = review.ProductId }, review);
        }
    }
}