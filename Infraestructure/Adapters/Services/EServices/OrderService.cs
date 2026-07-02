using Domain.Ports;
using Domain.Ports.Services;
using Domain.DTO_s.Orders;
using Domain.DTO_s.Orders.Mappers;
using Domain.Entities;
using Domain.Ports.Repositories;
using Domain.Ports.Services.EServices;

namespace Infraestructure.Adapters.Services.EServices;

public class OrderService : IOrderService
{
    private const decimal TaxRate = 0.18m;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuditService _auditService;

    public OrderService(IUnitOfWork unitOfWork, IAuditService auditService)
    {
        _unitOfWork = unitOfWork;
        _auditService = auditService;
    }

    public async Task<string> GenerateOrderNumberAsync(CancellationToken ct)
    {
        var year = DateTime.UtcNow.Year;
        var lastNumber = await _unitOfWork.OrderRepo.GetLastOrderNumberForYearAsync(year, ct);
        var sequence = 1;

        if (!string.IsNullOrWhiteSpace(lastNumber))
        {
            var lastPart = lastNumber.Split('-').LastOrDefault();
            sequence = int.TryParse(lastPart, out var parsed) ? parsed + 1 : 1;
        }

        return $"ORD-{year}-{sequence:000000}";
    }

    public async Task<OrderDto> CheckoutAsync(int customerId, CheckoutRequestDto request, CancellationToken ct)
    {
        var address = await _unitOfWork.CustomerAddressRepo
            .GetActiveByIdAndCustomerIdAsync(request.ShippingAddressId, customerId, ct);

        if (address == null)
        {
            throw new InvalidOperationException("La dirección no existe o no pertenece al cliente.");
        }

        var paymentMethod = await _unitOfWork.PaymentMethodRepo.GetByIdAsync(request.PaymentMethodId, ct);

        if (paymentMethod == null || !paymentMethod.is_active)
        {
            throw new InvalidOperationException("El método de pago no está disponible.");
        }

        var cart = await _unitOfWork.CartRepo.GetActiveByCustomerIdWithItemsAsync(customerId, ct);

        if (cart == null || !cart.cart_items.Any())
        {
            throw new InvalidOperationException("El carrito está vacío.");
        }

        foreach (var item in cart.cart_items)
        {
            await EnsureStockAvailableAsync(item.product_variant_id, item.quantity, ct);
        }

        var pendingStatusId = await GetOrderStatusIdAsync("Pending", ct);
        var subtotal = cart.cart_items.Sum(i => (i.unit_price_snapshot ?? i.product_variant.price) * i.quantity);
        var taxAmount = decimal.Round(subtotal * TaxRate, 2);
        var total = subtotal + taxAmount + request.ShippingCost;

        var order = new Order
        {
            order_number = await GenerateOrderNumberAsync(ct),
            customer_id = customerId,
            order_status_id = pendingStatusId,
            shipping_address_id = request.ShippingAddressId,
            subtotal = subtotal,
            tax_amount = taxAmount,
            shipping_cost = request.ShippingCost,
            discount_amount = 0,
            total = total,
            customer_notes = request.CustomerNotes,
            created_at = DateTime.UtcNow
        };

        await _unitOfWork.BeginTransactionAsync(ct);

        try
        {
            await _unitOfWork.OrderRepo.AddAsync(order, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            foreach (var cartItem in cart.cart_items)
            {
                var variant = cartItem.product_variant;
                var unitPrice = cartItem.unit_price_snapshot ?? variant.price;
                var orderItem = new OrderItem
                {
                    order_id = order.order_id,
                    product_variant_id = variant.product_variant_id,
                    quantity = cartItem.quantity,
                    unit_price = unitPrice,
                    discount_amount = 0,
                    subtotal = unitPrice * cartItem.quantity,
                    product_name_snapshot = variant.product.name,
                    variant_desc_snapshot = BuildVariantDescription(variant),
                    sku_snapshot = variant.sku
                };

                await _unitOfWork.OrderItemRepo.AddAsync(orderItem, ct);
            }

            await _unitOfWork.OrderPaymentRepo.AddAsync(new OrderPayment
            {
                order_id = order.order_id,
                payment_method_id = request.PaymentMethodId,
                amount = total,
                payment_status = "Pending",
                created_at = DateTime.UtcNow
            }, ct);

            await _unitOfWork.OrderStatusHistoryRepo.AddAsync(new OrderStatusHistory
            {
                order_id = order.order_id,
                order_status_id = pendingStatusId,
                notes = "Pedido creado desde checkout.",
                created_at = DateTime.UtcNow
            }, ct);

            cart.is_active = false;
            cart.updated_at = DateTime.UtcNow;
            await _unitOfWork.CartRepo.UpdateAsync(cart.cart_id, cart, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            var savedOrder = await _unitOfWork.OrderRepo.GetByIdWithDetailsAsync(order.order_id, ct)
                ?? order;

            await _unitOfWork.CommitTransactionAsync(ct);

            return savedOrder.ToDto();
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync(ct);
            throw;
        }
    }

    public async Task<OrderDto> ChangeStatusAsync(int orderId, int statusId, int? changedByUserId, string? notes, CancellationToken ct)
    {
        var order = await _unitOfWork.OrderRepo.GetByIdWithDetailsAsync(orderId, ct);

        if (order == null)
        {
            throw new InvalidOperationException("El pedido no existe.");
        }

        var status = await _unitOfWork.Repositories<OrderStatus>().GetByIdAsync(statusId, ct);

        if (status == null || !status.is_active)
        {
            throw new InvalidOperationException("El estado de pedido no está disponible.");
        }

        var previousStatusId = order.order_status_id;
        order.order_status_id = statusId;
        order.updated_at = DateTime.UtcNow;

        await _unitOfWork.OrderRepo.UpdateAsync(order.order_id, order, ct);
        await _unitOfWork.OrderStatusHistoryRepo.AddAsync(new OrderStatusHistory
        {
            order_id = order.order_id,
            order_status_id = statusId,
            changed_by_user_id = changedByUserId,
            notes = notes,
            created_at = DateTime.UtcNow
        }, ct);

        await _auditService.RegisterAsync(
            "orders",
            order.order_id.ToString(),
            "STATUS_CHANGE",
            new { order_status_id = previousStatusId },
            new { order_status_id = statusId, notes },
            changedByUserId,
            null,
            ct);

        var updated = await _unitOfWork.OrderRepo.GetByIdWithDetailsAsync(orderId, ct)
            ?? order;

        return updated.ToDto();
    }

    private async Task EnsureStockAvailableAsync(int productVariantId, int requestedQuantity, CancellationToken ct)
    {
        var inventory = await _unitOfWork.InventoryRepo.GetByProductVariantIdAsync(productVariantId, ct);
        var available = inventory == null ? 0 : Math.Max(0, inventory.quantity_on_hand - inventory.reserved_quantity);

        if (requestedQuantity > available)
        {
            throw new InvalidOperationException("No hay stock suficiente para completar el checkout.");
        }
    }

    private async Task<int> GetOrderStatusIdAsync(string fallbackName, CancellationToken ct)
    {
        var statuses = await _unitOfWork.Repositories<OrderStatus>().GetAllAsync(ct);
        var status = statuses.FirstOrDefault(s =>
            s.is_active && s.name.Equals(fallbackName, StringComparison.OrdinalIgnoreCase))
            ?? statuses.Where(s => s.is_active).OrderBy(s => s.sort_order).FirstOrDefault();

        if (status == null)
        {
            throw new InvalidOperationException("No hay estados de pedido configurados.");
        }

        return status.order_status_id;
    }

    private static string BuildVariantDescription(ProductVariant variant)
    {
        var concentration = variant.concentration?.ToString("0.###") ?? "";
        var package = string.IsNullOrWhiteSpace(variant.package_description)
            ? $"x{variant.package_size}"
            : variant.package_description;

        return $"{concentration} {package}".Trim();
    }
}
