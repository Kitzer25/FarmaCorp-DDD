using Domain.Ports;
using Domain.Ports.Repositories;
using Domain.Ports.Repositories.ERepository;
using Infraestructure.Adapters.Repositories.ERepository;
using Infraestructure.Context;

namespace Infraestructure.Adapters.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly IDictionary<Type, object> _repositories;
    
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        _repositories = new Dictionary<Type, object>();
        
        
        //Implementación de Repositorios TODO
        AuditLogRepo = new AuditLogRepository(_context);
        UserRepo = new UserRepository(_context);
        RoleRepo = new RoleRepository(_context);
        ProductRepo = new ProductRepository(_context);
        ProductCategoryRepo = new ProductCategoryRepository(_context);
        ProductVariantRepo = new ProductVariantRepository(_context);
        ProductBatchRepo = new ProductBatchRepository(_context);
        CartRepo = new CartRepository(_context);
        InventoryRepo = new InventoryRepository(_context);
        InventoryMovementTypeRepo = new InventoryMovementTypeRepository(_context);
        InventoryMovementRepo = new InventoryMovementRepository(_context);
        OrderRepo = new OrderRepository(_context);
        OrderItemRepo = new OrderItemRepository(_context);
        OrderPaymentRepo = new OrderPaymentRepository(_context);
        PaymentMethodRepo = new PaymentMethodRepository(_context);
        CustomerRepo = new CustomerRepository(_context);
        CustomerAddressRepo = new CustomerAddressRepository(_context);
        CartItemRepo = new CartItemRepository(_context);
        PrescriptionUploadRepo = new PrescriptionUploadRepository(_context);
        PromotionRepo = new PromotionRepository(_context);
        PromotionCodeRepo = new PromotionCodeRepository(_context);
        CategoryRepo = new CategoryRepository(_context);
        OrderStatusHistoryRepo = new OrderStatusHistoryRepository(_context);
        VExpiringBatchRepo = new VExpiringBatchRepository(_context);
        VAvalibleStockRepo = new VAvalibleStockRepository(_context);
        VCustomerOrderSumaryRepo = new VCustomerOrderSumaryRepository(_context);
        ProductImageRepo = new ProductImageRepository(_context);
        CustomerWishlistRepo = new CustomerWishlistRepository(_context);
        LaboratoryRepo = new LaboratoryRepository(_context);
        DrugFormRepo = new DrugFormRepository(_context);
        MeasurementUnitRepo = new MeasurementUnitRepository(_context);
        DiscountTypeRepo = new DiscountTypeRepository(_context);
    }
    //Repositorios Específicos TODO
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

    //Funcionalidades
    public IGRepositories<T> Repositories<T>() where T : class
    {
        var type = typeof(T);

        if (_repositories.TryGetValue(type, out var repositories))
        {
            return (IGRepositories<T>)repositories;
        }
        
        var repositoryInstance = new GRepositories<T>(_context);
        
        _repositories.Add(type, repositoryInstance);
        
        return repositoryInstance;
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct)
    {
        return await _context.SaveChangesAsync(ct);
    }


    public void Dispose()
    {
        _context.Dispose();
    }
}
