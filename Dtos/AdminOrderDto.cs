using System;

namespace BookStoreApiV2.Models
{
    public class AdminOrderDto
    {
        public int Id { get; set; } // int olarak değiştirdik
        public int BookId { get; set; } // int olarak değiştirdik
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Price { get; set; } // Price'ı decimal olarak
    }
}
