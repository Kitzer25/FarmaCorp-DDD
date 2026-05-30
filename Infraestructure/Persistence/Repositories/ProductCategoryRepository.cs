using Core.Entities;
using Core.Ports;
using Core.Ports.Repositories;
using Infraestructure.Persistence; 
namespace Infraestructure.Persistence.Repositories;
public class ProductCategoryRepository : 
    GRepositories<ProductCategory>,
    IProductCategoryRepository
{ 
    public ProductCategoryRepository(AppDbContext context) : base(context)
    { }  
}
