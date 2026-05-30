using Core.Entities;
using Core.Ports;
using Core.Ports.Repositories;
using Infraestructure.Persistence; 
namespace Infraestructure.Persistence.Repositories;
public class PaymentMethodRepository : 
    GRepositories<PaymentMethod>,
    IPaymentMethodRepository
{ 
    public PaymentMethodRepository(AppDbContext context) : base(context)
    { }  
}
