using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User newUser);
        Task<User> GetByEmailAsync(string email);
        Task<bool> IsEmailUsedAsync(string email);
       
    }
}
