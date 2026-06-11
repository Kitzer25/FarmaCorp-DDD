using Core.Entities;
using Core.Ports.Repositories;
using Core.Ports.Repositories.ERepository;
using Infraestructure.Context;

namespace Infraestructure.Adapters.Repositories.ERepository;
public class PaymentMethodRepository : 
    GRepositories<PaymentMethod>,
    IPaymentMethodRepository
{ 
    public PaymentMethodRepository(AppDbContext context) : base(context)
    { }  
}
