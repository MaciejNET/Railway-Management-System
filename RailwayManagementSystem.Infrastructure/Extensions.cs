using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using RailwayManagementSystem.Infrastructure.Auth;
using RailwayManagementSystem.Infrastructure.DAL;
using RailwayManagementSystem.Infrastructure.DAL.Queries;
using RailwayManagementSystem.Infrastructure.Exceptions;
using RailwayManagementSystem.Infrastructure.Logging;
using RailwayManagementSystem.Infrastructure.Security;

namespace RailwayManagementSystem.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        
        services
            .AddPostgres(configuration)
            .AddSingleton(TimeProvider.System);

        services.AddCustomLogging();
        
        services.AddSecurity();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(swagger =>
        {
            swagger.EnableAnnotations();
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "RailwayManagementSystem Api",
                Version = "v1"
            });
            swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        
        services.AddQueries();
        services.AddExceptionHandler<CustomExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        
        services.AddAuth(configuration);
        
        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseExceptionHandler();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseAuthentication();
        app.UseAuthorization();
        
        return app;
    }
}