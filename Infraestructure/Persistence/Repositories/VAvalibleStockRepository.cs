using Core.Entities;
using Core.Ports;
using Core.Ports.Repositories;
using Infraestructure.Persistence; 
namespace Infraestructure.Persistence.Repositories;
public class VAvalibleStockRepository : 
    GRepositories<VAvailableStock>,
    IVAvalibleStockRepository
{ 
    public VAvalibleStockRepository(AppDbContext context) : base(context)
    { }  
}
