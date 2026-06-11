using Core.Entities;
using Core.Ports.Repositories;
using Core.Ports.Repositories.ERepository;
using Infraestructure.Context;

namespace Infraestructure.Adapters.Repositories.ERepository;
public class DrugFormRepository : 
    GRepositories<DrugForm>,
    IDrugFormRepository
{ 
    public DrugFormRepository(AppDbContext context) : base(context)
    { }  
}
