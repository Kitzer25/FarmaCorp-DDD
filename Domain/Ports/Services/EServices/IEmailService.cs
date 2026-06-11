namespace Core.Ports.Services;

public interface IEmailService
{
    Task SendOrderConfirmationAsync(string toEmail, string orderNumber, decimal total, CancellationToken ct);
}