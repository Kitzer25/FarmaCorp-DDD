using Core.Entities;
using Core.Ports;
using Core.Ports.Repositories;
using Infraestructure.Persistence; 
namespace Infraestructure.Persistence.Repositories;
public class OrderStatusHistoryRepository : 
    GRepositories<OrderStatusHistory>,
    IOrderStatusHistoryRepository
{ 
    public OrderStatusHistoryRepository(AppDbContext context) : base(context)
    { }  
}
