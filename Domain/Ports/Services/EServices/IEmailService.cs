namespace Domain.Ports.Services.EServices;

public interface IEmailService
{
    Task SendOrderConfirmationAsync(string toEmail, string orderNumber, decimal total, CancellationToken ct);
}