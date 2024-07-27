using System;

namespace BookStoreApiV2.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }

        // Soft delete fields
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }  // Username of who deleted the book
        public string DeletedByRole { get; set; }  // Role of who deleted the book (Admin, Seller)
        public DateTime? DeletedDate { get; set; } // When the book was deleted
    }
}
