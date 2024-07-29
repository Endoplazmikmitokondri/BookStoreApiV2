using BookStoreApiV2.Models;
using System;

namespace BookStoreApiV2.Extensions
{
    public static class OrderExtensions
    {
        public static OrderDto ToPublicDto(this Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                BookId = order.BookId,
                OrderDate = TimeHelper.ConvertUtcToIstanbul(order.OrderDate),
                Price = order.Price
            };
        }

        public static AdminOrderDto ToAdminDto(this Order order)
        {
            return new AdminOrderDto
            {
                Id = order.Id,
                BookId = order.BookId,
                BuyerId = order.BuyerId,
                SellerId = order.SellerId,
                OrderDate = TimeHelper.ConvertUtcToIstanbul(order.OrderDate),
                Price = order.Price
            };
        }
    }
}
