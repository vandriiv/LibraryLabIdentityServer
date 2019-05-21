using AutoMapper;
using DataEFCore.Context;
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
    public class UserRepository : IUserRepository
    {
        private readonly LibraryAPIContext _context;
        public UserRepository(LibraryAPIContext context)
        {
            _context = context;
        }
        public async Task AddAsync(User newUser)
        {
            var entity = Mapper.Map<Entities.User>(newUser);
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Email == email);

            return Mapper.Map<User>(user);
        }      

        public async Task<bool> IsEmailUsedAsync(string email)
        {
            return await _context.User.AnyAsync(u => u.Email == email);
        }
    }
}
