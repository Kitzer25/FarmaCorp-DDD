using Core.Entities;
using Core.Ports;
using Core.Ports.Repositories;
using Infraestructure.Persistence; 
namespace Infraestructure.Persistence.Repositories;
public class VCustomerOrderSumaryRepository : 
    GRepositories<VCustomerOrderSummary>,
    IVCustomerOrderSumaryRepository
{ 
    public VCustomerOrderSumaryRepository(AppDbContext context) : base(context)
    { }  
}
