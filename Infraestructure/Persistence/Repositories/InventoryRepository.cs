using Core.Entities;
using Core.Ports;
using Core.Ports.Repositories;
using Infraestructure.Persistence; 
namespace Infraestructure.Persistence.Repositories;
public class InventoryRepository : 
    GRepositories<Inventory>,
    IInventoryRepository
{ 
    public InventoryRepository(AppDbContext context) : base(context)
    { }  
}
