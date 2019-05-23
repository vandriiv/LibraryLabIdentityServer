using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
namespace Api.Configurations
{
    public static class ConfigureIdentityService
    {
        public static IServiceCollection IdentityServiceAuthoProvided(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.Authority = "http://localhost:5000";
                o.Audience = "resourceapi";
                o.RequireHttpsMetadata = false;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiReader", policy => policy.RequireClaim("scope", "api.read"));
                options.AddPolicy("Consumer", policy => policy.RequireClaim(ClaimTypes.Role, "consumer"));
                options.AddPolicy("Admin", policy => policy.RequireClaim(ClaimTypes.Role, "admin"));
            });

            return services;
        }
    }
}
