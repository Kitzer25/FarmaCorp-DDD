using Application.AdminProducts.Dtos;

namespace Application.AdminProducts.Services;

public interface IAdminProductService
{
    Task<ProductAdminDto> CreateAsync(SaveProductAdminDto request, CancellationToken ct);
    Task<ProductAdminDto> UpdateAsync(int productId, SaveProductAdminDto request, CancellationToken ct);
    Task SoftDeleteAsync(int productId, CancellationToken ct);
}
