using Core.Entities;
using Core.Ports.Repositories;
using Core.Ports.Repositories.ERepository;
using Infraestructure.Context;

namespace Infraestructure.Adapters.Repositories.ERepository;
public class MeasurementUnitRepository : 
    GRepositories<MeasurementUnit>,
    IMeasurementUnitRepository
{ 
    public MeasurementUnitRepository(AppDbContext context) : base(context)
    { }  
}
