using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.Notification.Commands;

public sealed class POSTSendOrderConfirmationCommand : IRequest
{
    public string Email { get; set; } = null!;
    public string OrderNumber { get; set; } = null!;
    public decimal Total { get; set; }
}

public sealed class POSTSendOrderConfirmationCommandHandler : IRequestHandler<POSTSendOrderConfirmationCommand>
{
    private readonly IEmailService _emailService;

    public POSTSendOrderConfirmationCommandHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Handle(POSTSendOrderConfirmationCommand command, CancellationToken ct)
    {
        await _emailService.SendOrderConfirmationAsync(command.Email, command.OrderNumber, command.Total, ct);
    }
}
