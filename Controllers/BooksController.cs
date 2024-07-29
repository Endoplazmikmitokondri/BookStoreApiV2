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

            // Kullanıcı ID'sini JWT'den al
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdClaim, out int userId))
            {
                book.CreatedById = userId;
            }
            else
            {
                return BadRequest("User ID is invalid.");
            }

            book.CreatedBy = username;
            book.CreatedByRole = role;
            book.CreatedDate = TimeHelper.ConvertUtcToIstanbul(DateTime.UtcNow);
            book.IsDeleted = false;

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book.ToPublicDto());
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<BookPublicDto>> GetBookById(int id)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            Book book;
            if (userRole == "Admin")
            {
                book = await _context.Books.FindAsync(id);
                if (book == null)
                {
                    return NotFound();
                }
                return Ok(book.ToAdminDto());
            }
            else
            {
                book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);
                if (book == null)
                {
                    return NotFound();
                }
                return Ok(book.ToPublicDto());
            }
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
                .Where(b => !b.IsDeleted)
                .ToListAsync();

            var result = books.Select(b => b.ToPublicDto());

            return Ok(result);
        }

        [HttpGet("admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllBooksForAdmin()
        {
            var books = await _context.Books.ToListAsync();

            var result = books.Select(b => b.ToAdminDto());

            return Ok(result);
        }
    }

    public static class TimeHelper
    {
        public static DateTime ConvertUtcToIstanbul(DateTime utcDateTime)
        {
            var istanbulTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, istanbulTimeZone);
        }
    }
}
