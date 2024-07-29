using BookStoreApiV2.Dtos;
using BookStoreApiV2.Models;

namespace BookStoreApiV2.Extensions
{
    public static class OrderExtensions
    {
        public static OrderPublicDto ToPublicDto(this Order order)
        {
            return new OrderPublicDto
            {
                Id = order.Id,
                BookId = order.BookId,
                BookTitle = order.Book.Title,
                BookAuthor = order.Book.Author,
                Price = order.Price,
                OrderDate = TimeHelper.ConvertUtcToIstanbul(order.OrderDate)
            };
        }

        public static OrderAdminDto ToAdminDto(this Order order)
        {
            return new OrderAdminDto
            {
                Id = order.Id,
                BookId = order.BookId,
                BookTitle = order.Book.Title,
                BookAuthor = order.Book.Author,
                Price = order.Price,
                OrderDate = TimeHelper.ConvertUtcToIstanbul(order.OrderDate),
                BuyerId = order.BuyerId,
                SellerId = order.SellerId
            };
        }
    }
}
