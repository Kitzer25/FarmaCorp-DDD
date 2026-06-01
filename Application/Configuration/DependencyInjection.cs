using System.Reflection;
using Application.Auth.Services;
using Application.Cart.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly())
        );

        services.AddScoped<JwtTokenGenerator>();
        services.AddScoped<ICartService, CartService>();

        return services;
    }
}
