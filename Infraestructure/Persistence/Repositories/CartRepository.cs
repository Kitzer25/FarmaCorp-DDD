using Core.Entities;
using Core.Ports;
using Core.Ports.Repositories;
using Infraestructure.Persistence; 
namespace Infraestructure.Persistence.Repositories;
public class CartRepository : 
    GRepositories<Cart>,
    ICartRepository
{ 
    public CartRepository(AppDbContext context) : base(context)
    { }  
}
