namespace Domain.DTO_s.MeasurementUnit.Mappers;

public static class MeasurementUnitMapper
{
    public static MeasurementUnitDto ToDto(this Domain.Entities.MeasurementUnit measurementUnit)
    {
        return new MeasurementUnitDto
        {
            Id = measurementUnit.unit_id,
            Name = measurementUnit.name,
            Symbol = measurementUnit.symbol,
            IsActive = measurementUnit.is_active
        };
    }
}
