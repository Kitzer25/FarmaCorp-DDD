namespace Domain.DTO_s.DrugForm.Mappers;

public static class DrugFormMapper
{
    public static DrugFormDto ToDto(this Domain.Entities.DrugForm drugForm)
    {
        return new DrugFormDto
        {
            Id = drugForm.drug_form_id,
            Name = drugForm.name,
            Description = drugForm.description,
            IsActive = drugForm.is_active
        };
    }
}
