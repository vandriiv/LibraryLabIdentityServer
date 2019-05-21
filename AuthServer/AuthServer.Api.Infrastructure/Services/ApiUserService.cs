using AuthServer.Api.Infrastructure.Context;
using AuthServer.Api.Infrastructure.Models;
using AuthServer.Api.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Api.Infrastructure.Services
{
    public class ApiUserService : IApiUserService
    {
        private readonly LibraryAPIContext _context;
        public ApiUserService(LibraryAPIContext context)
        {
            _context = context;
        }        
        public async Task AddAsync(User user)
        {
            user.RoleId = 1;
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
