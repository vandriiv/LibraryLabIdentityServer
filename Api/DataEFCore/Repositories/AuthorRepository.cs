using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataEFCore.Context;
using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataEFCore.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryAPIContext _context;
        public AuthorRepository(LibraryAPIContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Author newAuthor)
        {
            var entity = Mapper.Map<Entities.Author>(newAuthor);
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _context.Author.ProjectTo<Author>().ToListAsync();
        }

        public async Task<bool> UpdateAsync(Author author)
        {
            var entity = await _context.Author.FirstOrDefaultAsync(a => a.Id == author.Id);
            if (entity == null)
            {
                return false;
            }

            Mapper.Map(author, entity);

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
