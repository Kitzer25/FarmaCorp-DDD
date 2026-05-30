using Core.Entities;
using Core.Ports;
using Core.Ports.Repositories;
using Infraestructure.Persistence; 
namespace Infraestructure.Persistence.Repositories;
public class PrescriptionUploadRepository : 
    GRepositories<PrescriptionUpload>,
    IPrescriptionUploadRepository
{ 
    public PrescriptionUploadRepository(AppDbContext context) : base(context)
    { }  
}
