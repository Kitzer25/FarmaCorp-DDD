using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Application.Auth.Services;

namespace Application.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //MediatR
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly())
        );
        
        //Inyección de Dependencias
        services.AddScoped<JwtTokenGenerator>();
        
        return services;
    }
}