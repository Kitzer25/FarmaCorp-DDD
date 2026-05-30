using Core.Entities;
using Core.Ports;
using Core.Ports.Repositories;
using Infraestructure.Persistence; 
namespace Infraestructure.Persistence.Repositories;
public class UserRoleRepository : 
    GRepositories<UserRole>,
    IUserRoleRepository
{ 
    public UserRoleRepository(AppDbContext context) : base(context)
    { }  
}
