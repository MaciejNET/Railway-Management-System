using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace RailwayManagementSystem.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.Configure<JsonOptions>(opt => opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
        
        return services;
    }
}