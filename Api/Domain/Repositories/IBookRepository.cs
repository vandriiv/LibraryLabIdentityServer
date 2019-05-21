using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetRangeByAuthorIdAsync(long authorId, int limit, int offset);     
        Task<IEnumerable<Book>> GetRangeByTitleAsync(string title, int limit, int offset);
        Task<IEnumerable<Book>> GetRangeAsync(int limit, int offset);
        Task<IEnumerable<Book>> GetRangeByGenreNameAsync(string genreName, int limit, int offset);
        Task<Dictionary<long, int>> GetAvailableCountAsync(IEnumerable<long> books);
        Task AddAsync(Book newBook);
        Task<bool> UpdateAsync(Book book);
    }
}
