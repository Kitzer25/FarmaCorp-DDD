using Core.Ports.Repositories;

namespace Core.Ports;

public interface IUnitOfWork : IDisposable
{
    public IGRepositories<T> Repositories<T>() where T : class;
    
    Task<int> SaveChangesAsync(CancellationToken ct);
    
    //Repositories
    public IUserRepository UserRepo { get; }
    public IRoleRepository RoleRepo { get; }

    public IProductRepository ProductRepo { get; }
    public IProductCategoryRepository ProductCategoryRepo { get; }
    public IProductVariantRepository ProductVariantRepo { get; }
    public IProductBatchRepository ProductBatchRepo { get; }

    public IInventoryRepository InventoryRepo { get; }
    public IInventoryMovementRepository InventoryMovementRepo { get; }

    public IOrderRepository OrderRepo { get; }
    public IOrderItemRepository OrderItemRepo { get; }
    public IOrderPaymentRepository OrderPaymentRepo { get; }

    public IPaymentMethodRepository PaymentMethodRepo { get; }

    public ICustomerRepository CustomerRepo { get; }
    public ICustomerAddressRepository CustomerAddressRepo { get; }

    public IPromotionRepository PromotionRepo { get; }
    public IPromotionCodeRepository PromotionCodeRepo { get; }
}