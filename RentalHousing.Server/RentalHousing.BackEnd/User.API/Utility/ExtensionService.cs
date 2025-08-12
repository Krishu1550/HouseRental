using AutoMapper;
using User.Application.Interface;
using User.Application.ProfileMapper;
using User.Application.Service;
using User.Domain.Repositories;
using User.Infrastructure.Respositories;

namespace User.API.Utility
{
    public static class ExtensionService
    {

        public static IServiceCollection AddExtensionServices(this IServiceCollection services)
        {
            // Add any extension services here
            // Example: services.AddScoped<IMyService, MyService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();

            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddAutoMapper(typeof(UserProfile));
            return services;
        }
    }
}
