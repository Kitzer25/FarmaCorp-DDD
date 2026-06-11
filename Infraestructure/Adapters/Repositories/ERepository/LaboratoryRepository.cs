using Core.Entities;
using Core.Ports.Repositories;
using Core.Ports.Repositories.ERepository;
using Infraestructure.Context;

namespace Infraestructure.Adapters.Repositories.ERepository;
public class LaboratoryRepository : 
    GRepositories<Laboratory>,
    ILaboratoryRepository
{ 
    public LaboratoryRepository(AppDbContext context) : base(context)
    { }  
}
