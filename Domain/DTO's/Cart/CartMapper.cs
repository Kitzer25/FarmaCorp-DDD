using Core.Entities;

namespace Core.DTO_s.Cart;

public static class CartMapper
{
    public static CartDto ToDto(Core.Entities.Cart cart)
    {
        var items = cart.cart_items
            .OrderBy(ci => ci.cart_item_id)
            .Select(ToItemDto)
            .ToList();

        return new CartDto
        {
            CartId = cart.cart_id,
            CustomerId = cart.customer_id ?? 0,
            TotalItems = items.Sum(i => i.Quantity),
            Subtotal = items.Sum(i => i.LineTotal),
            Items = items
        };
    }

    private static CartItemDto ToItemDto(CartItem cartItem)
    {
        var unitPrice = cartItem.unit_price_snapshot ?? cartItem.product_variant.price;

        return new CartItemDto
        {
            CartItemId = cartItem.cart_item_id,
            ProductVariantId = cartItem.product_variant_id,
            ProductName = cartItem.product_variant.product.name,
            Sku = cartItem.product_variant.sku,
            Quantity = cartItem.quantity,
            UnitPrice = unitPrice,
            LineTotal = unitPrice * cartItem.quantity
        };
    }
}
