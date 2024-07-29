using System;
using System.Collections.Generic;

namespace BookStoreApiV2.Models
{
    public class CartDto
    {
        public int Id { get; set; }
        public List<CartItemDto> CartItems { get; set; }
    }

    public class CartItemDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public int Quantity { get; set; }
    }
}
