using Domain.Entities;
using Domain.Ports.Repositories.ERepository;
using Infraestructure.Context;

namespace Infraestructure.Adapters.Repositories.ERepository;
public class VAvalibleStockRepository : 
    GRepositories<VAvailableStock>,
    IVAvalibleStockRepository
{ 
    public VAvalibleStockRepository(AppDbContext context) : base(context)
    { }  
}
