using Core.Ports;
using Core.Ports.Repositories;
using Infraestructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly IDictionary<Type, object> _repositories;
    
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        _repositories = new Dictionary<Type, object>();
        
        
        //Implementación de Repositorios TODO
        UserRepo = new UserRepository(_context);
        RoleRepo = new RoleRepository(_context);
        ProductRepo = new ProductRepository(_context);
        ProductCategoryRepo = new ProductCategoryRepository(_context);
        ProductVariantRepo = new ProductVariantRepository(_context);
        ProductBatchRepo = new ProductBatchRepository(_context);
        InventoryRepo = new InventoryRepository(_context);
        InventoryMovementRepo = new InventoryMovementRepository(_context);
        OrderRepo = new OrderRepository(_context);
        OrderItemRepo = new OrderItemRepository(_context);
        OrderPaymentRepo = new OrderPaymentRepository(_context);
        PaymentMethodRepo = new PaymentMethodRepository(_context);
        CustomerRepo = new CustomerRepository(_context);
        CustomerAddressRepo = new CustomerAddressRepository(_context);
        PromotionRepo = new PromotionRepository(_context);
        PromotionCodeRepo = new PromotionCodeRepository(_context);
    }
    //Repositorios Específicos TODO
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