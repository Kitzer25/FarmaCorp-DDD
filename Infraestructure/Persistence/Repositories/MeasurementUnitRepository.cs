using Core.Entities;
using Core.Ports;
using Core.Ports.Repositories;
using Infraestructure.Persistence; 
namespace Infraestructure.Persistence.Repositories;
public class MeasurementUnitRepository : 
    GRepositories<MeasurementUnit>,
    IMeasurementUnitRepository
{ 
    public MeasurementUnitRepository(AppDbContext context) : base(context)
    { }  
}
