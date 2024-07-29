using System;

namespace BookStoreApiV2.Models
{
    public class BookAdminDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public string DeletedByRole { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByRole { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
