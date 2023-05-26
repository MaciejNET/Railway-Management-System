using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Commands;

namespace RailwayManagementSystem.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddCommands();

        return services;
    }
}