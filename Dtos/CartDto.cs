using System;
using System.Collections.Generic;

namespace BookStoreApiV2.Models
{
    public class CartDto
    {
        public int Id { get; set; } // string olarak kaldı
        public List<CartItemDto> CartItems { get; set; }
    }

    public class CartItemDto
    {
        public int Id { get; set; } // string olarak kaldı
        public int BookId { get; set; } // int olarak değiştirdik
        public string BookTitle { get; set; }
        public int Quantity { get; set; }
    }
}
