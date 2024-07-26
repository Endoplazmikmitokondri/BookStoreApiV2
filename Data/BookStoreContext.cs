using Microsoft.EntityFrameworkCore;
using BookStoreApiV2.Models;

namespace BookStoreApiV2.Data
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }

    }
}
