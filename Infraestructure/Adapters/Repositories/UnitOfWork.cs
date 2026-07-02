using Domain.Ports;
using Domain.Ports.Repositories;
using Domain.Ports.Repositories.ERepository;
using Infraestructure.Adapters.Repositories.ERepository;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infraestructure.Adapters.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly IDictionary<Type, object> _repositories;
    private IDbContextTransaction? _transaction;

    private IAuditLogRepository? _auditLogRepo;
    private IUserRepository? _userRepo;
    private IRoleRepository? _roleRepo;
    private IProductRepository? _productRepo;
    private IProductCategoryRepository? _productCategoryRepo;
    private IProductVariantRepository? _productVariantRepo;
    private IProductBatchRepository? _productBatchRepo;
    private ICartRepository? _cartRepo;
    private IInventoryRepository? _inventoryRepo;
    private IInventoryMovementTypeRepository? _inventoryMovementTypeRepo;
    private IInventoryMovementRepository? _inventoryMovementRepo;
    private IOrderRepository? _orderRepo;
    private IOrderItemRepository? _orderItemRepo;
    private IOrderPaymentRepository? _orderPaymentRepo;
    private IPaymentMethodRepository? _paymentMethodRepo;
    private ICustomerRepository? _customerRepo;
    private ICustomerAddressRepository? _customerAddressRepo;
    private ICartItemRepository? _cartItemRepo;
    private IPrescriptionUploadRepository? _prescriptionUploadRepo;
    private IPromotionRepository? _promotionRepo;
    private IPromotionCodeRepository? _promotionCodeRepo;
    private ICategoryRepository? _categoryRepo;
    private IOrderStatusHistoryRepository? _orderStatusHistoryRepo;
    private IVExpiringBatchRepository? _vExpiringBatchRepo;
    private IVAvalibleStockRepository? _vAvalibleStockRepo;
    private IVCustomerOrderSumaryRepository? _vCustomerOrderSumaryRepo;
    private IProductImageRepository? _productImageRepo;
    private ICustomerWishlistRepository? _customerWishlistRepo;
    private ILaboratoryRepository? _laboratoryRepo;
    private IDrugFormRepository? _drugFormRepo;
    private IMeasurementUnitRepository? _measurementUnitRepo;
    private IDiscountTypeRepository? _discountTypeRepo;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        _repositories = new Dictionary<Type, object>();
    }

    //Repositorios Específicos TODO
    public IAuditLogRepository AuditLogRepo => _auditLogRepo ??= new AuditLogRepository(_context);
    public IUserRepository UserRepo => _userRepo ??= new UserRepository(_context);
    public IRoleRepository RoleRepo => _roleRepo ??= new RoleRepository(_context);
    public IProductRepository ProductRepo => _productRepo ??= new ProductRepository(_context);
    public IProductCategoryRepository ProductCategoryRepo => _productCategoryRepo ??= new ProductCategoryRepository(_context);
    public IProductVariantRepository ProductVariantRepo => _productVariantRepo ??= new ProductVariantRepository(_context);
    public IProductBatchRepository ProductBatchRepo => _productBatchRepo ??= new ProductBatchRepository(_context);
    public ICartRepository CartRepo => _cartRepo ??= new CartRepository(_context);
    public IInventoryRepository InventoryRepo => _inventoryRepo ??= new InventoryRepository(_context);
    public IInventoryMovementTypeRepository InventoryMovementTypeRepo => _inventoryMovementTypeRepo ??= new InventoryMovementTypeRepository(_context);
    public IInventoryMovementRepository InventoryMovementRepo => _inventoryMovementRepo ??= new InventoryMovementRepository(_context);
    public IOrderRepository OrderRepo => _orderRepo ??= new OrderRepository(_context);
    public IOrderItemRepository OrderItemRepo => _orderItemRepo ??= new OrderItemRepository(_context);
    public IOrderPaymentRepository OrderPaymentRepo => _orderPaymentRepo ??= new OrderPaymentRepository(_context);
    public IPaymentMethodRepository PaymentMethodRepo => _paymentMethodRepo ??= new PaymentMethodRepository(_context);
    public ICustomerRepository CustomerRepo => _customerRepo ??= new CustomerRepository(_context);
    public ICustomerAddressRepository CustomerAddressRepo => _customerAddressRepo ??= new CustomerAddressRepository(_context);
    public ICartItemRepository CartItemRepo => _cartItemRepo ??= new CartItemRepository(_context);
    public IPrescriptionUploadRepository PrescriptionUploadRepo => _prescriptionUploadRepo ??= new PrescriptionUploadRepository(_context);
    public IPromotionRepository PromotionRepo => _promotionRepo ??= new PromotionRepository(_context);
    public IPromotionCodeRepository PromotionCodeRepo => _promotionCodeRepo ??= new PromotionCodeRepository(_context);
    public ICategoryRepository CategoryRepo => _categoryRepo ??= new CategoryRepository(_context);
    public IOrderStatusHistoryRepository OrderStatusHistoryRepo => _orderStatusHistoryRepo ??= new OrderStatusHistoryRepository(_context);
    public IVExpiringBatchRepository VExpiringBatchRepo => _vExpiringBatchRepo ??= new VExpiringBatchRepository(_context);
    public IVAvalibleStockRepository VAvalibleStockRepo => _vAvalibleStockRepo ??= new VAvalibleStockRepository(_context);
    public IVCustomerOrderSumaryRepository VCustomerOrderSumaryRepo => _vCustomerOrderSumaryRepo ??= new VCustomerOrderSumaryRepository(_context);
    public IProductImageRepository ProductImageRepo => _productImageRepo ??= new ProductImageRepository(_context);
    public ICustomerWishlistRepository CustomerWishlistRepo => _customerWishlistRepo ??= new CustomerWishlistRepository(_context);
    public ILaboratoryRepository LaboratoryRepo => _laboratoryRepo ??= new LaboratoryRepository(_context);
    public IDrugFormRepository DrugFormRepo => _drugFormRepo ??= new DrugFormRepository(_context);
    public IMeasurementUnitRepository MeasurementUnitRepo => _measurementUnitRepo ??= new MeasurementUnitRepository(_context);
    public IDiscountTypeRepository DiscountTypeRepo => _discountTypeRepo ??= new DiscountTypeRepository(_context);

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

    public async Task BeginTransactionAsync(CancellationToken ct)
    {
        if (_transaction != null)
        {
            return;
        }

        _transaction = await _context.Database.BeginTransactionAsync(ct);
    }

    public async Task CommitTransactionAsync(CancellationToken ct)
    {
        if (_transaction == null)
        {
            return;
        }

        await _transaction.CommitAsync(ct);
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    public async Task RollbackTransactionAsync(CancellationToken ct)
    {
        if (_transaction == null)
        {
            return;
        }

        await _transaction.RollbackAsync(ct);
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}
