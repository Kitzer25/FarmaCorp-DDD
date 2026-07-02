using Domain.Entities;

namespace Domain.DTO_s.Laboratory.Mappers;

public static class LaboratoryMapper
{
    public static LaboratoryDto ToDto(this Domain.Entities.Laboratory laboratory)
    {
        return new LaboratoryDto
        {
            Id = laboratory.laboratory_id,
            Name = laboratory.name,
            CountryOfOrigin = laboratory.country_of_origin,
            ContactEmail = laboratory.contact_email,
            Phone = laboratory.phone,
            Website = laboratory.website,
            IsActive = laboratory.is_active,
            CreatedAt = laboratory.created_at,
            UpdatedAt = laboratory.updated_at
        };
    }
}
