using System;

namespace BookStoreApiV2.Models
{
    public class BookPublicDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public bool IsDeleted { get; set; }
    }
}
