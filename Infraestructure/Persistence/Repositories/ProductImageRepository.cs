using Core.Entities;
using Core.Ports;
using Core.Ports.Repositories;
using Infraestructure.Persistence; 
namespace Infraestructure.Persistence.Repositories;
public class ProductImageRepository : 
    GRepositories<ProductImage>,
    IProductImageRepository
{ 
    public ProductImageRepository(AppDbContext context) : base(context)
    { }  
}
