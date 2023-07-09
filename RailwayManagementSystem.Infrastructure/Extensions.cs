using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Core.Abstractions;
using RailwayManagementSystem.Infrastructure.Auth;
using RailwayManagementSystem.Infrastructure.DAL;
using RailwayManagementSystem.Infrastructure.DAL.Queries;
using RailwayManagementSystem.Infrastructure.Exceptions;
using RailwayManagementSystem.Infrastructure.Logging;
using RailwayManagementSystem.Infrastructure.Security;
using RailwayManagementSystem.Infrastructure.Time;

namespace RailwayManagementSystem.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ExceptionMiddleware>();
        services.AddHttpContextAccessor();
        
        services
            .AddPostgres(configuration)
            .AddSingleton<IClock, Clock>();

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
                    new string[]{}
                }
            });
        });
        
        services.AddQueries();

        services.AddAuth(configuration);
        
        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseAuthentication();
        app.UseAuthorization();
        
        return app;
    }
}