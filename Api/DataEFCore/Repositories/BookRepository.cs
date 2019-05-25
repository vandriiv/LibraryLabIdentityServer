using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataEFCore.Context;
using Domain.Exceptions;
using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEFCore.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryAPIContext _context;

        public BookRepository(LibraryAPIContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Book newBook)
        {
            var entity = Mapper.Map<Entities.Book>(newBook);
            await _context.AddAsync(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {

                if (e.InnerException is SqlException sqlException)
                {
                    if (sqlException.Number == 547)
                    {
                        throw new MemberRelationException("Check entered value. ",
                            sqlException.Message);
                    }
                    else
                    {
                        throw;
                    }
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> UpdateAsync(Book book)
        {
            var entity = await _context.Book.FirstOrDefaultAsync(b => b.Id == book.Id);

            if (entity == null)
                return false;

            Mapper.Map(book, entity);

            try
            {
                await _context.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateException e)
            {

                if (e.InnerException is SqlException sqlException)
                {
                    if (sqlException.Number==547)
                    {
                        throw new MemberRelationException("Check entered value. ",
                            sqlException.Message);
                    }
                    else
                    {
                        throw;
                    }
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<Dictionary<long, int>> GetAvailableCountAsync(IEnumerable<long> books)
        {
           var result = await _context.Book.Where(a => books.Contains(a.Id))
                .Select(a => new { Id = a.Id, Count = a.AvailableCount }).ToDictionaryAsync(a => a.Id, a => a.Count);

           return result;
        }

        public async Task<IEnumerable<Book>> GetRangeAsync(int limit, int offset)
        {
            var result = await _context.Book.Include(b => b.Author).Include(b => b.Genre).Skip(offset).Take(limit)
                .ProjectTo<Book>().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Book>> GetRangeByAuthorIdAsync(long authorId, int limit, int offset)
        {
            var result =  await _context.Book.Include(b => b.Author).Include(b => b.Genre).Where(b => b.AuthorId == authorId)
                .Skip(offset).Take(limit)
                .ProjectTo<Book>().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Book>> GetRangeByGenreNameAsync(string genreName, int limit, int offset)
        {
            var result =  await _context.Book.Include(b => b.Author).Include(b => b.Genre).Where(b => b.Genre.Name == genreName)
                 .Skip(offset).Take(limit)
                 .ProjectTo<Book>().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Book>> GetRangeByTitleAsync(string title, int limit, int offset)
        {
            var result = await _context.Book.Include(b => b.Author).Include(b => b.Genre).Where(b => b.Title.ToLower().Contains(title.ToLower()))
                  .Skip(offset).Take(limit)
                  .ProjectTo<Book>().ToListAsync();
            return result;
        }
           
    }
}
