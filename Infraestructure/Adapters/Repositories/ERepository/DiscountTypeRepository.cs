using Core.Entities;
using Core.Ports.Repositories;
using Core.Ports.Repositories.ERepository;
using Infraestructure.Context;

namespace Infraestructure.Adapters.Repositories.ERepository;
public class DiscountTypeRepository : 
    GRepositories<DiscountType>,
    IDiscountTypeRepository
{ 
    public DiscountTypeRepository(AppDbContext context) : base(context)
    { }  
}
