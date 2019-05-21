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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }

        public async Task<User> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null)
            {
                throw new UserNotFoundException("This email is not used");
            }

            if (user.Password != password)
            {
                throw new UserNotFoundException("Password is incorrect");
            }

            return user;

        }

        public async Task RegistrationAsync(User newUser)
        {
            var isExist = await _userRepository.IsEmailUsedAsync(newUser.Email);

            if (isExist)
            {
                throw new UserIsAlreadyExistException(newUser.Email);
            }
            newUser.RoleId = 1;
            await _userRepository.AddAsync(newUser);

        }
    }
}
