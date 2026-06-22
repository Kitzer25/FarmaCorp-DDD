using Core.DTO_s.AdminProducts;

namespace Core.Ports.Services.EServices;

public interface IAdminProductService
{
    Task<ProductAdminDto> CreateAsync(SaveProductAdminDto request, CancellationToken ct);
    Task<ProductAdminDto> UpdateAsync(int productId, SaveProductAdminDto request, CancellationToken ct);
    Task SoftDeleteAsync(int productId, CancellationToken ct);
}