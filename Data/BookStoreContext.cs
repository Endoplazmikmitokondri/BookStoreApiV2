using Microsoft.EntityFrameworkCore;
using BookStoreApiV2.Models;

namespace BookStoreApiV2.Data
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Username).IsUnique();
            });
            modelBuilder.Entity<Book>()
               .HasIndex(b => b.IsDeleted); // Index to improve performance for filtering
            modelBuilder.Entity<Book>().ToTable("Book");
            modelBuilder.Entity<Cart>().ToTable("Cart");
            modelBuilder.Entity<Order>().ToTable("Order");
        }
    }
}