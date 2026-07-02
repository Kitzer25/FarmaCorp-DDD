using Domain.Entities;

namespace Domain.DTO_s.Batches.Mappers;

public static class ProductBatchMapper
{
    public static ProductBatchDto ToDto(this ProductBatch batch)
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

    public static ExpiringBatchDto ToExpiringDto(this VExpiringBatch batch)
    {
        return new ExpiringBatchDto
        {
            BatchId = batch.batch_id,
            BatchNumber = batch.batch_number,
            ExpirationDate = batch.expiration_date,
            DaysUntilExpiry = batch.days_until_expiry,
            CurrentQuantity = batch.current_quantity,
            Sku = batch.sku,
            ProductName = batch.product_name,
            DrugForm = batch.drug_form
        };
    }
}
