using Domain.DTO_s.ProductImage;
using Domain.DTO_s.ProductImage.Mappers;
using Domain.Ports.Repositories;
using Domain.Ports.Services.EServices;

namespace Infraestructure.Adapters.Services.EServices;

public class ProductImageService : IProductImageService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductImageService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ProductImageDto>> GetByProductAsync(int productId, CancellationToken ct)
    {
        var images = await _unitOfWork.ProductImageRepo.GetByProductAsync(productId, ct);

        return images.Select(i => i.ToDto());
    }

    public async Task<ProductImageDto> AddImageAsync(CreateProductImageDto request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.ImageUrl))
        {
            throw new InvalidOperationException("La URL de la imagen es obligatoria.");
        }

        var image = request.ToEntity();

        await _unitOfWork.ProductImageRepo.AddAsync(image, ct);

        if (image.is_main)
        {
            await ClearOtherMainImagesAsync(image, ct);
        }

        await _unitOfWork.SaveChangesAsync(ct);

        return image.ToDto();
    }

    public async Task<ProductImageDto> SetMainImageAsync(int productImageId, CancellationToken ct)
    {
        var image = await _unitOfWork.ProductImageRepo.GetByIdAsync(productImageId, ct)
                    ?? throw new InvalidOperationException("La imagen no existe.");

        image.is_main = true;
        await _unitOfWork.ProductImageRepo.UpdateAsync(image.product_image_id, image, ct);

        await ClearOtherMainImagesAsync(image, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return image.ToDto();
    }

    public async Task<bool> DeleteImageAsync(int productImageId, CancellationToken ct)
    {
        var image = await _unitOfWork.ProductImageRepo.GetByIdAsync(productImageId, ct);

        if (image == null)
        {
            return false;
        }

        await _unitOfWork.ProductImageRepo.DeleteAsync(image, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return true;
    }

    private async Task ClearOtherMainImagesAsync(Domain.Entities.ProductImage mainImage, CancellationToken ct)
    {
        if (!mainImage.product_id.HasValue)
        {
            return;
        }

        var siblings = await _unitOfWork.ProductImageRepo.GetByProductAsync(mainImage.product_id.Value, ct);

        foreach (var sibling in siblings.Where(s => s.product_image_id != mainImage.product_image_id && s.is_main))
        {
            sibling.is_main = false;
            await _unitOfWork.ProductImageRepo.UpdateAsync(sibling.product_image_id, sibling, ct);
        }
    }
}