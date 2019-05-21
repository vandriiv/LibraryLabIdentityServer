using LibraryLab.TokenUtil;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryLab.Configurations
{
    public static class ConfigureAuthentication
    {
        public static IServiceCollection JwtAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {                            
                            ValidateIssuer = true,
                            
                            ValidIssuer = AuthOptions.ISSUER,
                           
                            ValidateAudience = true,
                            
                            ValidAudience = AuthOptions.AUDIENCE,
                           
                            ValidateLifetime = true,
                           
                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                           
                            ValidateIssuerSigningKey = true,
                        };
                    });
            services.AddAuthorization(opts => {
                opts.AddPolicy("Librarian", policy => {
                    policy.RequireClaim("RoleId", "2");
                });
            });

            return services;
        }
    }
}
