using Core.Entities;
using Core.Ports;
using Core.Ports.Repositories;
using Infraestructure.Persistence; 
namespace Infraestructure.Persistence.Repositories;
public class PromotionCodeRepository : 
    GRepositories<PromotionCode>,
    IPromotionCodeRepository
{ 
    public PromotionCodeRepository(AppDbContext context) : base(context)
    { }  
}
