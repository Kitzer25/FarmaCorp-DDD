using Application.Batches.Dtos;
using Core.Entities;
using Core.Ports;

namespace Application.Batches.Services;

public class BatchService : IBatchService
{
    private readonly IUnitOfWork _unitOfWork;

    public BatchService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ProductBatchDto>> GetBatchesAsync(CancellationToken ct)
    {
        var batches = await _unitOfWork.ProductBatchRepo.GetAllWithProductAsync(ct);

        return batches.Select(ToDto);
    }

    public async Task<ProductBatchDto> CreateAsync(CreateProductBatchDto request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.BatchNumber))
        {
            throw new InvalidOperationException("El número de lote es obligatorio.");
        }

        if (request.InitialQuantity <= 0)
        {
            throw new InvalidOperationException("La cantidad inicial debe ser mayor a 0.");
        }

        if (request.ManufactureDate.HasValue && request.ExpirationDate < request.ManufactureDate.Value)
        {
            throw new InvalidOperationException("La fecha de vencimiento no puede ser menor a la fecha de fabricación.");
        }

        var duplicate = await _unitOfWork.ProductBatchRepo.GetByVariantAndBatchNumberAsync(
            request.ProductVariantId,
            request.BatchNumber.Trim(),
            ct);

        if (duplicate != null)
        {
            throw new InvalidOperationException("Ya existe un lote con ese número para la variante indicada.");
        }

        var variant = await _unitOfWork.ProductVariantRepo.GetByIdAsync(request.ProductVariantId, ct);

        if (variant == null)
        {
            throw new InvalidOperationException("La variante no existe.");
        }

        var batch = new ProductBatch
        {
            product_variant_id = request.ProductVariantId,
            laboratory_id = request.LaboratoryId,
            batch_number = request.BatchNumber.Trim(),
            manufacture_date = request.ManufactureDate,
            expiration_date = request.ExpirationDate,
            initial_quantity = request.InitialQuantity,
            current_quantity = request.InitialQuantity,
            notes = request.Notes,
            is_active = true,
            created_at = DateTime.UtcNow
        };

        await _unitOfWork.ProductBatchRepo.AddAsync(batch, ct);

        return ToDto(batch);
    }

    private static ProductBatchDto ToDto(ProductBatch batch)
    {
        return new ProductBatchDto
        {
            BatchId = batch.batch_id,
            ProductVariantId = batch.product_variant_id,
            ProductName = batch.product_variant?.product?.name,
            Sku = batch.product_variant?.sku,
            LaboratoryId = batch.laboratory_id,
            BatchNumber = batch.batch_number,
            ManufactureDate = batch.manufacture_date,
            ExpirationDate = batch.expiration_date,
            InitialQuantity = batch.initial_quantity,
            CurrentQuantity = batch.current_quantity,
            IsActive = batch.is_active,
            Notes = batch.notes
        };
    }
}
