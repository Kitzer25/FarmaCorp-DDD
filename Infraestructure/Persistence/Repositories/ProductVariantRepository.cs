using Core.Entities;
using Core.Ports;
using Core.Ports.Repositories;
using Infraestructure.Persistence; 
namespace Infraestructure.Persistence.Repositories;
public class ProductVariantRepository : 
    GRepositories<ProductVariant>,
    IProductVariantRepository
{ 
    public ProductVariantRepository(AppDbContext context) : base(context)
    { }  
}
