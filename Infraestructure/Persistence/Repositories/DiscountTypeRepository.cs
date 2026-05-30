using Core.Entities;
using Core.Ports;
using Core.Ports.Repositories;
using Infraestructure.Persistence; 
namespace Infraestructure.Persistence.Repositories;
public class DiscountTypeRepository : 
    GRepositories<DiscountType>,
    IDiscountTypeRepository
{ 
    public DiscountTypeRepository(AppDbContext context) : base(context)
    { }  
}
