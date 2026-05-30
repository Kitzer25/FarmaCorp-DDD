using Core.Entities;
using Core.Ports;
using Core.Ports.Repositories;
using Infraestructure.Persistence; 
namespace Infraestructure.Persistence.Repositories;
public class OrderRepository : 
    GRepositories<Order>,
    IOrderRepository
{ 
    public OrderRepository(AppDbContext context) : base(context)
    { }  
}
