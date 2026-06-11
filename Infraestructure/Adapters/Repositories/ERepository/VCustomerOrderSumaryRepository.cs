using Core.Entities;
using Core.Ports.Repositories;
using Core.Ports.Repositories.ERepository;
using Infraestructure.Context;

namespace Infraestructure.Adapters.Repositories.ERepository;
public class VCustomerOrderSumaryRepository : 
    GRepositories<VCustomerOrderSummary>,
    IVCustomerOrderSumaryRepository
{ 
    public VCustomerOrderSumaryRepository(AppDbContext context) : base(context)
    { }  
}
