using Microsoft.EntityFrameworkCore;
using BookStoreApiV2.Models;

namespace BookStoreApiV2.Data
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Username).IsUnique();
            });
            modelBuilder.Entity<Book>()
               .HasIndex(b => b.IsDeleted); // Index to improve performance for filtering
        }
    }
}