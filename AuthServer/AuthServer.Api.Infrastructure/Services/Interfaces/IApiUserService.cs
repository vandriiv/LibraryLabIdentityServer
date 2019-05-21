using AuthServer.Api.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Api.Infrastructure.Services.Interfaces
{
    public interface IApiUserService
    {
        Task AddAsync(User user);
    }
}
