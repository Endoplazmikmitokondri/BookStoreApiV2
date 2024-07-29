using BookStoreApiV2.Models;
using System;

namespace BookStoreApiV2.Extensions
{
    public static class BookExtensions
    {
        public static BookAdminDto ToAdminDto(this Book book)
        {
            return new BookAdminDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Price = book.Price, // Burada Price'ı decimal olarak bırakıyoruz
                Description = book.Description,
                Stock = book.Stock,
                IsDeleted = book.IsDeleted,
                DeletedBy = book.DeletedBy,
                DeletedByRole = book.DeletedByRole,
                DeletedDate = book.DeletedDate.HasValue ? TimeHelper.ConvertUtcToIstanbul(book.DeletedDate.Value) : null,
                CreatedBy = book.CreatedBy,
                CreatedByRole = book.CreatedByRole,
                CreatedDate = book.CreatedDate.HasValue ? TimeHelper.ConvertUtcToIstanbul(book.CreatedDate.Value) : null
            };
        }

        public static BookPublicDto ToPublicDto(this Book book)
        {
            return new BookPublicDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Price = book.Price,
                Description = book.Description,
                Stock = book.Stock,
                IsDeleted = book.IsDeleted
            };
        }
    }
    public static class TimeHelper
    {
        public static DateTime ConvertUtcToIstanbul(DateTime utcDateTime)
        {
            var istanbulTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, istanbulTimeZone);
        }
    }
}
