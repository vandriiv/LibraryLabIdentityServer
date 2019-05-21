using Domain.Exceptions;
using Domain.Models;
using Domain.Repositories;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        public async Task AddAuthorAsync(Author newAuthor)
        {
            await _authorRepository.AddAsync(newAuthor);
        }

        public async Task AddBookAsync(Book newBook)
        {
            try
            {
                await _bookRepository.AddAsync(newBook);
            }
            catch(MemberRelationException e)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await _authorRepository.GetAllAsync();
        }

        public async Task<Dictionary<long, int>> GetBooksAvailableCountAsync(IEnumerable<long> books)
        {
            return await _bookRepository.GetAvailableCountAsync(books);
        }

        public async Task<IEnumerable<Book>> GetBooksRangeAsync(int limit, int offset)
        {
            return await _bookRepository.GetRangeAsync(limit, offset);
        }

        public async Task<IEnumerable<Book>> GetBooksRangeByAuthorIdAsync(long authorId, int limit, int offset)
        {
            return await _bookRepository.GetRangeByAuthorIdAsync(authorId, limit, offset);
        }

        public async Task<IEnumerable<Book>> GetBooksRangeByGenreNameAsync(string genreName, int limit, int offset)
        {
            return await _bookRepository.GetRangeByGenreNameAsync(genreName, limit, offset);
        }

        public async Task<IEnumerable<Book>> GetBooksRangeByTitleAsync(string title, int limit, int offset)
        {
            return await _bookRepository.GetRangeByTitleAsync(title, limit, offset);
        }      

        public async Task<bool> UpdateAuthorAsync(Author author)
        {
            return await _authorRepository.UpdateAsync(author);
        }

        public async Task<bool> UpdateBookAsync(Book book)
        {
            try
            {
                return await _bookRepository.UpdateAsync(book);
            }
            catch (MemberRelationException e)
            {
                throw;
            }           
        }
    }
}
