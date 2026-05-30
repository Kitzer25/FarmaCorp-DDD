using Core.Entities;
using Core.Ports;
using Core.Ports.Repositories;
using Infraestructure.Persistence; 
namespace Infraestructure.Persistence.Repositories;
public class CustomerWishlistRepository : 
    GRepositories<CustomerWishlist>,
    ICustomerWishlistRepository
{ 
    public CustomerWishlistRepository(AppDbContext context) : base(context)
    { }  
}
