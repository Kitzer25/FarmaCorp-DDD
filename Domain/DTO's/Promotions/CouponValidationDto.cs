namespace Core.DTO_s.Promotions;

public class CouponValidationDto
{
    public bool IsValid { get; set; }
    public string Message { get; set; } = null!;
    public string? Code { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal OrderTotalAfterDiscount { get; set; }
}
