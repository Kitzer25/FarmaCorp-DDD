using Domain.Entities;

namespace Domain.DTO_s.Cart.Mappers;

public static class CartMapper
{
    public static CartDto ToDto(Entities.Cart cart)
    {
        return new CartDto
        {
            CartId = cart.cart_id,
            CustomerId = cart.customer_id,
            SessionId = cart.session_id,
            IsActive = cart.is_active,
            ExpiresAt = cart.expires_at,
            Items = cart.cart_items.Select(ci => ci.ToDto()).ToList()
        };
    }

    public static CartItemDto ToDto(this CartItem item)
    {
        return new CartItemDto
        {
            CartItemId = item.cart_item_id,
            ProductVariantId = item.product_variant_id,
            Quantity = item.quantity,
            UnitPriceSnapshot = item.unit_price_snapshot
        };
    }
}