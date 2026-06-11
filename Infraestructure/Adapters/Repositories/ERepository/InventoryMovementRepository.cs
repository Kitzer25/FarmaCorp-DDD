using Core.Entities;
using Core.Ports.Repositories;
using Core.Ports.Repositories.ERepository;
using Infraestructure.Context;

namespace Infraestructure.Adapters.Repositories.ERepository;
public class InventoryMovementRepository : 
    GRepositories<InventoryMovement>,
    IInventoryMovementRepository
{ 
    public InventoryMovementRepository(AppDbContext context) : base(context)
    { }  
}
