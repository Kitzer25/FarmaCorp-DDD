using Core.Entities;
using Core.Ports;
using Core.Ports.Repositories;
using Infraestructure.Persistence; 
namespace Infraestructure.Persistence.Repositories;
public class UserRepository : 
    GRepositories<User>,
    IUserRepository
{ 
    public UserRepository(AppDbContext context) : base(context)
    { }  
}
