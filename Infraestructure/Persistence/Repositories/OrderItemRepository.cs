using Core.Entities;
using Core.Ports;
using Core.Ports.Repositories;
using Infraestructure.Persistence; 
namespace Infraestructure.Persistence.Repositories;
public class OrderItemRepository : 
    GRepositories<OrderItem>,
    IOrderItemRepository
{ 
    public OrderItemRepository(AppDbContext context) : base(context)
    { }  
}
