using System;
using System.Collections.Generic;

namespace BookStoreApiV2.Dtos
{
    public class OrderPublicDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public string BookAuthor { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderDate { get; set; }
    }

    public class OrderAdminDto : OrderPublicDto
    {
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
    }
}

