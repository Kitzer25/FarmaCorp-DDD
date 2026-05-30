using Core.Entities;
using Core.Ports;
using Core.Ports.Repositories;
using Infraestructure.Persistence; 
namespace Infraestructure.Persistence.Repositories;
public class ProductBatchRepository : 
    GRepositories<ProductBatch>,
    IProductBatchRepository
{ 
    public ProductBatchRepository(AppDbContext context) : base(context)
    { }  
}
