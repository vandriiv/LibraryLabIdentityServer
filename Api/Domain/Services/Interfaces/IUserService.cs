using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> LoginAsync(string email, string password);
        Task RegistrationAsync(User newUser);
        Task<User> GetUserByEmailAsync(string email);
    }
}
