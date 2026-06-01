using Application.Cart.Dtos;
using Core.Entities;

namespace Tests;

public class CartMapperTests
{
    [Fact]
    public void ToDto_Calculates_TotalItems_And_Subtotal()
    {
        var cart = BuildCart(
            new CartItem
            {
                cart_item_id = 1,
                product_variant_id = 10,
                quantity = 2,
                product_variant = BuildVariant(10, "PARA-500", "Paracetamol", 7.50m)
            },
            new CartItem
            {
                cart_item_id = 2,
                product_variant_id = 11,
                quantity = 1,
                product_variant = BuildVariant(11, "IBU-400", "Ibuprofeno", 12.00m)
            });

        var result = CartMapper.ToDto(cart);

        Assert.Equal(3, result.TotalItems);
        Assert.Equal(27.00m, result.Subtotal);
    }

    [Fact]
    public void ToDto_Uses_UnitPriceSnapshot_When_Available()
    {
        var cart = BuildCart(new CartItem
        {
            cart_item_id = 1,
            product_variant_id = 10,
            quantity = 3,
            unit_price_snapshot = 5.00m,
            product_variant = BuildVariant(10, "PARA-500", "Paracetamol", 7.50m)
        });

        var item = CartMapper.ToDto(cart).Items.Single();

        Assert.Equal(5.00m, item.UnitPrice);
        Assert.Equal(15.00m, item.LineTotal);
    }

    private static Cart BuildCart(params CartItem[] items)
    {
        return new Cart
        {
            cart_id = 1,
            customer_id = 99,
            is_active = true,
            cart_items = items.ToList()
        };
    }

    private static ProductVariant BuildVariant(int id, string sku, string productName, decimal price)
    {
        return new ProductVariant
        {
            product_variant_id = id,
            sku = sku,
            price = price,
            product = new Product
            {
                product_id = id,
                name = productName,
                slug = productName.ToLowerInvariant(),
                category_id = 1,
                laboratory_id = 1
            }
        };
    }
}
