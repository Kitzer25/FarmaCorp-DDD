using Core.Entities;
using Core.Ports;
using Core.Ports.Repositories;
using Infraestructure.Persistence; 
namespace Infraestructure.Persistence.Repositories;
public class ProductRepository : 
    GRepositories<Product>,
    IProductRepository
{ 
    public ProductRepository(AppDbContext context) : base(context)
    { }  
}
