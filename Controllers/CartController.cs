using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;
using BookStoreApiV2.Data;
using BookStoreApiV2.Models;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Kullanıcı ID'sini JWT'den al
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int buyerId))
                return Unauthorized("User ID is invalid.");

            var book = await _context.Books.FindAsync(dto.BookId);
            if (book == null)
                return NotFound("Book not found.");

            var existingCartItem = await _context.Carts
                .FirstOrDefaultAsync(c => c.BookId == dto.BookId && c.BuyerId == buyerId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += dto.Quantity;
                _context.Carts.Update(existingCartItem);
            }
            else
            {
                var newCartItem = new Cart
                {
                    BookId = dto.BookId,
                    BuyerId = buyerId,
                    Quantity = dto.Quantity,
                    CreatedDate = DateTime.UtcNow // Tarihi ata
                };
                _context.Carts.Add(newCartItem);
            }

            await _context.SaveChangesAsync();

            return Ok("Book added to cart.");
<<<<<<< HEAD
        }
        
        [HttpDelete]
        [Authorize(Roles = "Buyer")]
        public async Task<IActionResult> ClearCart()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var cartItems = await _context.Carts
                .Where(c => c.BuyerId == userId)
                .ToListAsync();

            if (!cartItems.Any())
                return NotFound("Cart is already empty.");

            _context.Carts.RemoveRange(cartItems);
            await _context.SaveChangesAsync();

            return Ok("Cart cleared successfully.");
=======
>>>>>>> 623c66a209499129ee4838c910c67486c72a4a4e
        }

        [HttpGet]
        [Authorize(Roles = "Buyer")]
        public async Task<IActionResult> GetCart()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int buyerId))
                return Unauthorized("User ID is invalid.");

<<<<<<< HEAD
            var oneDayAgoUtc = DateTime.UtcNow.AddDays(-1);
            var oneDayAgoIstanbul = TimeHelper.ConvertUtcToIstanbul(oneDayAgoUtc);
            var cartItems = await _context.Carts
                .Where(c => c.BuyerId == buyerId && c.CreatedDate > oneDayAgoUtc)
=======
            var oneDayAgo = DateTime.UtcNow.AddDays(-1); // 1 gün öncesi
            var cartItems = await _context.Carts
                .Where(c => c.BuyerId == buyerId && c.CreatedDate > oneDayAgo)
>>>>>>> 623c66a209499129ee4838c910c67486c72a4a4e
                .Include(c => c.Book)
                .ToListAsync();

            if (cartItems == null || !cartItems.Any())
                return NotFound("Cart is empty.");

            return Ok(cartItems.Select(c => new CartDto
            {
                Id = c.Id,
                BookId = c.BookId,
                Title = c.Book.Title,
                Author = c.Book.Author,
                Quantity = c.Quantity
            }));
        }

        public class AddToCartDto
        {
            public int BookId { get; set; }
            public int Quantity { get; set; }
        }

        public class CartDto
        {
            public int Id { get; set; }
            public int BookId { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public int Quantity { get; set; }
        }
    }
}
