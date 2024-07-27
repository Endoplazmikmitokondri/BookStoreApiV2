using Microsoft.AspNetCore.Mvc;
using BookStoreApiV2.Models;
using BookStoreApiV2.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;


namespace BookStoreApiV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookStoreContext _context;

        public BooksController(BookStoreContext context)
        {
            _context = context;
        }

        [HttpPost("{id}/delete")]
        [Authorize(Roles = "Admin, Seller")]
        public async Task<IActionResult> SoftDeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null || book.IsDeleted)
                return NotFound();

            var username = User.Identity.Name; // Get the username from JWT token
            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value; // Get the role from JWT token

            book.IsDeleted = true;
            book.DeletedBy = username;
            book.DeletedByRole = role;
            book.DeletedDate = DateTime.UtcNow;

            _context.Books.Update(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _context.Books
                .Where(b => !b.IsDeleted) // Filter out soft-deleted books
                .ToListAsync();

            return Ok(books);
        }

        [HttpGet("admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetDeletedBooks()
        {
            var books = await _context.Books
                .Where(b => b.IsDeleted)
                .ToListAsync();

            return Ok(books);
        }
    }
}