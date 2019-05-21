using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetBooksRangeByAuthorIdAsync(long authorId, int limit, int offset);        
        Task<IEnumerable<Book>> GetBooksRangeByTitleAsync(string title, int limit, int offset);
        Task<IEnumerable<Book>> GetBooksRangeAsync(int limit, int offset);
        Task<IEnumerable<Book>> GetBooksRangeByGenreNameAsync(string genreName, int limit, int offset);
        Task<Dictionary<long, int>> GetBooksAvailableCountAsync(IEnumerable<long> books);
        Task AddBookAsync(Book newBook);
        Task<bool> UpdateBookAsync(Book book);
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task AddAuthorAsync(Author newAuthor);
        Task<bool> UpdateAuthorAsync(Author author);
    }
}
