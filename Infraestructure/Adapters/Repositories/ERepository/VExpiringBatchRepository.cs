using Domain.Entities;
using Domain.Ports.Repositories.ERepository;
using Infraestructure.Context;

namespace Infraestructure.Adapters.Repositories.ERepository;
public class VExpiringBatchRepository : 
    GRepositories<VExpiringBatch>,
    IVExpiringBatchRepository
{ 
    public VExpiringBatchRepository(AppDbContext context) : base(context)
    { }  
}
