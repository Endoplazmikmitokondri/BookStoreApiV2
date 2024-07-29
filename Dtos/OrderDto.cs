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
        public decimal TotalPrice { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }

    public class OrderItemDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
