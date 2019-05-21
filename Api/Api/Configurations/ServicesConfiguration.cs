using DataEFCore.Repositories;
using Domain.Repositories;
using Domain.Services;
using Domain.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryLab.Configurations
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>()
                    .AddScoped<IBookRepository, BookRepository>()
                    .AddScoped<IUserBookRepository, UserBookRepository>()
                    .AddScoped<IAuthorRepository, AuthorRepository>();

            return services;
        }
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>()
                    .AddScoped<IOrderService, OrderService>()
                    .AddScoped<IUserService, UserService>();

            return services;
        }
        
    }
}
