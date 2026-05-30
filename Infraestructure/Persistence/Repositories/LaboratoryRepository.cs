using Core.Entities;
using Core.Ports.Repositories;
namespace Infraestructure.Persistence.Repositories;
public class LaboratoryRepository : 
    GRepositories<Laboratory>,
    ILaboratoryRepository
{ 
    public LaboratoryRepository(AppDbContext context) : base(context)
    { }  
}
