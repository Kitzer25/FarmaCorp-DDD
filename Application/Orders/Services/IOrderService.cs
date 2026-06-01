using Application.Orders.Dtos;

namespace Application.Orders.Services;

public interface IOrderService
{
    Task<string> GenerateOrderNumberAsync(CancellationToken ct);
    Task<OrderDto> CheckoutAsync(int customerId, CheckoutRequestDto request, CancellationToken ct);
    Task<OrderDto> ChangeStatusAsync(int orderId, int statusId, int? changedByUserId, string? notes, CancellationToken ct);
}
