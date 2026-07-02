using Domain.DTO_s.DiscountType;

namespace Domain.Ports.Services.EServices;

public interface IDiscountTypeService
{
    Task<List<DiscountTypeDto>> GetAllAsync(CancellationToken ct);
}
