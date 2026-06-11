using Core.Entities;
using Core.Ports.Repositories;
using Core.Ports.Repositories.ERepository;
using Infraestructure.Context;

namespace Infraestructure.Adapters.Repositories.ERepository;
public class OrderPaymentRepository : 
    GRepositories<OrderPayment>,
    IOrderPaymentRepository
{ 
    public OrderPaymentRepository(AppDbContext context) : base(context)
    { }  
}
