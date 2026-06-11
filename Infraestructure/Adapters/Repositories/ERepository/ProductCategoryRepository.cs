using Core.Entities;
using Core.Ports.Repositories;
using Core.Ports.Repositories.ERepository;
using Infraestructure.Context;

namespace Infraestructure.Adapters.Repositories.ERepository;
public class ProductCategoryRepository : 
    GRepositories<ProductCategory>,
    IProductCategoryRepository
{ 
    public ProductCategoryRepository(AppDbContext context) : base(context)
    { }  
}
