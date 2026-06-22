using Core.Ports.Repositories.ERepository;

namespace Core.Ports.Repositories;

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

    public ICartRepository CartRepo { get; }
    public IInventoryRepository InventoryRepo { get; }
    public IInventoryMovementTypeRepository InventoryMovementTypeRepo { get; }
    public IInventoryMovementRepository InventoryMovementRepo { get; }

    public IOrderRepository OrderRepo { get; }
    public IOrderItemRepository OrderItemRepo { get; }
    public IOrderPaymentRepository OrderPaymentRepo { get; }

    public IPaymentMethodRepository PaymentMethodRepo { get; }

    public ICustomerRepository CustomerRepo { get; }
    public ICustomerAddressRepository CustomerAddressRepo { get; }
    public ICartItemRepository CartItemRepo { get; }
    public IPrescriptionUploadRepository PrescriptionUploadRepo { get; }

    public IPromotionRepository PromotionRepo { get; }
    public IPromotionCodeRepository PromotionCodeRepo { get; }
    
    public ICategoryRepository CategoryRepo { get; }
}
