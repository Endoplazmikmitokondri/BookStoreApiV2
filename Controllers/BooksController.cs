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

namespace BookStoreApiV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public static class TimeHelper
{
    public static DateTime ConvertUtcToIstanbul(DateTime utcDateTime)
    {
        var istanbulTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
        return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, istanbulTimeZone);
    }
}
    public class BooksController : ControllerBase
    {
        private readonly BookStoreContext _context;

        public BooksController(BookStoreContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Seller")]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var username = User.Identity.Name;
            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            // Otomatik olarak doldurulacak alanları ayarla
            book.CreatedBy = username;
            book.CreatedByRole = role;
            book.CreatedDate = TimeHelper.ConvertUtcToIstanbul(DateTime.UtcNow);
            book.IsDeleted = false; // Yeni eklenen kitaplar için varsayılan değer

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpPost("{id}/delete")]
        [Authorize(Roles = "Admin, Seller")]
        public async Task<IActionResult> SoftDeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null || book.IsDeleted)
                return NotFound();

            var username = User.Identity.Name;
            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            // Automatically set soft delete properties
            book.IsDeleted = true;
            book.DeletedBy = username;
            book.DeletedByRole = role;
            book.DeletedDate = TimeHelper.ConvertUtcToIstanbul(DateTime.UtcNow);

            _context.Books.Update(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _context.Books
                .Where(b => !b.IsDeleted) // Only return non-deleted books
                .ToListAsync();

            return Ok(books);
        }

        [HttpGet("admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllBooksForAdmin()
        {
            var books = await _context.Books
                .ToListAsync(); // Return all books, including soft-deleted

            var result = books.Select(b => new
            {
                b.Id,
                b.Title,
                b.Author,
                b.Price,
                b.Description,
                b.Stock,
                b.IsDeleted,
                DeletedBy = b.IsDeleted ? b.DeletedBy : null,
                DeletedByRole = b.IsDeleted ? b.DeletedByRole : null,
                DeletedDate = b.IsDeleted ? b.DeletedDate : (DateTime?)null
            });

            return Ok(result);
        }
    }
}
