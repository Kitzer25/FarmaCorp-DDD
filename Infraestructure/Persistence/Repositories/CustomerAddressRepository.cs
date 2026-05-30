using Core.Entities;
using Core.Ports;
using Core.Ports.Repositories;
using Infraestructure.Persistence; 
namespace Infraestructure.Persistence.Repositories;
public class CustomerAddressRepository : 
    GRepositories<CustomerAddress>,
    ICustomerAddressRepository
{ 
    public CustomerAddressRepository(AppDbContext context) : base(context)
    { }  
}
