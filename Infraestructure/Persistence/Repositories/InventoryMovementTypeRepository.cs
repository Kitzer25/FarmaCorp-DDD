using Core.Entities;
using Core.Ports;
using Core.Ports.Repositories;
using Infraestructure.Persistence; 
namespace Infraestructure.Persistence.Repositories;
public class InventoryMovementTypeRepository : 
    GRepositories<InventoryMovementType>,
    IInventoryMovementTypeRepository
{ 
    public InventoryMovementTypeRepository(AppDbContext context) : base(context)
    { }  
}
