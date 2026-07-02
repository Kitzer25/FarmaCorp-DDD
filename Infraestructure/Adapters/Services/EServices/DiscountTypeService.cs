using Domain.DTO_s.DiscountType;
using Domain.DTO_s.DiscountType.Mappers;
using Domain.Ports.Repositories;
using Domain.Ports.Services.EServices;

namespace Infraestructure.Adapters.Services.EServices;

public class DiscountTypeService : IDiscountTypeService
{
    private readonly IUnitOfWork _unitOfWork;

    public DiscountTypeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<DiscountTypeDto>> GetAllAsync(CancellationToken ct)
    {
        var discountTypes = await _unitOfWork.DiscountTypeRepo.GetAllAsync(ct);

        return discountTypes.Select(d => d.ToDto()).ToList();
    }
}
