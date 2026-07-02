namespace Domain.DTO_s.CustomerWhislist;

public record CustomerWishlistDto

{

    public int CustomerId { get; init; }

    public int ProductVariantId { get; init; }

    public DateTime AddedAt { get; init; }

}
