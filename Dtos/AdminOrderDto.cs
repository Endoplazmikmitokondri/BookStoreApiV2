using System;

namespace BookStoreApiV2.Models
{
    public class AdminOrderDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Price { get; set; }
    }
}
