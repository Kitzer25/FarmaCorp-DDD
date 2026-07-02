using Domain.DTO_s.Prescriptions;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.Prescription.Commands;

public sealed class VerifyPrescriptionCommand : IRequest<PrescriptionDto>
{
    public int PrescriptionId { get; set; }
    public int UserId { get; set; }
    public VerifyPrescriptionDto Request { get; set; } = null!;
}

public sealed class VerifyPrescriptionCommandHandler : IRequestHandler<VerifyPrescriptionCommand, PrescriptionDto>
{
    private readonly IPrescriptionService _prescriptionService;

    public VerifyPrescriptionCommandHandler(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    public async Task<PrescriptionDto> Handle(VerifyPrescriptionCommand command, CancellationToken ct)
    {
        return await _prescriptionService.VerifyAsync(command.PrescriptionId, command.UserId, command.Request, ct);
    }
}
