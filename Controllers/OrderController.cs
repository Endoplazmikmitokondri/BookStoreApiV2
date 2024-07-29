using Microsoft.AspNetCore.Mvc;
using BookStoreApiV2.Models;
using BookStoreApiV2.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;
using System;
using BookStoreApiV2.Extensions;

namespace BookStoreApiV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly BookStoreContext _context;

        public OrderController(BookStoreContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize(Roles = "Buyer")]
        public async Task<IActionResult> PlaceOrder()
        {
            // Kullanıcı ID'sini JWT'den al
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // Kullanıcının kartındaki tüm kitapları al
            var cartItems = await _context.Carts
                .Include(c => c.Book)
                .Where(c => c.BuyerId == userId)
                .ToListAsync();

            if (!cartItems.Any())
                return BadRequest("Cart is empty.");

            foreach (var cartItem in cartItems)
            {
                var book = cartItem.Book;
                
                if (book.Stock <= 0)
                {
                    return BadRequest($"Book {book.Title} is out of stock.");
                }

                var newOrder = new Order
                {
                    BookId = cartItem.BookId,
                    BuyerId = userId,
                    SellerId = cartItem.Book.CreatedById,
                    OrderDate = TimeHelper.ConvertUtcToIstanbul(DateTime.UtcNow),
                    Price = cartItem.Book.Price
                };

                // Stoktan düş
                book.Stock--;

                _context.Orders.Add(newOrder);
                _context.Carts.Remove(cartItem);
            }

            await _context.SaveChangesAsync();

            return Ok("Order placed successfully.");
        }

        [HttpGet("buyer")]
        [Authorize(Roles = "Buyer")]
        public async Task<IActionResult> GetBuyerOrders()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var orders = await _context.Orders
                .Where(o => o.BuyerId == userId)
                .ToListAsync();

            var result = orders.Select(o => o.ToPublicDto());

            return Ok(result);
        }

        [HttpGet("seller")]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> GetSellerOrders()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var orders = await _context.Orders
                .Where(o => o.SellerId == userId)
                .ToListAsync();

            var result = orders.Select(o => o.ToPublicDto());

            return Ok(result);
        }

        [HttpGet("admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllOrdersForAdmin()
        {
            var orders = await _context.Orders.ToListAsync();
            var result = orders.Select(o => o.ToAdminDto());

            return Ok(result);
        }
    }
}
