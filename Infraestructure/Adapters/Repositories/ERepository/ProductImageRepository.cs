using Core.Entities;
using Core.Ports.Repositories;
using Core.Ports.Repositories.ERepository;
using Infraestructure.Context;

namespace Infraestructure.Adapters.Repositories.ERepository;
public class ProductImageRepository : 
    GRepositories<ProductImage>,
    IProductImageRepository
{ 
    public ProductImageRepository(AppDbContext context) : base(context)
    { }  
}
