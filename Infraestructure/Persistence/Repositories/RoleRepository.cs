using Core.Entities;
using Core.Ports;
using Core.Ports.Repositories;
using Infraestructure.Persistence; 
namespace Infraestructure.Persistence.Repositories;
public class RoleRepository : 
    GRepositories<Role>,
    IRoleRepository
{ 
    public RoleRepository(AppDbContext context) : base(context)
    { }  
}
