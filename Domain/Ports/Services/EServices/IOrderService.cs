using Core.DTO_s.Orders;

namespace Core.Ports.Services.EServices;

public interface IOrderService
{
    Task<string> GenerateOrderNumberAsync(CancellationToken ct);
    Task<OrderDto> CheckoutAsync(int customerId, CheckoutRequestDto request, CancellationToken ct);

    Task<OrderDto> ChangeStatusAsync(int orderId, int statusId, int? changedByUserId, string? notes,
        CancellationToken ct);
}