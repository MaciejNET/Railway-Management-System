using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RailwayManagementSystem.Application.Security;
using RailwayManagementSystem.Core.Entities;

namespace RailwayManagementSystem.Infrastructure.Security;

internal static class Extensions
{
    public static IServiceCollection AddSecurity(this IServiceCollection services)
    {
        services
            .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
            .AddSingleton<IPasswordManager, PasswordManager>();

        return services;
    }
}