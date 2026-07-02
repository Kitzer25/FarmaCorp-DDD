namespace Domain.DTO_s.Promotions;

public class PromotionDto
{
    public int PromotionId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int DiscountTypeId { get; set; }
    public decimal DiscountValue { get; set; }
    public decimal? MinOrderAmount { get; set; }
    public decimal? MaxDiscountAmount { get; set; }
    public int? MaxUses { get; set; }
    public int CurrentUses { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsActive { get; set; }
    public List<string> Codes { get; set; } = new();
}
