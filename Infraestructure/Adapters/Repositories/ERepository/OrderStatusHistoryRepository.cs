using Core.Entities;
using Core.Ports.Repositories;
using Core.Ports.Repositories.ERepository;
using Infraestructure.Context;

namespace Infraestructure.Adapters.Repositories.ERepository;
public class OrderStatusHistoryRepository : 
    GRepositories<OrderStatusHistory>,
    IOrderStatusHistoryRepository
{ 
    public OrderStatusHistoryRepository(AppDbContext context) : base(context)
    { }  
}
