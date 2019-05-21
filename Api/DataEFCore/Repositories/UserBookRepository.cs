using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataEFCore.Context;
using Domain.Exceptions;
using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEFCore.Repositories
{
    public class UserBookRepository : IUserBookRepository
    {
        private readonly LibraryAPIContext _context;
        public UserBookRepository(LibraryAPIContext context)
        {
            _context = context;
        }
        public async Task AddRangeAsync(IEnumerable<UserBook> userBooks)
        {
            var bookIds = userBooks.Select(u => u.BookId);

            var books = await _context.Book.Where(b => bookIds.Contains(b.Id)).ToListAsync();

            foreach(var item in userBooks)
            {
                var book = books.Find(b => b.Id == item.BookId);
                if (book.AvailableCount >= item.Count)
                {
                    var entity = Mapper.Map<Entities.UserBook>(item);
                    await _context.AddAsync(entity);
                    book.AvailableCount -= item.Count;
                   
                }
                else
                {
                    _context.Dispose();
                    throw new OrderedBookAvailabilityException("Books available count has been updated!");
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserBook>> GetUserBooksAsync(string userEmail)
        {
            var result = await _context.UserBook.Include(b => b.Book)
                .Where(ub => ub.User.Email == userEmail).ProjectTo<UserBook>().ToListAsync();
            return result;
        }

        public async Task UpdateAsync(long userId, long bookId, int newCount, int oldCount)
        {
            var userBook = await _context.UserBook.FirstOrDefaultAsync(u => u.UserId == userId && u.BookId == bookId);

            var book = await _context.Book.FirstOrDefaultAsync(b => b.Id == bookId);
            if (newCount == 0)
            {
                _context.Remove(userBook);
                book.AvailableCount += oldCount;                
            }
            else
            {
                userBook.Count = newCount;
                book.AvailableCount += (oldCount - newCount);
            }
            await _context.SaveChangesAsync();
        }
    }
}
