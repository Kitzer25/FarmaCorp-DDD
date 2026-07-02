using Domain.DTO_s.CustomerWhislist;
using Domain.DTO_s.CustomerWhislist.Mappers;
using Domain.Entities;
using Domain.Ports.Repositories;
using Domain.Ports.Services.EServices;

namespace Infraestructure.Adapters.Services.EServices;

public class CustomerWishlistService : ICustomerWishlistService
{
    private readonly IUnitOfWork _unitOfWork;

    public CustomerWishlistService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<CustomerWishlistDto>> GetByCustomerAsync(int customerId, CancellationToken ct)
    {
        var items = await _unitOfWork.CustomerWishlistRepo.GetByCustomerAsync(customerId, ct);

        return items.Select(i => i.ToDto());
    }

    public async Task<CustomerWishlistDto> AddAsync(int customerId, int productVariantId, CancellationToken ct)
    {
        var alreadyExists = await _unitOfWork.CustomerWishlistRepo.ExistsAsync(customerId, productVariantId, ct);

        if (alreadyExists)
        {
            throw new InvalidOperationException("La variante ya está en la lista de deseos.");
        }

        var item = new CustomerWishlist
        {
            customer_id = customerId,
            product_variant_id = productVariantId,
            added_at = DateTime.UtcNow
        };

        await _unitOfWork.CustomerWishlistRepo.AddAsync(item, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return item.ToDto();
    }

    public async Task<bool> RemoveAsync(int customerId, int productVariantId, CancellationToken ct)
    {
        var item = await _unitOfWork.CustomerWishlistRepo.GetByKeyAsync(customerId, productVariantId, ct);

        if (item == null)
        {
            return false;
        }

        await _unitOfWork.CustomerWishlistRepo.DeleteAsync(item, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return true;
    }

    public async Task<IEnumerable<MostWishlistedVariantDto>> GetMostWishlistedAsync(int top, CancellationToken ct)
    {
        var results = await _unitOfWork.CustomerWishlistRepo.GetMostWishlistedAsync(top, ct);

        return results.Select(r => r.ToDto());
    }
}