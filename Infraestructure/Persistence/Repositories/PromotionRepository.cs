using Core.Entities;
using Core.Ports;
using Core.Ports.Repositories;
using Infraestructure.Persistence; 
namespace Infraestructure.Persistence.Repositories;
public class PromotionRepository : 
    GRepositories<Promotion>,
    IPromotionRepository
{ 
    public PromotionRepository(AppDbContext context) : base(context)
    { }  
}
