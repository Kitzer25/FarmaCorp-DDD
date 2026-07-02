using Domain.Ports.Repositories.ERepository;

namespace Domain.Ports.Repositories;

public interface IUnitOfWork : IDisposable
{
    public IGRepositories<T> Repositories<T>() where T : class;
    
    Task<int> SaveChangesAsync(CancellationToken ct);
    
    //Repositories
    public IAuditLogRepository AuditLogRepo { get; }
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

    public IOrderStatusHistoryRepository OrderStatusHistoryRepo { get; }
    public IVExpiringBatchRepository VExpiringBatchRepo { get; }
    public IVAvalibleStockRepository VAvalibleStockRepo { get; }
    public IVCustomerOrderSumaryRepository VCustomerOrderSumaryRepo { get; }
    public IProductImageRepository ProductImageRepo { get; }
    public ICustomerWishlistRepository CustomerWishlistRepo { get; }
    public ILaboratoryRepository LaboratoryRepo { get; }
    public IDrugFormRepository DrugFormRepo { get; }
    public IMeasurementUnitRepository MeasurementUnitRepo { get; }
    public IDiscountTypeRepository DiscountTypeRepo { get; }
}
