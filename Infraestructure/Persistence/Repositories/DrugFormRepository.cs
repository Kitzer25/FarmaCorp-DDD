using Core.Entities;
using Core.Ports;
using Core.Ports.Repositories;
using Infraestructure.Persistence; 
namespace Infraestructure.Persistence.Repositories;
public class DrugFormRepository : 
    GRepositories<DrugForm>,
    IDrugFormRepository
{ 
    public DrugFormRepository(AppDbContext context) : base(context)
    { }  
}
