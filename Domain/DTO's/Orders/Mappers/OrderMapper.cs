using Domain.Entities;

namespace Domain.DTO_s.Orders.Mappers;

public static class OrderMapper
{
    public static OrderDto ToDto(this Order order)
    {
        return new OrderDto
        {
            OrderId = order.order_id,
            OrderNumber = order.order_number,
            CustomerId = order.customer_id,
            OrderStatusId = order.order_status_id,
            OrderStatus = order.order_status?.name,
            Subtotal = order.subtotal,
            TaxAmount = order.tax_amount,
            ShippingCost = order.shipping_cost,
            DiscountAmount = order.discount_amount,
            Total = order.total,
            CreatedAt = order.created_at,
            Items = order.order_items.Select(i => new OrderItemDto
            {
                OrderItemId = i.order_item_id,
                ProductVariantId = i.product_variant_id,
                Quantity = i.quantity,
                UnitPrice = i.unit_price,
                Subtotal = i.subtotal,
                ProductName = i.product_name_snapshot,
                VariantDescription = i.variant_desc_snapshot,
                Sku = i.sku_snapshot
            }).ToList()
        };
    }
}
