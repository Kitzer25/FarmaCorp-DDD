using System.Reflection;
using Core.Ports.Services;
using Core.Ports.Services.EServices;
using Infraestructure.Adapters.Services;
using Infraestructure.Adapters.Services.AuthService;
using Infraestructure.Adapters.Services.EServices;
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
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IPrescriptionService, PrescriptionService>();
        services.AddScoped<IInventoryAdminService, InventoryAdminService>();
        services.AddScoped<IBatchService, BatchService>();
        services.AddScoped<IAdminProductService, AdminProductService>();
        services.AddScoped<IPromotionService, PromotionService>();
        services.AddScoped<IReportService, ReportService>();
        services.AddScoped<IEmailService, SimulatedEmailService>();
        services.AddScoped<IAuditService, AuditService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        
        return services;
    }
}
