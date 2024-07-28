using Microsoft.AspNetCore.Mvc;
using BookStoreApiV2.Models;
using BookStoreApiV2.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;

namespace BookStoreApiV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly BookStoreContext _context;

        public CartController(BookStoreContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize(Roles = "Buyer")]
        public async Task<IActionResult> AddToCart([FromBody] Cart cart)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Check if book is available and not deleted
            var book = await _context.Books.FindAsync(cart.BookId);
            if (book == null || book.IsDeleted || book.Stock <= 0)
                return BadRequest("Book is not available.");

            cart.UserId = userId;
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();

            return Ok(cart);
        }

        [HttpGet]
        [Authorize(Roles = "Buyer")]
        public async Task<IActionResult> GetCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartItems = await _context.Carts
                .Include(c => c.Book)
                .Where(c => c.UserId == userId)
                .ToListAsync();

            return Ok(cartItems);
        }

        [HttpGet("{buyerId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCartByBuyer(string buyerId)
        {
            var cartItems = await _context.Carts
                .Include(c => c.Book)
                .Where(c => c.UserId == buyerId)
                .ToListAsync();

            return Ok(cartItems);
        }
    }
}
