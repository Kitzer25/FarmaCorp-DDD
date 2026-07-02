namespace Domain.DTO_s.CustomerWhislist;

public record MostWishlistedVariantDto

{

    public int ProductVariantId { get; init; }

    public int TimesWishlisted { get; init; }

}
