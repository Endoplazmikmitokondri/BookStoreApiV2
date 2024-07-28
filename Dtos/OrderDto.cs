using System;
using System.Collections.Generic;

namespace BookStoreApiV2.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Price { get; set; }
        public string TotalPrice { get; set; } // Sipariş toplam fiyatı
        public List<OrderItemDto> OrderItems { get; set; }
    }

    public class OrderItemDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public int Quantity { get; set; }
        public string Price { get; set; } // Sipariş kaleminin fiyatı
    }
     public class AdminOrderDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
        public string Price { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
