using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.Notification.Commands;

public sealed class SendTestOrderConfirmationCommand : IRequest
{
    public string Email { get; set; } = null!;
    public string OrderNumber { get; set; } = null!;
    public decimal Total { get; set; }
}

public sealed class SendTestOrderConfirmationCommandHandler : IRequestHandler<SendTestOrderConfirmationCommand>
{
    private readonly IEmailService _emailService;

    public SendTestOrderConfirmationCommandHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Handle(SendTestOrderConfirmationCommand command, CancellationToken ct)
    {
        await _emailService.SendOrderConfirmationAsync(command.Email, command.OrderNumber, command.Total, ct);
    }
}
