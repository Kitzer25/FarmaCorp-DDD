namespace Application.Promotions.Dtos;

public class CreatePromotionDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int DiscountTypeId { get; set; }
    public decimal DiscountValue { get; set; }
    public decimal? MinOrderAmount { get; set; }
    public decimal? MaxDiscountAmount { get; set; }
    public int? MaxUses { get; set; }
    public bool AppliesToAll { get; set; } = true;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<string> Codes { get; set; } = new();
}
