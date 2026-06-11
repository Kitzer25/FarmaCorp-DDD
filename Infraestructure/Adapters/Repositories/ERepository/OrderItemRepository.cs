using Core.Entities;
using Core.Ports.Repositories;
using Core.Ports.Repositories.ERepository;
using Infraestructure.Context;

namespace Infraestructure.Adapters.Repositories.ERepository;
public class OrderItemRepository : 
    GRepositories<OrderItem>,
    IOrderItemRepository
{ 
    public OrderItemRepository(AppDbContext context) : base(context)
    { }  
}
