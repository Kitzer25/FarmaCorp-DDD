using Core.Entities;
using Core.Ports;
using Core.Ports.Repositories;
using Infraestructure.Persistence; 
namespace Infraestructure.Persistence.Repositories;
public class CartItemRepository : 
    GRepositories<CartItem>,
    ICartItemRepository
{ 
    public CartItemRepository(AppDbContext context) : base(context)
    { }  
}
