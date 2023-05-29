using Microsoft.Extensions.DependencyInjection;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Infrastructure.DAL.Repositories;

internal static class Extensions
{
    internal static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAdminRepository, PostgresAdminRepository>();
        services.AddScoped<ICarrierRepository, PostgresCarrierRepository>();
        services.AddScoped<IDiscountRepository, PostgresDiscountRepository>();
        services.AddScoped<IPassengerRepository, PostgresPassengerRepository>();
        services.AddScoped<ISeatRepository, PostgresSeatRepository>();
        services.AddScoped<IStationRepository, PostgresStationRepository>();
        services.AddScoped<ITrainRepository, PostgresTrainRepository>();
        services.AddScoped<ITripRepository, PostgresTripRepository>();
        
        return services;
    }
}