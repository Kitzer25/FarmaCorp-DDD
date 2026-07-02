using Domain.Entities;
using Domain.Ports.Repositories.ERepository;
using Infraestructure.Context;

namespace Infraestructure.Adapters.Repositories.ERepository;
public class UserRoleRepository : 
    GRepositories<UserRole>,
    IUserRoleRepository
{ 
    public UserRoleRepository(AppDbContext context) : base(context)
    { }  
}
