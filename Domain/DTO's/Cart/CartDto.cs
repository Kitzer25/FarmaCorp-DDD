namespace Domain.DTO_s.Cart;

public class CartDto
{
    public int CartId { get; set; }
    public int? CustomerId { get; set; }
    public string? SessionId { get; set; }
    public bool IsActive { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public List<CartItemDto> Items { get; set; } = new();
}

