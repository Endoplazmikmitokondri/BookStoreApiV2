using System;
using System.Collections.Generic;

namespace BookStoreApiV2.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderDate { get; set; }
        public Book Book { get; set; }
    }
}


    
