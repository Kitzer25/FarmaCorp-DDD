using Core.Entities;
using Core.Ports;
using Core.Ports.Repositories;
using Infraestructure.Persistence; 
namespace Infraestructure.Persistence.Repositories;
public class CategoryRepository : 
    GRepositories<Category>,
    ICategoryRepository
{ 
    public CategoryRepository(AppDbContext context) : base(context)
    { }  
}
