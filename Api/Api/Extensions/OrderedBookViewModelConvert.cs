using Domain.Models;
using LibraryLab.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryLab.Extensions
{
    public static class OrderedBookViewModelConvert
    {
        public static IEnumerable<UserBook> ConvertToUserBooks(this IEnumerable<OrderedBookViewModel> orderedBooks
            ,long userId)
        {
            var userBooks = new List<UserBook>(orderedBooks.Count());
            foreach(var item in orderedBooks)
            {
                userBooks.Add(new UserBook
                {
                    UserId=userId,
                    BookId = item.BookId,
                    Count = item.Count
                });
            }
            return userBooks;
        }
    }
}
