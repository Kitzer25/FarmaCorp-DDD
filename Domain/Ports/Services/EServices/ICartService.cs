using Domain.DTO_s.Cart;
using Domain.DTO_s.Categories;
using Domain.Entities;
using Domain.Ports.Repositories;

namespace Domain.Ports.Services.EServices;

public interface ICartService : IGRepositories<Cart>
{
    Task<List<CategoryDto>> GetCategoriesAsync(CancellationToken ct);
}