using Core.Entities;
using Core.Ports;
using Core.Ports.Repositories;
using Infraestructure.Persistence; 
namespace Infraestructure.Persistence.Repositories;
public class CustomerRepository : 
    GRepositories<Customer>,
    ICustomerRepository
{ 
    public CustomerRepository(AppDbContext context) : base(context)
    { }  
}
