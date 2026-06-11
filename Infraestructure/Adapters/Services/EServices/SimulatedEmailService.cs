using Core.Ports.Services;
using Microsoft.Extensions.Logging;

namespace Infraestructure.Adapters.Services;

public class SimulatedEmailService : IEmailService
{
    private readonly ILogger<SimulatedEmailService> _logger;

    public SimulatedEmailService(ILogger<SimulatedEmailService> logger)
    {
        _logger = logger;
    }

    public Task SendOrderConfirmationAsync(string toEmail, string orderNumber, decimal total, CancellationToken ct)
    {
        _logger.LogInformation(
            "Simulated email sent to {Email}. Order {OrderNumber}. Total {Total}",
            toEmail,
            orderNumber,
            total);

        return Task.CompletedTask;
    }
}
