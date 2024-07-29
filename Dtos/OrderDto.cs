using System;
using System.Collections.Generic;

namespace BookStoreApiV2.Models
{
    public class OrderDto
    {
        public int Id { get; set; } // int olarak değiştirdik
        public int BookId { get; set; } // int olarak değiştirdik
        public DateTime OrderDate { get; set; }
        public decimal Price { get; set; } // Price'ı decimal olarak bıraktık
        public decimal TotalPrice { get; set; } // Sipariş toplam fiyatı decimal olarak
        public List<OrderItemDto> OrderItems { get; set; }
    }

    public class OrderItemDto
    {
        public int Id { get; set; } // int olarak değiştirdik
        public int BookId { get; set; } // int olarak değiştirdik
        public string BookTitle { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; } // Sipariş kaleminin fiyatı decimal olarak
    }
}
