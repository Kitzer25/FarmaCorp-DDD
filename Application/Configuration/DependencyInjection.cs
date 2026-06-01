using System.Reflection;
using Application.AdminProducts.Services;
using Application.Audit.Services;
using Application.Auth.Services;
using Application.Batches.Services;
using Application.Cart.Services;
using Application.Inventory.Services;
using Application.Notifications.Services;
using Application.Orders.Services;
using Application.Prescriptions.Services;
using Application.Promotions.Services;
using Application.Reports.Services;
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

        return services;
    }
}
