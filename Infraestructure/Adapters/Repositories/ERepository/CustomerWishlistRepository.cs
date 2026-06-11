using Core.Entities;
using Core.Ports.Repositories;
using Core.Ports.Repositories.ERepository;
using Infraestructure.Context;

namespace Infraestructure.Adapters.Repositories.ERepository;
public class CustomerWishlistRepository : 
    GRepositories<CustomerWishlist>,
    ICustomerWishlistRepository
{ 
    public CustomerWishlistRepository(AppDbContext context) : base(context)
    { }  
}
