using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IUserBookRepository
    {
        Task AddRangeAsync(IEnumerable<UserBook> userBooks);
        Task UpdateAsync(long userId, long bookId, int newCount, int oldCount);
        Task<IEnumerable<UserBook>> GetUserBooksAsync(string userEmail);
    }
}
