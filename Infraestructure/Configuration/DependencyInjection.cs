using System.Net.Mime;
using Domain.Ports;
using Domain.Ports.Repositories;
using Domain.Ports.Repositories.ERepository;
using Domain.Ports.Services.AuthService;
using Infraestructure.Adapters.Repositories;
using Infraestructure.Adapters.Repositories.ERepository;
using Infraestructure.Adapters.Services.AuthService;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure.Configuration;


public static class DependencyInjection
{
    // IServiceCollection funciona con EFCore
    public static IServiceCollection Addinfraestructure(this IServiceCollection services)
    {
        //Inyección de Dependencias
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IGRepositories<>), typeof(GRepositories<>));
        
        //Interface - Implementacion
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
        services.AddScoped<IProductVariantRepository, ProductVariantRepository>();
        services.AddScoped<IProductBatchRepository, ProductBatchRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<IInventoryRepository, InventoryRepository>();
        services.AddScoped<IInventoryMovementTypeRepository, InventoryMovementTypeRepository>();
        services.AddScoped<IInventoryMovementRepository, InventoryMovementRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        services.AddScoped<IOrderPaymentRepository, OrderPaymentRepository>();
        services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ICustomerAddressRepository, CustomerAddressRepository>();
        services.AddScoped<ICartItemRepository, CartItemRepository>();
        services.AddScoped<IPrescriptionUploadRepository, PrescriptionUploadRepository>();
        services.AddScoped<IPromotionRepository, PromotionRepository>();
        services.AddScoped<IPromotionCodeRepository, PromotionCodeRepository>();
        services.AddScoped<IOrderStatusHistoryRepository, OrderStatusHistoryRepository>();
        services.AddScoped<IVExpiringBatchRepository, VExpiringBatchRepository>();
        services.AddScoped<IVAvalibleStockRepository, VAvalibleStockRepository>();
        services.AddScoped<IVCustomerOrderSumaryRepository, VCustomerOrderSumaryRepository>();
        services.AddScoped<IProductImageRepository, ProductImageRepository>();
        services.AddScoped<ICustomerWishlistRepository, CustomerWishlistRepository>();
        services.AddScoped<ILaboratoryRepository, LaboratoryRepository>();
        services.AddScoped<IDrugFormRepository, DrugFormRepository>();
        services.AddScoped<IMeasurementUnitRepository, MeasurementUnitRepository>();
        services.AddScoped<IDiscountTypeRepository, DiscountTypeRepository>();

        //Security
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IHashPassword, HashPassword>();
        
        return services;
    }
}
