using Core.Entities;
using Core.Ports;
using Core.Ports.Repositories;
using Infraestructure.Persistence; 
namespace Infraestructure.Persistence.Repositories;
public class InventoryMovementRepository : 
    GRepositories<InventoryMovement>,
    IInventoryMovementRepository
{ 
    public InventoryMovementRepository(AppDbContext context) : base(context)
    { }  
}
