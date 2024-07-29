using System;
using System.ComponentModel.DataAnnotations;

namespace BookStoreApiV2.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string Author { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Fiyat 0'a eşit veya daha fazlası olabilir.")]
        public decimal Price { get; set; }


        [StringLength(1000)]
        public string Description { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stok negatif olamaz.")]
        public int Stock { get; set; }

        // Soft delete fields
        public bool IsDeleted { get; set; }
        public string? DeletedBy { get; set; }  // Nullable
        public string? DeletedByRole { get; set; }  // Nullable
        public DateTime? DeletedDate { get; set; } // Nullable

        // Tracking fields
        public string? CreatedBy { get; set; } // Nullable
        public string? CreatedByRole { get; set; } // Nullable
        public DateTime? CreatedDate { get; set; } // Nullable
        public int CreatedById { get; set; }
        public ICollection<Order> Order { get; set; }

    }
}
