using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Interfaces
{
    public interface IOrderService
    {
        Task MakeOrder(IEnumerable<UserBook> orderData);
        Task UpdateBookCount(long userId, long bookId, int newCount, int oldCount);
        Task<IEnumerable<UserBook>> GetUserOrderedBooks(string userEmail);
    }
}
