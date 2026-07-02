using Domain.DTO_s.Prescriptions;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.PrescriptionUseCase.Commands;

public sealed class PATCHVerifyPrescriptionCommand : IRequest<PrescriptionDto>
{
    public int PrescriptionId { get; set; }
    public int UserId { get; set; }
    public VerifyPrescriptionDto Request { get; set; } = null!;
}

public sealed class PatchVerifyPrescriptionCommandHandler : IRequestHandler<PATCHVerifyPrescriptionCommand, PrescriptionDto>
{
    private readonly IPrescriptionService _prescriptionService;

    public PatchVerifyPrescriptionCommandHandler(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    public async Task<PrescriptionDto> Handle(PATCHVerifyPrescriptionCommand command, CancellationToken ct)
    {
        return await _prescriptionService.VerifyAsync(command.PrescriptionId, command.UserId, command.Request, ct);
    }
}
