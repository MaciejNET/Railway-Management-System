using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Infrastructure.DAL.Repositories;

namespace RailwayManagementSystem.Infrastructure.DAL;

internal static class Extensions
{
    private const string SectionName = "postgres";
    
    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection(SectionName);
        services.Configure<PostgresOptions>(section);
        var options = configuration.GetOptions<PostgresOptions>(SectionName);

        services.AddDbContext<RailwayManagementSystemDbContext>(x => x.UseNpgsql(options.ConnectionString));
        
        services.AddRepositories();
        
        services.AddHostedService<DatabaseInitializer>();
        return services;
    }

    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = configuration.GetSection(sectionName);
        section.Bind(options);

        return options;
    }
}