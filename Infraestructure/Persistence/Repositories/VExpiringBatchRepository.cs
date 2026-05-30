using Core.Entities;
using Core.Ports;
using Core.Ports.Repositories;
using Infraestructure.Persistence; 
namespace Infraestructure.Persistence.Repositories;
public class VExpiringBatchRepository : 
    GRepositories<VExpiringBatch>,
    IVExpiringBatchRepository
{ 
    public VExpiringBatchRepository(AppDbContext context) : base(context)
    { }  
}
